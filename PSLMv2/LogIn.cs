using PSLMv2.Properties;

namespace PSLMv2
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textPassword.PasswordChar = '*'; // Fixed the issue by referencing the correct control
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate to the registration form
            this.Hide();
            Registration register = new Registration();
            register.Show();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            //username field (textUsername)
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e) 
        {
            //password field (textPassword)
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Login button click event
            string username = textUsername.Text.Trim();
            string password = textPassword.Text.Trim();

            if (LoginUser.Authenticate(username, password))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
