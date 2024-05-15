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
using System.Data.SqlClient;

namespace A2Z_Bike_And_Car_Repair_Service_with_parts
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void EXITBUTTON_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            UsernameTextBox.Text = "";
            PasswordFieldTextBox.Text = "";
            guna2TextBox3.Text = "";
            guna2CustomRadioButton1.Checked=false;
            guna2CustomRadioButton2.Checked=false;
            guna2CustomRadioButton3.Checked=false;
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
          
        }




        private void Confirm_Click(object sender, EventArgs e)
        {
            string userInput = guna2TextBox3.Text.ToUpper();

            if (userInput != "BUYER" && userInput != "SELLER")
            {
                MessageBox.Show("Please enter either 'BUYER' or 'SELLER'.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=DESKTOP-O06QAG9\\SQLEXPRESS;Initial Catalog=A2Z;Integrated Security=True";
            string query = "INSERT INTO UserTable (UserName, Password, UserType, Gender) VALUES (@UserName, @Password, @UserType, @Gender); SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UsernameTextBox.Text);
                    command.Parameters.AddWithValue("@Password", PasswordFieldTextBox.Text);
                    command.Parameters.AddWithValue("@UserType", guna2TextBox3.Text);

                    string gender;
                    if (guna2CustomRadioButton1.Checked)
                        gender = "Male";
                    else if (guna2CustomRadioButton2.Checked)
                        gender = "Female";
                    else
                        gender = "Others";
                    command.Parameters.AddWithValue("@Gender", gender);

                    try
                    {
                        connection.Open();
                        // Execute the query to insert user and retrieve the generated UserID
                        int userId = Convert.ToInt32(command.ExecuteScalar());

                        if (userInput == "SELLER")
                        {
                            
                            SubscriptionForm subscriptionForm = new SubscriptionForm(userId);
                            subscriptionForm.ShowDialog();
                            this.Hide();
                        }
                        else if (userInput == "BUYER")
                        {

                            MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            LogIn logIn = new LogIn();
                            logIn.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


    }
}
