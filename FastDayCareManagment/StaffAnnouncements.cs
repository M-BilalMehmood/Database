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
    }
}
