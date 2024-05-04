using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FastDayCareManagment
{
    public partial class AdminDashboard1 : Form
    {
        private string connectionString = "Data Source=EI;Initial Catalog=projectDB;Integrated Security=True;";
        public AdminDashboard1()
        {
            InitializeComponent();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadUpcomingBirthdays();
            LoadUnpaidFeeChildren();
            LoadAnnouncements();
            label5_Click(sender, e);
        }

        private void LoadUnpaidFeeChildren()
        {
            AdminController adminController = new AdminController();
            DataTable dataTable = adminController.GetChildrenWithUnpaidFees();
            dataGridView2.DataSource = dataTable;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }


        private void LoadUpcomingBirthdays()
        {
            AdminController adminController = new AdminController();
            DataTable dataTable = adminController.getUpcomingBirthdays();
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void LoadAnnouncements()
        {
            AdminController adminController = new AdminController();
            DataTable dataTable = adminController.GetAnnouncements();
            dataGridView3.DataSource = dataTable;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            dataGridView3.Columns["DateTime"].Width = 100;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to go back to the login screen?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
            else
            {
                // User clicked 'No', do nothing or handle accordingly
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {
            AdminController adminController = new AdminController();
            decimal revenue = adminController.GetPreviousMonthProfit();
            string revenueText = revenue > 999 ? (revenue / 1000).ToString("0.#") + "K" : revenue.ToString();
            label5.Text = "$" + revenueText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminAnnouncments adminAnnouncments = new AdminAnnouncments();
            adminAnnouncments.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnrollmentAdmin enrollmentAdmin = new EnrollmentAdmin();
            enrollmentAdmin.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChildrenAdmin childrenAdmin = new ChildrenAdmin();
            childrenAdmin.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            StaffAdmin staffAdmin = new StaffAdmin();
            staffAdmin.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AdminMails adminMails = new AdminMails();
            adminMails.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdminProfile adminProfile = new AdminProfile();
            adminProfile.Show();
            this.Hide();
        }
    }
}