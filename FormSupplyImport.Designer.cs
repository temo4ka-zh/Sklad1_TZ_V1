namespace Sklad1.Forms
{
    partial class FormSupply
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
            cmbUnit = new ComboBox();
            lblPurchaseCost = new Label();
            lblExpirationDate = new Label();
            lblUnOfMeasurement = new Label();
            lblQuantity = new Label();
            lblName = new Label();
            cmbCurrency = new ComboBox();
            lblPurchaseCurrency = new Label();
            btnCancel = new Button();
            btnAdd = new Button();
            cmbName = new ComboBox();
            txtQuantity = new TextBox();
            txtPrice = new TextBox();
            dtpExpiryDate = new DateTimePicker();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(181, 648);
            panel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(261, 0);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(420, 65);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Новая поставка";
            // 
            // cmbUnit
            // 
            cmbUnit.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbUnit.FormattingEnabled = true;
            cmbUnit.Location = new Point(198, 271);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.Size = new Size(420, 33);
            cmbUnit.TabIndex = 24;
            // 
            // lblPurchaseCost
            // 
            lblPurchaseCost.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblPurchaseCost.AutoSize = true;
            lblPurchaseCost.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPurchaseCost.Location = new Point(198, 398);
            lblPurchaseCost.Margin = new Padding(4, 0, 4, 0);
            lblPurchaseCost.Name = "lblPurchaseCost";
            lblPurchaseCost.Size = new Size(232, 28);
            lblPurchaseCost.TabIndex = 22;
            lblPurchaseCost.Text = "Себестоимость закупки:";
            // 
            // lblExpirationDate
            // 
            lblExpirationDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblExpirationDate.AutoSize = true;
            lblExpirationDate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblExpirationDate.Location = new Point(198, 319);
            lblExpirationDate.Margin = new Padding(4, 0, 4, 0);
            lblExpirationDate.Name = "lblExpirationDate";
            lblExpirationDate.Size = new Size(151, 28);
            lblExpirationDate.TabIndex = 18;
            lblExpirationDate.Text = "Срок годности:";
            // 
            // lblUnOfMeasurement
            // 
            lblUnOfMeasurement.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblUnOfMeasurement.AutoSize = true;
            lblUnOfMeasurement.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblUnOfMeasurement.Location = new Point(198, 240);
            lblUnOfMeasurement.Margin = new Padding(4, 0, 4, 0);
            lblUnOfMeasurement.Name = "lblUnOfMeasurement";
            lblUnOfMeasurement.Size = new Size(201, 28);
            lblUnOfMeasurement.TabIndex = 17;
            lblUnOfMeasurement.Text = "Единица измерения:";
            // 
            // lblQuantity
            // 
            lblQuantity.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblQuantity.Location = new Point(198, 164);
            lblQuantity.Margin = new Padding(4, 0, 4, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(124, 28);
            lblQuantity.TabIndex = 16;
            lblQuantity.Text = "Количество:";
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblName.Location = new Point(198, 94);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(223, 28);
            lblName.TabIndex = 15;
            lblName.Text = "Наименование товара:";
            // 
            // cmbCurrency
            // 
            cmbCurrency.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbCurrency.FormattingEnabled = true;
            cmbCurrency.Location = new Point(198, 503);
            cmbCurrency.Name = "cmbCurrency";
            cmbCurrency.Size = new Size(420, 33);
            cmbCurrency.TabIndex = 27;
            // 
            // lblPurchaseCurrency
            // 
            lblPurchaseCurrency.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblPurchaseCurrency.AutoSize = true;
            lblPurchaseCurrency.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPurchaseCurrency.Location = new Point(198, 472);
            lblPurchaseCurrency.Margin = new Padding(4, 0, 4, 0);
            lblPurchaseCurrency.Name = "lblPurchaseCurrency";
            lblPurchaseCurrency.Size = new Size(160, 28);
            lblPurchaseCurrency.TabIndex = 28;
            lblPurchaseCurrency.Text = "Валюта закупки:";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.BackColor = SystemColors.ActiveCaption;
            btnCancel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCancel.Location = new Point(444, 564);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(167, 70);
            btnCancel.TabIndex = 30;
            btnCancel.Text = "Сохранить";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom;
            btnAdd.BackColor = SystemColors.ActiveCaption;
            btnAdd.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnAdd.Location = new Point(244, 564);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(167, 70);
            btnAdd.TabIndex = 29;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // cmbName
            // 
            cmbName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbName.FormattingEnabled = true;
            cmbName.Location = new Point(198, 125);
            cmbName.Name = "cmbName";
            cmbName.Size = new Size(420, 33);
            cmbName.TabIndex = 31;
            // 
            // txtQuantity
            // 
            txtQuantity.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtQuantity.Location = new Point(198, 197);
            txtQuantity.Margin = new Padding(4, 5, 4, 5);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(422, 31);
            txtQuantity.TabIndex = 32;
            // 
            // txtPrice
            // 
            txtPrice.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPrice.Location = new Point(198, 431);
            txtPrice.Margin = new Padding(4, 5, 4, 5);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(422, 31);
            txtPrice.TabIndex = 33;
            // 
            // dtpExpiryDate
            // 
            dtpExpiryDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpExpiryDate.Location = new Point(198, 363);
            dtpExpiryDate.Name = "dtpExpiryDate";
            dtpExpiryDate.Size = new Size(420, 31);
            dtpExpiryDate.TabIndex = 34;
            // 
            // FormSupply
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(818, 648);
            Controls.Add(dtpExpiryDate);
            Controls.Add(txtPrice);
            Controls.Add(txtQuantity);
            Controls.Add(cmbName);
            Controls.Add(btnCancel);
            Controls.Add(btnAdd);
            Controls.Add(lblPurchaseCurrency);
            Controls.Add(cmbCurrency);
            Controls.Add(cmbUnit);
            Controls.Add(lblPurchaseCost);
            Controls.Add(lblExpirationDate);
            Controls.Add(lblUnOfMeasurement);
            Controls.Add(lblQuantity);
            Controls.Add(lblName);
            Controls.Add(lblTitle);
            Controls.Add(panel1);
            Name = "FormSupply";
            Text = "Добавление поставки";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private ComboBox cmbUnit;
        private Label lblPurchaseCost;
        private Label lblExpirationDate;
        private Label lblUnOfMeasurement;
        private Label lblQuantity;
        private Label lblName;
        private ComboBox cmbCurrency;
        private Label lblPurchaseCurrency;
        private Button btnCancel;
        private Button btnAdd;
        private ComboBox cmbName;
        private TextBox txtQuantity;
        private TextBox txtPrice;
        private DateTimePicker dtpExpiryDate;
    }
}