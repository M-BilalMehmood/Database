using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public partial class ParentChangePass : Form
    {
        public ParentChangePass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldPassword = textBox1.Text;
            string newPassword = textBox2.Text;
            string confirmPassword = textBox3.Text;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            ParentController controller = new ParentController();
            bool success = controller.ChangeParentPassword(oldPassword, newPassword);

            if (success)
            {
                MessageBox.Show("Password changed successfully!");
            }
            else
            {
                MessageBox.Show("Failed to change password. Please check your old password.");
            }
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
        }
    }
}
