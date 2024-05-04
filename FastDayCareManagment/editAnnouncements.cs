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
    public partial class editAnnouncements : Form
    {
        private readonly int announcementID;

        public editAnnouncements(int announcementID, string currentMessage)
        {
            InitializeComponent();
            this.announcementID = announcementID;
            textBox1.Text = currentMessage;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Publish_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update this announcement?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AdminController adminController = new AdminController();
                bool success = adminController.UpdateAnnouncement(announcementID, textBox1.Text);
                if (success)
                {
                    MessageBox.Show("Announcement updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update announcement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
