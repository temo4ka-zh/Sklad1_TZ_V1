using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Models;
using Sklad1.Properties;

namespace Sklad1.Forms
{
    /// <summary>
    /// Форма ручного ввода поставки
    /// </summary>
    public partial class FormSupply : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly List<SupplyItemTemp> _items = new List<SupplyItemTemp>();
        private ContractorCheckResult? _contractorCheck;

        private TextBox txtSupplierInn = null!;
        private Button btnCheckInn = null!;
        private Label lblContractorStatus = null!;
        private Label lblContractorOrganization = null!;
        private Label lblContractorCheckedAt = null!;

        public FormSupply()
        {
            InitializeComponent();
            InitializeContractorBlock();
            AppCurrencyManager.CurrencyChanged += OnCurrencyChanged;//
            LoadProducts();
            LoadCurrencies();
            LoadUnits();
        }

        private void InitializeContractorBlock()
        {
            ClientSize = new Size(Math.Max(ClientSize.Width, 860), 835);
            btnAdd.Location = new Point(244, 742);
            btnCancel.Location = new Point(444, 742);
            panel1.Height = ClientSize.Height;

            var gbInn = new GroupBox
            {
                Text = "ИНН контрагента",
                Location = new Point(198, 552),
                Size = new Size(520, 175)
            };

            var lblInn = new Label
            {
                Text = "Введите ИНН",
                AutoSize = true,
                Location = new Point(15, 34)
            };
            txtSupplierInn = new TextBox
            {
                BackColor = SystemColors.ActiveCaption,
                Location = new Point(15, 64),
                Size = new Size(230, 31),
                MaxLength = 12
            };
            btnCheckInn = new Button
            {
                Text = "Проверить по API",
                BackColor = SystemColors.ActiveCaption,
                Location = new Point(265, 62),
                Size = new Size(180, 38)
            };
            lblContractorStatus = new Label
            {
                Text = ContractorCheckService.NotPerformedText,
                AutoSize = false,
                Location = new Point(15, 104),
                Size = new Size(480, 25),
                ForeColor = Color.DimGray,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204)
            };
            lblContractorOrganization = new Label
            {
                Text = "Организация: —",
                AutoSize = false,
                Location = new Point(15, 129),
                Size = new Size(480, 22)
            };
            lblContractorCheckedAt = new Label
            {
                Text = "Дата проверки: —",
                AutoSize = false,
                Location = new Point(15, 151),
                Size = new Size(480, 22)
            };

            gbInn.Controls.Add(lblInn);
            gbInn.Controls.Add(txtSupplierInn);
            gbInn.Controls.Add(btnCheckInn);
            gbInn.Controls.Add(lblContractorStatus);
            gbInn.Controls.Add(lblContractorOrganization);
            gbInn.Controls.Add(lblContractorCheckedAt);
            Controls.Add(gbInn);

