namespace FastDayCareManagment
{
    partial class ComposeEmailAdmin
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
            label1 = new Label();
            button6 = new Button();
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Century Gothic", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(255, 166, 43);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(391, 56);
            label1.TabIndex = 60;
            label1.Text = "Compose Email";
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(255, 166, 43);
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 192, 204);
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.Location = new Point(495, 472);
            button6.Name = "button6";
            button6.Size = new Size(139, 36);
            button6.TabIndex = 65;
            button6.Text = "Send";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(12, 116);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(623, 27);
            textBox1.TabIndex = 66;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(12, 177);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(623, 289);
            richTextBox1.TabIndex = 67;
            richTextBox1.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(72, 159, 181);
            label2.Location = new Point(12, 150);
            label2.Name = "label2";
            label2.Size = new Size(106, 24);
            label2.TabIndex = 68;
            label2.Text = "Message:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(72, 159, 181);
            label3.Location = new Point(12, 89);
            label3.Name = "label3";
            label3.Size = new Size(69, 24);
            label3.TabIndex = 69;
            label3.Text = "Email:";
            // 
            // ComposeEmailAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(647, 520);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(richTextBox1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(button6);
            Name = "ComposeEmailAdmin";
            Text = "ComposeEmailAdmin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button button6;
        private TextBox textBox1;
        private RichTextBox richTextBox1;
        private Label label2;
        private Label label3;
    }
}