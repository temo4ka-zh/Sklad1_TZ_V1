namespace Sklad1.Forms
{
    partial class FormMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainMenu));
            menuStripMain = new MenuStrip();
            tsmiSklad = new ToolStripMenuItem();
            tsmiSupplies = new ToolStripMenuItem();
            tsmiSupply = new ToolStripMenuItem();
            tsmiSupplyImport = new ToolStripMenuItem();
            tsmiExpiry = new ToolStripMenuItem();
            tsmiReports = new ToolStripMenuItem();
            tsmiSettings = new ToolStripMenuItem();
            tsmiLogOut = new ToolStripMenuItem();
            lblTitle = new Label();
            lblSubtitle = new Label();
            pnlWh = new Panel();
            pictureBox1 = new PictureBox();
            lblWh2 = new Label();
            lblWh1 = new Label();
            pnlExp = new Panel();
            pictureBox3 = new PictureBox();
            lblExp2 = new Label();
            lblExp1 = new Label();
            pnlDel = new Panel();
            pictureBox2 = new PictureBox();
            lblDel2 = new Label();
            lblDel1 = new Label();
            pnlRep = new Panel();
            pictureBox4 = new PictureBox();
            lblRep2 = new Label();
            lblRep1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            menuStripMain.SuspendLayout();
            pnlWh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlExp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            pnlDel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            pnlRep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = Color.MidnightBlue;
            menuStripMain.ImageScalingSize = new Size(24, 24);
            menuStripMain.Items.AddRange(new ToolStripItem[] { tsmiSklad, tsmiSupplies, tsmiExpiry, tsmiReports, tsmiSettings, tsmiLogOut });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(1085, 33);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStrip1";
            // 
            // tsmiSklad
            // 
            tsmiSklad.BackColor = Color.MidnightBlue;
            tsmiSklad.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tsmiSklad.ForeColor = SystemColors.ButtonFace;
            tsmiSklad.Name = "tsmiSklad";
            tsmiSklad.Size = new Size(81, 29);
            tsmiSklad.Text = "Склад";
            tsmiSklad.Click += tsmiSklad_Click;
            // 
            // tsmiSupplies
            // 
            tsmiSupplies.DropDownItems.AddRange(new ToolStripItem[] { tsmiSupply, tsmiSupplyImport });
            tsmiSupplies.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tsmiSupplies.ForeColor = SystemColors.ButtonFace;
            tsmiSupplies.Name = "tsmiSupplies";
            tsmiSupplies.Size = new Size(111, 29);
            tsmiSupplies.Text = "Поставки";
            tsmiSupplies.Click += tsmiSupplies_Click;
            // 
            // tsmiSupply
            // 
            tsmiSupply.BackColor = Color.MidnightBlue;
            tsmiSupply.ForeColor = SystemColors.ButtonFace;
            tsmiSupply.Name = "tsmiSupply";
            tsmiSupply.Size = new Size(308, 34);
            tsmiSupply.Text = "Добавление поставки";
            tsmiSupply.Click += tsmiSupply_Click;
            // 
            // tsmiSupplyImport
            // 
            tsmiSupplyImport.BackColor = Color.MidnightBlue;
            tsmiSupplyImport.ForeColor = SystemColors.ButtonFace;
            tsmiSupplyImport.Name = "tsmiSupplyImport";
            tsmiSupplyImport.Size = new Size(308, 34);
            tsmiSupplyImport.Text = "Импорт поставок";
            tsmiSupplyImport.Click += tsmiSupplyImport_Click;
            // 
            // tsmiExpiry
            // 
            tsmiExpiry.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tsmiExpiry.ForeColor = SystemColors.ButtonFace;
            tsmiExpiry.Name = "tsmiExpiry";
            tsmiExpiry.Size = new Size(166, 29);
            tsmiExpiry.Text = "Сроки годности";
            tsmiExpiry.Click += tsmiExpiry_Click;
            // 
            // tsmiReports
            // 
            tsmiReports.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tsmiReports.ForeColor = SystemColors.ButtonFace;
            tsmiReports.Name = "tsmiReports";
            tsmiReports.Size = new Size(94, 29);
            tsmiReports.Text = "Отчеты";
            tsmiReports.Click += tsmiReports_Click;
            // 
            // tsmiSettings
            // 
            tsmiSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tsmiSettings.ForeColor = SystemColors.ButtonFace;
            tsmiSettings.Name = "tsmiSettings";
            tsmiSettings.Size = new Size(123, 29);
            tsmiSettings.Text = "Настройки";
            tsmiSettings.Click += tsmiSettings_Click;
            // 
            // tsmiLogOut
            // 
            tsmiLogOut.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tsmiLogOut.ForeColor = SystemColors.ButtonFace;
            tsmiLogOut.Name = "tsmiLogOut";
            tsmiLogOut.Size = new Size(87, 29);
            tsmiLogOut.Text = "Выход";
            tsmiLogOut.Click += tsmiLogOut_Click;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.ForeColor = Color.MidnightBlue;
            lblTitle.Location = new Point(310, 33);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(473, 48);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Система складского учёта";
            // 
            // lblSubtitle
            // 
            lblSubtitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(347, 81);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(379, 25);
            lblSubtitle.TabIndex = 3;
            lblSubtitle.Text = "Выберите раздел в меню для начала работы";
            // 
            // pnlWh
            // 
            pnlWh.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlWh.AutoSize = true;
            pnlWh.BackColor = SystemColors.GradientInactiveCaption;
            pnlWh.Controls.Add(pictureBox1);
            pnlWh.Controls.Add(lblWh2);
            pnlWh.Controls.Add(lblWh1);
            pnlWh.ForeColor = SystemColors.ControlText;
            pnlWh.ImeMode = ImeMode.NoControl;
            pnlWh.Location = new Point(3, 3);
            pnlWh.Name = "pnlWh";
            pnlWh.Size = new Size(475, 216);
            pnlWh.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(177, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(124, 99);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // lblWh2
            // 
            lblWh2.Anchor = AnchorStyles.Bottom;
            lblWh2.AutoSize = true;
            lblWh2.Location = new Point(145, 183);
            lblWh2.Name = "lblWh2";
            lblWh2.Size = new Size(178, 25);
            lblWh2.TabIndex = 1;
            lblWh2.Text = "Товары и категории";
            // 
            // lblWh1
            // 
            lblWh1.Anchor = AnchorStyles.Bottom;
            lblWh1.AutoSize = true;
            lblWh1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblWh1.Location = new Point(180, 145);
            lblWh1.Name = "lblWh1";
            lblWh1.Size = new Size(97, 38);
            lblWh1.TabIndex = 0;
            lblWh1.Text = "Склад";
            // 
            // pnlExp
            // 
            pnlExp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlExp.BackColor = SystemColors.GradientInactiveCaption;
            pnlExp.Controls.Add(pictureBox3);
            pnlExp.Controls.Add(lblExp2);
            pnlExp.Controls.Add(lblExp1);
            pnlExp.Location = new Point(3, 225);
            pnlExp.Name = "pnlExp";
            pnlExp.Size = new Size(475, 216);
            pnlExp.TabIndex = 5;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Location = new Point(166, 14);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(135, 136);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // lblExp2
            // 
            lblExp2.Anchor = AnchorStyles.Bottom;
            lblExp2.AutoSize = true;
            lblExp2.Location = new Point(157, 180);
            lblExp2.Name = "lblExp2";
            lblExp2.Size = new Size(152, 25);
            lblExp2.TabIndex = 2;
            lblExp2.Text = "Контроль партий";
            // 
            // lblExp1
            // 
            lblExp1.Anchor = AnchorStyles.Bottom;
            lblExp1.AutoSize = true;
            lblExp1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblExp1.Location = new Point(129, 142);
            lblExp1.Name = "lblExp1";
            lblExp1.Size = new Size(228, 38);
            lblExp1.TabIndex = 2;
            lblExp1.Text = "Сроки годности";
            // 
            // pnlDel
            // 
            pnlDel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlDel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlDel.BackColor = SystemColors.GradientInactiveCaption;
            pnlDel.Controls.Add(pictureBox2);
            pnlDel.Controls.Add(lblDel2);
            pnlDel.Controls.Add(lblDel1);
            pnlDel.Location = new Point(484, 3);
            pnlDel.Name = "pnlDel";
            pnlDel.Size = new Size(476, 216);
            pnlDel.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(163, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(147, 146);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // lblDel2
            // 
            lblDel2.Anchor = AnchorStyles.Bottom;
            lblDel2.AutoSize = true;
            lblDel2.Location = new Point(158, 183);
            lblDel2.Name = "lblDel2";
            lblDel2.Size = new Size(149, 25);
            lblDel2.TabIndex = 2;
            lblDel2.Text = "Приход товаров";
            // 
            // lblDel1
            // 
            lblDel1.Anchor = AnchorStyles.Bottom;
            lblDel1.AutoSize = true;
            lblDel1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblDel1.Location = new Point(165, 145);
            lblDel1.Name = "lblDel1";
            lblDel1.Size = new Size(142, 38);
            lblDel1.TabIndex = 2;
            lblDel1.Text = "Поставки";
            // 
            // pnlRep
            // 
            pnlRep.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlRep.BackColor = SystemColors.GradientInactiveCaption;
            pnlRep.Controls.Add(pictureBox4);
            pnlRep.Controls.Add(lblRep2);
            pnlRep.Controls.Add(lblRep1);
            pnlRep.Location = new Point(484, 225);
            pnlRep.Name = "pnlRep";
            pnlRep.Size = new Size(476, 216);
            pnlRep.TabIndex = 5;
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox4.BackgroundImage = (Image)resources.GetObject("pictureBox4.BackgroundImage");
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.Location = new Point(164, 14);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(147, 136);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 4;
            pictureBox4.TabStop = false;
            // 
            // lblRep2
            // 
            lblRep2.Anchor = AnchorStyles.Bottom;
            lblRep2.AutoSize = true;
            lblRep2.Location = new Point(149, 180);
            lblRep2.Name = "lblRep2";
            lblRep2.Size = new Size(181, 25);
            lblRep2.TabIndex = 3;
            lblRep2.Text = "Аналитика и экспорт";
            // 
            // lblRep1
            // 
            lblRep1.Anchor = AnchorStyles.Bottom;
            lblRep1.AutoSize = true;
            lblRep1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblRep1.Location = new Point(178, 142);
            lblRep1.Name = "lblRep1";
            lblRep1.Size = new Size(114, 38);
            lblRep1.TabIndex = 3;
            lblRep1.Text = "Отчеты";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(pnlDel, 1, 0);
            tableLayoutPanel1.Controls.Add(pnlRep, 1, 1);
            tableLayoutPanel1.Controls.Add(pnlExp, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlWh, 0, 0);
            tableLayoutPanel1.Location = new Point(47, 109);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(963, 444);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // FormMainMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 565);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            Name = "FormMainMenu";
            Text = "Складской учет - Главное меню";
            WindowState = FormWindowState.Maximized;
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            pnlWh.ResumeLayout(false);
            pnlWh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlExp.ResumeLayout(false);
            pnlExp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            pnlDel.ResumeLayout(false);
            pnlDel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            pnlRep.ResumeLayout(false);
            pnlRep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStripMain;
        private ToolStripMenuItem tsmiSklad;
        private ToolStripMenuItem tsmiSupplies;
        private ToolStripMenuItem tsmiSupply;
        private ToolStripMenuItem tsmiSupplyImport;
        private ToolStripMenuItem tsmiExpiry;
        private ToolStripMenuItem tsmiReports;
        private ToolStripMenuItem tsmiSettings;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlWh;
        private Panel pnlExp;
        private Panel pnlDel;
        private Panel pnlRep;
        private Label lblWh2;
        private Label lblWh1;
        private Label lblExp1;
        private Label lblExp2;
        private Label lblDel2;
        private Label lblDel1;
        private Label lblRep2;
        private Label lblRep1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private ToolStripMenuItem tsmiLogOut;
        private TableLayoutPanel tableLayoutPanel1;
    }
}