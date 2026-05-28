using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Models;
using Sklad1.Properties;
using System.Globalization;

namespace Sklad1.Forms
{
    public partial class FormCurrencySettings : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private List<CurrencyRate> _rates;

        public FormCurrencySettings()
        {
            InitializeComponent();
            LoadCurrencies();
            LoadRates();
            LoadDisplayCurrency();
        }

        private void LoadCurrencies()
        {
            cmbDisplayCurrency.Items.Clear();
            cmbDisplayCurrency.Items.AddRange(new string[] { "RUB", "USD", "EUR", "CNY" });
        }

        private void LoadRates()
        {
            try
            {
                using (var bd = new Context())
                {
                    _rates = bd.CurrencyRates.ToList();

                    if (_rates.Count == 0)
                    {
                        _rates = new List<CurrencyRate>
                        {
                            new CurrencyRate { Id = Guid.NewGuid(), Code = "RUB", RateToRub = 1, UpdatedAt = DateTime.UtcNow },
                            new CurrencyRate { Id = Guid.NewGuid(), Code = "USD", RateToRub = 0, UpdatedAt = DateTime.UtcNow },
                            new CurrencyRate { Id = Guid.NewGuid(), Code = "EUR", RateToRub = 0, UpdatedAt = DateTime.UtcNow },
                            new CurrencyRate { Id = Guid.NewGuid(), Code = "CNY", RateToRub = 0, UpdatedAt = DateTime.UtcNow }
                        };
                        bd.CurrencyRates.AddRange(_rates);
                        bd.SaveChanges();
                    }

                    UpdateRatesGrid();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorLoadRates);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }
        private void UpdateRatesGrid()
        {
            var displayData = _rates.Select(r => new
            {
                r.Code,
                Rate = r.RateToRub.ToString("F4", CultureInfo.InvariantCulture).Replace('.', ','),
                Updated = r.UpdatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
            }).ToList();

            dgvRates.DataSource = null;
            dgvRates.DataSource = displayData;

            dgvRates.Columns["Code"].HeaderText = Resources.Currency;
            dgvRates.Columns["Rate"].HeaderText = Resources.RateToRub;
            dgvRates.Columns["Updated"].HeaderText = Resources.Updated;
        }

        private void LoadDisplayCurrency()
        {
            try
            {
                using (var bd = new Context())
                {
                    var setting = bd.Settings.FirstOrDefault();
                    if (setting != null)
                    {
                        cmbDisplayCurrency.Text = setting.DisplayCurrency;
                    }
                    else
                    {
                        cmbDisplayCurrency.Text = "RUB";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorLoadDisplayCurrency);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnUpdate.Text = Resources.Updating;

            try
            {
                var success = await CurrencyService.UpdateRates();

                if (success)
                {
                    MessageBox.Show(Resources.RatesUpdated);
                    LoadRates();
                }
                else
                {
                    MessageBox.Show(Resources.RatesUpdateFailed);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorUpdateRates);
                MessageBox.Show(Resources.ErrorSystem);
            }
            finally
            {
                btnUpdate.Enabled = true;
                btnUpdate.Text = Resources.UpdateRates;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AppCurrencyManager.CurrentCurrency = cmbDisplayCurrency.Text;

                MessageBox.Show(Resources.SettingsSaved);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorSaveSettings);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }
    }
}