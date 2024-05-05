using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public partial class ParentEnrollment : Form
    {
        public ParentEnrollment()
        {
            InitializeComponent();
            LoadEnrolledChildren();
            LoadPendingEnrollments();
            LoadRejectedEnrollments();
        }

        private void LoadEnrolledChildren()
        {
            ParentController parentController = new ParentController();
            Announcments.DataSource = parentController.GetEnrolledChildren();
        }

        private void LoadPendingEnrollments()
        {
            ParentController parentController = new ParentController();
            dataGridView1.DataSource = parentController.GetPendingEnrollments();
        }

        private void LoadRejectedEnrollments()
        {
            ParentController parentController = new ParentController();
            dataGridView2.DataSource = parentController.GetRejectedEnrollments();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ParentEnrolmentReq parentEnrolmentReq = new ParentEnrolmentReq();
            parentEnrolmentReq.Show();

            LoadPendingEnrollments();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParentDashboard parentDashboard = new ParentDashboard();
            parentDashboard.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParentAnnouncements parentAnnouncements = new ParentAnnouncements();
            parentAnnouncements.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ParentMail parentMail = new ParentMail();
            parentMail.Show();
            this.Hide();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ParentProfile parentProfile = new ParentProfile();
            parentProfile.Show();
            this.Hide();
        }
    }
}
