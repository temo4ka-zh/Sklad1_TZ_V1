namespace Sklad1.Forms
{
    partial class FormCurrencySettings
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
            lblDisplayCurrency = new Label();
            cmbDisplayCurrency = new ComboBox();
            label1 = new Label();
            btnUpdate = new Button();
            btnSave = new Button();
            dgvRates = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvRates).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(181, 587);
            panel1.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(378, 9);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(351, 60);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "КУРСЫ ВАЛЮТ";
            // 
            // lblDisplayCurrency
            // 
            lblDisplayCurrency.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDisplayCurrency.AutoSize = true;
            lblDisplayCurrency.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblDisplayCurrency.Location = new Point(199, 79);
            lblDisplayCurrency.Margin = new Padding(4, 0, 4, 0);
            lblDisplayCurrency.Name = "lblDisplayCurrency";
            lblDisplayCurrency.Size = new Size(212, 28);
            lblDisplayCurrency.TabIndex = 30;
            lblDisplayCurrency.Text = "Валюта отображения:";
            // 
            // cmbDisplayCurrency
            // 
            cmbDisplayCurrency.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbDisplayCurrency.FormattingEnabled = true;
            cmbDisplayCurrency.Location = new Point(199, 110);
            cmbDisplayCurrency.Name = "cmbDisplayCurrency";
            cmbDisplayCurrency.Size = new Size(628, 33);
            cmbDisplayCurrency.TabIndex = 29;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(199, 174);
            label1.Name = "label1";
            label1.Size = new Size(183, 25);
            label1.TabIndex = 31;
            label1.Text = "Текущие курсы (API):";
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Bottom;
            btnUpdate.BackColor = SystemColors.ActiveCaption;
            btnUpdate.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnUpdate.Location = new Point(299, 495);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(204, 53);
            btnUpdate.TabIndex = 33;
            btnUpdate.Text = "Обновить курсы";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom;
            btnSave.BackColor = SystemColors.InactiveCaption;
            btnSave.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSave.Location = new Point(525, 495);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(213, 53);
            btnSave.TabIndex = 34;
            btnSave.Text = "Сохранить настройки";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // dgvRates
            // 
            dgvRates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRates.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvRates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRates.Location = new Point(199, 204);
            dgvRates.Name = "dgvRates";
            dgvRates.RowHeadersWidth = 62;
            dgvRates.Size = new Size(628, 244);
            dgvRates.TabIndex = 35;
            // 
            // FormCurrencySettings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 587);
            Controls.Add(dgvRates);
            Controls.Add(btnSave);
            Controls.Add(btnUpdate);
            Controls.Add(label1);
            Controls.Add(lblDisplayCurrency);
            Controls.Add(cmbDisplayCurrency);
            Controls.Add(lblTitle);
            Controls.Add(panel1);
            Name = "FormCurrencySettings";
            Text = "Настройки валют";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvRates).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private Label lblDisplayCurrency;
        private ComboBox cmbDisplayCurrency;
        private Label label1;
        private Button btnUpdate;
        private Button btnSave;
        private DataGridView dgvRates;
    }
}