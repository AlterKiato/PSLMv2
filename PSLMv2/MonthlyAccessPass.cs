using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSLMv2
{
    public partial class MonthlyAccessPass : Form
    {
        public MonthlyAccessPass()
        {
            InitializeComponent();
            this.Load += MonthlyAccessPass_Load;
            this.Load += VisitorTransactions_Load;
        }

        private void VisitorTransactions_Load(object sender, EventArgs e)
        {
            LoadVisitorTransactions();
        }
        private void MonthlyAccessPass_Load(object sender, EventArgs e)
        {
            LoadAccessPassList();
        }
        private void LoadAccessPassList()
        {
            accessPassList.ReadOnly = true;
            accessPassList.AllowUserToAddRows = false;
            accessPassList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            accessPassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            accessPassList.ColumnHeadersVisible = true;
            accessPassList.RowTemplate.Height = 35;
            accessPassList.ColumnHeadersHeight = 50;
            accessPassList.DefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            accessPassList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);

            accessPassList.AutoGenerateColumns = true;
            accessPassList.DataSource = SystemOperators.GetMonthlyAccessPasses();
        }

        private void LoadVisitorTransactions()
        {
            // Fetch only Visitor transactions with required columns
            var visitorTransactions = SystemOperators.GetVisitorTransactions();
            transacVisitorList.ReadOnly = true;
            transacVisitorList.AllowUserToAddRows = false;
            transacVisitorList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            transacVisitorList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            transacVisitorList.ColumnHeadersVisible = true;
            transacVisitorList.RowTemplate.Height = 35;
            transacVisitorList.ColumnHeadersHeight = 50;
            transacVisitorList.DefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            transacVisitorList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);

            transacVisitorList.AutoGenerateColumns = true;
            transacVisitorList.DataSource = visitorTransactions;
        }
        private void textPlatenum_TextChanged(object sender, EventArgs e)
        {

        }

        private void Checkin_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckIn checkIn = new CheckIn();
            checkIn.Show();
        }

        private void MonthlyAccPass_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TransactionHistory transactionHistory = new TransactionHistory();
            transactionHistory.Show();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void checkInConfirm_Click(object sender, EventArgs e)
        {
            string plateNumber = textPlatenum.Text.Trim();

            if (string.IsNullOrWhiteSpace(plateNumber))
            {
                MessageBox.Show("Please enter a plate number.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Only "Visitors" can avail, so we pass "Visitors" as the customerType
            var result = SystemOperators.GrantMonthlyPass(plateNumber, "Visitors");

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccessPassList(); // Refresh the DataGridView to show the new entry
                LoadVisitorTransactions();
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accessPassList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void transacVisitorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void accessPassRenew_Click(object sender, EventArgs e)
        {
            string plateNumber = textPlatenum.Text.Trim();

            if (string.IsNullOrWhiteSpace(plateNumber))
            {
                MessageBox.Show("Please enter a plate number.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = SystemOperators.RenewMonthlyPass(plateNumber);

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Renewal Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccessPassList();
                LoadVisitorTransactions();
            }
            else
            {
                MessageBox.Show(result.Message, "Renewal Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
