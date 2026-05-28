namespace Sklad1
{
    partial class FormLogin
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
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            btnLogin = new Button();
            lnkRegister = new LinkLabel();
            panel1 = new Panel();
            lblPassword = new Label();
            lblEmail = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(290, 33);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(126, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ВХОД";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtPassword.Location = new Point(234, 225);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(263, 29);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtEmail.Location = new Point(234, 144);
            txtEmail.Margin = new Padding(3, 2, 3, 2);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(263, 29);
            txtEmail.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Bottom;
            btnLogin.BackColor = SystemColors.ActiveCaption;
            btnLogin.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLogin.Location = new Point(302, 324);
            btnLogin.Margin = new Padding(3, 2, 3, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(115, 56);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Вход";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // lnkRegister
            // 
            lnkRegister.Anchor = AnchorStyles.Bottom;
            lnkRegister.AutoSize = true;
            lnkRegister.Location = new Point(353, 410);
            lnkRegister.Name = "lnkRegister";
            lnkRegister.Size = new Size(122, 15);
            lnkRegister.TabIndex = 3;
            lnkRegister.TabStop = true;
            lnkRegister.Text = " Зарегистрироваться";
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(127, 511);
            panel1.TabIndex = 7;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPassword.Location = new Point(234, 204);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(59, 19);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Пароль:";
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblEmail.Location = new Point(234, 123);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(51, 19);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Почта:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Location = new Point(250, 410);
            label1.Name = "label1";
            label1.Size = new Size(107, 15);
            label1.TabIndex = 8;
            label1.Text = "Еще нет аккаунта?";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(597, 511);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(lnkRegister);
            Controls.Add(btnLogin);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(lblEmail);
            Controls.Add(lblTitle);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtPassword;
        private TextBox txtEmail;
        private Button btnLogin;
        private LinkLabel lnkRegister;
        private Panel panel1;
        private Label lblPassword;
        private Label lblEmail;
        private Label label1;
    }
}
