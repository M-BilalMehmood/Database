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
    public partial class EnrollmentAdmin : Form
    {
        public EnrollmentAdmin()
        {
            InitializeComponent();
            LoadActiveEnrollmentChildren();
            LoadWaitingChildren();
            LoadUnEnrollmentOrRejectedChildren();
        }

        private void ReloadData()
        {
            LoadActiveEnrollmentChildren();
            LoadWaitingChildren();
            LoadUnEnrollmentOrRejectedChildren();
        }

        private void LoadActiveEnrollmentChildren()
        {
            try
            {
                AdminController adminController = new AdminController();
                DataTable dataTable = adminController.GetActiveEnrollmentChildren();
                activeChildren.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                activeChildren.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading active enrollment children: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUnEnrollmentOrRejectedChildren()
        {
            try
            {
                AdminController adminController = new AdminController();
                DataTable dataTable = adminController.GetUnEnrollmentOrRejectedChildren();
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading active enrollment children: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
        private void LoadWaitingChildren()
        {
            try
            {
                AdminController adminController = new AdminController();
                DataTable dataTable = adminController.GetWaitingEnrollmentChildren();

                dataGridView1.DataSource = dataTable;

                // Add DataGridViewComboBoxColumn for status
                DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading waiting enrollment children: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Status"].Index)
            {
                DataGridViewComboBoxCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                string status = cell.Value.ToString();
                int childID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ChildID"].Value);

                AdminController adminController = new AdminController();
                bool success = adminController.UpdateChildEnrollmentStatus(childID, status);
                if (!success)
                {
                    MessageBox.Show("Failed to update child enrollment status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Approve button clicked
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected child's ID from the DataGridView
                int childID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ChildID"].Value);

                // Update the database to set the status as "Approved"
                AdminController adminController = new AdminController();
                bool success = adminController.UpdateChildEnrollmentStatus(childID, "Approved");
                if (success)
                {
                    // Refresh the DataGridView to reflect the changes
                    LoadWaitingChildren();
                }
                else
                {
                    MessageBox.Show("Failed to approve enrollment request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a student to approve.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ReloadData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Reject button clicked
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected child's ID from the DataGridView
                int childID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ChildID"].Value);

                // Update the database to set the status as "Rejected"
                AdminController adminController = new AdminController();
                bool success = adminController.UpdateChildEnrollmentStatus(childID, "Rejected");
                if (success)
                {
                    // Refresh the DataGridView to reflect the changes
                    LoadWaitingChildren();
                }
                else
                {
                    MessageBox.Show("Failed to reject enrollment request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a student to reject.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ReloadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Unenroll button clicked
            if (activeChildren.SelectedRows.Count > 0)
            {
                // Get the selected child's ID from the DataGridView
                int childID = Convert.ToInt32(activeChildren.SelectedRows[0].Cells["ChildID"].Value);

                // Update the database to set the status as "Unenrolled" or remove the child from the active enrollment
                AdminController adminController = new AdminController();
                bool success = adminController.UnenrollChild(childID);
                if (success)
                {
                    // Refresh the DataGridView to reflect the changes
                    LoadActiveEnrollmentChildren();
                }
                else
                {
                    MessageBox.Show("Failed to unenroll the student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a student to unenroll.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ReloadData();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AdminMails adminMails = new AdminMails();
            adminMails.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdminAnnouncments adminAnnouncments = new AdminAnnouncments();
            adminAnnouncments.Show();
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
            AdminProfile adminProfile = new AdminProfile();
            adminProfile.Show();
            this.Hide();
        }
    }
}
