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
    public partial class StaffClassInfo : Form
    {
        public StaffClassInfo()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected class section from the combo box
                string className = comboBox1.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(className))
                {
                    // Retrieve students based on the selected class section
                    StaffController staffController = new StaffController();
                    DataTable studentTable = staffController.GetStudentsByClassSection(className);

                    if (studentTable != null && studentTable.Rows.Count > 0)
                    {
                        // Clear existing columns
                        Announcments.Columns.Clear();
                        Announcments.DataSource = studentTable;

                        Announcments.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("No students found for the selected class section.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students for the selected class section: " + ex.Message);
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StaffmarkAttendance staffmarkAttendance = new StaffmarkAttendance();
            staffmarkAttendance.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffAnnouncements staffAnnouncements = new StaffAnnouncements();
            staffAnnouncements.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffDash staffDash = new StaffDash();
            staffDash.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            StaffMail staffMail = new StaffMail();
            staffMail.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StaffProfile staffProfile = new StaffProfile();
            staffProfile.Show();
            this.Hide();
        }
    }
}
