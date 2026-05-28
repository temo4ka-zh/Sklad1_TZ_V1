using Microsoft.EntityFrameworkCore;
using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Properties;

namespace Sklad1.Forms
{
    /// <summary>
    /// Форма приложения, отображает список товаров
    /// </summary>
    public partial class FormMain : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Свойство для сохранения роли пользователя 
        /// </summary>
        public static UserRole UserRole { get; set; }

        public FormMain()
        {
            InitializeComponent();

            AppCurrencyManager.CurrencyChanged += OnCurrencyChanged;

            var displayRole = UserRole == UserRole.Admin ? Resources.Admin : Resources.Storekeeper;

            this.Text = string.Format(Resources.Title, displayRole);

            LoadProducts();

            btnCreate.Click += btnCreate_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            menuEditProduct.Click += menuEditProduct_Click;
            btnHistory.Click += btnHistory_Click;
            menuShipment.Click += menuShipment_Click;

            if (!IsAdmin())
            {
                btnDelete.Visible = btnEdit.Visible = btnHistory.Visible = false;
                menuProduct.Visible = menuCategory.Visible = false;
                menuEditProduct.Visible = menuEditCategory.Visible = false;
            }

            if (IsAdmin())
            {
                menuShipment.Visible = false;
            }
        }

        private void OnCurrencyChanged()
        {
            LoadProducts();
        }

        private bool IsAdmin() => UserRole == UserRole.Admin;

