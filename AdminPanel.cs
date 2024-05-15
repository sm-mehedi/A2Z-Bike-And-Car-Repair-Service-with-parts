using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace A2Z_Bike_And_Car_Repair_Service_with_parts
{
    public partial class AdminPanel : Form
    {
        private string currentUserName;
        private string connectionString = @"Data Source=DESKTOP-O06QAG9\SQLEXPRESS;Initial Catalog=A2Z;Integrated Security=True;";

        public AdminPanel(string userName)
        {
            InitializeComponent();
            currentUserName = userName;
            User.Text = "Welcome, " + userName;
            ShowTotalUsers();
            ShowAdminCount();
            ShowTotalUsers1();
            ShowSellerCount();
            ShowTotalRevenue();
            SqlConnection c = new SqlConnection(@"Data Source = DESKTOP - O06QAG9\SQLEXPRESS; Initial Catalog = A2Z; Integrated Security = True; ");
        }

        public void SetUserName(string userName)
        {
            currentUserName = userName;
            User.Text = "Welcome, " + userName;
        }

        private void ShowTotalUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserTable", connection))
                    {
                        int totalUsers = (int)command.ExecuteScalar();
                        label3.Text = $"{totalUsers}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching total users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void User_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Current user: " + currentUserName);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Current user: " + currentUserName);
        }

        private void TotalUser_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void ShowAdminCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserTable WHERE UserType = 'Admin'", connection))
                    {
                        int adminCount = (int)command.ExecuteScalar();
                        label4.Text = $"Number of Admin Users: {adminCount}"; // Update the label text
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching admin user count: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowTotalUsers1()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserTable WHERE UserType = 'Buyer'", connection))
                    {
                        int buyerCount = (int)command.ExecuteScalar();
                        label1.Text = $"Number of Buyer: {buyerCount}"; // Update label1 text directly with the count of buyers
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching total buyers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void ShowSellerCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserTable WHERE UserType = 'Seller'", connection))
                    {
                        int sellerCount = (int)command.ExecuteScalar();
                        label5.Text = $"Number of Seller: {sellerCount}"; // Update label5 text directly with the count of sellers
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching total sellers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void ShowTotalRevenue()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT SUM(PaymentAmount) FROM PaymentDetails", connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int totalRevenue = Convert.ToInt32(result);
                            label6.Text = $"Total Revenue: {totalRevenue}"; // Update label6 text directly with the total revenue
                        }
                        else
                        {
                            label6.Text = "Total Revenue: 0"; // If there are no payment details, display 0 as the total revenue
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching total revenue: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadUserData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT UserID, UserName, UserType, Gender FROM UserTable";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        guna2DataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'a2ZDataSet1.UserTable' table. You can move, or remove it, as needed.
            LoadUserData();

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = guna2DataGridView1.SelectedRows[0].Index;
                    int selectedUserID = Convert.ToInt32(guna2DataGridView1.Rows[selectedRowIndex].Cells["UserID"].Value);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string deleteQuery = "DELETE FROM UserTable WHERE UserID = @UserID";
                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserID", selectedUserID);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("User deleted successfully.");

                    // Refresh DataGridView after deletion
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            this.Hide();
            logIn.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            CheckReview checkReview = new CheckReview();
            checkReview.Show();
            this.Hide();
        }
    }
}
