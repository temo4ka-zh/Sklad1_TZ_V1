using Microsoft.EntityFrameworkCore;
using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Models;
using Sklad1.Properties;

namespace Sklad1.Forms
{
    /// <summary>
    /// Форма контроля сроков годности
    /// </summary>
    public partial class FormExpiryDates : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FormExpiryDates()
        {
            InitializeComponent();
            AppCurrencyManager.CurrencyChanged += OnCurrencyChanged;
            LoadBatches();
        }
        private void OnCurrencyChanged()
        {
            LoadBatches();
        }
        private void LoadBatches()
        {
            try
            {
                using (var bd = new Context())
                {
                    var symbol = AppCurrencyManager.GetCurrencySymbol();

                    var data = bd.ProductBatches
                        .Include(b => b.Product)
                        .Where(b => b.Quantity > 0 && b.Product != null)
                        .Select(b => new
                        {
                            Article = b.Product.Article ?? Resources.NoArticle,
                            ProductName = b.Product.Name ?? Resources.UnknownProduct,
                            b.Quantity,
                            ExpiryDate = b.ExpiryDate,
                            DaysLeft = (b.ExpiryDate - DateTime.UtcNow.Date).Days,
                           b.PurchasePrice
                        }).ToList();

                    var displayData = data.Select(b => new
                    {
                        b.Article,
                        b.ProductName,
                        b.Quantity,
                        ExpiryDate = b.ExpiryDate.ToString("dd.MM.yyyy"),
                        b.DaysLeft,
                        PurchasePrice = $"{ConvertCurrency(b.PurchasePrice):F2} {symbol}",
                        Status = GetStatusText(b.DaysLeft)
                    }).ToList();

                    dgvExpDates.DataSource = displayData;

                    dgvExpDates.Columns["Article"].HeaderText = Resources.Article;
                    dgvExpDates.Columns["ProductName"].HeaderText = Resources.ProductName;
                    dgvExpDates.Columns["Quantity"].HeaderText = Resources.Quantity;
                    dgvExpDates.Columns["ExpiryDate"].HeaderText = Resources.ExpiryDate;
                    dgvExpDates.Columns["DaysLeft"].HeaderText = Resources.DaysLeft;
                    dgvExpDates.Columns["PurchasePrice"].HeaderText = Resources.PurchasePrice;
                    dgvExpDates.Columns["Status"].HeaderText = Resources.Status;
                    dgvExpDates.Columns["ExpiryDate"].DefaultCellStyle.Format = "dd.MM.yyyy";

                    foreach (DataGridViewRow row in dgvExpDates.Rows)
                    {
                        if (row.Cells["DaysLeft"].Value == null) continue;
                        int daysLeft = Convert.ToInt32(row.Cells["DaysLeft"].Value);
                        if (daysLeft < 0) row.DefaultCellStyle.BackColor = Color.LightCoral;
                        else if (daysLeft <= 3) row.DefaultCellStyle.BackColor = Color.Orange;
                        else if (daysLeft <= 7) row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorLoadBatches);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }

        private string GetStatusText(int daysLeft)
        {
            if (daysLeft < 0) return Resources.StatusExpired;
            if (daysLeft <= 3) return Resources.Discount30;
            if (daysLeft <= 7) return Resources.Discount10;
            return Resources.Normal;
        }

        private decimal ConvertCurrency(decimal amount)
        {
            string targetCurrency = AppCurrencyManager.CurrentCurrency;
            if (targetCurrency == "RUB") return amount;

            using (var bd = new Context())
            {
                var rate = bd.CurrencyRates.FirstOrDefault(c => c.Code == targetCurrency);
                if (rate != null && rate.RateToRub > 0)
                    return amount / rate.RateToRub;
            }
            return amount;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LoadBatches();
        }

        private async void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.ConfirmWriteOff, Resources.Confirmation, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            btnWriteOff.Enabled = false;
            btnWriteOff.Text = Resources.WritingOff;

            try
            {
                var count = await ExpiryService.WriteOffExpiredBatches();
                MessageBox.Show(string.Format(Resources.WriteOffComplete, count));
                dgvExpDates.DataSource = null;
                LoadBatches();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorWriteOff);
                MessageBox.Show(Resources.ErrorSystem);
            }
            finally
            {
                btnWriteOff.Enabled = true;
                btnWriteOff.Text = Resources.WriteOff;
            }
        }
    }
}
