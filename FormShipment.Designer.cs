namespace Sklad1.Forms
{
    partial class FormRegister
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
            lblTitle = new Label();
            txtLastName = new TextBox();
            txtFirstName = new TextBox();
            txtMiddleName = new TextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            label5 = new Label();
            txtPassword = new TextBox();
            label6 = new Label();
            txtConfirmPassword = new TextBox();
            btnRegister = new Button();
            panel1 = new Panel();
            lblFirstName = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(240, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(242, 33);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "РЕГИСТРАЦИЯ";
            // 
            // txtLastName
            // 
            txtLastName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtLastName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtLastName.ForeColor = SystemColors.WindowText;
            txtLastName.Location = new Point(230, 73);
            txtLastName.Margin = new Padding(3, 2, 3, 2);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(263, 29);
            txtLastName.TabIndex = 1;
            // 
            // txtFirstName
            // 
            txtFirstName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtFirstName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtFirstName.Location = new Point(230, 133);
            txtFirstName.Margin = new Padding(3, 2, 3, 2);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(263, 29);
            txtFirstName.TabIndex = 4;
            // 
            // txtMiddleName
            // 
            txtMiddleName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtMiddleName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtMiddleName.Location = new Point(230, 193);
            txtMiddleName.Margin = new Padding(3, 2, 3, 2);
            txtMiddleName.Name = "txtMiddleName";
            txtMiddleName.Size = new Size(263, 29);
            txtMiddleName.TabIndex = 6;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(230, 234);
            label4.Name = "label4";
            label4.Size = new Size(135, 19);
            label4.TabIndex = 7;
            label4.Text = "Электронная почта:";
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtEmail.Location = new Point(230, 255);
            txtEmail.Margin = new Padding(3, 2, 3, 2);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(263, 29);
            txtEmail.TabIndex = 8;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(230, 294);
            label5.Name = "label5";
            label5.Size = new Size(59, 19);
            label5.TabIndex = 9;
            label5.Text = "Пароль:";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtPassword.Location = new Point(230, 315);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(263, 29);
            txtPassword.TabIndex = 10;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(230, 354);
            label6.Name = "label6";
            label6.Size = new Size(163, 19);
            label6.TabIndex = 11;
            label6.Text = "Подтверждение пароля:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConfirmPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtConfirmPassword.Location = new Point(230, 375);
            txtConfirmPassword.Margin = new Padding(3, 2, 3, 2);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(263, 29);
            txtConfirmPassword.TabIndex = 12;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnRegister.BackColor = SystemColors.ActiveCaption;
            btnRegister.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnRegister.Location = new Point(296, 419);
            btnRegister.Margin = new Padding(3, 2, 3, 2);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(129, 54);
            btnRegister.TabIndex = 0;
            btnRegister.Text = "Регистрация";
            btnRegister.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(127, 511);
            panel1.TabIndex = 15;
            // 
            // lblFirstName
            // 
            lblFirstName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblFirstName.Location = new Point(230, 54);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(64, 17);
            lblFirstName.TabIndex = 16;
            lblFirstName.Text = "Фамилия:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(230, 114);
            label2.Name = "label2";
            label2.Size = new Size(34, 17);
            label2.TabIndex = 17;
            label2.Text = "Имя";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(230, 174);
            label3.Name = "label3";
            label3.Size = new Size(63, 17);
            label3.TabIndex = 18;
            label3.Text = "Отчество";
            // 
            // FormRegister
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(599, 511);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblFirstName);
            Controls.Add(panel1);
            Controls.Add(btnRegister);
            Controls.Add(txtConfirmPassword);
            Controls.Add(label6);
            Controls.Add(txtPassword);
            Controls.Add(label5);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(txtMiddleName);
            Controls.Add(txtFirstName);
            Controls.Add(txtLastName);
            Controls.Add(lblTitle);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Регистрация";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtMiddleName;
        private Label label4;
        private TextBox txtEmail;
        private Label label5;
        private TextBox txtPassword;
        private Label label6;
        private TextBox txtConfirmPassword;
        private Button btnRegister;
        private Panel panel1;
        private Label lblFirstName;
        private Label label2;
        private Label label3;
    }

}
