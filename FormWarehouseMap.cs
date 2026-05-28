namespace Sklad1.Forms
{
    partial class FormSupplyImport
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
            lblFile = new Label();
            btnCancel = new Button();
            btnImport = new Button();
            panel2 = new Panel();
            label2 = new Label();
            label1 = new Label();
            dgvPreview = new DataGridView();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(181, 618);
            panel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(355, 9);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(474, 65);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "ИМПОРТ ДАННЫХ";
            // 
            // lblFile
            // 
            lblFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblFile.AutoSize = true;
            lblFile.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblFile.Location = new Point(199, 74);
            lblFile.Name = "lblFile";
            lblFile.Size = new Size(202, 30);
            lblFile.TabIndex = 4;
            lblFile.Text = "Файл (CSV/Excel):";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.BackColor = SystemColors.ActiveCaption;
            btnCancel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCancel.Location = new Point(601, 536);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(239, 54);
            btnCancel.TabIndex = 32;
            btnCancel.Text = "Сохранить";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnImport
            // 
            btnImport.Anchor = AnchorStyles.Bottom;
            btnImport.BackColor = SystemColors.ActiveCaption;
            btnImport.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnImport.Location = new Point(314, 536);
            btnImport.Margin = new Padding(4, 5, 4, 5);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(241, 54);
            btnImport.TabIndex = 31;
            btnImport.Text = "Импортировать";
            btnImport.UseVisualStyleBackColor = false;
            btnImport.Click += btnImport_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.Gainsboro;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(199, 131);
            panel2.Name = "panel2";
            panel2.Size = new Size(792, 88);
            panel2.TabIndex = 33;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.AppWorkspace;
            label2.Location = new Point(248, 52);
            label2.Name = "label2";
            label2.Size = new Size(327, 25);
            label2.TabIndex = 1;
            label2.Text = "Поддерживаемые форматы: .csv, .xlsx ";
            // 
            // label1
            // 
            label1.ForeColor = SystemColors.WindowFrame;
            label1.Location = new Point(301, 0);
            label1.Name = "label1";
            label1.Size = new Size(223, 63);
            label1.TabIndex = 0;
            label1.Text = "Перетащите файл сюда или нажмите для выбора";
            // 
            // dgvPreview
            // 
            dgvPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPreview.Location = new Point(199, 231);
            dgvPreview.Name = "dgvPreview";
            dgvPreview.RowHeadersWidth = 62;
            dgvPreview.Size = new Size(792, 303);
            dgvPreview.TabIndex = 34;
            // 
            // FormSupplyImport
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 618);
            Controls.Add(dgvPreview);
            Controls.Add(panel2);
            Controls.Add(btnCancel);
            Controls.Add(btnImport);
            Controls.Add(lblFile);
            Controls.Add(lblTitle);
            Controls.Add(panel1);
            Name = "FormSupplyImport";
            Text = "Импорт поставок";
            WindowState = FormWindowState.Maximized;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private Label lblFile;
        private Button btnCancel;
        private Button btnImport;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private DataGridView dgvPreview;
    }
}