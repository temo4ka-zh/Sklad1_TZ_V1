using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Models;
using Sklad1.Properties;

namespace Sklad1.Forms
{
    public partial class FormShipment : Form
    {
        private readonly List<ShipmentItemTemp> _items = new List<ShipmentItemTemp>();
        private ContractorCheckResult? _contractorCheck;
        private WeatherCheckResult? _weatherCheck;

        private TextBox txtContractorInn = null!;
        private Button btnCheckInn = null!;
        private Label lblContractorStatus = null!;
        private Label lblContractorOrganization = null!;
        private Label lblContractorCheckedAt = null!;

        private TextBox txtDeliveryCity = null!;
        private DateTimePicker dtpDeliveryDate = null!;
        private Button btnGetWeather = null!;
        private Label lblWeatherResult = null!;

        public FormShipment()
        {
            InitializeComponent();
            InitializeContractorAndWeatherBlocks();
            AppCurrencyManager.CurrencyChanged += OnCurrencyChanged;
            LoadProducts();

            btnAdd.Click += BtnAdd_Click;
            btnShip.Click += BtnShip_Click;
            btnCancel.Click += btnCancel_Click;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
        }

        private void InitializeContractorAndWeatherBlocks()
        {
            panel1.Height = 870;
            btnAdd.Location = new Point(127, 535);
            btnCancel.Location = new Point(47, 735);
            btnShip.Location = new Point(223, 735);

            var lblInnTitle = new Label
            {
                Text = "ИНН контрагента",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Location = new Point(23, 325)
            };
            var lblInn = new Label
            {
                Text = "Введите ИНН",
                AutoSize = true,
                Location = new Point(23, 356)
            };
            txtContractorInn = new TextBox
            {
                BackColor = SystemColors.ActiveCaption,
                Location = new Point(23, 386),
                Size = new Size(220, 31),
                MaxLength = 12
            };
            btnCheckInn = new Button
            {
                Text = "Проверить по API",
                BackColor = SystemColors.ActiveCaption,
                Location = new Point(255, 384),
                Size = new Size(138, 38)
            };
            lblContractorStatus = new Label
            {
                Text = ContractorCheckService.NotPerformedText,
                AutoSize = false,
                Location = new Point(23, 430),
                Size = new Size(370, 28),
                ForeColor = Color.DimGray,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204)
            };
            lblContractorOrganization = new Label
            {
                Text = "Организация: —",
                AutoSize = false,
                Location = new Point(23, 458),
                Size = new Size(370, 25)
            };
            lblContractorCheckedAt = new Label
            {
                Text = "Дата проверки: —",
                AutoSize = false,
                Location = new Point(23, 483),
                Size = new Size(370, 25)
            };

            var lblWeatherTitle = new Label
            {
                Text = "Геолокация и погода",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Location = new Point(23, 600)
            };
            var lblCity = new Label
            {
                Text = "Город доставки",
                AutoSize = true,
                Location = new Point(23, 630)
            };
            txtDeliveryCity = new TextBox
            {
                BackColor = SystemColors.ActiveCaption,
                Location = new Point(23, 658),
                Size = new Size(180, 31)
            };
            var lblDate = new Label
            {
                Text = "Дата доставки",
                AutoSize = true,
                Location = new Point(215, 630)
            };
            dtpDeliveryDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd.MM.yyyy",
                Location = new Point(215, 658),
                Size = new Size(178, 31),
                MinDate = DateTime.Today
            };
            btnGetWeather = new Button
            {
                Text = "Получить прогноз",
                BackColor = SystemColors.ActiveCaption,
                Location = new Point(23, 696),
                Size = new Size(180, 34)
            };
            lblWeatherResult = new Label
            {
                Text = "Прогноз не получен",
                AutoSize = false,
                Location = new Point(215, 695),
                Size = new Size(178, 62),
                ForeColor = Color.DimGray
            };

            panel1.Controls.Add(lblInnTitle);
            panel1.Controls.Add(lblInn);
            panel1.Controls.Add(txtContractorInn);
            panel1.Controls.Add(btnCheckInn);
            panel1.Controls.Add(lblContractorStatus);
            panel1.Controls.Add(lblContractorOrganization);
            panel1.Controls.Add(lblContractorCheckedAt);
            panel1.Controls.Add(lblWeatherTitle);
            panel1.Controls.Add(lblCity);
            panel1.Controls.Add(txtDeliveryCity);
            panel1.Controls.Add(lblDate);
            panel1.Controls.Add(dtpDeliveryDate);
            panel1.Controls.Add(btnGetWeather);
            panel1.Controls.Add(lblWeatherResult);

