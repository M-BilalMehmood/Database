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
    public partial class StaffMail : Form
    {
        public StaffMail()
        {
            InitializeComponent();
            loadRecievedMails();
        }

        private void loadRecievedMails()
        {
            StaffController staffController = new StaffController();
            DataTable dataTable = staffController.GetReceivedMails();
            RecievedMails.DataSource = dataTable;
            RecievedMails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RecievedMails.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StaffMailSent staffMailSent = new StaffMailSent();
            staffMailSent.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StaffComposeMail staffComposeMail = new StaffComposeMail();
            staffComposeMail.Show();

            loadRecievedMails();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            StaffClassInfo staffClassInfo = new StaffClassInfo();
            staffClassInfo.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StaffmarkAttendance staffmarkAttendance = new StaffmarkAttendance();
            staffmarkAttendance.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffAnnouncements staffAnnouncements = new StaffAnnouncements();
            staffAnnouncements.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffDash staffDash = new StaffDash();
            staffDash.Show();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StaffProfile staffProfile = new StaffProfile();
            staffProfile.Show();
            this.Hide();
        }
    }
}
