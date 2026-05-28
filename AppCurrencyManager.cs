using NLog;
using OfficeOpenXml;
using Sklad1.Data;
using Sklad1.Models;
using Sklad1.Properties;
using System.Text;

namespace Sklad1.Forms
{
    public partial class FormSupplyImport : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private string _selectedFilePath = string.Empty;
        private List<ImportRow> _rows = new List<ImportRow>();

        public FormSupplyImport()
        {
            InitializeComponent();

            // Настройка EPPlus (внутри конструктора!)
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Drag & Drop для панели
            panel2.AllowDrop = true;
            panel2.DragEnter += Panel2_DragEnter;
            panel2.DragDrop += Panel2_DragDrop;
            panel2.Click += Panel2_Click;
        }


        private bool HasNegativeQuantities()
        {
            var negatives = _rows.Where(r => r.Quantity < 0).ToList();

            if (negatives.Any())
            {
                var badProducts = string.Join(", ", negatives.Take(5).Select(r => r.ProductName));
                var message = string.Format(Resources.ErrorNegativeQuantityMessage, badProducts);

                if (negatives.Count > 5)
                    message += string.Format(Resources.ErrorNegativeQuantityAndMore, negatives.Count - 5);

                message += Resources.ErrorNegativeQuantityFooter;

                MessageBox.Show(Resources.ErrorNegativeQuantityFooter);
                return true;
            }

            return false;
        }