            txtSupplierInn.TextChanged += (_, _) => ResetContractorCheck();
            btnCheckInn.Click += async (_, _) => await CheckContractorInnAsync();
        }

        private void OnCurrencyChanged()
        {
            LoadProducts();
        }
        private void LoadProducts()
        {
            try
            {
                using (var bd = new Context())
                {
                    var products = bd.Products.ToList();
                    cmbName.DisplayMember = "Name";
                    cmbName.ValueMember = "Id";
                    cmbName.DataSource = products;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorLoadProducts);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }

        private void LoadCurrencies()
        {
            cmbCurrency.Items.Clear();
            cmbCurrency.Items.AddRange(new string[] { "RUB", "USD", "EUR", "CNY" });
            if (cmbCurrency.Items.Count > 0)
                cmbCurrency.SelectedIndex = 0;
        }

        private void LoadUnits()
        {
            cmbUnit.Items.Clear();
            cmbUnit.Items.AddRange(new string[] { "шт", "кг", "л", "уп", "м", "пач", "кор" });
            cmbUnit.SelectedIndex = 0;
        }

        private decimal ConvertToRub(decimal price, string currency)
        {
            try
            {
                using (var bd = new Context())
                {
                    var rate = bd.CurrencyRates.FirstOrDefault(c => c.Code == currency);
                    if (rate != null && rate.RateToRub > 0)
                    {
                        return price * rate.RateToRub;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorCurrencyConversion);
            }

            MessageBox.Show(Resources.ErrorSaveSupply);
            return price;
        }

        private bool ValidateFields()
        {
            if (cmbName.SelectedItem == null)
            {
                MessageBox.Show(Resources.SelectProduct);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show(Resources.EnterQuantity);
                txtQuantity.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show(Resources.EnterPrice);
                txtPrice.Focus();
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            txtQuantity.Clear();
            txtPrice.Clear();
            dtpExpiryDate.Value = DateTime.Now.AddMonths(6);
            cmbName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show(Resources.InvalidQuantity);
                txtQuantity.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show(Resources.InvalidCost);
                txtPrice.Focus();
                return;
            }

            if (dtpExpiryDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show(Resources.InvalidExpiryDate);
                return;
            }

            var selectedProduct = (Product)cmbName.SelectedItem;
            var selectedCurrency = cmbCurrency.SelectedItem?.ToString() ?? "RUB";

            if (selectedCurrency != "RUB")
            {
                price = ConvertToRub(price, selectedCurrency);
            }

            _items.Add(new SupplyItemTemp
            {
                ProductId = selectedProduct.Id,
                ProductName = selectedProduct.Name,
                Quantity = quantity,
                PurchasePrice = price,
                ExpiryDate = dtpExpiryDate.Value,
                Unit = cmbUnit.SelectedItem?.ToString() ?? Resources.DefaultUnit
            });

            MessageBox.Show(Resources.ProductAdded);
            ClearFields();
        }

        private async Task<Guid?> SaveSupply()
        {
            if (_items.Count == 0) return null;

            try
            {
                using (var bd = new Context())
                {
                    using (var transaction = await bd.Database.BeginTransactionAsync())
                    {
                        var supply = new Supply
                        {
                            Id = Guid.NewGuid(),
                            UserId = CurrentUser.Id,
                            Supplier = string.IsNullOrWhiteSpace(_contractorCheck?.OrganizationName)
                                ? Resources.ManualInput
                                : _contractorCheck.OrganizationName,
                            Date = DateTime.UtcNow,
                            Source = "manual"
                        };
                        bd.Supplies.Add(supply);
                        await bd.SaveChangesAsync();

                        foreach (var item in _items)
                        {
                            var product = await bd.Products.FindAsync(item.ProductId);
                            if (product == null) continue;

                            var expiryDateUtc = DateTime.SpecifyKind(item.ExpiryDate, DateTimeKind.Utc);

                            var batch = new ProductBatch
                            {
                                Id = Guid.NewGuid(),
                                ProductId = item.ProductId,
                                SupplyId = supply.Id,
                                Quantity = item.Quantity,
                                PurchasePrice = item.PurchasePrice,
                                ExpiryDate = expiryDateUtc,
                                Status = "active",
                                CellCode = WarehouseMapRules.GetDefaultCellCode(item.ProductId)
                            };
                            bd.ProductBatches.Add(batch);

                            product.Quantity += item.Quantity;

                            var supplyItem = new SupplyItem
                            {
                                Id = Guid.NewGuid(),
                                SupplyId = supply.Id,
                                ProductId = item.ProductId,
                                BatchId = batch.Id,
                                Quantity = item.Quantity,
                                PurchasePrice = item.PurchasePrice
                            };
                            bd.SupplyItems.Add(supplyItem);
                        }

                        await bd.SaveChangesAsync();
                        await transaction.CommitAsync();

                        await ContractorCheckService.LinkToSupplyAsync(_contractorCheck?.CheckId, supply.Id);

                        MessageBox.Show(Resources.SupplySaved);
                        DialogResult = DialogResult.OK;
                        Close();
                        return supply.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorSaveSupply);
                MessageBox.Show(Resources.ErrorSaveSupply);
                return null;
            }
        }

        private class SupplyItemTemp
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public decimal PurchasePrice { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string Unit { get; set; } = string.Empty;
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            if (_items.Count == 0)
            {
                Close();
                return;
            }

            if (!EnsureContractorCheckAllowsSaving(out var blockMessage))
            {
                MessageBox.Show(blockMessage);
                await ContractorCheckService.LogBlockedOperationAsync("Supply", txtSupplierInn.Text.Trim(), blockMessage);
                return;
            }

            var result = MessageBox.Show(string.Format(Resources.ConfirmSaveSupply, _items.Count),
                Resources.Confirmation, MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                await SaveSupply();
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (result == DialogResult.No)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private async Task CheckContractorInnAsync()
        {
            var inn = txtSupplierInn.Text.Trim();
            if (!ContractorCheckService.IsValidInn(inn))
            {
                MessageBox.Show(ContractorCheckService.InvalidInnMessage);
                return;
            }

            btnCheckInn.Enabled = false;
            try
            {
                var result = await ContractorCheckService.CheckAndSaveAsync(inn, "Supply", CurrentUser.Id);
                _contractorCheck = result;
                ShowContractorCheckResult(result);

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Message);
                    if (result.ErrorKind != "network")
                        btnCheckInn.Enabled = true;
                }
                else
                {
                    btnCheckInn.Enabled = true;
                }
            }
            finally
            {
                if (_contractorCheck?.ErrorKind != "network")
                    btnCheckInn.Enabled = true;
            }
        }

        private void ResetContractorCheck()
        {
            _contractorCheck = null;
            btnCheckInn.Enabled = true;
            lblContractorStatus.Text = ContractorCheckService.NotPerformedText;
            lblContractorStatus.ForeColor = Color.DimGray;
            lblContractorOrganization.Text = "Организация: —";
            lblContractorCheckedAt.Text = "Дата проверки: —";
        }

        private void ShowContractorCheckResult(ContractorCheckResult result)
        {
            if (!result.IsSuccess)
            {
                lblContractorStatus.Text = result.Message;
                lblContractorStatus.ForeColor = Color.DarkRed;
                lblContractorOrganization.Text = "Организация: —";
                lblContractorCheckedAt.Text = "Дата проверки: —";
                return;
            }

            lblContractorStatus.Text = result.ResultStatus == ContractorCheckService.StatusReliable
                ? ContractorCheckService.ReliableText
                : ContractorCheckService.BlacklistedText;
            lblContractorStatus.ForeColor = result.ResultStatus == ContractorCheckService.StatusReliable
                ? Color.DarkGreen
                : Color.DarkRed;
            lblContractorOrganization.Text = $"Организация: {result.OrganizationName}";
            lblContractorCheckedAt.Text = $"Дата проверки: {result.CheckedAt.ToLocalTime():dd.MM.yyyy HH:mm}";
        }

        private bool EnsureContractorCheckAllowsSaving(out string message)
        {
            message = string.Empty;
            var currentInn = txtSupplierInn.Text.Trim();

            if (_contractorCheck == null || string.IsNullOrWhiteSpace(currentInn) || _contractorCheck.Inn != currentInn)
            {
                message = ContractorCheckService.NeedCheckMessage;
                return false;
            }

            if (!_contractorCheck.IsSuccess)
            {
                message = ContractorCheckService.CheckErrorMessage;
                return false;
            }

            if (!_contractorCheck.IsReliable || _contractorCheck.ResultStatus == ContractorCheckService.StatusBlacklisted)
            {
                message = ContractorCheckService.BlockedBlackListMessage;
                return false;
            }

            return true;
        }
    }
}
