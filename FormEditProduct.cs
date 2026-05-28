using NLog;
using Sklad1.Data;
using Sklad1.Helpers;
using Sklad1.Models;
using Sklad1.Properties;
using System.Text.RegularExpressions;

namespace Sklad1.Forms
{
    /// <summary>
    /// Форма редактирования категории товаров
    /// </summary>
    public partial class FormEditCategory : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Guid _categoryId;

        public FormEditCategory(Category category)
        {
            InitializeComponent();

            _categoryId = category.Id;
            txtName.Text = category.Name;
            txtDescription.Text = category.Description;

            btnUpdate.Click += BtnUpdate_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsValidName(string name)
        {
            var trimmed = name.Trim();
            return Regex.IsMatch(trimmed, @"^[а-яА-ЯёЁa-zA-Z\s\-]+$");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
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
                    if (bd.Categories.Any(c => c.Name == name && c.Id != _categoryId))
                    {
                        MessageBox.Show(Resources.CategoryExists);
                        return;
                    }

                    var category = bd.Categories.Find(_categoryId);

                    if (category != null)
                    {
                        category.Name = name;
                        category.Description = description;
                        bd.SaveChanges();

                        MessageBox.Show(Resources.CategoryEdit);

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, Resources.ErrorEditCategory);
                MessageBox.Show(Resources.ErrorSystem);
            }
        }
    }
}

