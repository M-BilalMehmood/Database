﻿using System;
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
    public partial class ParentComposeMail : Form
    {
        public ParentComposeMail()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ParentController parentController = new ParentController();
            parentController.SendMail(textBox1.Text, richTextBox1.Text);

            this.Hide();
        }
    }
}
