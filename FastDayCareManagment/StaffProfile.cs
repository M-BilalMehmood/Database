using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FastDayCareManagment.AdminController;
using static FastDayCareManagment.StaffController;

namespace FastDayCareManagment
{
    public partial class StaffProfile : Form
    {
        public StaffProfile()
        {
            InitializeComponent();
            LoadStaff();
        }

        private void LoadStaff()
        {
            StaffController controller = new StaffController();
            StaffDetails Staff = controller.GetLatestStaffDetails();
            if (Staff != null)
            {
                label1.Text = Staff.Name;
                label2.Text = Staff.Email;
            }
            else
            {
                MessageBox.Show("Staff details not found.");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            StaffDash staffDashboard = new StaffDash();
            staffDashboard.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffChangePass staffChangePass = new StaffChangePass();
            staffChangePass.Show();
        }
    }
}