        public void LoadProducts()
        {
            try
            {
                using (var bd = new Context())
                {
                    var products = bd.Products.Where(p => p.Quantity > 0).ToList();
                    var categories = bd.Categories.ToDictionary(c => c.Id, c => c.Name);

                    var symbol = AppCurrencyManager.GetCurrencySymbol();

                    var batches = bd.ProductBatches
                       .Where(b => b.Status == "active")
                       .ToList();

                    var data = products.Select(p => new
                    {
                        p.Article,
                        p.Name,
                        Category = categories.ContainsKey(p.CategoryId) ? categories[p.CategoryId] : string.Empty,
                        p.InitialQuantity,
                        Unit = p.Unit ?? Resources.DefaultUnit,
                        PurchasePrice = p.PurchasePrice,
                        ExpiryDate = batches.Where(b => b.ProductId == p.Id && b.Quantity > 0)
                                           .Select(b => (DateTime?)b.ExpiryDate)
                                           .DefaultIfEmpty()
                                           .Min(),
                        CurrentQuantity = p.Quantity,
                        DaysLeft = batches.Where(b => b.ProductId == p.Id)
                                 .Select(b => (b.ExpiryDate - DateTime.Today).Days)
                                 .DefaultIfEmpty(999)
                                 .Min(),
                        BatchCount = batches.Count(b => b.ProductId == p.Id && b.Quantity > 0)
                    }).ToList();

                    var displayData = data.Select(d => new
                    {
                        d.Article,
                        d.Name,
                        d.Category,
                        d.InitialQuantity,
                        d.Unit,
                        PurchasePrice = $"{ConvertCurrency(d.PurchasePrice):F2} {symbol}",
                        DiscountPrice = $"{ExpiryService.GetDiscountedPrice(ConvertCurrency(d.PurchasePrice), d.DaysLeft):F2} {symbol}",
                        Discount = ExpiryService.GetDiscountText(d.DaysLeft),
                        d.ExpiryDate,
                        d.CurrentQuantity
                    }).ToList();

                    var sortedData = displayData
                        .OrderBy(d => d.ExpiryDate.HasValue ? d.ExpiryDate.Value : DateTime.MaxValue)
    .ToList();

                    dgvProducts.DataSource = sortedData;

                    dgvProducts.Columns["Article"].HeaderText = Resources.Article;
                    dgvProducts.Columns["Name"].HeaderText = Resources.Name;
                    dgvProducts.Columns["Category"].HeaderText = Resources.Category;
                    dgvProducts.Columns["InitialQuantity"].HeaderText = Resources.InitialQuantity;
                    dgvProducts.Columns["Unit"].HeaderText = Resources.Unit;
                    dgvProducts.Columns["PurchasePrice"].HeaderText = "Цена закупки";
                    dgvProducts.Columns["DiscountPrice"].HeaderText = "Цена со скидкой";
                    dgvProducts.Columns["Discount"].HeaderText = "Скидка";
                    dgvProducts.Columns["ExpiryDate"].HeaderText = Resources.ExpiryDate;
                    dgvProducts.Columns["CurrentQuantity"].HeaderText = Resources.CurrentQuantity;
                    dgvProducts.Columns["ExpiryDate"].DefaultCellStyle.Format = "dd.MM.yyyy";

                    if (dgvProducts.Columns["BatchCount"] != null)
                        dgvProducts.Columns["BatchCount"].Visible = false;

                    ApplyExpiryHighlighting();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ProductLoadError);
                MessageBox.Show(Resources.ProductLoadError);
            }
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

        private void ApplyExpiryHighlighting()
        {
            int warningDays = GetExpiryWarningDays();
            int dangerDays = GetExpiryDangerDays();
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["ExpiryDate"].Value == null) continue;
                if (DateTime.TryParse(row.Cells["ExpiryDate"].Value.ToString(), out DateTime expiryDate))
                {
                    int daysLeft = (expiryDate - DateTime.Today).Days;
                    if (daysLeft < 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (daysLeft <= dangerDays)
                    {
                        row.DefaultCellStyle.BackColor = Color.Orange;
                    }
                    else if (daysLeft <= warningDays)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }
        }

        private int GetExpiryWarningDays()
        {
            try
            {
                using (var bd = new Context())
                {
                    var setting = bd.Settings.FirstOrDefault();
                    return setting?.ExpiryWarningDays ?? 7;
                }
            }
            catch
            {
                return 7;
            }
        }

        private int GetExpiryDangerDays()
        {
            try
            {
                using (var bd = new Context())
                {
                    var setting = bd.Settings.FirstOrDefault();
                    return setting?.ExpiryDangerDays ?? 3;
                }
            }
            catch
            {
                return 3;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateMenu.Show(btnCreate, 0, btnCreate.Height);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMenu.Show(btnEdit, 0, btnEdit.Height);
        }

        private void menuCategory_Click(object sender, EventArgs e)
        {
            var form = new FormCategory();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void menuProduct_Click(object sender, EventArgs e)
        {
            var form = new FormProduct();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void menuEditCategory_Click(object sender, EventArgs e)
        {
            var selectedRow = dgvProducts.SelectedRows[0];
            var categoryName = selectedRow.Cells["Category"].Value.ToString();

            using (var bd = new Context())
            {
                var category = bd.Categories.FirstOrDefault(c => c.Name == categoryName);

                if (category == null)
                {
                    MessageBox.Show(Resources.CategoryNotFound);
                    return;
                }

                var form = new FormEditCategory(category);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }

            }
        }
        private void menuEditProduct_Click(object sender, EventArgs e)
        {

            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.SelectProduct);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            var article = selectedRow.Cells["Article"].Value.ToString();

            using (var bd = new Context())
            {
                var product = bd.Products.FirstOrDefault(p => p.Article == article);
                if (product != null)
                {
                    var form = new FormEditProduct(product);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadProducts();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.SelectProduct);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            var article = selectedRow.Cells["Article"].Value.ToString();

            if (MessageBox.Show(Resources.ConfirmDeleteProductText, Resources.ConfirmDelete, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var bd = new Context())
                    {
                        var product = bd.Products.FirstOrDefault(p => p.Article == article);
                        if (product == null) return;

                        var losses = bd.Losses.Where(l => l.ProductId == product.Id).ToList();
                        if (losses.Any())
                        {
                            bd.Losses.RemoveRange(losses);
                        }

                        var batches = bd.ProductBatches.Where(b => b.ProductId == product.Id).ToList();
                        var batchIds = batches.Select(b => b.Id).ToList();

                        if (batchIds.Any())
                        {
                            var supplyItems = bd.SupplyItems.Where(si => batchIds.Contains(si.BatchId)).ToList();
                            if (supplyItems.Any())
                            {
                                bd.SupplyItems.RemoveRange(supplyItems);
                            }

                            bd.ProductBatches.RemoveRange(batches);
                        }

                        var shipmentItems = bd.ShipmentItems.Where(si => si.ProductId == product.Id).ToList();
                        if (shipmentItems.Any())
                        {
                            bd.ShipmentItems.RemoveRange(shipmentItems);
                        }
                        bd.Products.Remove(product);

                        bd.SaveChanges();
                        MessageBox.Show(Resources.ProductDelete);
                        LoadProducts();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, Resources.ErrorDeletingProduct);
                    MessageBox.Show(Resources.ErrorSystem);
                }
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            new FormShipmentHistory().ShowDialog();
        }

        private void menuShipment_Click(object sender, EventArgs e)
        {
            var form = new FormShipment();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }
    }
}
