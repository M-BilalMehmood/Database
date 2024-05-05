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
    }
}
