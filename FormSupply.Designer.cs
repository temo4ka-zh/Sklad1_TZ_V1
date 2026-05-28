namespace Sklad1.Forms
{
    partial class FormShipmentHistory
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
            lblHistory = new Label();
            dgvHistory = new DataGridView();
            panel1 = new Panel();
            txtSearchValue = new TextBox();
            cmbSearchField = new ComboBox();
            btnSearch = new Button();
            btnClear = new Button();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // lblHistory
            // 
            lblHistory.Anchor = AnchorStyles.Top;
            lblHistory.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblHistory.Location = new Point(520, 15);
            lblHistory.Margin = new Padding(4, 0, 4, 0);
            lblHistory.Name = "lblHistory";
            lblHistory.Size = new Size(261, 42);
            lblHistory.TabIndex = 3;
            lblHistory.Text = "История отгрузок";
            lblHistory.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvHistory
            // 
            dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Location = new Point(0, 168);
            dgvHistory.Margin = new Padding(4, 5, 4, 5);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 62;
            dgvHistory.Size = new Size(1286, 695);
            dgvHistory.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 895);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1286, 105);
            panel1.TabIndex = 8;
            // 
            // txtSearchValue
            // 
            txtSearchValue.Location = new Point(28, 129);
            txtSearchValue.Name = "txtSearchValue";
            txtSearchValue.Size = new Size(150, 31);
            txtSearchValue.TabIndex = 9;
            // 
            // cmbSearchField
            // 
            cmbSearchField.FormattingEnabled = true;
            cmbSearchField.Location = new Point(28, 70);
            cmbSearchField.Name = "cmbSearchField";
            cmbSearchField.Size = new Size(182, 33);
            cmbSearchField.TabIndex = 11;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(274, 70);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(112, 34);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "Поиск";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(274, 126);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(112, 34);
            btnClear.TabIndex = 13;
            btnClear.Text = "Сброс";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(28, 32);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(60, 25);
            lblTotal.TabIndex = 14;
            lblTotal.Text = "Итоги";
            // 
            // FormShipmentHistory
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1286, 1000);
            Controls.Add(lblTotal);
            Controls.Add(btnClear);
            Controls.Add(btnSearch);
            Controls.Add(cmbSearchField);
            Controls.Add(txtSearchValue);
            Controls.Add(panel1);
            Controls.Add(dgvHistory);
            Controls.Add(lblHistory);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormShipmentHistory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "История отгрузок";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHistory;
        private DataGridView dgvHistory;
        private Panel panel1;
        private TextBox txtSearchValue;
        private ComboBox cmbSearchField;
        private Button btnSearch;
        private Button btnClear;
        private Label lblTotal;
    }
}