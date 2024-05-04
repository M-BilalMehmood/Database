using System;
using System.Data;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public partial class editStaffAdmin : Form
    {
        private int staffID;
        private AdminController adminController;

        public editStaffAdmin(int staffID)
        {
            InitializeComponent();
            this.staffID = staffID;
            adminController = new AdminController();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dataTable = adminController.GetStaffMemberDetails(staffID);
                if (dataTable.Rows.Count > 0)
                {
                    textBox1.Text = dataTable.Rows[0]["Name"].ToString();
                    textBox5.Text = dataTable.Rows[0]["Email"].ToString();
                    textBox6.Text = dataTable.Rows[0]["Password"].ToString();
                    textBox2.Text = dataTable.Rows[0]["Pay"].ToString();
                    textBox3.Text = dataTable.Rows[0]["AssignedClassrooms"].ToString();
                }
                else
                {
                    MessageBox.Show("Staff member not found.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Get updated values from the form
                string name = textBox1.Text;
                string email = textBox5.Text;
                string password = textBox6.Text;
                decimal pay = Convert.ToDecimal(textBox2.Text);
                string assignedClass = textBox3.Text;

                // Update staff member details
                adminController.UpdateStaffMember(staffID, email, password, name, pay, assignedClass);
                MessageBox.Show("Staff member details updated successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
