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
    public partial class TransactionHistory : Form
    {
        public TransactionHistory()
        {
            InitializeComponent();
            LoadTransactionHistory();
        }

        private void LoadTransactionHistory()
        {
            getTransacHistory.ReadOnly = true;
            getTransacHistory.AllowUserToAddRows = false;
            getTransacHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            getTransacHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            getTransacHistory.ColumnHeadersVisible = true;
            getTransacHistory.RowTemplate.Height = 35;
            getTransacHistory.ColumnHeadersHeight = 50;
            getTransacHistory.DefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            getTransacHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);

            // Let DataGridView auto-generate columns based on the DataTable
            getTransacHistory.AutoGenerateColumns = true;
            getTransacHistory.DataSource = SystemOperators.GetCompletedTransactions();
        }

        private void getTransacHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckIn checkIn = new CheckIn();
            checkIn.Show();
        }

        private void MonthlyAccPass_Click(object sender, EventArgs e)
        {
            this.Hide();
            MonthlyAccessPass monthlyAccPass = new MonthlyAccessPass();
            monthlyAccPass.Show();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
