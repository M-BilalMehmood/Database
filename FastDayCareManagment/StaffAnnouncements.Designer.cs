namespace FastDayCareManagment
{
    partial class StaffAnnouncements
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffAnnouncements));
            button5 = new Button();
            pictureBox2 = new PictureBox();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            Announcments = new DataGridView();
            panel1 = new Panel();
            button11 = new Button();
            button9 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Announcments).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Century Gothic", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(255, 166, 43);
            label1.Location = new Point(178, 40);
            label1.Name = "label1";
            label1.Size = new Size(121, 56);
            label1.TabIndex = 34;
            label1.Text = "Staff";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1002, 55);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(234, 53);
            pictureBox1.TabIndex = 37;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(22, 105, 122);
            label3.Location = new Point(178, 164);
            label3.Name = "label3";
            label3.Size = new Size(246, 36);
            label3.TabIndex = 36;
            label3.Text = "Announcements";
            // 
            // Announcments
            // 
            Announcments.AllowUserToAddRows = false;
            Announcments.AllowUserToResizeColumns = false;
            Announcments.AllowUserToResizeRows = false;
            Announcments.BackgroundColor = Color.White;
            Announcments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Announcments.EditMode = DataGridViewEditMode.EditProgrammatically;
            Announcments.Location = new Point(178, 203);
            Announcments.Name = "Announcments";
            Announcments.Size = new Size(1049, 449);
            Announcments.TabIndex = 35;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(22, 105, 122);
            panel1.Controls.Add(button11);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(140, 681);
            panel1.TabIndex = 33;
            // 
            // button11
            // 
            button11.BackColor = Color.FromArgb(22, 105, 122);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 159, 181);
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button11.ForeColor = Color.FromArgb(237, 231, 227);
            button11.Location = new Point(0, 413);
            button11.Name = "button11";
            button11.Size = new Size(140, 45);
            button11.TabIndex = 35;
            button11.Text = "Mails";
            button11.UseVisualStyleBackColor = false;
            button11.Click += button11_Click;
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
            button9.Text = "Class Info";
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
            button3.Text = "Attendance";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // StaffAnnouncements
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1264, 681);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(Announcments);
            Controls.Add(panel1);
            Name = "StaffAnnouncements";
            Text = "StaffAnnouncements";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Announcments).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button5;
        private PictureBox pictureBox2;
        private Button button2;
        private Button button1;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label3;
        private DataGridView Announcments;
        private Panel panel1;
        private Button button11;
        private Button button9;
        private Button button3;
    }
}