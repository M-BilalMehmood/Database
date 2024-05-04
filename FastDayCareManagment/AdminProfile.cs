using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FastDayCareManagment.AdminController;

namespace FastDayCareManagment
{
    public partial class AdminProfile : Form
    {
        public AdminProfile()
        {
            InitializeComponent();
            DisplayAdminDetails();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void DisplayAdminDetails()
        {
            AdminController controller = new AdminController();
            AdminDetails admin = controller.GetLatestAdminDetails();
            if (admin != null)
            {
                label1.Text = admin.Name;
                label2.Text = admin.Email;
            }
            else
            {
                MessageBox.Show("Admin details not found.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminDashboard1 adminDashboard1 = new AdminDashboard1();
            adminDashboard1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminChangePass adminChangePass = new AdminChangePass();
            adminChangePass.Show();
        }
    }
}