        private void Panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Panel2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                _selectedFilePath = files[0];
                lblFile.Text = Path.GetFileName(_selectedFilePath);
                LoadFile();
            }
        }

        private void Panel2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = Resources.FileFilter;
                ofd.Title = Resources.DialogTitle;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = ofd.FileName;
                    lblFile.Text = Path.GetFileName(_selectedFilePath);
                    LoadFile();
                }
            }
        }

        private void LoadFile()
        {
            try
            {
                _rows.Clear();

                if (string.IsNullOrEmpty(_selectedFilePath))
                {
                    MessageBox.Show(Resources.SelectFileFirst);
                    return;
                }

                string ext = Path.GetExtension(_selectedFilePath).ToLower();

                if (ext == ".csv")
                    LoadCsv();
                else if (ext == ".xlsx")
                    LoadExcel();
                else
                {
                    MessageBox.Show(Resources.SupportedFormatOnly);
                    return;
                }

                ShowPreview();
                btnImport.Enabled = _rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorFileLoad);
            }
        }

        private void LoadCsv()
        {
            var lines = File.ReadAllLines(_selectedFilePath, Encoding.UTF8);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                var parts = lines[i].Split(';');
                if (parts.Length < 6) continue;

                _rows.Add(new ImportRow
                {
                    ProductName = parts[0].Trim(),
                    Quantity = ParseInt(parts[1]),
                    Price = ParseDecimal(parts[2]),
                    ExpiryDate = ParseDate(parts[3]),
                    Supplier = parts[4].Trim(),
                    Category = parts[5].Trim()
                });
            }
        }

        private void LoadExcel()
        {
            using (var package = new ExcelPackage(new FileInfo(_selectedFilePath)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int row = 2;

                while (!string.IsNullOrWhiteSpace(sheet.Cells[row, 1]?.Text))
                {
                    _rows.Add(new ImportRow
                    {
                        ProductName = sheet.Cells[row, 1]?.Text?.Trim() ?? "",
                        Quantity = ParseInt(sheet.Cells[row, 2]?.Text),
                        Price = ParseDecimal(sheet.Cells[row, 3]?.Text),
                        ExpiryDate = ParseDate(sheet.Cells[row, 4]?.Text),
                        Supplier = sheet.Cells[row, 5]?.Text?.Trim() ?? "Импорт",
                        Category = sheet.Cells[row, 6]?.Text?.Trim() ?? ""
                    });
                    row++;
                }
            }
        }

        private int ParseInt(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;
            if (int.TryParse(s, out int result)) return result;
            return 0;
        }

        private decimal ParseDecimal(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;
            if (decimal.TryParse(s, out decimal result)) return result;
            return 0;
        }

        private DateTime ParseDate(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return DateTime.Now.AddMonths(6);
            if (DateTime.TryParse(s, out DateTime result)) return result;
            return DateTime.Now.AddMonths(6);
        }

        private void ShowPreview()
        {
            var data = _rows.Select(r => new
            {
                r.ProductName,
                r.Quantity,
                Price = r.Price.ToString("F2"),
                ExpiryDate = r.ExpiryDate.ToString("dd.MM.yyyy"),
                r.Supplier,
                r.Category
            }).ToList();

            dgvPreview.DataSource = data;

            dgvPreview.Columns["ProductName"].HeaderText = Resources.ProductName;
            dgvPreview.Columns["Quantity"].HeaderText = Resources.Quantity;
            dgvPreview.Columns["Price"].HeaderText = Resources.Price;
            dgvPreview.Columns["ExpiryDate"].HeaderText = Resources.ExpiryDate;
            dgvPreview.Columns["Supplier"].HeaderText = Resources.Supplier;
            dgvPreview.Columns["Category"].HeaderText = "Категория";
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (_rows.Count == 0)
            {
                MessageBox.Show(Resources.NoDataToImport);
                return;
            }

            if (HasNegativeQuantities())
            {
                btnImport.Enabled = true;
                btnImport.Text = Resources.BtnImportText;
                return;
            }

            btnImport.Enabled = false;
            btnImport.Text = Resources.BtnImport;

            try
            {
                int imported = 0;
                int newProducts = 0;

                using (var bd = new Context())
                {
                    using (var transaction = await bd.Database.BeginTransactionAsync())
                    {
                        var supply = new Supply
                        {
                            Id = Guid.NewGuid(),
                            UserId = CurrentUser.Id,
                            Supplier = "Импорт",
                            Date = DateTime.UtcNow,
                            Source = "import"
                        };
                        bd.Supplies.Add(supply);
                        await bd.SaveChangesAsync();

                        foreach (var row in _rows)
                        {
                            var category = bd.Categories.FirstOrDefault(c => c.Name == row.Category);

                            if (category == null)
                            {
                                MessageBox.Show($"Категория '{row.Category}' не найдена. Товар '{row.ProductName}' пропущен.");
                                continue;
                            }

                            var product = bd.Products.FirstOrDefault(p => p.Name == row.ProductName);

                            if (product == null)
                            {
                                product = new Product
                                {
                                    Id = Guid.NewGuid(),
                                    Article = GenerateArticle(row.ProductName),
                                    Name = row.ProductName,
                                    CategoryId = category.Id,
                                    PurchasePrice = row.Price,
                                    Quantity = row.Quantity,
                                    InitialQuantity = row.Quantity,
                                    Unit = Resources.DefaultUnit
                                };
                                bd.Products.Add(product);
                                newProducts++;
                                await bd.SaveChangesAsync();
                            }
                            else
                            {
                                product.Quantity += row.Quantity;
                            }

                            var batch = new ProductBatch
                            {
                                Id = Guid.NewGuid(),
                                ProductId = product.Id,
                                Quantity = row.Quantity,
                                PurchasePrice = row.Price,
                                ExpiryDate = DateTime.SpecifyKind(row.ExpiryDate, DateTimeKind.Utc),
                                Status = "active"
                            };
                            bd.ProductBatches.Add(batch);

                            var supplyItem = new SupplyItem
                            {
                                Id = Guid.NewGuid(),
                                SupplyId = supply.Id,
                                ProductId = product.Id,
                                BatchId = batch.Id,
                                Quantity = row.Quantity,
                                PurchasePrice = row.Price
                            };
                            bd.SupplyItems.Add(supplyItem);

                            imported++;
                        }

                        await bd.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                }

                MessageBox.Show(Resources.ImportComplete);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex,Resources.ErrorImport);
                MessageBox.Show(Resources.ErrorImport);
            }
            finally
            {
                btnImport.Enabled = true;
                btnImport.Text = Resources.BtnImportText;
            }
        }

        private string GenerateArticle(string productName)
        {
            string prefix = productName.Length >= 3 ? productName.Substring(0, 3) : productName;
            return prefix.ToUpper() + new Random().Next(1000, 9999);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private class ImportRow
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string Supplier { get; set; }
            public string Category { get; set; }
        }
    }
}