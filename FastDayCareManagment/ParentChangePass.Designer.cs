namespace FastDayCareManagment
{
    partial class ParentChangePass
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
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(357, 180);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(29, 20);
            checkBox3.TabIndex = 78;
            checkBox3.Text = " ";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(357, 114);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(29, 20);
            checkBox2.TabIndex = 77;
            checkBox2.Text = " ";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(357, 52);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(29, 20);
            checkBox1.TabIndex = 76;
            checkBox1.Text = " ";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 166, 43);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(95, 218);
            button1.Name = "button1";
            button1.Size = new Size(195, 31);
            button1.TabIndex = 75;
            button1.Text = "Change Password";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(31, 151);
            label3.Name = "label3";
            label3.Size = new Size(176, 22);
            label3.TabIndex = 74;
            label3.Text = "Confirm Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(31, 86);
            label2.Name = "label2";
            label2.Size = new Size(147, 22);
            label2.TabIndex = 73;
            label2.Text = "New Password:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(31, 22);
            label1.Name = "label1";
            label1.Size = new Size(138, 22);
            label1.TabIndex = 72;
            label1.Text = "Old Password:";
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(31, 176);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(315, 26);
            textBox3.TabIndex = 71;
            textBox3.UseSystemPasswordChar = true;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(31, 111);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(315, 26);
            textBox2.TabIndex = 70;
            textBox2.UseSystemPasswordChar = true;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(31, 47);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(315, 26);
            textBox1.TabIndex = 69;
            textBox1.UseSystemPasswordChar = true;
            // 
            // ParentChangePass
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(416, 271);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "ParentChangePass";
            Text = "ParentChangePass";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button button1;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
    }
}