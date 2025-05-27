using Guna.UI2.WinForms;
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
    public partial class CheckIn : Form
    {
        private bool isCheckoutMode = false;
        public CheckIn()
        {
            InitializeComponent();
            textPlatenum.MaxLength = 6;
            textPlatenum.KeyPress += textPlatenum_KeyPress;
            checkInConfirm.Click += checkInConfirm_Click; // Only once!
            this.Load += CheckIn_Load;
        }

        private void LoadCheckInData()
        {
            GetRecentCheckIns.DataSource = SystemOperators.GetRecentCheckIns();
        }

        private void CheckIn_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupZoneStatusDataGridView();
            LoadCheckInData();
            RefreshZoneStatus();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginForm = new Login();
            loginForm.Show();
        }

        private void textPlatenum_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Get current text and caret position
            string currentText = textPlatenum.Text;
            int selectionStart = textPlatenum.SelectionStart;

            // Only allow up to 6 characters
            if (currentText.Length >= 6)
            {
                e.Handled = true;
                MessageBox.Show("Try again: Plate number must be 3 letters followed by 3 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // First 3 characters must be letters
            if (currentText.Length < 3)
            {
                if (char.IsLetter(e.KeyChar))
                {
                    // Convert to uppercase if not already
                    if (char.IsLower(e.KeyChar))
                    {
                        e.KeyChar = char.ToUpper(e.KeyChar);
                        MessageBox.Show("Try again: Letters must be uppercase.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    e.Handled = true;
                    MessageBox.Show("Try again: First 3 characters must be letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // Last 3 characters must be digits
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                    MessageBox.Show("Try again: Last 3 characters must be digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textPlatenum_TextChanged(object sender, EventArgs e)
        {
            // Validate format when text changes (e.g., after paste)
            string text = textPlatenum.Text;
            if (text.Length == 6)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(text, @"^[A-Z]{3}[0-9]{3}$"))
                {
                    MessageBox.Show("Try again: Plate number must be 3 uppercase letters followed by 3 digits.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textPlatenum.Text = "";
                }
            }
        }

        private void checkInConfirm_Click(object sender, EventArgs e)
        {
            string plateNumber = textPlatenum.Text.Trim();

            // 1. Handle Check-Out first if in check-out mode
            if (isCheckoutMode)
            {
                var result = SystemOperators.CheckOut(plateNumber);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Check-Out Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, "Check-Out Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Reset UI to normal check-in mode
                assignVtype.Enabled = true;
                assignCtype.Enabled = true;
                assignFzone.Enabled = true;
                isCheckoutMode = false;
                ClearFields();
                LoadCheckInData();
                return;
            }

            // 2. Check-In logic (only runs if not in check-out mode)
            string vehicleType = assignVtype.SelectedItem?.ToString();
            string customerType = assignCtype.SelectedItem?.ToString();
            string parkingZone = assignFzone.SelectedItem?.ToString();

            // Map parkingZone to floor
            string floor = "";
            if (parkingZone == "A1" || parkingZone == "A2" || parkingZone == "A3")
                floor = "B1";
            else if (parkingZone == "A4" || parkingZone == "A5" || parkingZone == "A6")
                floor = "B2";

            // Basic validation
            if (string.IsNullOrEmpty(plateNumber) ||
                string.IsNullOrEmpty(vehicleType) ||
                string.IsNullOrEmpty(customerType) ||
                string.IsNullOrEmpty(parkingZone) ||
                string.IsNullOrEmpty(floor))
            {
                return;
            }

            bool success = SystemOperators.CheckIn(
                plateNumber,
                vehicleType,
                customerType,
                floor,
                parkingZone,
                Session.UserID
            );

            if (success)
            {
                MessageBox.Show("Check-In Confirmed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadCheckInData();
                RefreshZoneStatus();
            }
            else
            {
                MessageBox.Show("Check-In Failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            textPlatenum.Text = "";
            assignVtype.SelectedIndex = -1;
            assignCtype.SelectedIndex = -1;
            assignFzone.SelectedIndex = -1;
        }

        private void RefreshZoneStatus()
        {
            DataTable dt = SystemOperators.GetZoneOccupancy();

            // Prepare a display table to ensure all zones are shown
            DataTable displayTable = new DataTable();
            displayTable.Columns.Add("Zone", typeof(string));
            displayTable.Columns.Add("Occupied", typeof(int));
            displayTable.Columns.Add("Available", typeof(int));

            string[] zones = { "A1", "A2", "A3", "A4", "A5", "A6" };
            foreach (string zone in zones)
            {
                DataRow[] found = dt.Select($"parking_zone = '{zone}'");
                int occupied = found.Length > 0 ? Convert.ToInt32(found[0]["occupied"]) : 0;
                int available = 20 - occupied;
                displayTable.Rows.Add(zone, occupied, available);
            }

            zoneStatus.DataSource = displayTable;

            // Optional: Set column widths and headers for better appearance
            if (zoneStatus.Columns.Count == 3)
            {
                zoneStatus.Columns[0].HeaderText = "Zone";
                zoneStatus.Columns[1].HeaderText = "Occupied";
                zoneStatus.Columns[2].HeaderText = "Available";
                zoneStatus.Columns[0].Width = 60;
                zoneStatus.Columns[1].Width = 80;
                zoneStatus.Columns[2].Width = 80;
            }
        }

        private void SetupZoneStatusDataGridView()
        {
            zoneStatus.AutoGenerateColumns = false;
            zoneStatus.Columns.Clear();
            zoneStatus.ColumnHeadersVisible = true;
            zoneStatus.RowTemplate.Height = 35; // Row height
            zoneStatus.ColumnHeadersHeight = 40; // Header height
            zoneStatus.DefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            zoneStatus.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            zoneStatus.ReadOnly = true;

            var colZone = new DataGridViewTextBoxColumn();
            colZone.HeaderText = "Zone";
            colZone.DataPropertyName = "Zone";
            colZone.Name = "Zone";
            colZone.Width = 40;
            zoneStatus.Columns.Add(colZone);

            var colOccupied = new DataGridViewTextBoxColumn();
            colOccupied.HeaderText = "Occupied";
            colOccupied.DataPropertyName = "Occupied";
            colOccupied.Name = "Occupied";
            colOccupied.Width = 40;
            zoneStatus.Columns.Add(colOccupied);

            var colAvailable = new DataGridViewTextBoxColumn();
            colAvailable.HeaderText = "Available";
            colAvailable.DataPropertyName = "Available";
            colAvailable.Name = "Available";
            colAvailable.Width = 40;
            zoneStatus.Columns.Add(colAvailable);
        }

        private void GetRecentCheckIns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SetupDataGridView()
        {
            GetRecentCheckIns.AutoGenerateColumns = false;
            GetRecentCheckIns.Columns.Clear();
            GetRecentCheckIns.ColumnHeadersVisible = true;
            GetRecentCheckIns.RowTemplate.Height = 35; // Row height
            GetRecentCheckIns.ColumnHeadersHeight = 50; // Header height
            GetRecentCheckIns.DefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            GetRecentCheckIns.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            GetRecentCheckIns.ReadOnly = true;


            var colPlate = new DataGridViewTextBoxColumn();
            colPlate.HeaderText = "Plate Number1";
            colPlate.DataPropertyName = "Plate Number";
            colPlate.Name = "PlateNumber";
            GetRecentCheckIns.Columns.Add(colPlate);

            var colZone = new DataGridViewTextBoxColumn();
            colZone.HeaderText = "Parking Zone";
            colZone.DataPropertyName = "Parking Zone";
            colZone.Name = "ParkingZone";
            GetRecentCheckIns.Columns.Add(colZone);

            var colTime = new DataGridViewTextBoxColumn();
            colTime.HeaderText = "Check-In Time";
            colTime.DataPropertyName = "Check-In Time";
            colTime.Name = "CheckInTime";
            GetRecentCheckIns.Columns.Add(colTime);



            var colFloor = new DataGridViewTextBoxColumn();
            colFloor.HeaderText = "Floor";
            colFloor.DataPropertyName = "Floor";
            colFloor.Name = "Floor";
            GetRecentCheckIns.Columns.Add(colFloor);

            GetRecentCheckIns.Columns["ParkingZone"].Width = 70;

            var colCustomerType = new DataGridViewTextBoxColumn();
            colCustomerType.HeaderText = "Customer Type";
            colCustomerType.DataPropertyName = "Customer Type";
            colCustomerType.Name = "CustomerType";
            GetRecentCheckIns.Columns.Add(colCustomerType);

            var colVehicleType = new DataGridViewTextBoxColumn();
            colVehicleType.HeaderText = "Vehicle Type";
            colVehicleType.DataPropertyName = "Vehicle Type";
            colVehicleType.Name = "VehicleType";
            GetRecentCheckIns.Columns.Add(colVehicleType);

            var colIsPaid = new DataGridViewTextBoxColumn();
            colIsPaid.HeaderText = "Is Paid";
            colIsPaid.DataPropertyName = "Is Paid";
            colIsPaid.Name = "Is Paid";
            GetRecentCheckIns.Columns.Add(colIsPaid);


            GetRecentCheckIns.AllowUserToAddRows = false;
            GetRecentCheckIns.Columns["CheckInTime"].Width = 200;
            GetRecentCheckIns.Columns["Floor"].Width = 50;
            GetRecentCheckIns.Columns["ParkingZone"].Width = 70;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void DashB_Click(object sender, EventArgs e)
        {

        }

        private void Logs_Click(object sender, EventArgs e)
        {
            this.Hide();
            TransactionHistory transactionHistory = new TransactionHistory();
            transactionHistory.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MonthlyAccPass_Click(object sender, EventArgs e)
        {
            this.Hide();
            MonthlyAccessPass monthlyAccessPass = new MonthlyAccessPass();
            monthlyAccessPass.Show();
        }

        private void assignFzone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void assignCtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void initiateCheckout_Click(object sender, EventArgs e)
        {
            // Disable ComboBoxes during checkout
            assignVtype.Enabled = false;
            assignCtype.Enabled = false;
            assignFzone.Enabled = false;

            // Enable plate number textbox and confirm button
            textPlatenum.Enabled = true;
            checkInConfirm.Enabled = true;

            // Optionally clear the plate number field for new input
            textPlatenum.Text = "";
            textPlatenum.Focus();

            // Set checkout mode flag
            isCheckoutMode = true;

            MessageBox.Show("Enter plate number and click Confirm to proceed with Check-Out.", "Check-Out Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCheckPass_Click(object sender, EventArgs e)
        {
            string plateNumber = textPlatenum.Text.Trim();

            if (string.IsNullOrEmpty(plateNumber))
            {
                MessageBox.Show("Please enter a plate number first.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isPassUser = SystemOperators.HasActiveMonthlyPass(plateNumber);

            if (isPassUser)
            {
                MessageBox.Show("This plate number has an active monthly access pass. No fee will be charged at checkout. The transaction will be marked as a pass user.",
                    "Monthly Pass User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("This plate number does not have an active monthly access pass. Usual checkout process applies.",
                    "Regular User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void zoneStatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
