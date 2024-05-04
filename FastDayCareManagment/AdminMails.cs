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
    public partial class AdminMails : Form
    {
        public AdminMails()
        {
            InitializeComponent();
            PopulateReceivedMailsGrid();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void PopulateReceivedMailsGrid()
        {
            AdminController adminController = new AdminController();
            RecievedMails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RecievedMails.DataSource = adminController.GetReceivedMails();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            StaffAdmin staffAdmin = new StaffAdmin();
            staffAdmin.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChildrenAdmin childrenAdmin = new ChildrenAdmin();
            childrenAdmin.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnrollmentAdmin enrollmentAdmin = new EnrollmentAdmin();
            enrollmentAdmin.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdminAnnouncments adminAnnouncments = new AdminAnnouncments();
            adminAnnouncments.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDashboard1 adminDashboard1 = new AdminDashboard1();
            adminDashboard1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminMailsSent adminMailsSent = new AdminMailsSent();
            adminMailsSent.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to go back to the login screen?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ComposeEmailAdmin composeEmailAdmin = new ComposeEmailAdmin();
            composeEmailAdmin.Show();

            PopulateReceivedMailsGrid();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdminProfile adminProfile = new AdminProfile();
            adminProfile.Show();
            this.Hide();
        }
    }
}
