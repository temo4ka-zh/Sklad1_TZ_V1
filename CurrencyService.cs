using Microsoft.EntityFrameworkCore;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Models;

namespace Sklad1.Forms
{
    /// <summary>
    /// Тепловая карта склада: цвет ячейки зависит от срока годности и количества товара.
    /// </summary>
    public class FormWarehouseMap : Form
    {
        private readonly Dictionary<string, Button> _cellButtons = new Dictionary<string, Button>();
        private readonly Dictionary<string, List<BatchInfo>> _cellData = new Dictionary<string, List<BatchInfo>>();
        private readonly HashSet<string> _highlightedCells = new HashSet<string>();

        private FlowLayoutPanel pnlGrid = null!;
        private TextBox txtSearch = null!;
        private ListBox lstSearchResults = null!;
        private Label lblSearchMessage = null!;
        private TextBox txtInfo = null!;
        private TextBox txtSummary = null!;
        private FlowLayoutPanel pnlLegend = null!;
        private Button btnRefresh = null!;

        public FormWarehouseMap()
        {
            if (CurrentUser.Role != UserRole.Admin)
            {
                MessageBox.Show("Нет доступа к карте склада");
                BeginInvoke(new Action(Close));
                return;
            }

            InitializeComponentRuntime();
            LoadMapData();
        }

        private void InitializeComponentRuntime()
        {
            Text = "Карта склада";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            BackColor = SystemColors.ActiveCaption;
            MinimumSize = new Size(1100, 760);

            var top = new Panel
            {
                Dock = DockStyle.Top,
                Height = 58,
                BackColor = Color.MidnightBlue
            };
            var title = new Label
            {
                Text = "Карта склада",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204),
                AutoSize = true,
                Location = new Point(20, 10)
            };
            btnRefresh = new Button
            {
                Text = "Обновить",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = SystemColors.ActiveCaption,
                Location = new Point(930, 11),
                Size = new Size(135, 38)
            };
            btnRefresh.Click += (_, _) => LoadMapData();
            top.Controls.Add(title);
            top.Controls.Add(btnRefresh);
            Controls.Add(top);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2,
                Padding = new Padding(14),
                BackColor = SystemColors.ActiveCaption
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
            Controls.Add(root);

