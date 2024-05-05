using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace FastDayCareManagment
{
    internal class StaffController
    {
        private string connectionString = "Data Source = EI; Initial Catalog = projectDB; Integrated Security = True;";

        public void LogLogin(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Logs (LogDateTime, Email) VALUES (@LogDateTime, @Email)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LogDateTime", DateTime.Now);
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                MessageBox.Show("An error occurred while logging the login: " + ex.Message);
            }
        }

        public DataTable getUpcomingBirthdays()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DateTime currentDate = DateTime.Today;
                DateTime endDate = currentDate.AddMonths(5);
                string query = @"
                                DECLARE @CurrentDate DATE = @CurrentDateParam;
                                DECLARE @EndDate DATE = DATEADD(MONTH, 5, @CurrentDate);

                                SELECT [Name], DateOfBirth,
                                       DATEDIFF(YEAR, DateOfBirth, @CurrentDate) - 
                                       CASE
                                           WHEN MONTH(DateOfBirth) > MONTH(@CurrentDate) OR 
                                                (MONTH(DateOfBirth) = MONTH(@CurrentDate) AND DAY(DateOfBirth) > DAY(@CurrentDate)) 
                                           THEN 1
                                           ELSE 0
                                       END AS Age,
                                       DATEDIFF(DAY, @CurrentDate, DATEFROMPARTS(YEAR(@CurrentDate), MONTH(DateOfBirth), DAY(DateOfBirth))) AS DaysLeft
                                FROM Child
                                WHERE 
                                    (MONTH(DateOfBirth) > MONTH(@CurrentDate) OR (MONTH(DateOfBirth) = MONTH(@CurrentDate) AND DAY(DateOfBirth) >= DAY(@CurrentDate)))
                                    AND (MONTH(DateOfBirth) <= MONTH(@EndDate) OR (MONTH(DateOfBirth) = MONTH(@EndDate) AND DAY(DateOfBirth) <= DAY(@EndDate)))
                                ORDER BY MONTH(DateOfBirth), DAY(DateOfBirth)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentDateParam", currentDate);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            row["Age"] = Convert.ToInt32(row["Age"]);
                            row["DaysLeft"] = Convert.ToInt32(row["DaysLeft"]);
                            row["DateOfBirth"] = ((DateTime)row["DateOfBirth"]).ToString("MM-dd");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetAnnouncements()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                                    SELECT
                                        a.[DateTime], 
                                        CONCAT('An announcement made by ', ad.[Name], ': ', a.[Message]) AS Announcement
                                    FROM 
                                        Announcement a
                                    JOIN 
                                        Administrator ad ON a.AdministratorID = ad.AdministratorID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        dataTable.Load(reader);
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable getStaffSchedule()
        {
            // Get the last logged-in staff member's email
            string userEmail = GetLastLoggedInStaffEmail();

            if (!string.IsNullOrEmpty(userEmail))
            {
                try
                {
                    // Query the Schedule table for the staff member's schedule
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"
                    SELECT S.*
                    FROM Schedule S
                    INNER JOIN assignedSchedules AS ASch ON S.ScheduleID = ASch.ScheduleID
                    INNER JOIN StaffMember SM ON ASch.StaffID = SM.StaffID
                    WHERE SM.Email = @Email";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Email", userEmail);
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable scheduleTable = new DataTable();
                            adapter.Fill(scheduleTable);

                            // Bind the schedule data to DataGridView or any other control
                            return scheduleTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No logged-in staff member found.");
            }
            return null;
        }


        private string GetLastLoggedInStaffEmail()
        {
            string lastLoggedInEmail = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT TOP 1 Staff.Email FROM Logs INNER JOIN StaffMember Staff ON Logs.Email = Staff.Email ORDER BY Logs.LogDateTime DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        lastLoggedInEmail = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return lastLoggedInEmail;
        }

        public float CalculateAttendancePercentage()
        {
            string userEmail = GetLastLoggedInStaffEmail();
            int staffId = GetStaffIdByEmail(userEmail); // Get the StaffID corresponding to the email

            float averageAttendancePercentage = 0.0f;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
            SELECT 
                AVG(AttendancePercentage) AS AverageAttendancePercentage
            FROM
                (SELECT 
                    StaffID,
                    SUM(CASE WHEN [Status] = 'Present' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS AttendancePercentage
                FROM 
                    StaffAttendance
                WHERE 
                    StaffID = @StaffID
                GROUP BY 
                    StaffID) AS IndividualAttendancePercentage";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@StaffID", staffId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                averageAttendancePercentage = Convert.ToSingle(reader["AverageAttendancePercentage"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error calculating average attendance percentage: " + ex.Message);
            }

            return averageAttendancePercentage;
        }

        private int GetStaffIdByEmail(string email)
        {
            int staffId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT StaffID FROM StaffMember WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Email", email);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            staffId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error retrieving StaffID: " + ex.Message);
            }
            return staffId;
        }

        public DataTable StudentsAttendance( DateTime selectedDate)
        {
            try
            {
                // Query to retrieve students registered in the selected class
                string query = @"
                    SELECT C.ChildID, C.Name AS StudentName
                    FROM Child C
                    INNER JOIN Enrollment E ON C.ChildID = E.ChildID
                    WHERE @SelectedDate BETWEEN E.StartDate AND E.EndDate
                    AND E.Status NOT IN ('Waiting', 'UnEnrolled', 'Rejected')";

                // Create a DataTable to store the results
                DataTable studentTable = new DataTable();

                // Open a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@SelectedDate", selectedDate);

                        // Create a SqlDataAdapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        // Fill the DataTable with the results
                        adapter.Fill(studentTable);
                    }
                }

                return studentTable;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error loading students for attendance: " + ex.Message);
                return null;
            }
        }

        public bool UpdateAttendance(int enrollmentID, DateTime selectedDate, bool isPresent)
        {
            try
            {
                // Convert boolean value to "Present" or "Absent" string
                string attendanceStatus = isPresent ? "Present" : "Absent";

                // Check if there is already an attendance record for the given enrollmentID and date
                bool recordExists = CheckAttendanceRecordExists(enrollmentID, selectedDate);

                // Construct the SQL query
                string query = string.Empty;

                if (recordExists)
                {
                    // Update the existing attendance record
                    query = @"
                            UPDATE AttendanceRecord
                            SET [Status] = @Status
                            WHERE EnrollmentID = @EnrollmentID AND [Date] = @Date";
                }
                else
                {
                    // Insert a new attendance record
                    query = @"
                            INSERT INTO AttendanceRecord (EnrollmentID, [Date], [Status])
                            VALUES (@EnrollmentID, @Date, @Status)";
                }

                // Open a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@EnrollmentID", enrollmentID);
                        command.Parameters.AddWithValue("@Date", selectedDate);
                        command.Parameters.AddWithValue("@Status", attendanceStatus);

                        // Execute the SQL command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            return true; // Attendance record updated successfully
                        }
                        else
                        {
                            return false; // No rows were affected
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine("Error updating attendance: " + ex.Message);
                return false; // Failed to update attendance record
            }
        }

        private bool CheckAttendanceRecordExists(int enrollmentID, DateTime selectedDate)
        {
            // Construct the SQL query to check if an attendance record exists
            string query = @"
                            SELECT COUNT(*) FROM AttendanceRecord 
                            WHERE EnrollmentID = @EnrollmentID AND [Date] = @Date";

            // Open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand object with the query and connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@EnrollmentID", enrollmentID);
                    command.Parameters.AddWithValue("@Date", selectedDate);

                    // Execute the SQL command and get the count
                    int count = (int)command.ExecuteScalar();

                    // If count > 0, record exists; otherwise, it doesn't
                    return count > 0;
                }
            }
        }

        public DataTable GetStudentsByClassSection(string className)
        {
            try
            {
                string query = @"
                                SELECT 
                                    Child.ChildID,  
                                    Child.Name,  
                                    Child.DateOfBirth, 
                                    ISNULL(
                                        (
                                            SELECT 
                                                SUM(CASE WHEN AttendanceRecord.Status = 'Present' THEN 1 ELSE 0 END) 
                                            FROM 
                                                AttendanceRecord 
                                            WHERE 
                                                AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                                        ), 0
                                    ) AS PresentCount, 
                                    ISNULL(
                                        (
                                            SELECT 
                                                COUNT(*) 
                                            FROM 
                                                AttendanceRecord 
                                            WHERE 
                                                AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                                        ), 0
                                    ) AS TotalAttendanceCount, 
                                    CASE 
                                        WHEN ISNULL(
                                            (
                                                SELECT 
                                                    COUNT(*) 
                                                FROM 
                                                    AttendanceRecord 
                                                WHERE 
                                                    AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                                            ), 0
                                        ) > 0 
                                        THEN 
                                            CAST(
                                                (
                                                    ISNULL(
                                                        (
                                                            SELECT 
                                                                SUM(CASE WHEN AttendanceRecord.Status = 'Present' THEN 1 ELSE 0 END) 
                                                            FROM 
                                                                AttendanceRecord 
                                                            WHERE 
                                                                AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                                                        ), 0
                                                    ) * 100.0 / 
                                                    (
                                                        SELECT 
                                                            COUNT(*) 
                                                        FROM 
                                                            AttendanceRecord 
                                                        WHERE 
                                                            AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                                                    )
                                                ) AS DECIMAL(5, 2)
                                            ) 
                                        ELSE 
                                            0 
                                    END AS AttendancePercentage 
                                FROM 
                                    Child 
                                INNER JOIN 
                                    Enrollment ON Child.ChildID = Enrollment.ChildID 
                                WHERE 
                                    Enrollment.ClassroomID IN 
                                        (
                                            SELECT 
                                                ClassroomID 
                                            FROM 
                                                Classroom 
                                            WHERE 
                                                [Name] = @className
                                        )
                            ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Use @className as the parameter name
                        command.Parameters.AddWithValue("@className", className);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable studentTable = new DataTable();
                            adapter.Fill(studentTable);
                            return studentTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching students by class section: " + ex.Message);
                return null;
            }
        }

    }
}


