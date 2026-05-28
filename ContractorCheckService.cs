using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;
using Sklad1.Data;
using Sklad1.Models;
using Sklad1.Properties;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Сервис для работы с курсами валют
    /// </summary>
    public static class CurrencyService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<bool> UpdateRates()
        {
            try
            {
                using (var client = new HttpClient()) 
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var response = await client.GetStringAsync("https://www.cbr-xml-daily.ru/daily_json.js");

                    var data = JsonSerializer.Deserialize<CbrResponse>(response);

                    if (data?.Valute == null)
                    {
                        MessageBox.Show(Resources.ErrorData);
                        return false;
                    }

                    using (var bd = new Context())
                    {
                        decimal usdRate = ConvertToDecimal(data.Valute.USD.Value);
                        decimal eurRate = ConvertToDecimal(data.Valute.EUR.Value);
                        decimal cnyRate = ConvertToDecimal(data.Valute.CNY.Value);

                        System.Windows.Forms.MessageBox.Show($"USD: {usdRate}, EUR: {eurRate}, CNY: {cnyRate}");

                        UpdateRate(bd, "USD", usdRate);
                        UpdateRate(bd, "EUR", eurRate);
                        UpdateRate(bd, "CNY", cnyRate);
                        UpdateRate(bd, "RUB", 1);

                        int saved = await bd.SaveChangesAsync();
                    }

                    return true;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorInternet);
                return false;
            }
        }

        private static decimal ConvertToDecimal(decimal value)
        {
            return value;
        }

        private static void UpdateRate(Context bd, string code, decimal rate)
        {
            var currency = bd.CurrencyRates.FirstOrDefault(c => c.Code == code);
            if (currency != null)
            {
                currency.RateToRub = Math.Round(rate, 4);
                currency.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                bd.CurrencyRates.Add(new CurrencyRate
                {
                    Id = Guid.NewGuid(),
                    Code = code,
                    RateToRub = Math.Round(rate, 4),
                    UpdatedAt = DateTime.UtcNow
                });
            }
        }

        public static decimal ConvertToCurrency(decimal amountRub, string targetCurrency)
        {
            if (targetCurrency == "RUB")
                return amountRub;

            try
            {
                using (var bd = new Context())
                {
                    var rate = bd.CurrencyRates.FirstOrDefault(c => c.Code == targetCurrency);
                    if (rate != null && rate.RateToRub > 0)
                        return amountRub / rate.RateToRub;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error(ex, Resources.ErrorCurrencyConversion);
            }

            return amountRub;
        }

        private class CbrResponse
        {
            public ValuteData Valute { get; set; }
        }

        private class ValuteData
        {
            public CurrencyInfo USD { get; set; }
            public CurrencyInfo EUR { get; set; }
            public CurrencyInfo CNY { get; set; }
        }

        private class CurrencyInfo
        {
            public decimal Value { get; set; }
        }
    }
}