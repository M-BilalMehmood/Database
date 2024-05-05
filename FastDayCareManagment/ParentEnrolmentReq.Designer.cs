namespace FastDayCareManagment
{
    partial class ParentEnrolmentReq
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
            button2 = new Button();
            button1 = new Button();
            label2 = new Label();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker3 = new DateTimePicker();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(22, 105, 122);
            label3.Location = new Point(26, 28);
            label3.Name = "label3";
            label3.Size = new Size(368, 44);
            label3.TabIndex = 55;
            label3.Text = "Enrollment Request:";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(237, 231, 227);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(137, 250);
            button2.Name = "button2";
            button2.Size = new Size(216, 39);
            button2.TabIndex = 54;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(244, 202, 68);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(369, 250);
            button1.Name = "button1";
            button1.Size = new Size(216, 39);
            button1.TabIndex = 53;
            button1.Text = "Register";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(34, 170);
            label2.Name = "label2";
            label2.Size = new Size(92, 21);
            label2.TabIndex = 52;
            label2.Text = "Start Time:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F);
            label6.Location = new Point(369, 170);
            label6.Name = "label6";
            label6.Size = new Size(84, 21);
            label6.TabIndex = 50;
            label6.Text = "End Time:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F);
            label5.Location = new Point(369, 93);
            label5.Name = "label5";
            label5.Size = new Size(111, 21);
            label5.TabIndex = 48;
            label5.Text = "Date of Birth:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F);
            label1.Location = new Point(34, 93);
            label1.Name = "label1";
            label1.Size = new Size(62, 21);
            label1.TabIndex = 46;
            label1.Text = "Name:";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 11.25F);
            textBox1.Location = new Point(34, 117);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(319, 27);
            textBox1.TabIndex = 45;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Century Gothic", 12F);
            dateTimePicker1.Location = new Point(34, 194);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(319, 27);
            dateTimePicker1.TabIndex = 56;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Font = new Font("Century Gothic", 12F);
            dateTimePicker2.Location = new Point(369, 194);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(319, 27);
            dateTimePicker2.TabIndex = 57;
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker3.Location = new Point(369, 117);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(319, 27);
            dateTimePicker3.TabIndex = 58;
            // 
            // ParentEnrolmentReq
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 320);
            Controls.Add(dateTimePicker3);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "ParentEnrolmentReq";
            Text = "ParentEnrolmentReq";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Button button2;
        private Button button1;
        private Label label2;
        private Label label6;
        private Label label5;
        private Label label1;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker3;
    }
}