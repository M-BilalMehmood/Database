using System;
using System.Data.SqlClient;

namespace FastDayCareManagment
{
    public class LoginController
    {
        private string connectionString = "Data Source = EI; Initial Catalog = projectDB; Integrated Security = True;";

        public string GetUserRole(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Role FROM Administrator WHERE Email = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                        return "Admin";
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Role FROM StaffMember WHERE Email = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                        return "Staff";
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ParentID FROM Parent WHERE Email = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                        return "Parent";
                }
            }

            // No matching user found
            return "Unknown";
        }
    }
}
