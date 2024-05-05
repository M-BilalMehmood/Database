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
    public partial class StaffAnnouncements : Form
    {
        public StaffAnnouncements()
        {
            InitializeComponent();
            LoadAnnouncements();
        }

        private void LoadAnnouncements()
        {
            StaffController staffController = new StaffController();
            DataTable dataTable = staffController.GetAnnouncements();
            Announcments.DataSource = dataTable;
            Announcments.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            Announcments.Columns["DateTime"].Width = 200;
            Announcments.Columns["Announcement"].Width = 800;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffDash staffDash = new StaffDash();
            staffDash.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StaffmarkAttendance staffmarkAttendance = new StaffmarkAttendance();
            staffmarkAttendance.Show();
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

        private void button9_Click(object sender, EventArgs e)
        {
            StaffClassInfo staffClassInfo = new StaffClassInfo();
            staffClassInfo.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            StaffMail staffMail = new StaffMail();
            staffMail.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StaffProfile staffProfile = new StaffProfile();
            staffProfile.Show();
            this.Hide();
        }
    }
}
