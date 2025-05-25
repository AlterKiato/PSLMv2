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
            LoadCheckInData();
        }
        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginForm = new Login();
            loginForm.Show();
        }

        private void textPlatenum_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, numbers, and control keys (like backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textPlatenum_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkInConfirm_Click(object sender, EventArgs e)
        {
            string plateNumber = textPlatenum.Text.Trim();
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

        private void GetRecentCheckIns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SetupDataGridView()
        {
            GetRecentCheckIns.AutoGenerateColumns = false;
            GetRecentCheckIns.Columns.Clear();
            GetRecentCheckIns.ColumnHeadersVisible = true;
            GetRecentCheckIns.RowTemplate.Height = 30; // Row height
            GetRecentCheckIns.ColumnHeadersHeight = 40; // Header height

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
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void DashB_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
