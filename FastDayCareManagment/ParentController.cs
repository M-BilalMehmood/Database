using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDayCareManagment
{
    internal class ParentController
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

        private int getCurrentUserID()
        {
            string currentUserEmail;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getEmailQuery = "SELECT TOP 1 p.Email FROM Logs INNER JOIN Parent p ON Logs.Email = p.Email ORDER BY Logs.LogDateTime DESC";
                using (SqlCommand getEmailCommand = new SqlCommand(getEmailQuery, connection))
                {
                    currentUserEmail = getEmailCommand.ExecuteScalar()?.ToString();
                }
            }
            int parentID;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getParentIDQuery = "SELECT ParentID FROM Parent WHERE Email = @Email";
                using (SqlCommand getParentIDCommand = new SqlCommand(getParentIDQuery, connection))
                {
                    getParentIDCommand.Parameters.AddWithValue("@Email", currentUserEmail);
                    object result = getParentIDCommand.ExecuteScalar();
                    parentID = (result == null || result == DBNull.Value) ? -1 : Convert.ToInt32(result);
                }
            }
            return parentID;
        }

        public DataTable GetChildrenFeeHistory()
        {
            try
            {
                int parentID = getCurrentUserID();
                string query = "SELECT * FROM ParentChildrenFees WHERE ParentID = @ParentID";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ParentID", parentID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable feeHistoryTable = new DataTable();
                            adapter.Fill(feeHistoryTable);
                            return feeHistoryTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving children fee history: " + ex.Message);
            }
        }

        public DataTable GetEnrolledChildren()
        {
            {
                try
                {
                    int parentID = getCurrentUserID();
                    DateTime currentDate = DateTime.Now;
                    DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1); // First day of the current month
                    DateTime endDate = startDate.AddMonths(1); // First day of next month

                    string query = @"
                                    SELECT *
                                    FROM ParentChildrenFees
                                    WHERE ParentID = @ParentID
                                    AND BillingDate >= @StartDate AND BillingDate < @EndDate";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ParentID", parentID);
                            command.Parameters.AddWithValue("@StartDate", startDate);
                            command.Parameters.AddWithValue("@EndDate", endDate);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable feeHistoryTable = new DataTable();
                                adapter.Fill(feeHistoryTable);
                                return feeHistoryTable;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    throw new Exception("Error retrieving children fee history: " + ex.Message);
                }
            }

        }

        private string loggedInUserEmail()
        {
            string currentUserEmail;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getEmailQuery = "SELECT TOP 1 p.Email FROM Logs INNER JOIN Parent p ON Logs.Email = p.Email ORDER BY Logs.LogDateTime DESC";
                using (SqlCommand getEmailCommand = new SqlCommand(getEmailQuery, connection))
                {
                    currentUserEmail = getEmailCommand.ExecuteScalar()?.ToString();
                }
            }
            return currentUserEmail;
        }

        public DataTable GetChildTimeTable()
        {
            try
            {
                int parentID = getCurrentUserID();
                string getChildTimeTableQuery = "SELECT * FROM ChildrenTimetable WHERE ChildID IN (SELECT ChildID FROM Child WHERE ParentID = @ParentID)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(getChildTimeTableQuery, connection);
                    command.Parameters.AddWithValue("@ParentID", parentID);
                    DataTable childTimeTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(childTimeTable);

                    return childTimeTable;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error getting child timetable: " + ex.Message);
            }
        }

        public DataTable GetReceivedMails()
        {
            DataTable receivedMailsTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT SendingDateTime, SenderEmail, [Message] FROM Mails WHERE ReceiverEmail = @ReceiverEmail";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ReceiverEmail", loggedInUserEmail());

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(receivedMailsTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return receivedMailsTable;
        }

        public DataTable GetSentMails()
        {
            DataTable sentMailsTable = new DataTable();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT SendingDateTime, ReceiverEmail, [Message] FROM Mails WHERE SenderEmail = @SenderEmail";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SenderEmail", loggedInUserEmail());

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(sentMailsTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return sentMailsTable;
        }

        public void SendMail(string receiverEmail, string message)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Mails (SenderEmail, ReceiverEmail, [Message], SendingDateTime) VALUES (@SenderEmail, @ReceiverEmail, @Message, @SendingDateTime)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SenderEmail", loggedInUserEmail());
                    command.Parameters.AddWithValue("@ReceiverEmail", receiverEmail);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@SendingDateTime", DateTime.Now);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Mail sent successfully!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public ParentDetails GetLatestParentDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT TOP 1 p.[Name], p.Email
                        FROM Logs
                        INNER JOIN Parent p ON Logs.Email = p.Email
                        ORDER BY Logs.LogDateTime DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new ParentDetails
                        {
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching admin details: " + ex.Message);
            }
        }
        public class ParentDetails
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public bool ChangeParentPassword(string oldPassword, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TOP 1 parent.Email FROM Logs INNER JOIN Parent parent ON Logs.Email = parent.Email ORDER BY Logs.LogDateTime DESC";
                    SqlCommand command = new SqlCommand(query, connection);
                    string parentEmail = command.ExecuteScalar()?.ToString();

                    if (parentEmail != null)
                    {
                        query = "SELECT COUNT(*) FROM Parent WHERE Email = @Email AND [Password] = @OldPassword";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Email", parentEmail);
                        command.Parameters.AddWithValue("@OldPassword", oldPassword);
                        int count = (int)command.ExecuteScalar();

                        if (count == 1)
                        {
                            // Update the password
                            query = "UPDATE Parent SET [Password] = @NewPassword WHERE Email = @Email";
                            command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@Email", parentEmail);
                            command.Parameters.AddWithValue("@NewPassword", newPassword);
                            int rowsAffected = command.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing password: " + ex.Message);
            }

            return false;
        }

        public DataTable GetPendingEnrollments()
        {
            try
            {
                int parentID = getCurrentUserID();
                string query = @"
                                SELECT c.ChildID, c.Name AS ChildName, c.DateOfBirth, e.Status AS EnrollmentStatus
                                FROM Child c
                                INNER JOIN Enrollment e ON c.ChildID = e.ChildID
                                WHERE c.ParentID = @ParentID AND e.Status = 'Waiting'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ParentID", parentID);
                    DataTable pendingEnrollmentsTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(pendingEnrollmentsTable);

                    return pendingEnrollmentsTable;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error getting pending enrollments: " + ex.Message);
            }
        }

        public DataTable GetRejectedEnrollments()
        {
            try
            {
                int parentID = getCurrentUserID();
                string query = @"
                                SELECT c.ChildID, c.Name AS ChildName, c.DateOfBirth, e.Status AS EnrollmentStatus
                                FROM Child c
                                INNER JOIN Enrollment e ON c.ChildID = e.ChildID
                                WHERE c.ParentID = @ParentID AND e.Status = 'Rejected'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ParentID", parentID);
                    DataTable pendingEnrollmentsTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(pendingEnrollmentsTable);

                    return pendingEnrollmentsTable;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error getting pending enrollments: " + ex.Message);
            }
        }

        public bool CreateEnrollmentRequest(string childName, DateTime childDOB, DateTime startDate, DateTime endDate)
        {
            try
            {
                int parentID = getCurrentUserID();
                Random random = new Random();
                int classroomID = random.Next(1, 4);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the child exists
                    string checkChildQuery = "SELECT COUNT(*) FROM Child WHERE [Name] = @ChildName AND DateOfBirth = @ChildDOB AND ParentID = @ParentID";
                    SqlCommand checkChildCommand = new SqlCommand(checkChildQuery, connection);
                    checkChildCommand.Parameters.AddWithValue("@ChildName", childName);
                    checkChildCommand.Parameters.AddWithValue("@ChildDOB", childDOB);
                    checkChildCommand.Parameters.AddWithValue("@ParentID", parentID);
                    int childCount = (int)checkChildCommand.ExecuteScalar();

                    if (childCount == 0)
                    {
                        // Child does not exist, insert the child
                        string insertChildQuery = "INSERT INTO Child ([Name], DateOfBirth, ParentID) VALUES (@ChildName, @ChildDOB, @ParentID); SELECT SCOPE_IDENTITY();";
                        SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                        insertChildCommand.Parameters.AddWithValue("@ChildName", childName);
                        insertChildCommand.Parameters.AddWithValue("@ChildDOB", childDOB);
                        insertChildCommand.Parameters.AddWithValue("@ParentID", parentID);
                        int childID = Convert.ToInt32(insertChildCommand.ExecuteScalar());

                        // Insert enrollment request
                        string insertEnrollmentQuery = "INSERT INTO Enrollment (EnrollmentDate, ChildID, StartDate, EndDate, [Status], ClassroomID) VALUES (@EnrollmentDate, @ChildID, @StartDate, @EndDate, 'Pending', @ClassroomID)";
                        SqlCommand insertEnrollmentCommand = new SqlCommand(insertEnrollmentQuery, connection);
                        insertEnrollmentCommand.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                        insertEnrollmentCommand.Parameters.AddWithValue("@ChildID", childID);
                        insertEnrollmentCommand.Parameters.AddWithValue("@StartDate", startDate);
                        insertEnrollmentCommand.Parameters.AddWithValue("@EndDate", endDate);
                        insertEnrollmentCommand.Parameters.AddWithValue("@ClassroomID", classroomID);
                        insertEnrollmentCommand.ExecuteNonQuery();

                        return true;
                    }
                    else
                    {
                        // Child already exists
                        Console.WriteLine("Child already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return false;
        }
    }
}
