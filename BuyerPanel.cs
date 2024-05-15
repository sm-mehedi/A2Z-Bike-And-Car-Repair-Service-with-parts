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
    public partial class BuyerPanel : Form
    {
        public BuyerPanel()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ProductID.Text = "";
            Description.Text = "";
        }

        private void BuyerPanel_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'a2ZDataSet8.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.a2ZDataSet8.Products);
            // TODO: This line of code loads data into the 'a2ZDataSet7.Review' table. You can move, or remove it, as needed.
            this.reviewTableAdapter1.Fill(this.a2ZDataSet7.Review);
            // TODO: This line of code loads data into the 'a2ZDataSet6.Review' table. You can move, or remove it, as needed.
            this.reviewTableAdapter.Fill(this.a2ZDataSet6.Review);

        }

        private void ProductID_TextChanged(object sender, EventArgs e)
        {

        }

        private void Description_TextChanged(object sender, EventArgs e)
        {

        }

        private void Upload_Click(object sender, EventArgs e)
        {
            string productIdText = ProductID.Text;
            string description = Description.Text;

            if (string.IsNullOrEmpty(productIdText) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please enter both Product ID and Description.");
                return;
            }

            if (!int.TryParse(productIdText, out int productId))
            {
                MessageBox.Show("Product ID must be a valid integer.");
                return;
            }

            // Check if productId is positive
            if (productId <= 0)
            {
                MessageBox.Show("Product ID must be a positive integer.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-O06QAG9\\SQLEXPRESS;Initial Catalog=A2Z;Integrated Security=True;"))
                {
                    connection.Open();
                    string query = "INSERT INTO Review (product_id, description) VALUES (@ProductId, @Description)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Description", description);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product uploaded successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)//payment amount
        {

        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {
            string category = SearchBox.Text.Trim();

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-O06QAG9\\SQLEXPRESS;Initial Catalog=A2Z;Integrated Security=True;"))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products WHERE Category LIKE @Category";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Category", "%" + category + "%");
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the DataTable to a DataGridView or display the results in any other suitable control
                        dataGridView2.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void CHECKOUT_Click(object sender, EventArgs e)
        {
            // Retrieve the entered payment amount
            string paymentAmountText = guna2TextBox1.Text;

            // Display the payment amount as a message and wait for user confirmation
            DialogResult result = MessageBox.Show($"Payment Amount: {paymentAmountText}\n\nClick OK to proceed to Payment2.", "Payment Confirmation", MessageBoxButtons.OKCancel);

            // Check if the user clicked OK
            if (result == DialogResult.OK)
            {
                // Redirect to Payment2 form
                Payment2 payment2Form = new Payment2();
                payment2Form.Show();

                // Optionally, you can hide the current form if needed
                this.Hide();
            }
        }



        private decimal CalculateTotalPaymentAmount()
        {
            decimal totalPaymentAmount = 0;

            // Iterate through the DataGridView to calculate the total payment amount
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                // Retrieve the price of the product from the DataGridView
                if (row.Cells["Price"].Value != null && decimal.TryParse(row.Cells["Price"].Value.ToString(), out decimal price))
                {
                    // Add the price to the total payment amount
                    totalPaymentAmount += price;
                }
            }

            return totalPaymentAmount;
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            this.Hide();
            logIn.Show();
        }
    }
}
