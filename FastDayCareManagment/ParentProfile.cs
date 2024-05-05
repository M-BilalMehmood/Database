using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FastDayCareManagment.ParentController;

namespace FastDayCareManagment
{
    public partial class ParentProfile : Form
    {
        public ParentProfile()
        {
            InitializeComponent();
            LoadParent();
        }

        private void LoadParent()
        {
            ParentController controller = new ParentController();
            ParentDetails Parent = controller.GetLatestParentDetails();
            if (Parent != null)
            {
                label1.Text = Parent.Name;
                label2.Text = Parent.Email;
            }
            else
            {
                MessageBox.Show("Parent details not found.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParentChangePass parentChangePass = new ParentChangePass();
            parentChangePass.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            ParentDashboard parentDashboard = new ParentDashboard();
            parentDashboard.Show();
            this.Hide();
        }
    }
}
