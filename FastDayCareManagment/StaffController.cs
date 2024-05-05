using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                        a.AnnouncementID,
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

    }
}


