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

    }
}
