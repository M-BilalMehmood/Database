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
    public partial class StaffDash : Form
    {
        public StaffDash()
        {
            InitializeComponent();
            LoadUpcomingBirthdays();
            LoadAnnouncements();
            LoadStaffSchedule();
            UpdateProgressBar();
        }

        private void StaffDash_Load(object sender, EventArgs e)
        {
        }

        private void LoadUpcomingBirthdays()
        {
            StaffController staffController = new StaffController();
            DataTable dataTable = staffController.getUpcomingBirthdays();
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void LoadAnnouncements()
        {
            StaffController staffController = new StaffController();
            DataTable dataTable = staffController.GetAnnouncements();
            dataGridView3.DataSource = dataTable;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void LoadStaffSchedule()
        {
            StaffController staffController = new StaffController();
            DataTable dataTable = staffController.getStaffSchedule();
            dataGridView2.DataSource = dataTable;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void UpdateProgressBar()
        {
            StaffController staffController = new StaffController();
            float attendancePercentage = staffController.CalculateAttendancePercentage();
            progressBar1.Value = (int)attendancePercentage;
            label5.Text = $"{attendancePercentage}%";
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

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

        private void button2_Click(object sender, EventArgs e)
        {
            StaffAnnouncements staffAnnouncements = new StaffAnnouncements();
            staffAnnouncements.Show();
            this.Hide();
        }
    }
}
