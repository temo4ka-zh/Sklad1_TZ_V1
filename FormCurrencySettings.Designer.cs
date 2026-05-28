namespace Sklad1.Forms
{
    partial class FormCategory
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
            txtName = new TextBox();
            txtDescription = new TextBox();
            lblName = new Label();
            lblDescription = new Label();
            btnCreate = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(127, 511);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(197, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(326, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Категория товара";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(219, 153);
            txtName.Name = "txtName";
            txtName.Size = new Size(284, 23);
            txtName.TabIndex = 1;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(219, 233);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(284, 83);
            txtDescription.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblName.Location = new Point(219, 133);
            lblName.Name = "lblName";
            lblName.Size = new Size(132, 17);
            lblName.TabIndex = 3;
            lblName.Text = "Название категории:";
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblDescription.Location = new Point(219, 213);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(69, 17);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Описание:";
            // 
            // btnCreate
            // 
            btnCreate.Anchor = AnchorStyles.Bottom;
            btnCreate.BackColor = SystemColors.ActiveCaption;
            btnCreate.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCreate.Location = new Point(229, 366);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(109, 42);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Создать";
            btnCreate.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.BackColor = SystemColors.ActiveCaption;
            btnCancel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCancel.Location = new Point(378, 366);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(109, 42);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // FormCategory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(599, 511);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(lblDescription);
            Controls.Add(lblName);
            Controls.Add(txtDescription);
            Controls.Add(txtName);
            Controls.Add(lblTitle);
            Controls.Add(panel1);
            Name = "FormCategory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Создание категории";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private TextBox txtName;
        private TextBox txtDescription;
        private Label lblName;
        private Label lblDescription;
        private Button btnCreate;
        private Button btnCancel;
    }
}