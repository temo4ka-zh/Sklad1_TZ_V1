namespace Sklad1.Forms
{
    partial class FormAnalyticReport
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
            panel1 = new Panel();
            lblTitle = new Label();
            label1 = new Label();
            label2 = new Label();
            btnGenerate = new Button();
            dtpDateFrom = new DateTimePicker();
            dtpDateTo = new DateTimePicker();
            pnlRevenue = new Panel();
            lblRevenue = new Label();
            label3 = new Label();
            pnlCost = new Panel();
            lblCost = new Label();
            label4 = new Label();
            pnlLoss = new Panel();
            lblLosses = new Label();
            label5 = new Label();
            pnlProfit = new Panel();
            lblProfit = new Label();
            label6 = new Label();
            dgvAnalyticReport = new DataGridView();
            btnExport = new Button();
            btnPrint = new Button();
            panel1.SuspendLayout();
            pnlRevenue.SuspendLayout();
            pnlCost.SuspendLayout();
            pnlLoss.SuspendLayout();
            pnlProfit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnalyticReport).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(lblTitle);
            panel1.Location = new Point(57, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(806, 77);
            panel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(225, 19);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(368, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "АНАЛИТИЧЕСКИЙ ОТЧЕТ";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(45, 89);
            label1.Name = "label1";
            label1.Size = new Size(95, 25);
            label1.TabIndex = 2;
            label1.Text = "Период с:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(270, 89);
            label2.Name = "label2";
            label2.Size = new Size(40, 25);
            label2.TabIndex = 3;
            label2.Text = "По:";
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = AnchorStyles.Top;
            btnGenerate.BackColor = SystemColors.ActiveCaption;
            btnGenerate.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnGenerate.Location = new Point(505, 105);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(192, 51);
            btnGenerate.TabIndex = 34;
            btnGenerate.Text = "Сформировать";
            btnGenerate.UseVisualStyleBackColor = false;
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Anchor = AnchorStyles.Top;
            dtpDateFrom.Location = new Point(49, 117);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(216, 31);
            dtpDateFrom.TabIndex = 35;
            // 
            // dtpDateTo
            // 
            dtpDateTo.Anchor = AnchorStyles.Top;
            dtpDateTo.Location = new Point(274, 117);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(216, 31);
            dtpDateTo.TabIndex = 36;
            // 
            // pnlRevenue
            // 
            pnlRevenue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlRevenue.BackColor = SystemColors.GradientInactiveCaption;
            pnlRevenue.Controls.Add(lblRevenue);
            pnlRevenue.Controls.Add(label3);
            pnlRevenue.Location = new Point(12, 162);
            pnlRevenue.Name = "pnlRevenue";
            pnlRevenue.Size = new Size(218, 76);
            pnlRevenue.TabIndex = 37;
            // 
            // lblRevenue
            // 
            lblRevenue.AutoSize = true;
            lblRevenue.Location = new Point(76, 44);
            lblRevenue.Name = "lblRevenue";
            lblRevenue.Size = new Size(51, 25);
            lblRevenue.TabIndex = 1;
            lblRevenue.Text = "0.00 ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(44, 14);
            label3.Name = "label3";
            label3.Size = new Size(125, 21);
            label3.TabIndex = 0;
            label3.Text = "Общая выручка";
            // 
            // pnlCost
            // 
            pnlCost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCost.BackColor = SystemColors.GradientInactiveCaption;
            pnlCost.Controls.Add(lblCost);
            pnlCost.Controls.Add(label4);
            pnlCost.Location = new Point(244, 162);
            pnlCost.Name = "pnlCost";
            pnlCost.Size = new Size(218, 76);
            pnlCost.TabIndex = 38;
            // 
            // lblCost
            // 
            lblCost.AutoSize = true;
            lblCost.Location = new Point(74, 44);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(51, 25);
            lblCost.TabIndex = 2;
            lblCost.Text = "0.00 ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(41, 14);
            label4.Name = "label4";
            label4.Size = new Size(119, 21);
            label4.TabIndex = 1;
            label4.Text = "Себестоимость";
            // 
            // pnlLoss
            // 
            pnlLoss.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlLoss.BackColor = SystemColors.GradientInactiveCaption;
            pnlLoss.Controls.Add(lblLosses);
            pnlLoss.Controls.Add(label5);
            pnlLoss.Location = new Point(474, 162);
            pnlLoss.Name = "pnlLoss";
            pnlLoss.Size = new Size(218, 76);
            pnlLoss.TabIndex = 38;
            // 
            // lblLosses
            // 
            lblLosses.AutoSize = true;
            lblLosses.Location = new Point(75, 44);
            lblLosses.Name = "lblLosses";
            lblLosses.Size = new Size(51, 25);
            lblLosses.TabIndex = 3;
            lblLosses.Text = "0.00 ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(38, 14);
            label5.Name = "label5";
            label5.Size = new Size(143, 21);
            label5.TabIndex = 2;
            label5.Text = "Убытки (списание)";
            // 
            // pnlProfit
            // 
            pnlProfit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlProfit.BackColor = SystemColors.GradientInactiveCaption;
            pnlProfit.Controls.Add(lblProfit);
            pnlProfit.Controls.Add(label6);
            pnlProfit.Location = new Point(716, 162);
            pnlProfit.Name = "pnlProfit";
            pnlProfit.Size = new Size(204, 76);
            pnlProfit.TabIndex = 38;
            // 
            // lblProfit
            // 
            lblProfit.AutoSize = true;
            lblProfit.Location = new Point(79, 44);
            lblProfit.Name = "lblProfit";
            lblProfit.Size = new Size(51, 25);
            lblProfit.TabIndex = 4;
            lblProfit.Text = "0.00 ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(69, 14);
            label6.Name = "label6";
            label6.Size = new Size(75, 21);
            label6.TabIndex = 3;
            label6.Text = "Прибыль";
            // 
            // dgvAnalyticReport
            // 
            dgvAnalyticReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAnalyticReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnalyticReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnalyticReport.Location = new Point(44, 243);
            dgvAnalyticReport.Name = "dgvAnalyticReport";
            dgvAnalyticReport.RowHeadersWidth = 62;
            dgvAnalyticReport.Size = new Size(841, 161);
            dgvAnalyticReport.TabIndex = 39;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Bottom;
            btnExport.BackColor = SystemColors.ActiveCaption;
            btnExport.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnExport.Location = new Point(282, 411);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(161, 53);
            btnExport.TabIndex = 40;
            btnExport.Text = "Экспорт в файл";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Bottom;
            btnPrint.BackColor = SystemColors.ActiveCaption;
            btnPrint.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnPrint.Location = new Point(494, 410);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(161, 53);
            btnPrint.TabIndex = 41;
            btnPrint.Text = "Печать";
            btnPrint.UseVisualStyleBackColor = false;
            // 
            // FormAnalyticReport
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(939, 476);
            Controls.Add(btnPrint);
            Controls.Add(btnExport);
            Controls.Add(dgvAnalyticReport);
            Controls.Add(pnlProfit);
            Controls.Add(pnlLoss);
            Controls.Add(pnlCost);
            Controls.Add(pnlRevenue);
            Controls.Add(dtpDateTo);
            Controls.Add(dtpDateFrom);
            Controls.Add(btnGenerate);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "FormAnalyticReport";
            Text = "Аналитический отчет";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlRevenue.ResumeLayout(false);
            pnlRevenue.PerformLayout();
            pnlCost.ResumeLayout(false);
            pnlCost.PerformLayout();
            pnlLoss.ResumeLayout(false);
            pnlLoss.PerformLayout();
            pnlProfit.ResumeLayout(false);
            pnlProfit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnalyticReport).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private Label label1;
        private Label label2;
        private Button btnGenerate;
        private DateTimePicker dtpDateFrom;
        private DateTimePicker dtpDateTo;
        private Panel pnlRevenue;
        private Panel pnlCost;
        private Panel pnlLoss;
        private Panel pnlProfit;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblRevenue;
        private Label lblCost;
        private Label lblLosses;
        private Label lblProfit;
        private DataGridView dgvAnalyticReport;
        private Button btnExport;
        private Button btnPrint;
    }
}