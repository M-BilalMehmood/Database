using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public partial class ParentEnrollment : Form
    {
        public ParentEnrollment()
        {
            InitializeComponent();
            LoadEnrolledChildren();
            LoadPendingEnrollments();
            LoadRejectedEnrollments();
        }

        private void LoadEnrolledChildren()
        {
            ParentController parentController = new ParentController();
            Announcments.DataSource = parentController.GetEnrolledChildren();
        }

        private void LoadPendingEnrollments()
        {
            ParentController parentController = new ParentController();
            dataGridView1.DataSource = parentController.GetPendingEnrollments();
        }

        private void LoadRejectedEnrollments()
        {
            ParentController parentController = new ParentController();
            dataGridView2.DataSource = parentController.GetRejectedEnrollments();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ParentEnrolmentReq parentEnrolmentReq = new ParentEnrolmentReq();
            parentEnrolmentReq.Show();

            LoadPendingEnrollments();
        }
    }
}
