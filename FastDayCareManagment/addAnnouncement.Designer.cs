namespace FastDayCareManagment
{
    partial class addAnnouncement
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
            label3 = new Label();
            textBox1 = new TextBox();
            Publish = new Button();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(22, 105, 122);
            label3.Location = new Point(12, 16);
            label3.Name = "label3";
            label3.Size = new Size(300, 36);
            label3.TabIndex = 26;
            label3.Text = "Add Announcement";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(12, 63);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(776, 62);
            textBox1.TabIndex = 27;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // Publish
            // 
            Publish.BackColor = Color.FromArgb(255, 166, 43);
            Publish.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Publish.Location = new Point(617, 136);
            Publish.Name = "Publish";
            Publish.Size = new Size(171, 41);
            Publish.TabIndex = 28;
            Publish.Text = "Publish";
            Publish.UseVisualStyleBackColor = false;
            Publish.Click += Publish_Click;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(237, 231, 227);
            button1.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(440, 136);
            button1.Name = "button1";
            button1.Size = new Size(171, 41);
            button1.TabIndex = 29;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // addAnnouncement
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 189);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(Publish);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Name = "addAnnouncement";
            Text = "addAnnouncement";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private TextBox textBox1;
        private Button Publish;
        private Label label1;
        private Button button1;
    }
}