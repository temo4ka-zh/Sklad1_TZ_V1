using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Sklad1.Data;
using Sklad1.Models;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Сервис интеграции с Open-Meteo для получения прогноза и рекомендации по доставке.
    /// </summary>
    public static class WeatherService
    {
        public const string NormalRisk = "Нормальный";
        public const string WarningRisk = "Предупреждение";
        public const string CriticalRisk = "Критический риск";

        public const string NormalRecommendation = "Погодные условия благоприятные. Дополнительные меры защиты товара не требуются";
        public const string WarningRecommendation = "Есть неблагоприятная погода. Рекомендуется защитная упаковка и проверка маршрута доставки";
        public const string CriticalRecommendation = "Высокий риск порчи товара. Рекомендуется перенести доставку или использовать специализированный транспорт";

        public const string EnterCityMessage = "Введите город доставки";
        public const string DateInPastMessage = "Дата доставки не может быть в прошлом";
        public const string CityNotFoundMessage = "Город не найден. Проверьте написание";
        public const string GeocodingUnavailableMessage = "Не удалось определить координаты города. Сервер погоды недоступен";
        public const string ForecastUnavailableMessage = "Не удалось получить прогноз погоды. Повторите попытку позже";
        public const string ForecastMissingMessage = "Прогноз погоды на указанную дату недоступен. Выберите другую дату";
        public const string NoInternetMessage = "Нет подключения к интернету. Прогноз погоды недоступен";

        private static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        private static void EnsureFeatureSchema()
        {
            try
            {
                DatabaseSchemaInitializer.Initialize();
            }
            catch
            {
                // Если схема не подготовилась, операция сохранения вернёт штатную ошибку.
            }
        }

        public static async Task<WeatherCheckResult> GetForecastAndSaveAsync(string city, DateTime deliveryDate, Guid userId, CancellationToken cancellationToken = default)
        {
            city = (city ?? string.Empty).Trim();
            if (city.Length < 2)
                return WeatherCheckResult.Error(EnterCityMessage);

            if (deliveryDate.Date < DateTime.Today)
                return WeatherCheckResult.Error(DateInPastMessage);

            if (!await ContractorCheckService.HasInternetConnectionAsync(cancellationToken))
                return WeatherCheckResult.Error(NoInternetMessage);

            var coordinates = await GeocodeAsync(city, cancellationToken);
            if (coordinates == null)
                return WeatherCheckResult.Error(CityNotFoundMessage);

            var forecast = await GetDailyForecastAsync(coordinates.Value.Latitude, coordinates.Value.Longitude, deliveryDate.Date, cancellationToken);
            if (forecast == null)
                return WeatherCheckResult.Error(ForecastMissingMessage);

            var risk = DetermineRisk(forecast.Value.TemperatureMin, forecast.Value.TemperatureMax, forecast.Value.Precipitation, forecast.Value.WindSpeed);
            var recommendation = risk switch
            {
                CriticalRisk => CriticalRecommendation,
                WarningRisk => WarningRecommendation,
                _ => NormalRecommendation
            };

            var checkedAt = DateTime.UtcNow;
            var check = new WeatherCheck
            {
                Id = Guid.NewGuid(),
                City = city,
                DeliveryDate = DateTime.SpecifyKind(deliveryDate.Date, DateTimeKind.Utc),
                Temperature = Math.Round((forecast.Value.TemperatureMin + forecast.Value.TemperatureMax) / 2d, 1),
                Precipitation = forecast.Value.Precipitation,
                WindSpeed = forecast.Value.WindSpeed,
                RiskLevel = risk,
                Recommendation = recommendation,
                CheckedAt = checkedAt,
                UserId = userId
            };

            EnsureFeatureSchema();
            using (var db = new TzContext())
            {
                db.WeatherChecks.Add(check);
                await db.SaveChangesAsync(cancellationToken);
            }

            return new WeatherCheckResult
            {
                CheckId = check.Id,
                City = city,
                DeliveryDate = deliveryDate.Date,
                Temperature = check.Temperature,
                Precipitation = check.Precipitation,
                WindSpeed = check.WindSpeed,
                RiskLevel = risk,
                Recommendation = recommendation,
                CheckedAt = checkedAt,
                IsSuccess = true
            };
        }

        public static async Task LinkToShipmentAsync(Guid? weatherCheckId, Guid shipmentId)
        {
            if (weatherCheckId == null) return;

            EnsureFeatureSchema();
            using var db = new TzContext();
            var check = await db.WeatherChecks.FindAsync(weatherCheckId.Value);
            if (check == null) return;

            check.ShipmentId = shipmentId;
            await db.SaveChangesAsync();
        }

        public static string DetermineRisk(double tempMin, double tempMax, double precipitation, double windSpeed)
        {
            if (tempMin < -25 || tempMax > 35 || precipitation >= 20 || windSpeed >= 25)
                return CriticalRisk;

            if (tempMin < -10 || tempMax > 30 || precipitation >= 5 || windSpeed >= 15)
                return WarningRisk;

            return NormalRisk;
        }

        private static async Task<(double Latitude, double Longitude)?> GeocodeAsync(string city, CancellationToken cancellationToken)
        {
            try
            {
                var url = "https://geocoding-api.open-meteo.com/v1/search?count=1&language=ru&format=json&name=" + Uri.EscapeDataString(city);
                await using var stream = await Http.GetStreamAsync(url, cancellationToken);
                using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

                if (!json.RootElement.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
                    return null;

                var first = results[0];
                return (first.GetProperty("latitude").GetDouble(), first.GetProperty("longitude").GetDouble());
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException(GeocodingUnavailableMessage, ex);
            }
        }

        private static async Task<(double TemperatureMin, double TemperatureMax, double Precipitation, double WindSpeed)?> GetDailyForecastAsync(
            double latitude,
            double longitude,
            DateTime deliveryDate,
            CancellationToken cancellationToken)
        {
            try
            {
                var date = deliveryDate.ToString("yyyy-MM-dd");
                var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}&longitude={longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,wind_speed_10m_max&timezone=auto&start_date={date}&end_date={date}";
                await using var stream = await Http.GetStreamAsync(url, cancellationToken);
                using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

                if (!json.RootElement.TryGetProperty("daily", out var daily))
                    return null;

                var time = daily.GetProperty("time");
                if (time.GetArrayLength() == 0)
                    return null;

                var tempMax = daily.GetProperty("temperature_2m_max")[0].GetDouble();
                var tempMin = daily.GetProperty("temperature_2m_min")[0].GetDouble();
                var precipitation = daily.GetProperty("precipitation_sum")[0].GetDouble();
                var windSpeed = daily.GetProperty("wind_speed_10m_max")[0].GetDouble();
                return (tempMin, tempMax, precipitation, windSpeed);
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException(ForecastUnavailableMessage, ex);
            }
        }
    }

    public sealed class WeatherCheckResult
    {
        public Guid? CheckId { get; set; }
        public string City { get; set; } = string.Empty;
        public DateTime DeliveryDate { get; set; }
        public double Temperature { get; set; }
        public double Precipitation { get; set; }
        public double WindSpeed { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
        public DateTime CheckedAt { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public static WeatherCheckResult Error(string message)
        {
            return new WeatherCheckResult
            {
                IsSuccess = false,
                ErrorMessage = message,
                CheckedAt = DateTime.UtcNow
            };
        }
    }
}
