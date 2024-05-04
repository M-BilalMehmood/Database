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
    public partial class StaffAdmin : Form
    {
        public StaffAdmin()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                AdminController adminController = new AdminController();
                DataTable dataTable = adminController.getStaffData();
                activeChildren.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                activeChildren.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void activeChildren_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChildrenAdmin childrenAdmin = new ChildrenAdmin();
            childrenAdmin.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnrollmentAdmin enrollmentAdmin = new EnrollmentAdmin();
            enrollmentAdmin.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdminAnnouncments adminAnnouncments = new AdminAnnouncments();
            adminAnnouncments.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDashboard1 adminDashboard1 = new AdminDashboard1();
            adminDashboard1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addStaffMember addStaffMember = new addStaffMember();
            addStaffMember.ShowDialog();

            LoadData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (activeChildren.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a staff member to remove.");
                return;
            }
            int staffID = Convert.ToInt32(activeChildren.SelectedRows[0].Cells["StaffID"].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to remove this staff member?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    AdminController adminController = new AdminController();
                    adminController.RemoveStaffMember(staffID);
                    LoadData();
                    MessageBox.Show("Staff member removed successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (activeChildren.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a staff member to edit.");
                return;
            }

            int staffID = Convert.ToInt32(activeChildren.SelectedRows[0].Cells["StaffID"].Value);
            editStaffAdmin editForm = new editStaffAdmin(staffID);
            editForm.ShowDialog();
            LoadData(); // Refresh the data after editing
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
