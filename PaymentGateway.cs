using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace A2Z_Bike_And_Car_Repair_Service_with_parts
{
    public partial class PaymentGateway : Form
    {
        public event EventHandler PaymentCompleted;
        string connectionString = @"Data Source=DESKTOP-O06QAG9\SQLEXPRESS;Initial Catalog = A2Z; Integrated Security = True;";
        public PaymentGateway()
        {
            InitializeComponent();
        }

        private void Card_Number_TextChanged(object sender, EventArgs e)
        {

        }

        private void AmountTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Pay_Click(object sender, EventArgs e)
        {
            // Retrieve card number and payment amount
            string cardNumber = Card_Number.Text;
            if (!int.TryParse(AmountTextBox.Text, out int paymentAmount))
            {
                MessageBox.Show("Please enter a valid integer amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insert payment details into the database
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO PaymentDetails (CardNumber, PaymentAmount) VALUES (@CardNumber, @PaymentAmount)", connection))
                    {
                        command.Parameters.AddWithValue("@CardNumber", cardNumber);
                        command.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Payment details saved to the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to save payment details to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PaymentGateway_Load(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            LogIn x = new LogIn();
            this.Hide();
            x.Show();
        }
    }
}
