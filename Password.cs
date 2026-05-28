using Microsoft.EntityFrameworkCore;
using Sklad1.Data;
using Sklad1.Models;
using Sklad1.Properties;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Централизованное управление валютой приложения
    /// </summary>
    public static class AppCurrencyManager
    {
        private static string _currentCurrency = "RUB";
        private static bool _isInitialized = false;

        public static event Action CurrencyChanged;

        public static string CurrentCurrency
        {
            get
            {
                if (!_isInitialized)
                    LoadFromDatabase();
                return _currentCurrency;
            }
            set
            {
                if (_currentCurrency != value)
                {
                    _currentCurrency = value;
                    SaveToDatabase(value);
                    OnCurrencyChanged();
                }
            }
        }

        public static void LoadFromDatabase()
        {
            try
            {
                using (var bd = new Context())
                {
                    var setting = bd.Settings.AsNoTracking().FirstOrDefault();
                    _currentCurrency = setting?.DisplayCurrency ?? "RUB";
                    _isInitialized = true;
                }
            }
            catch
            {
                _currentCurrency = "RUB";
                _isInitialized = true;
            }
        }

        private static void SaveToDatabase(string currency)
        {
            try
            {
                using (var bd = new Context())
                {
                    var setting = bd.Settings.FirstOrDefault();
                    if (setting == null)
                    {
                        setting = new Setting
                        {
                            Id = Guid.NewGuid(),
                            DisplayCurrency = currency,
                            ExpiryWarningDays = 7,
                            ExpiryDangerDays = 3
                        };
                        bd.Settings.Add(setting);
                    }
                    else
                    {
                        setting.DisplayCurrency = currency;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorSaveCurrency);
            }
        }

        private static void OnCurrencyChanged()
        {
            CurrencyChanged?.Invoke();
        }

        public static string GetCurrencySymbol(string currency = null)
        {
            string cur = currency ?? CurrentCurrency;
            return cur switch
            {
                "RUB" => "₽",
                "USD" => "$",
                "EUR" => "€",
                "CNY" => "¥",
                _ => cur
            };
        }
    }
}