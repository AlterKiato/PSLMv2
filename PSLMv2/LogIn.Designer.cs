namespace PSLMv2
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            label2 = new Label();
            label3 = new Label();
            Logged = new Guna.UI2.WinForms.Guna2Button();
            linkLabel1 = new LinkLabel();
            textUsername = new Guna.UI2.WinForms.Guna2TextBox();
            textPassword = new Guna.UI2.WinForms.Guna2TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Century Gothic", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(461, 253);
            label2.Name = "label2";
            label2.Size = new Size(156, 34);
            label2.TabIndex = 4;
            label2.Text = "Username:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(461, 363);
            label3.Name = "label3";
            label3.Size = new Size(149, 34);
            label3.TabIndex = 5;
            label3.Text = "Password:";
            // 
            // Logged
            // 
            Logged.BackColor = Color.Transparent;
            Logged.BorderColor = Color.White;
            Logged.BorderRadius = 6;
            Logged.BorderThickness = 2;
            Logged.CustomizableEdges = customizableEdges1;
            Logged.DisabledState.BorderColor = Color.DarkGray;
            Logged.DisabledState.CustomBorderColor = Color.DarkGray;
            Logged.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            Logged.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            Logged.FillColor = Color.Transparent;
            Logged.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Logged.ForeColor = Color.White;
            Logged.Location = new Point(575, 510);
            Logged.Name = "Logged";
            Logged.ShadowDecoration.CustomizableEdges = customizableEdges2;
            Logged.Size = new Size(162, 63);
            Logged.TabIndex = 6;
            Logged.Text = "Login";
            Logged.Click += guna2Button1_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = Color.Black;
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.Transparent;
            linkLabel1.Font = new Font("Century Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(634, 464);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(201, 21);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Don't have account?\r\n";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // textUsername
            // 
            textUsername.BackColor = Color.Transparent;
            textUsername.BorderRadius = 5;
            textUsername.BorderThickness = 2;
            textUsername.CustomizableEdges = customizableEdges3;
            textUsername.DefaultText = "";
            textUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textUsername.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textUsername.Font = new Font("Century Gothic", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textUsername.ForeColor = Color.Black;
            textUsername.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textUsername.Location = new Point(461, 292);
            textUsername.Margin = new Padding(4, 5, 4, 5);
            textUsername.Name = "textUsername";
            textUsername.PlaceholderText = "Username ";
            textUsername.SelectedText = "";
            textUsername.ShadowDecoration.CustomizableEdges = customizableEdges4;
            textUsername.Size = new Size(374, 45);
            textUsername.TabIndex = 8;
            textUsername.TextChanged += guna2TextBox1_TextChanged;
            // 
            // textPassword
            // 
            textPassword.BackColor = Color.Transparent;
            textPassword.BorderRadius = 5;
            textPassword.BorderThickness = 2;
            textPassword.CustomizableEdges = customizableEdges5;
            textPassword.DefaultText = "";
            textPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textPassword.Font = new Font("Century Gothic", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textPassword.ForeColor = Color.Black;
            textPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textPassword.Location = new Point(461, 402);
            textPassword.Margin = new Padding(4, 5, 4, 5);
            textPassword.Name = "textPassword";
            textPassword.PlaceholderText = "Password";
            textPassword.SelectedText = "";
            textPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            textPassword.Size = new Size(374, 45);
            textPassword.TabIndex = 9;
            textPassword.TextChanged += guna2TextBox2_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Century Gothic", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(461, 186);
            label4.Name = "label4";
            label4.Size = new Size(135, 51);
            label4.TabIndex = 10;
            label4.Text = "Login";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1282, 753);
            Controls.Add(label4);
            Controls.Add(textPassword);
            Controls.Add(textUsername);
            Controls.Add(linkLabel1);
            Controls.Add(Logged);
            Controls.Add(label3);
            Controls.Add(label2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Guna.UI2.WinForms.Guna2Button Logged;
        private LinkLabel linkLabel1;
        private Guna.UI2.WinForms.Guna2TextBox textUsername;
        private Guna.UI2.WinForms.Guna2TextBox textPassword;
        private Label label4;
    }
}
