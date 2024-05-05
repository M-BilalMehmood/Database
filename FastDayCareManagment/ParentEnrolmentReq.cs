using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public partial class ParentEnrolmentReq : Form
    {
        public ParentEnrolmentReq()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string childName = textBox1.Text;
            DateTime childDOB = dateTimePicker3.Value;
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            ParentController parentController = new ParentController();
            if (parentController.CreateEnrollmentRequest(childName, childDOB, startDate, endDate))
            {
                MessageBox.Show("Enrollment request created successfully.");
            }
            else
            {
                MessageBox.Show("Failed to create enrollment request.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
