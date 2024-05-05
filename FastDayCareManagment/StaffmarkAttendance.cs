using System;
using System.Data;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    partial class StaffmarkAttendance : Form
    {
        private StaffController staffController;

        public StaffmarkAttendance()
        {
            InitializeComponent();
            staffController = new StaffController();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadStudentsForAttendance();
            Announcments.CellContentClick += Announcments_CellContentClick;

        }

        private void Announcments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the click is on the checkbox column
                if (Announcments.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)Announcments.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    // Check if the cell value is not null
                    if (cell.Value != null)
                    {
                        bool isPresent = (bool)cell.Value;
                        // Get the enrollment ID from the corresponding row
                        int enrollmentID = Convert.ToInt32(Announcments.Rows[e.RowIndex].Cells["ChildID"].Value);
                        // Get the selected date from the DateTimePicker
                        DateTime selectedDate = dateTimePicker1.Value.Date;

                        // Update the attendance record in the database
                        bool success = staffController.UpdateAttendance(enrollmentID, selectedDate, isPresent);

                        if (success)
                        {
                            MessageBox.Show("Attendance updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update attendance.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating attendance: " + ex.Message);
            }
        }

        private void LoadStudentsForAttendance()
        {
            try
            {
                // Get the selected date from the datetimepicker
                DateTime selectedDate = dateTimePicker1.Value.Date;

                // Load students for attendance using the StaffController instance
                DataTable studentTable = staffController.StudentsAttendance(selectedDate);

                if (studentTable != null && studentTable.Rows.Count > 0)
                {
                    // Clear existing columns
                    Announcments.Columns.Clear();

                    // Bind the DataTable to the DataGridView for display
                    Announcments.DataSource = studentTable;

                    // Add a checkbox column to the DataGridView for marking attendance
                    DataGridViewCheckBoxColumn attendanceColumn = new DataGridViewCheckBoxColumn();
                    attendanceColumn.HeaderText = "Attendance";
                    attendanceColumn.Name = "Attendance";
                    attendanceColumn.TrueValue = true;
                    attendanceColumn.FalseValue = false;
                    Announcments.AutoGenerateColumns = false;
                    Announcments.Columns.Add(attendanceColumn);

                    // Add an AttendanceStatus column to the DataTable
                    studentTable.Columns.Add("AttendanceStatus", typeof(string));

                    // Make DataGridView editable
                    Announcments.ReadOnly = false;

                    // Set all checkboxes to checked by default
                    foreach (DataGridViewRow row in Announcments.Rows)
                    {
                        row.Cells["Attendance"].Value = true;
                    }
                }
                else
                {
                    MessageBox.Show("No students found for the selected class and date.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students for attendance: " + ex.Message);
            }
        }

        private void StaffmarkAttendance_Load(object sender, EventArgs e)
        {

        }
    }
}
