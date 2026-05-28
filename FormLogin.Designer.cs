namespace Sklad1.Forms
{
    partial class FormExpiryDates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExpiryDates));
            panel1 = new Panel();
            lblTitle = new Label();
            Норма = new Label();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            pictureBox3 = new PictureBox();
            label2 = new Label();
            panel2 = new Panel();
            dgvExpDates = new DataGridView();
            btnWriteOff = new Button();
            btnUpdate = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpDates).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(lblTitle);
            panel1.Location = new Point(67, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(846, 77);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(256, 17);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(365, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "УЧЁТ СРОКОВ ГОДНОСТИ";
            // 
            // Норма
            // 
            Норма.Location = new Point(36, 4);
            Норма.Name = "Норма";
            Норма.Size = new Size(74, 36);
            Норма.TabIndex = 2;
            Норма.Text = "Норма";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(156, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(27, 27);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(189, 4);
            label1.Name = "label1";
            label1.Size = new Size(293, 25);
            label1.TabIndex = 5;
            label1.Text = "Скидка автоматически применена";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(524, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(27, 27);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(557, 5);
            label2.Name = "label2";
            label2.Size = new Size(253, 25);
            label2.TabIndex = 7;
            label2.Text = "Подлежит списанию (убыток)";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(Норма);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(67, 91);
            panel2.Name = "panel2";
            panel2.Size = new Size(846, 30);
            panel2.TabIndex = 8;
            // 
            // dgvExpDates
            // 
            dgvExpDates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvExpDates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExpDates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpDates.Location = new Point(36, 134);
            dgvExpDates.Name = "dgvExpDates";
            dgvExpDates.RowHeadersWidth = 62;
            dgvExpDates.Size = new Size(911, 336);
            dgvExpDates.TabIndex = 9;
            // 
            // btnWriteOff
            // 
            btnWriteOff.Anchor = AnchorStyles.Bottom;
            btnWriteOff.BackColor = Color.LightCoral;
            btnWriteOff.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnWriteOff.ForeColor = Color.DarkRed;
            btnWriteOff.Location = new Point(223, 503);
            btnWriteOff.Margin = new Padding(4, 5, 4, 5);
            btnWriteOff.Name = "btnWriteOff";
            btnWriteOff.Size = new Size(291, 47);
            btnWriteOff.TabIndex = 32;
            btnWriteOff.Text = "Списать просроченные";
            btnWriteOff.UseVisualStyleBackColor = false;
            btnWriteOff.Click += btnWriteOff_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Bottom;
            btnUpdate.BackColor = SystemColors.ScrollBar;
            btnUpdate.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnUpdate.Location = new Point(591, 503);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(126, 44);
            btnUpdate.TabIndex = 33;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // FormExpiryDates
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(990, 575);
            Controls.Add(btnUpdate);
            Controls.Add(btnWriteOff);
            Controls.Add(dgvExpDates);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FormExpiryDates";
            Text = "Учет сроков годности";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpDates).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private Label Норма;
        private PictureBox pictureBox2;
        private Label label1;
        private PictureBox pictureBox3;
        private Label label2;
        private Panel panel2;
        private DataGridView dgvExpDates;
        private Button btnWriteOff;
        private Button btnUpdate;
    }
}