            txtContractorInn.TextChanged += (_, _) => ResetContractorCheck();
            btnCheckInn.Click += async (_, _) => await CheckContractorInnAsync();
            txtDeliveryCity.TextChanged += (_, _) => ResetWeatherCheck();
            dtpDeliveryDate.ValueChanged += (_, _) => ResetWeatherCheck();
            btnGetWeather.Click += async (_, _) => await GetWeatherAsync();
        }

        private void OnCurrencyChanged()
        {
            LoadProducts();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadProducts()
        {
            using (var bd = new Context())
            {
                var products = bd.Products.Where(p => p.Quantity > 0).Select(p => new ProductItem
                {
                    Article = p.Article,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    PurchasePrice = p.PurchasePrice
                })
                    .ToList();

                cmbProduct.DisplayMember = nameof(ProductItem.Name);
                cmbProduct.ValueMember = nameof(ProductItem.Article);
                cmbProduct.DataSource = products;
            }
        }

        private void UpdateQuantityDropdown()
        {
            cmbQuantity.Items.Clear();
            if (cmbProduct.SelectedItem == null) return;

            var selectedProduct = (ProductItem)cmbProduct.SelectedItem;

            using (var bd = new Context())
            {
                var product = bd.Products.FirstOrDefault(p => p.Article == selectedProduct.Article);
                if (product != null)
                {
                    var availableQuantity = bd.ProductBatches
                        .Where(b => b.ProductId == product.Id && b.Status == "active" && b.Quantity > 0 && b.ExpiryDate >= DateTime.UtcNow.Date)
                        .Sum(b => b.Quantity);

                    selectedProduct.Quantity = availableQuantity;
                }
            }

            int maxQuantity = selectedProduct.Quantity;
            if (maxQuantity == 0)
            {
                MessageBox.Show(Resources.InvalidShipment);
                btnShip.Enabled = false;
                return;
            }
            else
            {
                btnShip.Enabled = true;
            }
            int maxItemsToShow = Math.Min(maxQuantity, 20);
            for (int i = 1; i <= maxItemsToShow; i++)
            {
                cmbQuantity.Items.Add(i);
            }

            if (maxQuantity > 20)
            {
                cmbQuantity.Items.Add($"Другое до {maxQuantity}");
            }
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateQuantityDropdown();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateClient())
                return;

            if (!ValidateSelection())
                return;

            if (!int.TryParse(cmbQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show(Resources.InvalidQuantity);
                return;
            }

            var selected = (ProductItem)cmbProduct.SelectedItem;

            if (quantity > selected.Quantity)
            {
                MessageBox.Show(Resources.InsufficientStock);
                return;
            }

            AddOrUpdateItem(selected, quantity);
            UpdateGrid();
            cmbQuantity.Text = "";
        }

        private bool ValidateSelection()
        {
            if (cmbProduct.SelectedItem == null)
            {
                MessageBox.Show(Resources.SelectProduct);
                return false;
            }
            return true;
        }

        private void AddOrUpdateItem(ProductItem selected, int quantity)
        {
            var currentClient = txtClient.Text.Trim();

            var existing = _items.FirstOrDefault(i => i.Article == selected.Article && i.Client == currentClient);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _items.Add(new ShipmentItemTemp
                {
                    Article = selected.Article,
                    Name = selected.Name,
                    Quantity = quantity,
                    Price = selected.PurchasePrice,
                    Client = currentClient
                });
            }
        }

        private void UpdateGrid()
        {
            dgvItems.DataSource = null;
            dgvItems.DataSource = _items;

            dgvItems.Columns[nameof(ShipmentItemTemp.Article)].HeaderText = Resources.Article;
            dgvItems.Columns[nameof(ShipmentItemTemp.Name)].HeaderText = Resources.Name;
            dgvItems.Columns[nameof(ShipmentItemTemp.Quantity)].HeaderText = Resources.Quantity;
            dgvItems.Columns[nameof(ShipmentItemTemp.Price)].HeaderText = Resources.Price;
            dgvItems.Columns[nameof(ShipmentItemTemp.Client)].HeaderText = Resources.Client;

            btnShip.Enabled = _items.Count > 0;
        }

        private async void BtnShip_Click(object sender, EventArgs e)
        {
            if (!ValidateItems())
                return;

            if (!EnsureContractorCheckAllowsSaving("Shipment", out var blockMessage))
            {
                MessageBox.Show(blockMessage);
                await ContractorCheckService.LogBlockedOperationAsync("Shipment", txtContractorInn.Text.Trim(), blockMessage);
                return;
            }

            foreach (var group in _items.GroupBy(i => i.Client))
            {
                var itemsForShipment = group.Select(i => (i.Article, i.Quantity)).ToList();

                var result = ShipmentService.ProcessShipmentWithResult(group.Key, itemsForShipment);
                if (!result.Success || result.ShipmentId == null)
                {
                    MessageBox.Show(Resources.ShipmentError);
                    return;
                }

                await ContractorCheckService.LinkToShipmentAsync(_contractorCheck?.CheckId, result.ShipmentId.Value);
                await WeatherService.LinkToShipmentAsync(_weatherCheck?.CheckId, result.ShipmentId.Value);
            }

            MessageBox.Show(Resources.ShipmentSuccess);
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateClient()
        {
            if (string.IsNullOrWhiteSpace(txtClient.Text))
            {
                MessageBox.Show(Resources.ShipmentNoClient);
                txtClient.Focus();
                return false;
            }

            var client = txtClient.Text.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(client, @"^[а-яА-ЯёЁa-zA-Z0-9\s\-\.]+$"))
            {
                MessageBox.Show(Resources.InvalidClientName);
                txtClient.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateItems()
        {
            if (_items.Count == 0)
            {
                MessageBox.Show(Resources.ShipmentNoItems);
                return false;
            }
            return true;
        }

        private async Task CheckContractorInnAsync()
        {
            var inn = txtContractorInn.Text.Trim();
            if (!ContractorCheckService.IsValidInn(inn))
            {
                MessageBox.Show(ContractorCheckService.InvalidInnMessage);
                return;
            }

            btnCheckInn.Enabled = false;
            try
            {
                var result = await ContractorCheckService.CheckAndSaveAsync(inn, "Shipment", CurrentUser.Id);
                _contractorCheck = result;
                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Message);
                    if (result.ErrorKind == "network")
                    {
                        btnCheckInn.Enabled = false;
                    }
                    else
                    {
                        btnCheckInn.Enabled = true;
                    }
                }
                else
                {
                    btnCheckInn.Enabled = true;
                }

                ShowContractorCheckResult(result);
            }
            finally
            {
                if (_contractorCheck?.ErrorKind != "network")
                    btnCheckInn.Enabled = true;
            }
        }

        private async Task GetWeatherAsync()
        {
            var city = txtDeliveryCity.Text.Trim();
            if (city.Length < 2)
            {
                MessageBox.Show(WeatherService.EnterCityMessage);
                return;
            }

            if (dtpDeliveryDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show(WeatherService.DateInPastMessage);
                return;
            }

            btnGetWeather.Enabled = false;
            try
            {
                var result = await WeatherService.GetForecastAndSaveAsync(city, dtpDeliveryDate.Value.Date, CurrentUser.Id);
                _weatherCheck = result;
                ShowWeatherResult(result);
                if (!result.IsSuccess)
                    MessageBox.Show(result.ErrorMessage ?? WeatherService.ForecastUnavailableMessage);
            }
            catch (InvalidOperationException ex)
            {
                _weatherCheck = WeatherCheckResult.Error(ex.Message);
                ShowWeatherResult(_weatherCheck);
                MessageBox.Show(ex.Message);
            }
            catch
            {
                _weatherCheck = WeatherCheckResult.Error(WeatherService.ForecastUnavailableMessage);
                ShowWeatherResult(_weatherCheck);
                MessageBox.Show(WeatherService.ForecastUnavailableMessage);
            }
            finally
            {
                btnGetWeather.Enabled = true;
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

        private void ResetWeatherCheck()
        {
            _weatherCheck = null;
            lblWeatherResult.Text = "Прогноз не получен";
            lblWeatherResult.ForeColor = Color.DimGray;
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

        private void ShowWeatherResult(WeatherCheckResult result)
        {
            if (!result.IsSuccess)
            {
                lblWeatherResult.Text = result.ErrorMessage ?? "Прогноз не получен";
                lblWeatherResult.ForeColor = Color.DarkRed;
                return;
            }

            lblWeatherResult.Text = $"{result.RiskLevel}. {result.Recommendation}";
            lblWeatherResult.ForeColor = result.RiskLevel == WeatherService.NormalRisk ? Color.DarkGreen : Color.DarkOrange;
            if (result.RiskLevel == WeatherService.CriticalRisk)
                lblWeatherResult.ForeColor = Color.DarkRed;
        }

        private bool EnsureContractorCheckAllowsSaving(string documentType, out string message)
        {
            message = string.Empty;
            var currentInn = txtContractorInn.Text.Trim();

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
