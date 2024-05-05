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
    public partial class ParentMail : Form
    {
        public ParentMail()
        {
            InitializeComponent();
            loadRecievedMails();
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
        }

        private void loadRecievedMails()
        {
            ParentController parentController = new ParentController();
            DataTable dataTable = parentController.GetReceivedMails();
            RecievedMails.DataSource = dataTable;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ParentComposeMail parentComposeMail = new ParentComposeMail();
            parentComposeMail.Show();

            loadRecievedMails();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ParentMailSent parentMailSent = new ParentMailSent();
            parentMailSent.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParentEnrollment parentEnrollment = new ParentEnrollment();
            parentEnrollment.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParentAnnouncements parentAnnouncements = new ParentAnnouncements();
            parentAnnouncements.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParentDashboard parentDashboard = new ParentDashboard();
            parentDashboard.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ParentProfile parentProfile = new ParentProfile();
            parentProfile.Show();
            this.Hide();
        }
    }
}
