using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Models;
using Sklad1.Properties;

namespace Sklad1.Forms
{
    /// <summary>
    /// Главное меню приложения
    /// </summary>
    public partial class FormMainMenu : Form
    {
        private ToolStripMenuItem? tsmiWarehouseMap;
        private Panel? pnlMap;

        public FormMainMenu()
        {
            InitializeComponent();

            // Новая плитка карты склада не должна мешать входу в уже существующую систему.
            // Если создание дополнительной навигации завершится ошибкой, главное меню всё равно откроется.
            try
            {
                CreateWarehouseMapNavigation();
            }
            catch (Exception ex)
            {
                AppLogger.Error(ex, "Ошибка создания навигации карты склада");
            }

            SetPermissions();
            this.FormClosing += FormMainMenu_FormClosing; 
        }

        private void SetPermissions()
        {
            if (CurrentUser.Role != UserRole.Admin)
            {
                tsmiReports.Visible = false;
                tsmiSettings.Visible = false;
                if (tsmiWarehouseMap != null) tsmiWarehouseMap.Visible = false;
                if (pnlMap != null) pnlMap.Visible = false;
            }
        }

        private void CreateWarehouseMapNavigation()
        {
            tsmiWarehouseMap = new ToolStripMenuItem
            {
                Text = "Карта склада",
                BackColor = Color.MidnightBlue,
                ForeColor = SystemColors.ButtonFace,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204)
            };
            tsmiWarehouseMap.Click += OpenWarehouseMap;
            menuStripMain.Items.Insert(Math.Max(0, menuStripMain.Items.Count - 2), tsmiWarehouseMap);

            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel1.Height = Math.Max(tableLayoutPanel1.Height, 650);

            pnlMap = new Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = SystemColors.GradientInactiveCaption,
                Margin = new Padding(3),
                Cursor = Cursors.Hand
            };
            var lblMap1 = new Label
            {
                Text = "Карта склада",
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Location = new Point(145, 75)
            };
            var lblMap2 = new Label
            {
                Text = "Тепловая карта",
                AutoSize = true,
                Location = new Point(155, 120)
            };
            var lblMapIcon = new Label
            {
                Text = "▦",
                AutoSize = true,
                Font = new Font("Segoe UI", 42F, FontStyle.Bold, GraphicsUnit.Point, 204),
                ForeColor = Color.RoyalBlue,
                Location = new Point(190, 5)
            };
            pnlMap.Controls.Add(lblMapIcon);
            pnlMap.Controls.Add(lblMap1);
            pnlMap.Controls.Add(lblMap2);
            pnlMap.Click += OpenWarehouseMap;
            foreach (Control control in pnlMap.Controls)
                control.Click += OpenWarehouseMap;

            tableLayoutPanel1.Controls.Add(pnlMap, 0, 2);
        }

        private void OpenWarehouseMap(object? sender, EventArgs e)
        {
            if (CurrentUser.Role != UserRole.Admin)
            {
                MessageBox.Show("Нет доступа к карте склада");
                return;
            }

            var form = new FormWarehouseMap();
            form.ShowDialog();
        }

        private void tsmiSklad_Click(object sender, EventArgs e)
        {
            var form = new FormMain();
            form.ShowDialog();
        }

        private void tsmiSupply_Click(object sender, EventArgs e)
        {
            var form = new FormSupply();
            form.ShowDialog();
        }

        private void tsmiSupplyImport_Click(object sender, EventArgs e)
        {
            var form = new FormSupplyImport();
            form.ShowDialog();
        }

        private void tsmiExpiry_Click(object sender, EventArgs e)
        {
            var form = new FormExpiryDates();
            form.ShowDialog();
        }

        private void tsmiReports_Click(object sender, EventArgs e)
        {
            var form = new FormAnalyticReport();
            form.ShowDialog();
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            var form = new FormCurrencySettings();
            form.ShowDialog();
        }

        private void tsmiSupplies_Click(object sender, EventArgs e)
        {
            //чтоб открыть подпункты
        }

        private void tsmiLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.LogOut, Resources.LogOutText, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FormLogin loginForm = new FormLogin();
                loginForm.Show();

                this.Close();
            }
        }
        private void FormMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(Resources.LogOut, Resources.LogOutText, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
            }
            else
            {
                e.Cancel = true;  
            }
        }
    }
}