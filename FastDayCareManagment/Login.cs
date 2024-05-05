using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FastDayCareManagment
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            LoginController loginController = new LoginController();
            string role = loginController.GetUserRole(email, password);
            switch (role)
            {
                case "Admin":
                    AdminController adminController = new AdminController();
                    adminController.LogLogin(email);
                    AdminDashboard1 adminDashboard = new AdminDashboard1();
                    adminDashboard.Show();
                    this.Hide();
                    break;
                case "Staff":
                    StaffController staffController = new StaffController();
                    staffController.LogLogin(email);
                    StaffDash staffDashboard = new StaffDash();
                    staffDashboard.Show();
                    this.Hide();
                    break;
                case "Parent":
                    ParentController parentController = new ParentController();
                    parentController.LogLogin(email);
                    ParentDashboard parentDashboard = new ParentDashboard();
                    parentDashboard.Show();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Invalid email or password. Please try again.");
                    break;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
