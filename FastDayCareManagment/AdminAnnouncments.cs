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
    public partial class AdminAnnouncments : Form
    {
        public AdminAnnouncments()
        {
            InitializeComponent();
            LoadAnnouncements();
        }

        private void LoadAnnouncements()
        {
            AdminController adminController = new AdminController();
            DataTable dataTable = adminController.GetAnnouncements();
            Announcments.DataSource = dataTable;
            Announcments.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            Announcments.Columns["DateTime"].Width = 200;
            Announcments.Columns["AnnouncementID"].Visible = false;
            Announcments.Columns["Announcement"].Width = 800;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDashboard1 adminDashboard1 = new AdminDashboard1();
            adminDashboard1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            addAnnouncement addAnnouncementForm = new addAnnouncement();
            addAnnouncementForm.ShowDialog();
            LoadAnnouncements();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Announcments.SelectedRows.Count > 0)
            {
                int announcementID = Convert.ToInt32(Announcments.SelectedRows[0].Cells["AnnouncementID"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete the announcement?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    AdminController adminController = new AdminController();
                    bool success = adminController.DeleteAnnouncement(announcementID);
                    if (success)
                    {
                        MessageBox.Show("Announcement deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAnnouncements();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete announcement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an announcement to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (Announcments.SelectedRows.Count > 0)
            {
                int announcementID = Convert.ToInt32(Announcments.SelectedRows[0].Cells["AnnouncementID"].Value);
                string announcementMessage = Announcments.SelectedRows[0].Cells["Announcement"].Value.ToString();

                editAnnouncements editForm = new editAnnouncements(announcementID, announcementMessage);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadAnnouncements(); // Refresh the announcements after update
                }
            }
            else
            {
                MessageBox.Show("Please select an announcement to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            else
            {
                // User clicked 'No', do nothing or handle accordingly
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            EnrollmentAdmin enrollmentAdmin = new EnrollmentAdmin();
            enrollmentAdmin.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChildrenAdmin childrenAdmin = new ChildrenAdmin();
            childrenAdmin.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            StaffAdmin staffAdmin = new StaffAdmin();
            staffAdmin.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AdminMails adminMails = new AdminMails();
            adminMails.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdminProfile adminProfile = new AdminProfile();
            adminProfile.Show();
            this.Hide();
        }
    }
}