            var left = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lblInfo = new Label
            {
                Text = "Информация о товаре",
                Dock = DockStyle.Top,
                Height = 32,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204)
            };
            txtInfo = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text = "Выберите ячейку на карте"
            };
            txtSummary = new TextBox
            {
                Dock = DockStyle.Bottom,
                Height = 210,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            var lblSummary = new Label
            {
                Text = "Сводка по складу",
                Dock = DockStyle.Bottom,
                Height = 32,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204)
            };
            left.Controls.Add(txtInfo);
            left.Controls.Add(lblInfo);
            left.Controls.Add(txtSummary);
            left.Controls.Add(lblSummary);
            root.Controls.Add(left, 0, 0);

            var center = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(16) };
            pnlGrid = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown
            };
            center.Controls.Add(pnlGrid);
            root.Controls.Add(center, 1, 0);

            var right = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lblSearch = new Label
            {
                Text = "Поиск товара",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204)
            };
            txtSearch = new TextBox { Dock = DockStyle.Top, Height = 31 };
            lblSearchMessage = new Label
            {
                Text = string.Empty,
                Dock = DockStyle.Top,
                Height = 62,
                ForeColor = Color.DarkRed
            };
            lstSearchResults = new ListBox
            {
                Dock = DockStyle.Fill,
                DisplayMember = nameof(SearchProductItem.Name)
            };
            txtSearch.TextChanged += (_, _) => SearchProducts();
            lstSearchResults.SelectedIndexChanged += (_, _) => HighlightSelectedProduct();
            right.Controls.Add(lstSearchResults);
            right.Controls.Add(lblSearchMessage);
            right.Controls.Add(txtSearch);
            right.Controls.Add(lblSearch);
            root.Controls.Add(right, 2, 0);

            pnlLegend = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(8),
                WrapContents = true
            };
            root.SetColumnSpan(pnlLegend, 3);
            root.Controls.Add(pnlLegend, 0, 1);

            BuildEmptyGrid();
            BuildLegend();
        }

        private void BuildEmptyGrid()
        {
            pnlGrid.Controls.Clear();
            _cellButtons.Clear();

            var headerRow = new FlowLayoutPanel
            {
                Width = 660,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            headerRow.Controls.Add(new Label { Text = string.Empty, Width = 48, Height = 32, TextAlign = ContentAlignment.MiddleCenter });
            foreach (var col in WarehouseMapRules.Columns)
            {
                headerRow.Controls.Add(new Label { Text = col.ToString(), Width = 88, Height = 32, TextAlign = ContentAlignment.MiddleCenter });
            }
            pnlGrid.Controls.Add(headerRow);

            foreach (var row in WarehouseMapRules.Rows)
            {
                var rowPanel = new FlowLayoutPanel
                {
                    Width = 660,
                    Height = 68,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false
                };
                rowPanel.Controls.Add(new Label { Text = row, Width = 48, Height = 58, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10F, FontStyle.Bold) });

                foreach (var col in WarehouseMapRules.Columns)
                {
                    var code = $"{row}{col}";
                    var button = new Button
                    {
                        Text = code,
                        Tag = code,
                        Width = 88,
                        Height = 58,
                        Margin = new Padding(2),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.White
                    };
                    button.FlatAppearance.BorderColor = Color.Gray;
                    button.FlatAppearance.BorderSize = 1;
                    button.Click += (_, _) => ShowCellInfo(code);
                    _cellButtons[code] = button;
                    rowPanel.Controls.Add(button);
                }
                pnlGrid.Controls.Add(rowPanel);
            }
        }

        private void LoadMapData()
        {
            try
            {
                _cellData.Clear();
                DatabaseSchemaInitializer.Initialize();
                using var db = new Context();
                var batches = db.ProductBatches
                    .Include(b => b.Product)
                    .Where(b => b.Status == "active" && b.Quantity > 0)
                    .ToList();

                var changed = false;
                foreach (var batch in batches)
                {
                    var normalized = WarehouseMapRules.NormalizeCellCode(batch.CellCode, batch.ProductId);
                    if (batch.CellCode != normalized)
                    {
                        batch.CellCode = normalized;
                        changed = true;
                    }
                }
                if (changed)
                    db.SaveChanges();

                foreach (var batch in batches)
                {
                    var code = WarehouseMapRules.NormalizeCellCode(batch.CellCode, batch.ProductId);
                    if (!_cellData.ContainsKey(code))
                        _cellData[code] = new List<BatchInfo>();

                    _cellData[code].Add(new BatchInfo
                    {
                        BatchId = batch.Id,
                        ProductId = batch.ProductId,
                        ProductName = batch.Product?.Name ?? "Без названия",
                        Quantity = batch.Quantity,
                        Unit = batch.Product?.Unit ?? "шт",
                        ExpiryDate = batch.ExpiryDate.Date,
                        CellCode = code
                    });
                }

                ApplyCellVisuals();
                UpdateSummary();
            }
            catch
            {
                MessageBox.Show("Ошибка базы данных. Повторите попытку");
            }
        }

        private void ApplyCellVisuals()
        {
            foreach (var pair in _cellButtons)
            {
                var code = pair.Key;
                var button = pair.Value;
                var batches = _cellData.TryGetValue(code, out var infos)
                    ? infos.Select(i => new ProductBatch { Quantity = i.Quantity, ExpiryDate = i.ExpiryDate, Status = "active" })
                    : Enumerable.Empty<ProductBatch>();
                var state = WarehouseMapRules.CalculateCellState(batches, DateTime.Today);

                button.BackColor = state.BackColor;
                button.FlatAppearance.BorderColor = _highlightedCells.Contains(code)
                    ? Color.Black
                    : state.HasBlueBorder ? Color.RoyalBlue : Color.Gray;
                button.FlatAppearance.BorderSize = _highlightedCells.Contains(code)
                    ? 4
                    : state.HasBlueBorder ? 3 : 1;
            }
        }

        private void SearchProducts()
        {
            var query = txtSearch.Text.Trim();
            lblSearchMessage.Text = string.Empty;
            lstSearchResults.DataSource = null;
            _highlightedCells.Clear();
            ApplyCellVisuals();

            if (query.Length == 0)
            {
                txtInfo.Text = "Выберите ячейку на карте";
                return;
            }

            if (query.Length == 1)
            {
                lblSearchMessage.Text = "Введите минимум 2 символа для поиска";
                return;
            }

            try
            {
                using var db = new Context();
                var lower = query.ToLowerInvariant();
                var catalogProducts = db.Products
                    .Where(p => p.Name.ToLower().Contains(lower))
                    .Select(p => new SearchProductItem { ProductId = p.Id, Name = p.Name })
                    .ToList();

                var stockProductIds = _cellData.Values
                    .SelectMany(v => v)
                    .Where(b => b.ProductName.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                    .Select(b => b.ProductId)
                    .Distinct()
                    .ToHashSet();

                var result = catalogProducts
                    .Where(p => stockProductIds.Contains(p.ProductId))
                    .OrderBy(p => p.Name)
                    .ToList();

                if (result.Count == 0)
                {
                    lblSearchMessage.Text = catalogProducts.Count > 0
                        ? "Товар найден в справочнике, но отсутствует на складе"
                        : "Товар не найден на складе";
                    return;
                }

                lstSearchResults.DataSource = result;
            }
            catch
            {
                lblSearchMessage.Text = "Ошибка базы данных. Повторите попытку";
            }
        }

        private void HighlightSelectedProduct()
        {
            if (lstSearchResults.SelectedItem is not SearchProductItem selected)
                return;

            _highlightedCells.Clear();
            var cells = _cellData
                .Where(pair => pair.Value.Any(b => b.ProductId == selected.ProductId))
                .Select(pair => pair.Key)
                .ToList();

            foreach (var cell in cells)
                _highlightedCells.Add(cell);

            ApplyCellVisuals();

            if (cells.Count > 0)
                ShowCellInfo(cells[0]);
        }

        private void ShowCellInfo(string cellCode)
        {
            if (!_cellData.TryGetValue(cellCode, out var batches) || batches.Count == 0)
            {
                txtInfo.Text = $"Ячейка: {cellCode}. Состояние: Пусто";
                return;
            }

            var lines = new List<string> { $"Ячейка: {cellCode}", string.Empty };
            foreach (var batch in batches.OrderBy(b => b.ExpiryDate))
            {
                var daysLeft = (batch.ExpiryDate.Date - DateTime.Today).Days;
                lines.Add($"Товар: {batch.ProductName}");
                lines.Add($"Количество: {batch.Quantity} {batch.Unit}");
                lines.Add($"Срок годности: {batch.ExpiryDate:dd.MM.yyyy}");
                lines.Add($"Осталось дней: {daysLeft}");
                lines.Add(string.Empty);
            }
            txtInfo.Text = string.Join(Environment.NewLine, lines);
        }

        private void UpdateSummary()
        {
            var all = _cellData.Values.SelectMany(x => x).ToList();
            if (all.Count == 0)
            {
                txtSummary.Text = "Всего товаров: 0";
                return;
            }

            var today = DateTime.Today;
            var total = all.Sum(b => b.Quantity);
            var normal = all.Where(b => (b.ExpiryDate - today).Days > 30).Sum(b => b.Quantity);
            var avgDays = all.Average(b => (b.ExpiryDate - today).Days);
            var expiring = all.Where(b => (b.ExpiryDate - today).Days <= 30).Sum(b => b.Quantity);
            var lowQuantity = all.Where(b => b.Quantity < WarehouseMapRules.LowQuantityThreshold).Sum(b => b.Quantity);
            var critical = all.Where(b => (b.ExpiryDate - today).Days < 15 && b.Quantity < WarehouseMapRules.LowQuantityThreshold).Sum(b => b.Quantity);

            txtSummary.Text = string.Join(Environment.NewLine, new[]
            {
                $"Всего товаров: {total}",
                $"Норма (>30 дн.): {normal}",
                $"Средний срок: {avgDays:0} дн.",
                $"Истекает (<=30 дн.): {expiring}",
                $"Меньше 10 шт.: {lowQuantity}",
                $"Критические товары: {critical}"
            });
        }

        private void BuildLegend()
        {
            pnlLegend.Controls.Clear();
            AddLegendItem(Color.LightGreen, "Зелёный: срок больше 30 дней");
            AddLegendItem(Color.Khaki, "Жёлтый: срок 15–30 дней включительно");
            AddLegendItem(Color.Orange, "Оранжевый: срок меньше 15 дней, количество от 10");
            AddLegendItem(Color.IndianRed, "Красный: срок меньше 15 дней и количество меньше 10");
            AddLegendItem(Color.White, "Синяя рамка: количество меньше 10 и срок от 15 дней", Color.RoyalBlue, 3);
            AddLegendItem(Color.White, "Толстая чёрная рамка: найденный товар", Color.Black, 4);
        }

        private void AddLegendItem(Color backColor, string text, Color? borderColor = null, int borderSize = 1)
        {
            var panel = new Panel { Width = 34, Height = 24, BackColor = backColor, Margin = new Padding(8, 8, 3, 3) };
            panel.Paint += (_, e) =>
            {
                using var pen = new Pen(borderColor ?? Color.Gray, borderSize);
                e.Graphics.DrawRectangle(pen, 1, 1, panel.Width - 3, panel.Height - 3);
            };
            var label = new Label { Text = text, AutoSize = true, Height = 32, Margin = new Padding(3, 8, 20, 3) };
            pnlLegend.Controls.Add(panel);
            pnlLegend.Controls.Add(label);
        }

        private sealed class BatchInfo
        {
            public Guid BatchId { get; set; }
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public string Unit { get; set; } = string.Empty;
            public DateTime ExpiryDate { get; set; }
            public string CellCode { get; set; } = string.Empty;
        }

        private sealed class SearchProductItem
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; } = string.Empty;
            public override string ToString() => Name;
        }
    }
}
