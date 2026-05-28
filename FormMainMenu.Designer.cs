namespace Sklad1.Forms
{
    partial class FormMain
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
            components = new System.ComponentModel.Container();
            dgvProducts = new DataGridView();
            btnCreate = new Button();
            CreateMenu = new ContextMenuStrip(components);
            menuProduct = new ToolStripMenuItem();
            menuCategory = new ToolStripMenuItem();
            menuShipment = new ToolStripMenuItem();
            btnEdit = new Button();
            EditMenu = new ContextMenuStrip(components);
            menuEditProduct = new ToolStripMenuItem();
            menuEditCategory = new ToolStripMenuItem();
            btnDelete = new Button();
            btnHistory = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            CreateMenu.SuspendLayout();
            EditMenu.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.ColumnHeadersHeight = 34;
            dgvProducts.Location = new Point(0, 62);
            dgvProducts.Margin = new Padding(4, 5, 4, 5);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 62;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1060, 408);
            dgvProducts.TabIndex = 2;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.White;
            btnCreate.ContextMenuStrip = CreateMenu;
            btnCreate.Location = new Point(563, 20);
            btnCreate.Margin = new Padding(4, 5, 4, 5);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(171, 67);
            btnCreate.TabIndex = 3;
            btnCreate.Text = "Создать ";
            btnCreate.UseVisualStyleBackColor = false;
            // 
            // CreateMenu
            // 
            CreateMenu.ImageScalingSize = new Size(24, 24);
            CreateMenu.Items.AddRange(new ToolStripItem[] { menuProduct, menuCategory, menuShipment });
            CreateMenu.Name = "createMenu";
            CreateMenu.Size = new Size(168, 100);
            // 
            // menuProduct
            // 
            menuProduct.Name = "menuProduct";
            menuProduct.Size = new Size(167, 32);
            menuProduct.Text = "Товар";
            menuProduct.Click += menuProduct_Click;
            // 
            // menuCategory
            // 
            menuCategory.Name = "menuCategory";
            menuCategory.Size = new Size(167, 32);
            menuCategory.Text = "Категория";
            menuCategory.Click += menuCategory_Click;
            // 
            // menuShipment
            // 
            menuShipment.Name = "menuShipment";
            menuShipment.Size = new Size(167, 32);
            menuShipment.Text = "Отгрузка";
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.White;
            btnEdit.ContextMenuStrip = EditMenu;
            btnEdit.Location = new Point(383, 18);
            btnEdit.Margin = new Padding(4, 5, 4, 5);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(171, 67);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Редактировать ";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // EditMenu
            // 
            EditMenu.ImageScalingSize = new Size(24, 24);
            EditMenu.Items.AddRange(new ToolStripItem[] { menuEditProduct, menuEditCategory });
            EditMenu.Name = "editMenu";
            EditMenu.Size = new Size(168, 68);
            // 
            // menuEditProduct
            // 
            menuEditProduct.Name = "menuEditProduct";
            menuEditProduct.Size = new Size(167, 32);
            menuEditProduct.Text = "Товар";
            // 
            // menuEditCategory
            // 
            menuEditCategory.Name = "menuEditCategory";
            menuEditCategory.Size = new Size(167, 32);
            menuEditCategory.Text = "Категория";
            menuEditCategory.Click += menuEditCategory_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.Location = new Point(23, 20);
            btnDelete.Margin = new Padding(4, 5, 4, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(171, 67);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnHistory
            // 
            btnHistory.BackColor = Color.White;
            btnHistory.Location = new Point(203, 20);
            btnHistory.Margin = new Padding(4, 5, 4, 5);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(171, 67);
            btnHistory.TabIndex = 6;
            btnHistory.Text = "История отгрузок";
            btnHistory.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnCreate);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnHistory);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 480);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1060, 105);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(dgvProducts);
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(1060, 585);
            panel2.TabIndex = 8;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(401, 15);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(293, 40);
            label1.TabIndex = 0;
            label1.Text = "СПИСОК ТОВАРОВ";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 585);
            Controls.Add(panel2);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormMain";
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Список товаров";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            CreateMenu.ResumeLayout(false);
            EditMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvProducts;
        private Button btnCreate;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnHistory;
        private ContextMenuStrip CreateMenu;
        private ContextMenuStrip EditMenu;
        private ToolStripMenuItem menuProduct;
        private ToolStripMenuItem menuCategory;
        private ToolStripMenuItem menuShipment;
        private ToolStripMenuItem menuEditProduct;
        private ToolStripMenuItem menuEditCategory;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
    }
}