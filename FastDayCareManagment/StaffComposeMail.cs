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
    public partial class StaffComposeMail : Form
    {
        public StaffComposeMail()
        {
            InitializeComponent();
        }

        private void StaffComposeMail_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            StaffController staffController = new StaffController();
            staffController.SendMail(textBox1.Text, richTextBox1.Text);

            this.Hide();
        }
    }
}
