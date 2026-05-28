using NLog;
using Sklad1.Data;
using Sklad1.Properties;
using System.Text.RegularExpressions;
using Sklad1.Models;

namespace Sklad1.Forms
{
    /// <summary>
    /// Форма создания новой категории товаров
    /// </summary>
    public partial class FormCategory : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FormCategory()
        {
            InitializeComponent();

            btnCreate.Click += BtnCreate_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsValidName(string name)
        {
            var trimmed = name.Trim();
            return Regex.IsMatch(trimmed, @"^[а-яА-ЯёЁa-zA-Z\s\-]+$");
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(Resources.EnterCategoryName);
                return;
            }

            if (!IsValidName(name))
            {
                MessageBox.Show(Resources.InvalidCategoryName);
                return;
            }

            try
            {
                using (var bd = new Context())
                {
                    if (bd.Categories.Any(c => c.Name == name))
                    {
                        MessageBox.Show(Resources.CategoryExists);
                        return;
                    }

                    var category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Description = description
                    };

                    bd.Categories.Add(category);
                    bd.SaveChanges();

                    MessageBox.Show(Resources.CategoryCreate);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorCreatingCategory);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }
    }
}

