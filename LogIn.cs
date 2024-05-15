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
    public partial class LogIn : Form
    {
        private string connectionString = @"Data Source=DESKTOP-O06QAG9\SQLEXPRESS;Initial Catalog=A2Z;Integrated Security=True;";

        public class User
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string UserType { get; set; }
        }
        public LogIn()
        {
            InitializeComponent();
        }

        private User GetUserDetailsFromDatabase(string username)
        {
            User user = null;
            string query = "SELECT * FROM UserTable WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                UserName = Convert.ToString(reader["UserName"]),
                                Password = Convert.ToString(reader["Password"]),
                                UserType = Convert.ToString(reader["UserType"])
                            };
                        }
                    }
                }
            }

            return user;
        }

        /* private DateTime GetSubscriptionEndDate(int userId)
         {
             DateTime subscriptionEndDate = DateTime.MinValue;
             string query = "SELECT EndDate FROM Subscription WHERE UserID = @UserID";

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 using (SqlCommand command = new SqlCommand(query, connection))
                 {
                     command.Parameters.AddWithValue("@UserID", userId);
                     connection.Open();

                     object result = command.ExecuteScalar();
                     if (result != null && result != DBNull.Value)
                     {
                         subscriptionEndDate = Convert.ToDateTime(result);
                     }
                 }
             }

             return subscriptionEndDate;
         }
        */



        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.ToUpper();
            string password = txtPass.Text.ToUpper();

            User userDetails = GetUserDetailsFromDatabase(username);

            // Debug messages to check retrieved user details
            if (userDetails != null)
            {
                Console.WriteLine($"Retrieved user details: Username = {userDetails.UserName}, Password = {userDetails.Password}, UserType = {userDetails.UserType}");
            }
            else
            {
                Console.WriteLine("User details not found.");
            }

            // Check if user details retrieved and password matches
            if (userDetails != null && userDetails.Password == password)
            {
                if (userDetails.UserType == "BUYER")
                {
                    MessageBox.Show("Login successful as Buyer!");
                    BuyerPanel buyerPanel = new BuyerPanel();
                    buyerPanel.ShowDialog();
                    this.Hide();
                }
                else if (userDetails.UserType == "SELLER")
                {
                    MessageBox.Show("Login successful as Seller!");
                    SellerPanel sellerPanel = new SellerPanel(username);
                    this.Hide();
                    sellerPanel.Show();
                }
                else if (userDetails.UserType == "ADMIN")
                {
                    MessageBox.Show("Login successful as Admin!");
                    AdminPanel adminPanel = new AdminPanel(username); // Pass the username to the constructor
                    adminPanel.Show(); // Or adminPanel.ShowDialog() if you want to show it as a dialog
                }
            }
            else
            {
                MessageBox.Show("Incorrect username or password. Please try again.");
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            
            string termsAndServicesText = "Welcome to A2Z Car/Bike Repair Service and Parts.\n\n" +
                                          "By using our platform, you agree to abide by the following terms and conditions:\n\n" +
                                          "- Our platform facilitates the booking of repair services for both cars and bikes.\n" +
                                          "- We also offer a variety of parts for sale through our platform.\n" +
                                          "- Users must provide accurate information when booking services or purchasing parts.\n" +
                                          "- A2Z Car/Bike Repair Service and Parts is not responsible for any inaccuracies in user-provided information.\n" +
                                          "- Users are responsible for the safety and security of their accounts.\n" +
                                          "- Any unauthorized use of accounts should be reported immediately.\n" +
                                          "- Our platform reserves the right to modify or discontinue services at any time without prior notice.\n" +
                                          "- Users are expected to treat service providers and other users with respect and professionalism.\n" +
                                          "- Any misuse of our platform or violation of these terms may result in account suspension or termination.\n" +
                                          "- By continuing to use our platform, you acknowledge and agree to these terms and conditions.\n\n" +
                                          "Do you agree to the terms and conditions?";

           
            DialogResult result = MessageBox.Show(termsAndServicesText, "Terms and Services",
                                                   MessageBoxButtons.OKCancel,
                                                   MessageBoxIcon.Information);

           
            if (result == DialogResult.OK)
            {
                Signup signup = new Signup();
                this.Hide();
                signup.ShowDialog(); 
            }
           
            else
            {
                
            }
        }

    }
}
