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
    public partial class ParentDashboard : Form
    {
        public ParentDashboard()
        {
            InitializeComponent();
            LoadAnnouncements();
            LoadUpcomingBirthdays();
            LoadChildTimeTable();
            LoadChildrenFee();
        }

        private void LoadAnnouncements()
        {
            ParentController parentController = new ParentController();
            DataTable dataTable = parentController.GetAnnouncements();
            dataGridView3.DataSource = dataTable;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void LoadChildTimeTable()
        {
            ParentController parentController = new ParentController();
            DataTable dataTable = parentController.GetChildTimeTable();
            dataGridView2.DataSource = dataTable;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void LoadUpcomingBirthdays()
        {
            ParentController parentController = new ParentController();
            DataTable dataTable = parentController.getUpcomingBirthdays();
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Century Gothic", 10, FontStyle.Regular);
        }

        private void LoadChildrenFee()
        {
            ParentController parentController = new ParentController();
            DataTable dataTable = parentController.GetChildrenFeeHistory();
            dataGridView4.DataSource = dataTable;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.Font = new Font("Century Gothic", 10, FontStyle.Regular);
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

        private void button2_Click(object sender, EventArgs e)
        {
            ParentAnnouncements parentAnnouncements = new ParentAnnouncements();
            parentAnnouncements.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParentEnrollment parentEnrollment = new ParentEnrollment();
            parentEnrollment.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ParentMail parentMail = new ParentMail();
            parentMail.Show();
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
