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
    public partial class ParentAnnouncements : Form
    {
        public ParentAnnouncements()
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

        private void button3_Click(object sender, EventArgs e)
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
        }
    }
}
