using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Sklad1.Data;
using Sklad1.Models;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Единый сервис проверки ИНН для отгрузки и поставки.
    /// </summary>
    public static class ContractorCheckService
    {
        public const string StatusReliable = "Надежный";
        public const string StatusBlacklisted = "В черном списке";
        public const string NotPerformedText = "Проверка не выполнена";
        public const string ReliableText = "Контрагент надежный. Можно продолжать";
        public const string BlacklistedText = "Контрагент в черном списке. Оформление запрещено";
        public const string InvalidInnMessage = "ИНН должен содержать только цифры и иметь длину 10 или 12 символов";
        public const string NoInternetMessage = "Нет подключения к интернету. Проверьте сеть и повторите попытку";
        public const string OrganizationNotFoundMessage = "Организация не найдена. Оформление запрещено";
        public const string GenericErrorMessage = "Не удалось проверить ИНН. Повторите попытку";
        public const string BlockedBlackListMessage = "Оформление запрещено: контрагент находится в чёрном списке";
        public const string NeedCheckMessage = "Сначала проверьте ИНН контрагента";
        public const string CheckErrorMessage = "Проверка не выполнена. Повторите запрос";

        private static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(12)
        };

        private static void EnsureFeatureSchema()
        {
            try
            {
                DatabaseSchemaInitializer.Initialize();
            }
            catch
            {
                // Если схема не подготовилась, дальнейшая операция вернёт штатную ошибку пользователю.
            }
        }

        public static bool IsValidInn(string inn)
        {
            inn = (inn ?? string.Empty).Trim();
            return Regex.IsMatch(inn, "^\\d{10}$|^\\d{12}$");
        }

        public static async Task<bool> HasInternetConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Head, "https://api.open-meteo.com/");
                using var response = await Http.SendAsync(request, cancellationToken);
                return true;
            }
            catch
            {
                try
                {
                    await Dns.GetHostEntryAsync("api.open-meteo.com");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static async Task<ContractorCheckResult> CheckAndSaveAsync(
            string inn,
            string documentType,
            Guid userId,
            Guid? shipmentId = null,
            Guid? supplyId = null,
            CancellationToken cancellationToken = default)
        {
            inn = (inn ?? string.Empty).Trim();
            if (!IsValidInn(inn))
            {
                return ContractorCheckResult.Error(inn, InvalidInnMessage, "validation");
            }

            if (!await HasInternetConnectionAsync(cancellationToken))
            {
                return ContractorCheckResult.Error(inn, NoInternetMessage, "network");
            }

            try
            {
                var externalInfo = await GetOrganizationInfoAsync(inn, cancellationToken);
                if (externalInfo == null)
                {
                    await SaveCheckAsync(inn, string.Empty, "NOT_FOUND", StatusBlacklisted, false, OrganizationNotFoundMessage, documentType, userId, shipmentId, supplyId, cancellationToken);
                    return ContractorCheckResult.Error(inn, OrganizationNotFoundMessage, "not_found");
                }

                EnsureFeatureSchema();

                bool inLocalBlacklist;
                using (var db = new TzContext())
                {
                    inLocalBlacklist = await db.BlacklistedInns.AnyAsync(b => b.Inn == inn, cancellationToken);
                }

                var externalStatus = string.IsNullOrWhiteSpace(externalInfo.ExternalStatus)
                    ? "UNKNOWN"
                    : externalInfo.ExternalStatus.Trim();

                var externalProblem = IsExternalStatusProblem(externalStatus);
                var resultStatus = (inLocalBlacklist || externalProblem) ? StatusBlacklisted : StatusReliable;
                var message = resultStatus == StatusReliable ? ReliableText : BlacklistedText;

                var checkId = await SaveCheckAsync(
                    inn,
                    externalInfo.OrganizationName,
                    externalStatus,
                    resultStatus,
                    true,
                    null,
                    documentType,
                    userId,
                    shipmentId,
                    supplyId,
                    cancellationToken);

                return new ContractorCheckResult
                {
                    CheckId = checkId,
                    Inn = inn,
                    OrganizationName = externalInfo.OrganizationName,
                    ExternalStatus = externalStatus,
                    ResultStatus = resultStatus,
                    CheckedAt = DateTime.UtcNow,
                    IsSuccess = true,
                    IsReliable = resultStatus == StatusReliable,
                    Message = message
                };
            }
            catch
            {
                await SaveCheckAsync(inn, string.Empty, "ERROR", string.Empty, false, GenericErrorMessage, documentType, userId, shipmentId, supplyId, cancellationToken);
                return ContractorCheckResult.Error(inn, GenericErrorMessage, "error");
            }
        }

        public static async Task LinkToShipmentAsync(Guid? checkId, Guid shipmentId)
        {
            if (checkId == null) return;

            EnsureFeatureSchema();
            using var db = new TzContext();
            var check = await db.ContractorChecks.FindAsync(checkId.Value);
            if (check == null) return;

            check.ShipmentId = shipmentId;
            await db.SaveChangesAsync();
        }

        public static async Task LinkToSupplyAsync(Guid? checkId, Guid supplyId)
        {
            if (checkId == null) return;

            EnsureFeatureSchema();
            using var db = new TzContext();
            var check = await db.ContractorChecks.FindAsync(checkId.Value);
            if (check == null) return;

            check.SupplyId = supplyId;
            await db.SaveChangesAsync();
        }

        public static async Task LogBlockedOperationAsync(string documentType, string? inn, string reason)
        {
            try
            {
                EnsureFeatureSchema();
                using var db = new TzContext();
                db.BlockedOperationLogs.Add(new BlockedOperationLog
                {
                    Id = Guid.NewGuid(),
                    DocumentType = documentType,
                    Inn = inn,
                    Reason = reason,
                    AttemptedAt = DateTime.UtcNow,
                    UserId = CurrentUser.Id
                });
                await db.SaveChangesAsync();
            }
            catch
            {
                // Лог блокировки не должен ломать основной сценарий показа причины пользователю.
            }
        }

        private static async Task<Guid> SaveCheckAsync(
            string inn,
            string organizationName,
            string externalStatus,
            string resultStatus,
            bool isSuccess,
            string? errorMessage,
            string documentType,
            Guid userId,
            Guid? shipmentId,
            Guid? supplyId,
            CancellationToken cancellationToken)
        {
            EnsureFeatureSchema();
            using var db = new TzContext();
            var entity = new ContractorCheck
            {
                Id = Guid.NewGuid(),
                Inn = inn,
                OrganizationName = organizationName,
                ExternalStatus = externalStatus,
                ResultStatus = resultStatus,
                CheckedAt = DateTime.UtcNow,
                UserId = userId,
                DocumentType = documentType,
                ShipmentId = shipmentId,
                SupplyId = supplyId,
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage
            };
            db.ContractorChecks.Add(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        private static bool IsExternalStatusProblem(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return false;

            var normalized = status.Trim().ToUpperInvariant();
            return normalized.Contains("LIQUIDATED")
                || normalized.Contains("LIQUIDATING")
                || normalized.Contains("BANKRUPT")
                || normalized.Contains("REORGANIZ")
                || normalized.Contains("TERMINATED")
                || normalized.Contains("CLOSED")
                || normalized.Contains("PROBLEM")
                || normalized.Contains("НЕДЕЙСТВ")
                || normalized.Contains("ЛИКВИД")
                || normalized.Contains("БАНКРОТ");
        }

        private static async Task<ExternalContractorInfo?> GetOrganizationInfoAsync(string inn, CancellationToken cancellationToken)
        {
            var token = Environment.GetEnvironmentVariable("DADATA_TOKEN")
                ?? Environment.GetEnvironmentVariable("DADATA_API_KEY");

            if (!string.IsNullOrWhiteSpace(token))
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, "https://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/party");
                request.Headers.Authorization = new AuthenticationHeaderValue("Token", token.Trim());
                request.Content = new StringContent(JsonSerializer.Serialize(new { query = inn }), Encoding.UTF8, "application/json");

                using var response = await Http.SendAsync(request, cancellationToken);
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

                var suggestions = json.RootElement.GetProperty("suggestions");
                if (suggestions.GetArrayLength() == 0)
                    return null;

                var first = suggestions[0];
                var name = first.TryGetProperty("value", out var valueElement)
                    ? valueElement.GetString() ?? string.Empty
                    : string.Empty;

                var status = "ACTIVE";
                if (first.TryGetProperty("data", out var data)
                    && data.TryGetProperty("state", out var state)
                    && state.TryGetProperty("status", out var statusElement))
                {
                    status = statusElement.GetString() ?? "UNKNOWN";
                }

                return new ExternalContractorInfo
                {
                    OrganizationName = string.IsNullOrWhiteSpace(name) ? $"Организация ИНН {inn}" : name,
                    ExternalStatus = status
                };
            }

            // Демонстрационный режим для учебного проекта без API-ключа DaData.
            // Проверка ИНН и локального черного списка выполняется полностью, внешний статус фиксируется как LOCAL_DEMO_ACTIVE.
            await Task.Delay(150, cancellationToken);
            return new ExternalContractorInfo
            {
                OrganizationName = $"Организация ИНН {inn}",
                ExternalStatus = "LOCAL_DEMO_ACTIVE"
            };
        }

        private sealed class ExternalContractorInfo
        {
            public string OrganizationName { get; set; } = string.Empty;
            public string ExternalStatus { get; set; } = string.Empty;
        }
    }

    public sealed class ContractorCheckResult
    {
        public Guid? CheckId { get; set; }
        public string Inn { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string ExternalStatus { get; set; } = string.Empty;
        public string ResultStatus { get; set; } = string.Empty;
        public DateTime CheckedAt { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsReliable { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ErrorKind { get; set; }

        public static ContractorCheckResult Error(string inn, string message, string errorKind)
        {
            return new ContractorCheckResult
            {
                Inn = inn,
                IsSuccess = false,
                IsReliable = false,
                CheckedAt = DateTime.UtcNow,
                Message = message,
                ErrorKind = errorKind
            };
        }
    }
}
