using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PSLMv2.Properties;

namespace PSLMv2
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text.Trim();
            string password = textPassword.Text.Trim();
            string fullname = textFullname.Text.Trim();
            string role = accessRegistration.SelectedItem.ToString() ?? "";
            InsertUser.InsertNewUser(username, password, fullname, role);

            this.Hide();
            Login returntologin = new();
            returntologin.Show();

        }

        private void textUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void accessRegistration_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
