using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Properties;
using System.Text.RegularExpressions;
using Sklad1.Models;

namespace Sklad1.Forms
{
    public partial class FormProduct : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FormProduct()
        {
            InitializeComponent();
            LoadCategories();
            LoadUnits();

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
            cmbUnit.SelectedIndex = 0;
        }

        private bool IsValidName(string text)
        {
            var trimmed = text.Trim();
            return Regex.IsMatch(trimmed, @"^[а-яА-ЯёЁa-zA-Z0-9\s\-]+$");
        }

        private bool IsValidArticle(string text)
        {
            var trimmed = text.Trim();
            return Regex.IsMatch(trimmed, @"^[0-9\s\-]+$");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var article = txtArticle.Text.Trim();
            var name = txtName.Text.Trim();
            var categoryName = cmbCategory.Text.Trim();
            var priceText = txtPurchasePrice.Text.Trim();
            var quantityText = txtQuantity.Text.Trim();
            var unit = cmbUnit.SelectedItem?.ToString() ?? Resources.DefaultUnit;
            var expiryDate = dtpExpDate.Value;
            var expiryDateUtc = DateTime.SpecifyKind(expiryDate, DateTimeKind.Utc);

            if (string.IsNullOrWhiteSpace(article) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(categoryName) ||
                string.IsNullOrWhiteSpace(priceText) ||
                string.IsNullOrWhiteSpace(quantityText))
            {
                MessageBox.Show(Resources.FillAllFields);
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

            if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
            {
                MessageBox.Show(Resources.InvalidQuantity);
                return;
            }

            try
            {
                using (var bd = new Context())
                {
                    if (bd.Products.Any(p => p.Article == article))
                    {
                        MessageBox.Show(Resources.ArticleExists);
                        return;
                    }

                    if (bd.Products.Any(p => p.Name == name))
                    {
                        MessageBox.Show(Resources.ProductExists);
                        return;
                    }

                    var category = bd.Categories.FirstOrDefault(c => c.Name == categoryName);
                    if (category == null)
                    {
                        MessageBox.Show(Resources.CategoryNotFound);
                        return;
                    }

                    var product = new Product
                    {
                        Id = Guid.NewGuid(),
                        Article = article,
                        Name = name,
                        CategoryId = category.Id,
                        PurchasePrice = price,
                        Quantity = quantity,
                        InitialQuantity = quantity,
                        Unit = unit
                    };

                    bd.Products.Add(product);

                    var batch = new ProductBatch
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Quantity = quantity,
                        PurchasePrice = price,
                        ExpiryDate = expiryDateUtc,
                        Status = "active"
                    };
                    bd.ProductBatches.Add(batch);

                    bd.SaveChanges();

                    MessageBox.Show(Resources.ProductCreate);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorCreateProduct);
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