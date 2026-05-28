using Microsoft.EntityFrameworkCore;
using NLog;
using OfficeOpenXml;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Properties;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace Sklad1.Forms
{
    public partial class FormAnalyticReport : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private string _displayCurrency = "RUB";
        public FormAnalyticReport()
        {
            InitializeComponent();

            AppCurrencyManager.CurrencyChanged += OnCurrencyChanged;
            LoadDisplayCurrency();
            dtpDateFrom.Value = DateTime.Now.AddMonths(-1);
            dtpDateTo.Value = DateTime.Now;
            btnGenerate.Click += btnGenerate_Click;
            btnExport.Click += btnExport_Click;
            //btnPrint.Click += btnPrint_Click;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }


        private void OnCurrencyChanged()
        {
            _displayCurrency = AppCurrencyManager.CurrentCurrency;
            RefreshReport();
        }

        private void LoadDisplayCurrency()
        {
            _displayCurrency = AppCurrencyManager.CurrentCurrency;
        }

        public async void RefreshReport()
        {
            await GenerateAndDisplayReport();
        }

        private async Task GenerateAndDisplayReport()
        {
            var from = dtpDateFrom.Value.Date;
            var to = dtpDateTo.Value.Date.AddDays(1).AddSeconds(-1);
            var (revenue, cost, losses, details) = await GetReportData(from, to);

            var symbol = GetCurrencySymbol();
            var displayDetails = ((List<object>)details).Select(d =>
            {
                var prop = d.GetType().GetProperties();
                return new
                {
                    Date = prop.First(p => p.Name == "Date").GetValue(d),
                    Client = prop.First(p => p.Name == "Client").GetValue(d),
                    ProductName = prop.First(p => p.Name == "ProductName").GetValue(d),
                    Quantity = prop.First(p => p.Name == "Quantity").GetValue(d),
                    Revenue = $"{ConvertToRub((decimal)prop.First(p => p.Name == "Revenue").GetValue(d), _displayCurrency):F2} {symbol}",
                    Cost = $"{ConvertToRub((decimal)prop.First(p => p.Name == "Cost").GetValue(d), _displayCurrency):F2} {symbol}",
                    Profit = $"{ConvertToRub((decimal)prop.First(p => p.Name == "Revenue").GetValue(d) - (decimal)prop.First(p => p.Name == "Cost").GetValue(d), _displayCurrency):F2} {symbol}"
                };
            }).ToList();

            dgvAnalyticReport.DataSource = displayDetails;

            dgvAnalyticReport.Columns["Date"].HeaderText = Resources.Date;
            dgvAnalyticReport.Columns["Client"].HeaderText = Resources.Client;
            dgvAnalyticReport.Columns["ProductName"].HeaderText = Resources.ProductName;
            dgvAnalyticReport.Columns["Quantity"].HeaderText = Resources.Quantity;
            dgvAnalyticReport.Columns["Revenue"].HeaderText = Resources.Revenue;
            dgvAnalyticReport.Columns["Cost"].HeaderText = Resources.Cost;
            dgvAnalyticReport.Columns["Profit"].HeaderText = Resources.Profit;

            UpdateUI(ConvertToRub(revenue, _displayCurrency), ConvertToRub(cost, _displayCurrency), ConvertToRub(losses, _displayCurrency));
        }//

        private async Task<(decimal Revenue, decimal Cost, decimal Losses, List<object> Details)> GetReportData(DateTime from, DateTime to)
        {
            using (var bd = new Context())
            {
                var fromUtc = DateTime.SpecifyKind(from, DateTimeKind.Utc);
                var toUtc = DateTime.SpecifyKind(to, DateTimeKind.Utc);

                var shipments = await bd.Shipments.Where(s => s.Date >= fromUtc && s.Date <= toUtc).ToListAsync();
                var shipmentIds = shipments.Select(s => s.Id).ToList();

                if (shipmentIds.Count == 0)
                    return (0, 0, 0, new List<object>());

                var items = await bd.ShipmentItems.Where(si => shipmentIds.Contains(si.ShipmentId)).ToListAsync();
                var revenue = items.Sum(si => si.Quantity * si.PriceAtShipment);
                var cost = items.Sum(si => si.Quantity * (si.CostAtShipment > 0 ? si.CostAtShipment : si.PriceAtShipment * 0.7m));
                var losses = await bd.Losses.Where(l => l.Date >= fromUtc && l.Date <= toUtc).SumAsync(l => l.Quantity * l.PurchasePrice);

                var products = await bd.Products.ToDictionaryAsync(p => p.Id, p => p.Name);

                var details = shipments.SelectMany(s => items.Where(i => i.ShipmentId == s.Id).Select(i => new
                {
                    Date = s.Date.ToLocalTime().ToString("dd.MM.yyyy"),
                    Client = s.Client ?? Resources.UnknownClient,
                    ProductName = products.ContainsKey(i.ProductId) ? products[i.ProductId] : Resources.UnknownProduct,
                    Quantity = i.Quantity,
                    Revenue = i.Quantity * i.PriceAtShipment,
                    Cost = i.Quantity * (i.CostAtShipment > 0 ? i.CostAtShipment : i.PriceAtShipment * 0.7m)
                })).OrderByDescending(d => d.Date).ToList<object>();

                return (revenue, cost, losses, details);
            }
        }

        private void UpdateUI(decimal revenue, decimal cost, decimal losses)
        {
            var symbol = GetCurrencySymbol();
            var profit = revenue - cost - losses;

            if (profit < 0) profit = 0;

            lblRevenue.Text = $"{revenue:F2} {symbol}";
            lblCost.Text = $"{cost:F2} {symbol}";
            lblLosses.Text = $"{losses:F2} {symbol}";
            lblProfit.Text = $"{profit:F2} {symbol}";
            lblProfit.ForeColor = profit >= 0 ? Color.Green : Color.Red;
        }

        private string GetCurrencySymbol()
        {
            return _displayCurrency switch
            {
                "RUB" => "₽",
                "USD" => "$",
                "EUR" => "€",
                "CNY" => "¥",
                _ => _displayCurrency
            };
        }

        private bool ValidateDates()
        {
            if (dtpDateFrom.Value > dtpDateTo.Value)
            {
                MessageBox.Show(Resources.DateFromLaterThanDateTo);
                return false;
            }
            return true;
        }

        private decimal ConvertToRub(decimal amount, string targetCurrency)
        {
            if (targetCurrency == "RUB") return amount;
            try
            {
                using (var bd = new Context())
                {
                    var rate = bd.CurrencyRates.FirstOrDefault(c => c.Code == targetCurrency);
                    if (rate != null && rate.RateToRub > 0) return amount / rate.RateToRub;
                }
            }
            catch (Exception ex) { Logger.Error(ex, Resources.ErrorCurrencyConversion); }
            return amount;
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateDates()) return;

            btnGenerate.Enabled = false;
            btnGenerate.Text = Resources.Generating;
            try
            {
                await GenerateAndDisplayReport();
            }
            catch (Exception ex) { Logger.Error(ex, Resources.ErrorGenerateReport); MessageBox.Show(Resources.ErrorSystem); }
            finally { btnGenerate.Enabled = true; btnGenerate.Text = Resources.Generate; }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;

            if (dgvAnalyticReport.Rows.Count == 0) { MessageBox.Show(Resources.NoDataToExport); return; }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = Resources.CsvFilter;
                sfd.DefaultExt = "csv";
                sfd.FileName = $"Report_{dtpDateFrom.Value:yyyyMMdd}-{dtpDateTo.Value:yyyyMMdd}";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(sfd.FileName).ToLower();
                    try
                    {
                        if (ext == ".csv")
                            await ExportToCsv(sfd.FileName);
                        else if (ext == ".xlsx")
                            await ExportToExcel(sfd.FileName);

                        MessageBox.Show(string.Format(Resources.ReportSaved, sfd.FileName));
                    }
                    catch (Exception ex) 
                    { 
                        Logger.Error(ex, Resources.ErrorExport); 
                        MessageBox.Show(Resources.ErrorSystem); 
                    }
                }
            }
            btnExport.Enabled = true;
        }
        private async Task ExportToCsv(string fileName)
        {
            var csv = new StringBuilder();
            csv.AppendLine(string.Join(";", dgvAnalyticReport.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText)));
            foreach (DataGridViewRow row in dgvAnalyticReport.Rows)
            {
                if (row.IsNewRow) continue;
                csv.AppendLine(string.Join(";", row.Cells.Cast<DataGridViewCell>().Select(c => c.Value?.ToString() ?? "")));
            }
            csv.AppendLine();
            csv.AppendLine($"{Resources.Revenue};{lblRevenue.Text}");
            csv.AppendLine($"{Resources.Cost};{lblCost.Text}");
            csv.AppendLine($"{Resources.Losses};{lblLosses.Text}");
            csv.AppendLine($"{Resources.Profit};{lblProfit.Text}");
            await File.WriteAllTextAsync(fileName, csv.ToString(), Encoding.UTF8);
        }

        private async Task ExportToExcel(string fileName)
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add(Resources.ReportTitle);
                sheet.Cells["A1"].LoadFromDataTable(GetDataTable(), true);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                await package.SaveAsAsync(new FileInfo(fileName));
            }
        }

        private DataTable GetDataTable()
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn col in dgvAnalyticReport.Columns)
                dt.Columns.Add(col.HeaderText);

            foreach (DataGridViewRow row in dgvAnalyticReport.Rows)
            {
                if (row.IsNewRow) continue;
                dt.Rows.Add(row.Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());
            }
            return dt;
        }

        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    btnPrint.Enabled = false;

        //    if (dgvAnalyticReport.Rows.Count == 0) 
        //    { 
        //        MessageBox.Show(Resources.NoDataToPrint);
        //        btnPrint.Enabled = true;
        //        return; 
        //    }

        //    PrintDialog printDialog = new PrintDialog();
        //    PrintDocument printDocument = new PrintDocument();
        //    printDocument.PrintPage += (s, ev) =>
        //    {
        //        int y = 50, x = 50, rowHeight = 25;
        //        int[] colWidths = { 100, 150, 150, 80, 120, 120, 120 };
        //        string[] headers = { Resources.Date, Resources.Client, Resources.ProductName, Resources.Quantity, Resources.Revenue, Resources.Cost, Resources.Profit };

        //        ev.Graphics.DrawString(Resources.ReportTitle, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, x, y); y += 40;
        //        ev.Graphics.DrawString(string.Format(Resources.PeriodFormat, dtpDateFrom.Value.ToString("dd.MM.yyyy"), dtpDateTo.Value.ToString("dd.MM.yyyy")), new Font("Arial", 12), Brushes.Black, x, y); y += 25;
        //        ev.Graphics.DrawString(string.Format(Resources.DateFormat, DateTime.Now.ToString("dd.MM.yyyy HH:mm")), new Font("Arial", 10), Brushes.Black, x, y); y += 40;
        //        ev.Graphics.DrawString($"{Resources.Revenue}: {lblRevenue.Text}", new Font("Arial", 12, FontStyle.Bold), Brushes.Green, x, y); y += 25;
        //        ev.Graphics.DrawString($"{Resources.Cost}: {lblCost.Text}", new Font("Arial", 12), Brushes.Black, x, y); y += 25;
        //        ev.Graphics.DrawString($"{Resources.Losses}: {lblLosses.Text}", new Font("Arial", 12), Brushes.Red, x, y); y += 25;
        //        ev.Graphics.DrawString($"{Resources.Profit}: {lblProfit.Text}", new Font("Arial", 12, FontStyle.Bold), lblProfit.ForeColor == Color.Green ? Brushes.Green : Brushes.Red, x, y); y += 40;

        //        x = 50;
        //        for (int i = 0; i < headers.Length; i++)
        //        {
        //            ev.Graphics.FillRectangle(Brushes.LightGray, x, y, colWidths[i], rowHeight);
        //            ev.Graphics.DrawRectangle(Pens.Black, x, y, colWidths[i], rowHeight);
        //            ev.Graphics.DrawString(headers[i], new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + 5, y + 5);
        //            x += colWidths[i];
        //        }
        //        y += rowHeight;

        //        foreach (DataGridViewRow row in dgvAnalyticReport.Rows)
        //        {
        //            if (row.IsNewRow) continue;
        //            x = 50;
        //            ev.Graphics.DrawString(row.Cells["Date"]?.Value?.ToString() ?? "", new Font("Arial", 9), Brushes.Black, x + 5, y + 5); x += colWidths[0];
        //            ev.Graphics.DrawString(row.Cells["Client"]?.Value?.ToString() ?? "", new Font("Arial", 9), Brushes.Black, x + 5, y + 5); x += colWidths[1];
        //            ev.Graphics.DrawString(row.Cells["ProductName"]?.Value?.ToString() ?? "", new Font("Arial", 9), Brushes.Black, x + 5, y + 5); x += colWidths[2];
        //            ev.Graphics.DrawString(row.Cells["Quantity"]?.Value?.ToString() ?? "", new Font("Arial", 9), Brushes.Black, x + 5, y + 5); x += colWidths[3];
        //            ev.Graphics.DrawString(row.Cells["Revenue"]?.Value?.ToString() ?? "", new Font("Arial", 9), Brushes.Black, x + 5, y + 5); x += colWidths[4];
        //            ev.Graphics.DrawString(row.Cells["Cost"]?.Value?.ToString() ?? "", new Font("Arial", 9), Brushes.Black, x + 5, y + 5); x += colWidths[5];
        //            ev.Graphics.DrawString(row.Cells["Profit"]?.Value?.ToString() ?? "", new Font("Arial", 9), Brushes.Black, x + 5, y + 5);
        //            y += rowHeight;
        //            if (y > ev.MarginBounds.Bottom - 50) { ev.HasMorePages = true; return; }
        //        }
        //    };
        //    printDialog.Document = printDocument;
        //    if (printDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        try { printDocument.Print(); MessageBox.Show(Resources.PrintSuccess); }
        //        catch (Exception ex) { Logger.Error(ex, Resources.ErrorPrint); MessageBox.Show(Resources.ErrorSystem); }
        //    }
        //    btnPrint.Enabled = true;
        //}
    }
}