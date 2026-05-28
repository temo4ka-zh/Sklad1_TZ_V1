using Microsoft.EntityFrameworkCore;
using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Sklad1.Forms
{
    public partial class FormShipmentHistory : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FormShipmentHistory()
        {
            InitializeComponent();
            AppCurrencyManager.CurrencyChanged += OnCurrencyChanged;
            SetupSearchFields();
            LoadHistory();

            btnSearch.Click += (s, e) => LoadHistory();
            btnClear.Click += (s, e) => ClearFilters();
        }
        private void OnCurrencyChanged()
        {
            LoadHistory();
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
        private void SetupSearchFields()
        {
            if (cmbSearchField == null) return;

            cmbSearchField.Items.Clear();
            cmbSearchField.Items.AddRange(new string[] { Resources.Date, Resources.Client, Resources.ItemsCount });
            cmbSearchField.SelectedIndex = 0;

            LoadSearchSuggestions();
            cmbSearchField.SelectedIndexChanged += (s, e) => LoadSearchSuggestions();
        }

        private void LoadSearchSuggestions()
        {
            if (txtSearchValue == null) return;

            txtSearchValue.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSearchValue.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var autoCompleteCollection = new AutoCompleteStringCollection();

            try
            {
                using (var bd = new Context())
                {
                    string searchField = cmbSearchField?.SelectedItem?.ToString() ?? Resources.Client;

                    if (searchField == Resources.Client)
                    {
                        var clients = bd.Shipments
                            .Select(s => s.Client)
                            .Where(c => c != null)
                            .Distinct()
                            .ToList();
                        autoCompleteCollection.AddRange(clients.ToArray());
                    }
                    else if (searchField == Resources.ItemsCount)
                    {
                        var counts = bd.Shipments
                            .Select(s => s.ShipmentItems.Count)
                            .Distinct()
                            .OrderBy(c => c)
                            .Select(c => c.ToString())
                            .ToList();
                        autoCompleteCollection.AddRange(counts.ToArray());
                    }
                    else if (searchField == Resources.Date)
                    {
                        var dates = bd.Shipments
                            .Select(s => s.Date.Date)
                            .Distinct()
                            .OrderBy(d => d)
                            .Select(d => d.ToString("dd.MM.yyyy"))
                            .ToList();
                        autoCompleteCollection.AddRange(dates.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorLoadSearch);
            }

            txtSearchValue.AutoCompleteCustomSource = autoCompleteCollection;
        }

        private void ClearFilters()
        {
            cmbSearchField.SelectedIndex = 0;
            txtSearchValue.Text = "";
            LoadHistory();
        }

        private void LoadHistory()
        {
            try
            {
                using (var bd = new Context())
                {
                    var query = bd.Shipments
                        .Include(s => s.ShipmentItems)
                        .AsQueryable();

                    string searchField = cmbSearchField?.SelectedItem?.ToString() ?? Resources.Client;
                    string searchValue = txtSearchValue?.Text?.Trim() ?? "";

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        if (searchField == Resources.Date)
                        {
                            if (DateTime.TryParse(searchValue, out DateTime searchDate))
                            {
                                var startDate = new DateTime(searchDate.Year, searchDate.Month, searchDate.Day, 0, 0, 0, DateTimeKind.Utc);
                                var endDate = startDate.AddDays(1);
                                query = query.Where(s => s.Date >= startDate && s.Date <= endDate);
                            }
                        }
                        else if (searchField == Resources.Client)
                        {
                            query = query.Where(s => s.Client.Contains(searchValue));
                        }
                        else if (searchField == Resources.ItemsCount)
                        {
                            if (int.TryParse(searchValue, out int itemsCount))
                            {
                                query = query.Where(s => s.ShipmentItems.Count == itemsCount);
                            }
                        }
                    }
                    var symbol = AppCurrencyManager.GetCurrencySymbol();  

                    var shipments = query
                        .OrderByDescending(s => s.Date)
                        .Select(s => new
                        {
                            s.Id,
                            s.Date,
                            s.Client,
                            ItemsCount = s.ShipmentItems.Count,
                            TotalAmount = s.ShipmentItems.Sum(si => si.Quantity * si.PriceAtShipment)
                        })
                        .ToList();

                    var displayData = shipments.Select(s => new
                    {
                        s.Id,
                        Date = s.Date.Kind == DateTimeKind.Utc
                            ? s.Date.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
                            : DateTime.SpecifyKind(s.Date, DateTimeKind.Utc).ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
                        s.Client,
                        s.ItemsCount,
                        TotalAmount = $"{ConvertCurrency(s.TotalAmount):F2} {symbol}"
                    }).ToList();

                    if (dgvHistory != null)
                    {
                        dgvHistory.DataSource = displayData;

                        if (dgvHistory.Columns.Contains("Id")) dgvHistory.Columns["Id"].Visible = false;
                        if (dgvHistory.Columns.Contains("Date")) dgvHistory.Columns["Date"].HeaderText = Resources.Date;
                        if (dgvHistory.Columns.Contains("Client")) dgvHistory.Columns["Client"].HeaderText = Resources.Client;
                        if (dgvHistory.Columns.Contains("ItemsCount")) dgvHistory.Columns["ItemsCount"].HeaderText = Resources.ItemsCount;
                        if (dgvHistory.Columns.Contains("TotalAmount")) dgvHistory.Columns["TotalAmount"].HeaderText = Resources.TotalAmount;
                    }

                    if (lblTotal != null)
                    {
                        decimal totalSum = shipments.Sum(s => s.TotalAmount);
                        lblTotal.Text = string.Format(Resources.TotalInfo, shipments.Count, $"{ConvertCurrency(totalSum):F2} {symbol}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorLoadHistory);
                Logger.Error(ex, Resources.ErrorLoadHistory);
            }
        }
    }
}