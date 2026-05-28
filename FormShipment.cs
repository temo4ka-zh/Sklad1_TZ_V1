using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Properties;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Sklad1.Models;

namespace Sklad1.Forms
{
    /// <summary>
    /// Форма регистрации нового пользователя
    /// </summary>
    public partial class FormRegister : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FormRegister()
        {
            InitializeComponent();

            btnRegister.Click += BtnRegister_Click;
            this.FormClosing += FormRegister_FormClosing;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (!AllFieldsFilled())
                return;

            if (!AllDataValid())
                return;

            SaveUser();
        }

        private bool AllFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show(Resources.FillAllFields);
                return false;
            }

            return true;
        }

        private bool AllDataValid()
        {
            if (!IsValidName(txtLastName.Text))
            {
                return false;
            }

            if (!IsValidName(txtFirstName.Text))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtMiddleName.Text) && !IsValidName(txtMiddleName.Text))
            {
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show(Resources.InvalidEmail);
                return false;
            }

            if (!IsValidPassword(txtPassword.Text))
            {
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show(Resources.PasswordsDontMatch);
                return false;
            }

            return true;
        }

        private bool IsValidName(string name)
        {
            var trimmed = name.Trim();

            if (trimmed.Length < 2 || trimmed.Length > 50)
            {
                MessageBox.Show(Resources.NameLengthError);
                return false;
            }

            if (trimmed.Contains(" "))
            {
                MessageBox.Show(Resources.NameHasSpaces);
                return false;
            }

            if (!Regex.IsMatch(trimmed, @"^[а-яА-ЯёЁa-zA-Z\-]+$"))
            {
                MessageBox.Show(Resources.InvalidFullName);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            var trimmed = email.Trim();

            if (trimmed.Length < 5 || trimmed.Length > 50)
            {
                MessageBox.Show(Resources.EmailLengthError);
                return false;
            }

            if (trimmed.Contains(" "))
            {
                MessageBox.Show(Resources.InvalidEmail);
                return false;
            }

            try
            {
                var addr = new MailAddress(trimmed);

                if (addr.Address != trimmed)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool IsValidPassword(string password)
        {
            var trimmed = password.Trim();

            if (trimmed.Contains(" "))
            {
                MessageBox.Show(Resources.PasswordHasSpaces);
                return false;
            }

            if (trimmed.Length < 6 || trimmed.Length > 50)
            {
                MessageBox.Show(Resources.InvalidPasswordLength);
                return false;
            }

            if (Regex.IsMatch(trimmed, @"[\p{So}]"))
            {
                MessageBox.Show(Resources.InvalidPassword);
                return false;
            }

            return true;
        }

        private void SaveUser()
        {
            try
            {
                var email = txtEmail.Text.Trim().ToLower();
                var lastName = txtLastName.Text.Trim();
                var firstName = txtFirstName.Text.Trim();
                var middleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? string.Empty : txtMiddleName.Text.Trim();

                using (var bd = new Context())
                {
                    if (bd.Users.Any(u => u.Email == email))
                    {
                        MessageBox.Show(Resources.EmailExists);
                        return;
                    }

                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        LastName = lastName,
                        FirstName = firstName,
                        MiddleName = middleName,
                        Email = email,
                        PasswordHash = Password.HashPassword(txtPassword.Text),
                        Role = UserRole.Storekeeper
                    };

                    bd.Users.Add(newUser);
                    bd.SaveChanges();

                    MessageBox.Show(Resources.RegisterSuccess);
                    var loginForm = new FormLogin();
                    loginForm.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorRegister);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }

        private void FormRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            var loginForm = new FormLogin();
            loginForm.Show();
        }
    }
}          
