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
    public partial class ComposeEmailAdmin : Form
    {
        public ComposeEmailAdmin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminController adminController = new AdminController();
            adminController.SendMail(textBox1.Text, richTextBox1.Text);

            this.Hide();
        }
    }
}
