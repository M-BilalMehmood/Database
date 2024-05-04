using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public partial class addAnnouncement : Form
    {
        public addAnnouncement()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Publish_Click(object sender, EventArgs e)
        {
            string announcementMessage = textBox1.Text;
            DateTime currentDateTime = DateTime.Now;

            AdminController adminController = new AdminController();
            bool announcementAdded = adminController.AddAnnouncement(announcementMessage, currentDateTime);

            if (announcementAdded)
            {
                MessageBox.Show("Announcement added successfully.");
            }
            else
            {
                MessageBox.Show("Failed to add announcement.");
            }
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
