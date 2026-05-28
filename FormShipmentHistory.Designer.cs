namespace Sklad1.Forms
{
    partial class FormShipment
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
            dgvItems = new DataGridView();
            panel1 = new Panel();
            btnShip = new Button();
            btnCancel = new Button();
            btnAdd = new Button();
            txtClient = new TextBox();
            cmbProduct = new ComboBox();
            lblClient = new Label();
            lblQuantity = new Label();
            lblProduct = new Label();
            lblTitle = new Label();
            lblProductList = new Label();
            panel2 = new Panel();
            cmbQuantity = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvItems
            // 
            dgvItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Location = new Point(0, 80);
            dgvItems.Margin = new Padding(4, 5, 4, 5);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersVisible = false;
            dgvItems.RowHeadersWidth = 62;
            dgvItems.Size = new Size(820, 845);
            dgvItems.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.Controls.Add(cmbQuantity);
            panel1.Controls.Add(btnShip);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(txtClient);
            panel1.Controls.Add(cmbProduct);
            panel1.Controls.Add(lblClient);
            panel1.Controls.Add(lblQuantity);
            panel1.Controls.Add(lblProduct);
            panel1.Controls.Add(lblTitle);
            panel1.Location = new Point(853, 80);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(416, 650);
            panel1.TabIndex = 1;
            // 
            // btnShip
            // 
            btnShip.BackColor = SystemColors.ActiveCaption;
            btnShip.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnShip.Location = new Point(223, 482);
            btnShip.Margin = new Padding(4, 5, 4, 5);
            btnShip.Name = "btnShip";
            btnShip.Size = new Size(146, 95);
            btnShip.TabIndex = 16;
            btnShip.Text = "Совершить отгрузку";
            btnShip.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.ActiveCaption;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCancel.Location = new Point(47, 482);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(146, 95);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = SystemColors.ActiveCaption;
            btnAdd.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnAdd.Location = new Point(127, 365);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(143, 58);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // txtClient
            // 
            txtClient.BackColor = SystemColors.ActiveCaption;
            txtClient.Location = new Point(23, 280);
            txtClient.Margin = new Padding(4, 5, 4, 5);
            txtClient.Name = "txtClient";
            txtClient.Size = new Size(370, 31);
            txtClient.TabIndex = 14;
            // 
            // cmbProduct
            // 
            cmbProduct.BackColor = SystemColors.ActiveCaption;
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(23, 130);
            cmbProduct.Margin = new Padding(4, 5, 4, 5);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(370, 33);
            cmbProduct.TabIndex = 12;
            // 
            // lblClient
            // 
            lblClient.AutoSize = true;
            lblClient.Location = new Point(23, 250);
            lblClient.Margin = new Padding(4, 0, 4, 0);
            lblClient.Name = "lblClient";
            lblClient.Size = new Size(139, 25);
            lblClient.TabIndex = 11;
            lblClient.Text = "Куда отгрузить ";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblQuantity.Location = new Point(23, 173);
            lblQuantity.Margin = new Padding(4, 0, 4, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(201, 28);
            lblQuantity.TabIndex = 10;
            lblQuantity.Text = "Введите количество ";
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblProduct.Location = new Point(23, 97);
            lblProduct.Margin = new Padding(4, 0, 4, 0);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(252, 28);
            lblProduct.TabIndex = 9;
            lblProduct.Text = "Выберите товар из списка";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(127, 27);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(170, 40);
            lblTitle.TabIndex = 9;
            lblTitle.Text = "ОТГРУЗКА";
            // 
            // lblProductList
            // 
            lblProductList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblProductList.AutoSize = true;
            lblProductList.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblProductList.Location = new Point(251, 15);
            lblProductList.Margin = new Padding(4, 0, 4, 0);
            lblProductList.Name = "lblProductList";
            lblProductList.Size = new Size(293, 40);
            lblProductList.TabIndex = 2;
            lblProductList.Text = "СПИСОК ТОВАРОВ";
            // 
            // panel2
            // 
            panel2.BackColor = Color.MidnightBlue;
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 935);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(1286, 65);
            panel2.TabIndex = 8;
            // 
            // cmbQuantity
            // 
            cmbQuantity.BackColor = SystemColors.ActiveCaption;
            cmbQuantity.FormattingEnabled = true;
            cmbQuantity.Location = new Point(23, 212);
            cmbQuantity.Margin = new Padding(4, 5, 4, 5);
            cmbQuantity.Name = "cmbQuantity";
            cmbQuantity.Size = new Size(370, 33);
            cmbQuantity.TabIndex = 17;
            // 
            // FormShipment
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1286, 1000);
            Controls.Add(panel2);
            Controls.Add(lblProductList);
            Controls.Add(panel1);
            Controls.Add(dgvItems);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormShipment";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Отгрузка";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvItems;
        private Panel panel1;
        private Label lblProductList;
        private Panel panel2;
        private Label lblClient;
        private Label lblQuantity;
        private Label lblProduct;
        private Label lblTitle;
        private ComboBox cmbProduct;
        private TextBox txtClient;
        private Button btnAdd;
        private Button btnShip;
        private Button btnCancel;
        private ComboBox cmbQuantity;
    }
}