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
    }
}
