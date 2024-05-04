using System;
using System.Data;
using System.Data.SqlClient;

namespace FastDayCareManagment
{
    public class RegisterController
    {
        private string connectionString = "Data Source = EI; Initial Catalog = projectDB; Integrated Security = True;";

        public bool IsEmailExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Parent WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool IsCNICExists(string cnic)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Parent WHERE CNIC = @CNIC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CNIC", cnic);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool RegisterParent(string name, string phoneNumber, string cnic, string address, string email, string password)
        {
            if (phoneNumber.Length != 11)
            {
                MessageBox.Show("Phone number must be 11 digits long.");
                return false;
            }

            if (cnic.Length != 13)
            {
                MessageBox.Show("CNIC must be 13 digits long.");
                return false;
            }

            if (IsEmailExists(email))
            {
                MessageBox.Show("Email already exists. Please use a different email.");
                return false;
            }

            if (IsCNICExists(cnic))
            {
                MessageBox.Show("CNIC already exists. Please use a different CNIC.");
                return false;
            }

            // Proceed with registration
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Parent ([Name], PhoneNumber, CNIC, Address, Email, Password) VALUES (@Name, @PhoneNumber, @CNIC, @Address, @Email, @Password)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@CNIC", cnic);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Registration successful!");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
