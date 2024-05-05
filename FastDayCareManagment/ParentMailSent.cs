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
    public partial class ParentMailSent : Form
    {
        public ParentMailSent()
        {
            InitializeComponent();
            LoadSenMail();
        }

        private void LoadSenMail()
        {
            ParentController parentController = new ParentController();
            DataTable dataTable = parentController.GetSentMails();
            SentMails.DataSource = dataTable;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ParentComposeMail parentComposeMail = new ParentComposeMail();
            parentComposeMail.Show();

            LoadSenMail();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ParentMail parentMail = new ParentMail();
            parentMail.Show();
            this.Hide();
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
    }
}
