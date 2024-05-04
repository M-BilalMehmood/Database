using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastDayCareManagment
{
    public partial class addStaffMember : Form
    {
        public addStaffMember()
        {
            InitializeComponent();
            textBox2.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered key is a digit or the backspace key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the key press event
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            // Clear all textboxes
            textBox5.Text = "";
            textBox6.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string email = textBox5.Text;
            string password = textBox6.Text;
            string name = textBox1.Text;
            decimal pay;
            if (!decimal.TryParse(textBox2.Text, out pay))
            {
                MessageBox.Show("Pay must be a valid decimal value.");
                return;
            }
            string assignedClass = textBox3.Text;
            AdminController adminController = new AdminController();
            adminController.AddStaffMember(email, password, name, pay, assignedClass);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
