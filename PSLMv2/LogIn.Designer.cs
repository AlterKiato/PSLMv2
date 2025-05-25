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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            Logged = new Guna.UI2.WinForms.Guna2Button();
            linkLabel1 = new LinkLabel();
            textUsername = new Guna.UI2.WinForms.Guna2TextBox();
            textPassword = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Century Gothic", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(291, 108);
            label1.Name = "label1";
            label1.Size = new Size(135, 51);
            label1.TabIndex = 1;
            label1.Text = "Login";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Century Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(294, 175);
            label2.Name = "label2";
            label2.Size = new Size(132, 27);
            label2.TabIndex = 4;
            label2.Text = "Username:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(294, 262);
            label3.Name = "label3";
            label3.Size = new Size(124, 27);
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
            Logged.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Logged.ForeColor = Color.White;
            Logged.Location = new Point(294, 349);
            Logged.Name = "Logged";
            Logged.ShadowDecoration.CustomizableEdges = customizableEdges2;
            Logged.Size = new Size(126, 43);
            Logged.TabIndex = 6;
            Logged.Text = "Login";
            Logged.Click += guna2Button1_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = Color.Black;
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.Transparent;
            linkLabel1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(464, 334);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(166, 20);
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
            textUsername.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textUsername.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textUsername.Location = new Point(294, 207);
            textUsername.Margin = new Padding(4, 5, 4, 5);
            textUsername.Name = "textUsername";
            textUsername.PlaceholderText = "";
            textUsername.SelectedText = "";
            textUsername.ShadowDecoration.CustomizableEdges = customizableEdges4;
            textUsername.Size = new Size(336, 35);
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
            textPassword.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textPassword.Location = new Point(294, 294);
            textPassword.Margin = new Padding(4, 5, 4, 5);
            textPassword.Name = "textPassword";
            textPassword.PlaceholderText = "";
            textPassword.SelectedText = "";
            textPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            textPassword.Size = new Size(336, 35);
            textPassword.TabIndex = 9;
            textPassword.TextChanged += guna2TextBox2_TextChanged;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(882, 553);
            Controls.Add(textPassword);
            Controls.Add(textUsername);
            Controls.Add(linkLabel1);
            Controls.Add(Logged);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Guna.UI2.WinForms.Guna2Button Logged;
        private LinkLabel linkLabel1;
        private Guna.UI2.WinForms.Guna2TextBox textUsername;
        private Guna.UI2.WinForms.Guna2TextBox textPassword;
    }
}
