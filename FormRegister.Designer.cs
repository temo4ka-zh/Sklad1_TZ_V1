namespace Sklad1.Forms
{
    partial class FormProduct
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
            lblArticle = new Label();
            lblName = new Label();
            lblCategory = new Label();
            lblPurchasePrice = new Label();
            txtArticle = new TextBox();
            txtName = new TextBox();
            txtPurchasePrice = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            cmbCategory = new ComboBox();
            cmbUnit = new ComboBox();
            lblUnit = new Label();
            dtpExpDate = new DateTimePicker();
            lblDate = new Label();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(181, 852);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(301, 15);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(443, 65);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Создание товара";
            // 
            // lblArticle
            // 
            lblArticle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblArticle.AutoSize = true;
            lblArticle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblArticle.Location = new Point(313, 96);
            lblArticle.Margin = new Padding(4, 0, 4, 0);
            lblArticle.Name = "lblArticle";
            lblArticle.Size = new Size(92, 28);
            lblArticle.TabIndex = 2;
            lblArticle.Text = "Артикул:";
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblName.Location = new Point(313, 179);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(104, 28);
            lblName.TabIndex = 3;
            lblName.Text = "Название:";
            // 
            // lblCategory
            // 
            lblCategory.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblCategory.Location = new Point(313, 258);
            lblCategory.Margin = new Padding(4, 0, 4, 0);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(110, 28);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "Категория:";
            // 
            // lblPurchasePrice
            // 
            lblPurchasePrice.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblPurchasePrice.AutoSize = true;
            lblPurchasePrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPurchasePrice.Location = new Point(313, 325);
            lblPurchasePrice.Margin = new Padding(4, 0, 4, 0);
            lblPurchasePrice.Name = "lblPurchasePrice";
            lblPurchasePrice.Size = new Size(154, 28);
            lblPurchasePrice.TabIndex = 5;
            lblPurchasePrice.Text = "Себестоимость:";
            // 
            // txtArticle
            // 
            txtArticle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtArticle.Location = new Point(313, 129);
            txtArticle.Margin = new Padding(4, 5, 4, 5);
            txtArticle.Name = "txtArticle";
            txtArticle.Size = new Size(404, 31);
            txtArticle.TabIndex = 6;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(313, 212);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(404, 31);
            txtName.TabIndex = 7;
            // 
            // txtPurchasePrice
            // 
            txtPurchasePrice.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPurchasePrice.Location = new Point(313, 358);
            txtPurchasePrice.Margin = new Padding(4, 5, 4, 5);
            txtPurchasePrice.Name = "txtPurchasePrice";
            txtPurchasePrice.Size = new Size(404, 31);
            txtPurchasePrice.TabIndex = 9;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom;
            btnSave.BackColor = SystemColors.ActiveCaption;
            btnSave.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSave.Location = new Point(329, 667);
            btnSave.Margin = new Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(167, 70);
            btnSave.TabIndex = 10;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.BackColor = SystemColors.ActiveCaption;
            btnCancel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCancel.Location = new Point(529, 667);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(167, 70);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblQuantity
            // 
            lblQuantity.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblQuantity.Location = new Point(313, 394);
            lblQuantity.Margin = new Padding(4, 0, 4, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(124, 28);
            lblQuantity.TabIndex = 12;
            lblQuantity.Text = "Количество:";
            // 
            // txtQuantity
            // 
            txtQuantity.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtQuantity.Location = new Point(313, 427);
            txtQuantity.Margin = new Padding(4, 5, 4, 5);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(404, 31);
            txtQuantity.TabIndex = 13;
            // 
            // cmbCategory
            // 
            cmbCategory.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(313, 289);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(404, 33);
            cmbCategory.TabIndex = 14;
            // 
            // cmbUnit
            // 
            cmbUnit.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbUnit.FormattingEnabled = true;
            cmbUnit.Location = new Point(313, 494);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.Size = new Size(404, 33);
            cmbUnit.TabIndex = 15;
            // 
            // lblUnit
            // 
            lblUnit.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblUnit.AutoSize = true;
            lblUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblUnit.Location = new Point(313, 463);
            lblUnit.Margin = new Padding(4, 0, 4, 0);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(201, 28);
            lblUnit.TabIndex = 16;
            lblUnit.Text = "Единица измерения:";
            // 
            // dtpExpDate
            // 
            dtpExpDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpExpDate.Location = new Point(313, 563);
            dtpExpDate.Name = "dtpExpDate";
            dtpExpDate.Size = new Size(404, 31);
            dtpExpDate.TabIndex = 17;
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblDate.Location = new Point(313, 532);
            lblDate.Margin = new Padding(4, 0, 4, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(151, 28);
            lblDate.TabIndex = 18;
            lblDate.Text = "Срок годности:";
            // 
            // FormProduct
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 852);
            Controls.Add(lblDate);
            Controls.Add(dtpExpDate);
            Controls.Add(lblUnit);
            Controls.Add(cmbUnit);
            Controls.Add(cmbCategory);
            Controls.Add(txtQuantity);
            Controls.Add(lblQuantity);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtPurchasePrice);
            Controls.Add(txtName);
            Controls.Add(txtArticle);
            Controls.Add(lblPurchasePrice);
            Controls.Add(lblCategory);
            Controls.Add(lblName);
            Controls.Add(lblArticle);
            Controls.Add(lblTitle);
            Controls.Add(panel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormProduct";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Создание товара";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private Label lblArticle;
        private Label lblName;
        private Label lblCategory;
        private Label lblPurchasePrice;
        private TextBox txtArticle;
        private TextBox txtName;
        private TextBox txtPurchasePrice;
        private Button btnSave;
        private Button btnCancel;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private ComboBox cmbCategory;
        private ComboBox cmbUnit;
        private Label lblUnit;
        private DateTimePicker dtpExpDate;
        private Label lblDate;
    }
}