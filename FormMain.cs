using NLog;
using Sklad1.Data;
using Sklad1.Forms;
using Sklad1.Helpers;
using Sklad1.Models;
using Sklad1.Properties;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Sklad1
{
    /// <summary>
    /// Форма входа 
    /// </summary>
    public partial class FormLogin : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FormLogin()
        {
            InitializeComponent();

            btnLogin.Click += BtnLogin_Click;
            lnkRegister.Click += lnkRegister_Click;
        }

        private void lnkRegister_Click(object sender, EventArgs e)
        {
            new FormRegister().Show();
            this.Hide();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!FieldsFilled())
                return;

            if (!IsValidEmail(txtEmail.Text))
                return;

            FindUser();

        }

        //проверки 
        private bool FieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(Resources.EnterEmailAndPassword);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            var trimmed = email.Trim();

            if (trimmed.Contains(" "))
            {
                MessageBox.Show(Resources.InvalidEmail);
                return false;
            }

            try
            {
                var addr = new MailAddress(email.Trim());
                return addr.Address == email.Trim();
            }

            catch(Exception ex)
            { 
                MessageBox.Show(Resources.InvalidEmail);
                return false;
            }
        }

        private void FindUser()
        {
            try
            {
                using (var bd = new Context())
                {
                    var user = bd.Users.FirstOrDefault(u =>
                        u.Email == txtEmail.Text.Trim() &&
                        u.PasswordHash == Password.HashPassword(txtPassword.Text.Trim()));

                    if (user == null)
                    {
                        MessageBox.Show(Resources.InvalidCredentials);
                        return;
                    }

                    CurrentUser.Id = user.Id;
                    CurrentUser.FirstName = user.FirstName;
                    CurrentUser.LastName = user.LastName;
                    CurrentUser.Email = user.Email;
                    CurrentUser.Role = user.Role;

                    FormMain.UserRole = user.Role;

                    var mainMenu = new FormMainMenu();
                    mainMenu.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorLogin);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }
    }
}
