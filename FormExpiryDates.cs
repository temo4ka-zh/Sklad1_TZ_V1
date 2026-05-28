using NLog;
using Sklad1.Data;
using Sklad1.Models;
using Sklad1.Properties;
using System.Text.RegularExpressions;

namespace Sklad1
{
    /// <summary>
    /// Форма редактирования товара
    /// </summary>
    public partial class FormEditProduct : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Guid _productId;
        private Guid _batchId;

        public FormEditProduct(Product product)
        {
            InitializeComponent();
            LoadCategories();
            LoadUnits();

            _productId = product.Id;
            txtArticle.Text = product.Article;
            txtName.Text = product.Name;
            txtPurchasePrice.Text = product.PurchasePrice.ToString();
            cmbUnit.Text = product.Unit ?? "шт";

            using (var bd = new Context())
            {
                var batch = bd.ProductBatches
                    .FirstOrDefault(b => b.ProductId == product.Id && b.Status == "active");

                if (batch != null)
                {
                    _batchId = batch.Id;
                    dtpExpDate.Value = batch.ExpiryDate;
                }

                var category = bd.Categories.Find(product.CategoryId);
                cmbCategory.Text = category?.Name ?? string.Empty;
            }

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadUnits()
        {
            cmbUnit.Items.Clear();
            cmbUnit.Items.AddRange(new string[] { "шт", "кг", "л", "уп", "м", "пач", "кор" });
        }

        private bool IsValidName(string text)
        {
            var trimmed = text.Trim();
            return Regex.IsMatch(trimmed, @"^[а-яА-ЯёЁa-zA-Z0-9\s\-]+$");
        }

        private bool IsValidArticle(string text)
        {
            var trimmed = text.Trim();
            return Regex.IsMatch(trimmed, @"^[а-яА-ЯёЁa-zA-Z0-9\-]+$");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var article = txtArticle.Text.Trim();
            var name = txtName.Text.Trim();
            var categoryName = cmbCategory.Text.Trim();
            var priceText = txtPurchasePrice.Text.Trim();
            var unit = cmbUnit.SelectedItem?.ToString() ?? "шт";
            var expiryDate = dtpExpDate.Value;
            var expiryDateUtc = DateTime.SpecifyKind(expiryDate, DateTimeKind.Utc);

            if (string.IsNullOrWhiteSpace(article) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show(Resources.FillAllFields);
                return;
            }

            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show(Resources.CategoryError);
                return;
            }

            if (expiryDate.Date < DateTime.Today)
            {
                MessageBox.Show(Resources.InvalidExpiryDate);
                return;
            }

            if (!IsValidName(name))
            {
                MessageBox.Show(Resources.InvalidProductName);
                return;
            }

            if (!IsValidArticle(article))
            {
                MessageBox.Show(Resources.InvalidArticle);
                return;
            }

            if (!IsValidName(categoryName))
            {
                MessageBox.Show(Resources.InvalidCategoryName);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show(Resources.InvalidPrice);
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show(Resources.InvalidPositivePrice);
                return;
            }

            try
            {
                using (var bd = new Context())
                {
                    if (bd.Products.Any(p => p.Article == article && p.Id != _productId))
                    {
                        MessageBox.Show(Resources.ArticleExists);
                        return;
                    }

                    if (bd.Products.Any(p => p.Name == name && p.Id != _productId))
                    {
                        MessageBox.Show(Resources.ProductNameExists);
                        return;
                    }

                    var category = bd.Categories.FirstOrDefault(c => c.Name == categoryName);

                    if (category == null)
                    {
                        MessageBox.Show(Resources.CategoryNotFound);
                        return;
                    }

                    var product = bd.Products.Find(_productId);

                    if (product != null)
                    {
                        product.Article = article;
                        product.Name = name;
                        product.CategoryId = category.Id;
                        product.PurchasePrice = price;
                        product.Unit = unit;

                        var batch = bd.ProductBatches.Find(_batchId);
                        if (batch != null)
                        {
                            batch.ExpiryDate = expiryDateUtc;
                            batch.PurchasePrice = price;
                        }

                        var result = bd.SaveChanges();
                        MessageBox.Show(Resources.ProductEdit);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorEditProduct);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }

        private void LoadCategories()
        {
            using (var bd = new Context())
            {
                var categories = bd.Categories.OrderBy(c => c.Name).ToList();

                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";
            }
        }
    }
}
