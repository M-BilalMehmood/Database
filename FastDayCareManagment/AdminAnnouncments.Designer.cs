namespace FastDayCareManagment
{
    partial class AdminAnnouncments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminAnnouncments));
            pictureBox1 = new PictureBox();
            label3 = new Label();
            Announcments = new DataGridView();
            DateTime = new DataGridViewTextBoxColumn();
            AnnouncementID = new DataGridViewTextBoxColumn();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            button2 = new Button();
            button1 = new Button();
            panel1 = new Panel();
            button11 = new Button();
            button5 = new Button();
            button10 = new Button();
            button9 = new Button();
            button3 = new Button();
            button6 = new Button();
            Edit = new Button();
            button8 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Announcments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(999, 50);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(234, 53);
            pictureBox1.TabIndex = 29;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(22, 105, 122);
            label3.Location = new Point(171, 164);
            label3.Name = "label3";
            label3.Size = new Size(246, 36);
            label3.TabIndex = 25;
            label3.Text = "Announcements";
            // 
            // Announcments
            // 
            Announcments.AllowUserToAddRows = false;
            Announcments.AllowUserToResizeColumns = false;
            Announcments.AllowUserToResizeRows = false;
            Announcments.BackgroundColor = Color.White;
            Announcments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Announcments.Columns.AddRange(new DataGridViewColumn[] { DateTime, AnnouncementID });
            Announcments.EditMode = DataGridViewEditMode.EditProgrammatically;
            Announcments.Location = new Point(174, 203);
            Announcments.Name = "Announcments";
            Announcments.Size = new Size(1049, 384);
            Announcments.TabIndex = 24;
            // 
            // DateTime
            // 
            DateTime.DataPropertyName = "DateTime";
            DateTime.HeaderText = "DateTime";
            DateTime.Name = "DateTime";
            DateTime.Width = 1006;
            // 
            // AnnouncementID
            // 
            AnnouncementID.DataPropertyName = "AnnouncementID";
            AnnouncementID.HeaderText = "AnnouncementID";
            AnnouncementID.Name = "AnnouncementID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Century Gothic", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(255, 166, 43);
            label1.Location = new Point(173, 39);
            label1.Name = "label1";
            label1.Size = new Size(178, 56);
            label1.TabIndex = 21;
            label1.Text = "Admin";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(30, 28);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(75, 80);
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 166, 43);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 159, 181);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(0, 266);
            button2.Name = "button2";
            button2.Size = new Size(140, 45);
            button2.TabIndex = 7;
            button2.Text = "Announcments";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(22, 105, 122);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 159, 181);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(237, 231, 227);
            button1.Location = new Point(0, 221);
            button1.Name = "button1";
            button1.Size = new Size(140, 45);
            button1.TabIndex = 6;
            button1.Text = "Home";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(22, 105, 122);
            panel1.Controls.Add(button11);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button10);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(140, 681);
            panel1.TabIndex = 17;
            // 
            // button11
            // 
            button11.BackColor = Color.FromArgb(22, 105, 122);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 159, 181);
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button11.ForeColor = Color.FromArgb(237, 231, 227);
            button11.Location = new Point(0, 459);
            button11.Name = "button11";
            button11.Size = new Size(140, 45);
            button11.TabIndex = 35;
            button11.Text = "Mails";
            button11.UseVisualStyleBackColor = false;
            button11.Click += button11_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(22, 105, 122);
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = Color.LightCoral;
            button5.Location = new Point(0, 624);
            button5.Name = "button5";
            button5.Size = new Size(140, 58);
            button5.TabIndex = 15;
            button5.Text = "LogOut";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(22, 105, 122);
            button10.FlatAppearance.BorderSize = 0;
            button10.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 159, 181);
            button10.FlatStyle = FlatStyle.Flat;
            button10.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button10.ForeColor = Color.FromArgb(237, 231, 227);
            button10.Location = new Point(0, 410);
            button10.Name = "button10";
            button10.Size = new Size(140, 45);
            button10.TabIndex = 34;
            button10.Text = "Staff";
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // button9
            // 
            button9.BackColor = Color.FromArgb(22, 105, 122);
            button9.FlatAppearance.BorderSize = 0;
            button9.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 159, 181);
            button9.FlatStyle = FlatStyle.Flat;
            button9.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button9.ForeColor = Color.FromArgb(237, 231, 227);
            button9.Location = new Point(0, 362);
            button9.Name = "button9";
            button9.Size = new Size(140, 45);
            button9.TabIndex = 33;
            button9.Text = "Children";
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(22, 105, 122);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 159, 181);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.FromArgb(237, 231, 227);
            button3.Location = new Point(0, 311);
            button3.Name = "button3";
            button3.Size = new Size(140, 45);
            button3.TabIndex = 8;
            button3.Text = "Enrollments";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click_1;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(255, 166, 43);
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 192, 204);
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button6.Location = new Point(1039, 593);
            button6.Name = "button6";
            button6.Size = new Size(184, 41);
            button6.TabIndex = 30;
            button6.Text = "Add Announcement";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // Edit
            // 
            Edit.BackColor = Color.FromArgb(237, 231, 227);
            Edit.FlatAppearance.BorderSize = 0;
            Edit.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 192, 204);
            Edit.FlatStyle = FlatStyle.Flat;
            Edit.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Edit.Location = new Point(908, 593);
            Edit.Name = "Edit";
            Edit.Size = new Size(125, 41);
            Edit.TabIndex = 31;
            Edit.Text = "Edit";
            Edit.UseVisualStyleBackColor = false;
            Edit.Click += Edit_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(237, 231, 227);
            button8.FlatAppearance.BorderSize = 0;
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 192, 204);
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button8.Location = new Point(777, 593);
            button8.Name = "button8";
            button8.Size = new Size(125, 41);
            button8.TabIndex = 32;
            button8.Text = "Delete";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // AdminAnnouncments
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1264, 681);
            Controls.Add(button8);
            Controls.Add(Edit);
            Controls.Add(button6);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(Announcments);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "AdminAnnouncments";
            Text = "AdminAnnouncments";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Announcments).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private Label label3;
        private DataGridView Announcments;
        private Label label1;
        private PictureBox pictureBox2;
        private Button button2;
        private Button button1;
        private Panel panel1;
        private Button button3;
        private Button button5;
        private Button button6;
        private Button Edit;
        private Button button8;
        private DataGridViewTextBoxColumn DateTime;
        private DataGridViewTextBoxColumn AnnouncementID;
        private Button button11;
        private Button button10;
        private Button button9;
    }
}