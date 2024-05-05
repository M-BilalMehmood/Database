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
    public partial class ParentMail : Form
    {
        public ParentMail()
        {
            InitializeComponent();
            loadRecievedMails();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to go back to the login screen?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }

        private void loadRecievedMails()
        {
            ParentController parentController = new ParentController();
            DataTable dataTable = parentController.GetReceivedMails();
            RecievedMails.DataSource = dataTable;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ParentComposeMail parentComposeMail = new ParentComposeMail();
            parentComposeMail.Show();

            loadRecievedMails();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ParentMailSent parentMailSent = new ParentMailSent();
            parentMailSent.Show();
            this.Hide();
        }
    }
}
