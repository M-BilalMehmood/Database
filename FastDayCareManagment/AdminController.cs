using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public class AdminController
    {
        private string connectionString = "Data Source = EI; Initial Catalog = projectDB; Integrated Security = True;";

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

        public DataTable GetChildrenWithUnpaidFees()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                                    SELECT c.Name AS ChildName, p.Name AS ParentName, f.Amount AS FeeAmount
                                    FROM Child c
                                    JOIN Parent p ON c.ParentID = p.ParentID
                                    LEFT JOIN Fee f ON c.ChildID = f.ChildID
                                    WHERE (f.Status IS NULL OR f.Status <> 'Paid')
                                    AND f.Amount IS NOT NULL;
                                    ";

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

        public decimal GetPreviousMonthProfit()
        {
            decimal profit = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                                    SELECT Total_Revenue 
                                    FROM MonthlyRevenueView 
                                    WHERE Month = FORMAT(DATEADD(month, -1, GETDATE()), 'MM yyyy')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            profit = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return profit;
        }

        public bool AddAnnouncement(string message, DateTime dateTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the admin ID
                    int adminId = GetAdminId();
                    string query = @"
                                    INSERT INTO Announcement (AdministratorID, [DateTime], [Message])
                                    VALUES (@AdminId, @DateTime, @Message)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AdminId", adminId);
                        command.Parameters.AddWithValue("@DateTime", dateTime);
                        command.Parameters.AddWithValue("@Message", message);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }
        public int GetAdminId()
        {
            int adminId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                                    SELECT TOP 1 Admin.AdministratorID
                                    FROM Logs
                                    INNER JOIN Administrator Admin ON Logs.Email = Admin.Email
                                    ORDER BY Logs.LogDateTime DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            adminId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return adminId;
        }

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

        public bool DeleteAnnouncement(int announcementID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                                    DELETE FROM Announcement
                                    WHERE AnnouncementID = @AnnouncementID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AnnouncementID", announcementID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the announcement: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool EditAnnouncement(int announcementID, string updatedAnnouncement)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Announcement SET Message = @Message WHERE AnnouncementID = @AnnouncementID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Message", updatedAnnouncement);
                        command.Parameters.AddWithValue("@AnnouncementID", announcementID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while editing the announcement: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateAnnouncement(int announcementID, string updatedAnnouncement)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Announcement SET Message = @UpdatedMessage WHERE AnnouncementID = @AnnouncementID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UpdatedMessage", updatedAnnouncement);
                        command.Parameters.AddWithValue("@AnnouncementID", announcementID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the announcement: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetActiveEnrollmentChildren()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                                    SELECT c.ChildID, c.Name, c.DateOfBirth
                                    FROM Child c
                                    INNER JOIN Enrollment e ON c.ChildID = e.ChildID
                                    WHERE e.Status = 'Approved'";

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
                MessageBox.Show("An error occurred while fetching active enrollment children: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        public DataTable GetWaitingEnrollmentChildren()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                            SELECT c.ChildID, c.Name, c.DateOfBirth
                            FROM Child c
                            INNER JOIN Enrollment e ON c.ChildID = e.ChildID
                            WHERE e.Status = 'Waiting'";

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
                MessageBox.Show("An error occurred while fetching waiting enrollment children: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        public bool UpdateChildEnrollmentStatus(int childID, string status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                            UPDATE Enrollment 
                            SET Status = @Status 
                            WHERE ChildID = @ChildID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@ChildID", childID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating child enrollment status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UnenrollChild(int childID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Assuming you have a table named "Enrollment" with a column "ChildID" and a column "Status"
                    string query = "UPDATE Enrollment SET Status = 'Unenrolled' WHERE ChildID = @ChildID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChildID", childID);

                        int rowsAffected = command.ExecuteNonQuery();

                        // If at least one row is affected, consider it a success
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public DataTable GetUnEnrollmentOrRejectedChildren()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                            SELECT c.ChildID, c.Name, c.DateOfBirth, e.Status
                            FROM Child c
                            INNER JOIN Enrollment e ON c.ChildID = e.ChildID
                            WHERE e.Status = 'Unenrolled' OR e.Status = 'Rejected'";

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
                MessageBox.Show("An error occurred while fetching unenrolled or rejected children: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        public DataTable getChildrenData()
        {
            try
            {
                string query = @"
                    SELECT 
                        c.[Name] AS StudentName,
                        c.DateOfBirth,
                        p.[Name] AS ParentName,
                        p.Email AS ParentEmail,
                        AVG(CASE WHEN ar.[Status] = 'Present' THEN 1 ELSE 0 END) AS AvgAttendance,
                        AVG(f.Amount) AS AvgFee
                    FROM 
                        Child c
                    INNER JOIN 
                        Parent p ON c.ParentID = p.ParentID
                    LEFT JOIN 
                        Enrollment e ON c.ChildID = e.ChildID
                    LEFT JOIN 
                        AttendanceRecord ar ON e.EnrollmentID = ar.EnrollmentID
                    LEFT JOIN 
                        Fee f ON c.ChildID = f.ChildID
                    WHERE 
                        e.[Status] NOT IN ('Waiting', 'Unenrolled', 'Rejected')
                    GROUP BY 
                        c.ChildID, c.[Name], c.DateOfBirth, p.[Name], p.Email";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading data: " + ex.Message);
            }
        }

        public DataTable getStaffData()
        {
            try
            {
                string query = @"
                                SELECT 
                                sm.StaffID,
                                sm.[Name] AS StaffName,
                                sm.Email AS StaffEmail,
                                (SELECT TOP 1 Amount FROM Salary WHERE StaffID = sm.StaffID ORDER BY PaymentDate DESC) AS Pay,
                                STUFF((SELECT DISTINCT ', ' + c.[Name]
                                       FROM Classroom c
                                       JOIN assignedClassrooms ac ON c.ClassroomID = ac.ClassroomID
                                       WHERE ac.StaffID = sm.StaffID
                                       FOR XML PATH('')), 1, 2, '') AS AssignedClassrooms
                                FROM 
                                    StaffMember sm;
                            ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading data: " + ex.Message);
            }
        }

        public void AddStaffMember(string email, string password, string name, decimal pay, string assignedClass)
        {
            try
            {
                // Check if the email already exists
                if (IsEmailExists(email))
                {
                    MessageBox.Show("Email already exists. Please use a different email.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert into StaffMember table
                    string insertStaffMemberQuery = "INSERT INTO StaffMember (Email, [Password], [Name], Role) VALUES (@Email, @Password, @Name, 'Staff'); SELECT SCOPE_IDENTITY();";
                    SqlCommand insertStaffMemberCommand = new SqlCommand(insertStaffMemberQuery, connection);
                    insertStaffMemberCommand.Parameters.AddWithValue("@Email", email);
                    insertStaffMemberCommand.Parameters.AddWithValue("@Password", password);
                    insertStaffMemberCommand.Parameters.AddWithValue("@Name", name);
                    int staffID = Convert.ToInt32(insertStaffMemberCommand.ExecuteScalar());

                    // Insert into Salary table
                    string insertSalaryQuery = "INSERT INTO Salary (StaffID, Amount) VALUES (@StaffID, @Amount);";
                    SqlCommand insertSalaryCommand = new SqlCommand(insertSalaryQuery, connection);
                    insertSalaryCommand.Parameters.AddWithValue("@StaffID", staffID);
                    insertSalaryCommand.Parameters.AddWithValue("@Amount", pay);
                    insertSalaryCommand.ExecuteNonQuery();

                    // Insert into assignedClassrooms table if assignedClass is not null or empty
                    if (!string.IsNullOrEmpty(assignedClass))
                    {
                        string[] classrooms = assignedClass.Split(',');
                        foreach (string classroom in classrooms)
                        {
                            string insertAssignedClassroomQuery = "INSERT INTO assignedClassrooms (ClassroomID, StaffID) VALUES (@ClassroomID, @StaffID);";
                            SqlCommand insertAssignedClassroomCommand = new SqlCommand(insertAssignedClassroomQuery, connection);
                            insertAssignedClassroomCommand.Parameters.AddWithValue("@StaffID", staffID);
                            insertAssignedClassroomCommand.Parameters.AddWithValue("@ClassroomID", classroom.Trim());
                            insertAssignedClassroomCommand.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Staff member added successfully!");

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private bool IsEmailExists(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM StaffMember WHERE Email = @Email";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    int count = (int)command.ExecuteScalar();

                    connection.Close();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while checking email existence: " + ex.Message);
                return false;
            }
        }

        public void RemoveStaffMember(int staffID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete associated records in assignedClassrooms table
                    string deleteAssignedClassroomsQuery = "DELETE FROM assignedClassrooms WHERE StaffID = @StaffID;";
                    SqlCommand deleteAssignedClassroomsCommand = new SqlCommand(deleteAssignedClassroomsQuery, connection);
                    deleteAssignedClassroomsCommand.Parameters.AddWithValue("@StaffID", staffID);
                    deleteAssignedClassroomsCommand.ExecuteNonQuery();

                    // Delete from Salary table
                    string deleteSalaryQuery = "DELETE FROM Salary WHERE StaffID = @StaffID;";
                    SqlCommand deleteSalaryCommand = new SqlCommand(deleteSalaryQuery, connection);
                    deleteSalaryCommand.Parameters.AddWithValue("@StaffID", staffID);
                    deleteSalaryCommand.ExecuteNonQuery();

                    // Delete from StaffMember table
                    string deleteStaffMemberQuery = "DELETE FROM StaffMember WHERE StaffID = @StaffID;";
                    SqlCommand deleteStaffMemberCommand = new SqlCommand(deleteStaffMemberQuery, connection);
                    deleteStaffMemberCommand.Parameters.AddWithValue("@StaffID", staffID);
                    deleteStaffMemberCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing the staff member: " + ex.Message);
            }
        }

        public void UpdateStaffMember(int staffID, string email, string password, string name, decimal pay, string assignedClass)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update StaffMember table
                    string updateStaffMemberQuery = "UPDATE StaffMember SET Email = @Email, [Password] = @Password, [Name] = @Name WHERE StaffID = @StaffID;";
                    SqlCommand updateStaffMemberCommand = new SqlCommand(updateStaffMemberQuery, connection);
                    updateStaffMemberCommand.Parameters.AddWithValue("@Email", email);
                    updateStaffMemberCommand.Parameters.AddWithValue("@Password", password);
                    updateStaffMemberCommand.Parameters.AddWithValue("@Name", name);
                    updateStaffMemberCommand.Parameters.AddWithValue("@StaffID", staffID);
                    updateStaffMemberCommand.ExecuteNonQuery();

                    // Update StaffSalary table
                    string updateStaffSalaryQuery = "UPDATE Salary SET Amount = @Pay WHERE StaffID = @StaffID;";
                    SqlCommand updateStaffSalaryCommand = new SqlCommand(updateStaffSalaryQuery, connection);
                    updateStaffSalaryCommand.Parameters.AddWithValue("@Pay", pay);
                    updateStaffSalaryCommand.Parameters.AddWithValue("@StaffID", staffID);
                    updateStaffSalaryCommand.ExecuteNonQuery();

                    // Delete existing assigned classrooms
                    string deleteAssignedClassroomsQuery = "DELETE FROM assignedClassrooms WHERE StaffID = @StaffID;";
                    SqlCommand deleteAssignedClassroomsCommand = new SqlCommand(deleteAssignedClassroomsQuery, connection);
                    deleteAssignedClassroomsCommand.Parameters.AddWithValue("@StaffID", staffID);
                    deleteAssignedClassroomsCommand.ExecuteNonQuery();

                    // Insert new assigned classrooms
                    if (!string.IsNullOrEmpty(assignedClass))
                    {
                        string[] classrooms = assignedClass.Split(',');
                        foreach (string classroom in classrooms)
                        {
                            string insertAssignedClassroomQuery = "INSERT INTO assignedClassrooms (ClassroomID, StaffID) VALUES (@ClassroomID, @StaffID);";
                            SqlCommand insertAssignedClassroomCommand = new SqlCommand(insertAssignedClassroomQuery, connection);
                            insertAssignedClassroomCommand.Parameters.AddWithValue("@StaffID", staffID);
                            insertAssignedClassroomCommand.Parameters.AddWithValue("@ClassroomID", classroom.Trim());
                            insertAssignedClassroomCommand.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the staff member: " + ex.Message);
            }
        }

        public DataTable GetStaffMemberDetails(int staffID)
        {
            try
            {
                string query = @"
                                SELECT 
                                    sm.StaffID,
                                    sm.[Name],
                                    sm.Email,
                                    sm.[Password],
                                    s.Amount AS Pay,
                                    STUFF((SELECT DISTINCT ', ' + c.[Name]
                                           FROM Classroom c
                                           JOIN assignedClassrooms ac ON c.ClassroomID = ac.ClassroomID
                                           WHERE ac.StaffID = sm.StaffID
                                           FOR XML PATH('')), 1, 2, '') AS AssignedClassrooms
                                FROM 
                                    StaffMember sm
                                LEFT JOIN 
                                    Salary s ON sm.StaffID = s.StaffID
                                WHERE
                                    sm.StaffID = @StaffID;
                            ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StaffID", staffID);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading data: " + ex.Message);
            }
        }

        private string loggedInUserEmail()
        {
            string loggedInUserEmail = string.Empty; // Default value

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                                    SELECT TOP 1 Admin.Email
                                    FROM Logs
                                    INNER JOIN Administrator Admin ON Logs.Email = Admin.Email
                                    ORDER BY Logs.LogDateTime DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return loggedInUserEmail;
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

        public AdminDetails GetLatestAdminDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT TOP 1 Admin.[Name], Admin.Email
                        FROM Logs
                        INNER JOIN Administrator Admin ON Logs.Email = Admin.Email
                        ORDER BY Logs.LogDateTime DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new AdminDetails
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
        public class AdminDetails
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public bool ChangeAdminPassword(string oldPassword, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TOP 1 Admin.Email FROM Logs INNER JOIN Administrator Admin ON Logs.Email = Admin.Email ORDER BY Logs.LogDateTime DESC";
                    SqlCommand command = new SqlCommand(query, connection);
                    string adminEmail = command.ExecuteScalar()?.ToString();

                    if (adminEmail != null)
                    {
                        // Check if old password matches
                        query = "SELECT COUNT(*) FROM Administrator WHERE Email = @Email AND [Password] = @OldPassword";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Email", adminEmail);
                        command.Parameters.AddWithValue("@OldPassword", oldPassword);
                        int count = (int)command.ExecuteScalar();

                        if (count == 1)
                        {
                            // Update the password
                            query = "UPDATE Administrator SET [Password] = @NewPassword WHERE Email = @Email";
                            command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@Email", adminEmail);
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
    }
}

