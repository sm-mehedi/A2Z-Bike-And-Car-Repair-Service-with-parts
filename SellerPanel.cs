using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static A2Z_Bike_And_Car_Repair_Service_with_parts.LogIn;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Guna.UI2.WinForms;
namespace A2Z_Bike_And_Car_Repair_Service_with_parts
{
    public partial class SellerPanel : Form
    {
        private SqlConnection con;
        private int productId; 
        public SellerPanel(string userName)
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=DESKTOP-O06QAG9\\SQLEXPRESS;Initial Catalog=A2Z;Integrated Security=True;");

        }

        
        private void SellerPanel_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'a2ZDataSet5.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter1.Fill(this.a2ZDataSet5.Products);
            // TODO: This line of code loads data into the 'a2ZDataSet4.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.a2ZDataSet4.Products);

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)//product id
        {

        }

        private void Item_Name_TextChanged(object sender, EventArgs e)//item name
        {

        }

        private void Description_TextChanged(object sender, EventArgs e)//description
        {

        }

        private void Category_TextChanged(object sender, EventArgs e)//category
        {

        }

        private void Insert_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int productId;
                decimal price;
                int quantity;

                if (!int.TryParse(guna2TextBox1.Text, out productId))
                {
                    MessageBox.Show("Product ID must be a valid integer.");
                    return;
                }

                if (!decimal.TryParse(PricetEXTbox.Text, out price))
                {
                    MessageBox.Show("Price must be a valid decimal number.");
                    return;
                }

                if (!int.TryParse(Quantity.Text, out quantity))
                {
                    MessageBox.Show("Quantity must be a valid integer.");
                    return;
                }

                string query = "INSERT INTO Products (ProductID, Category, ItemName, Description, Price, Quantity) VALUES (@ProductID, @Category, @ItemName, @Description, @Price, @Quantity)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.Parameters.AddWithValue("@Category", Category.Text);
                command.Parameters.AddWithValue("@ItemName", Item_Name.Text);
                command.Parameters.AddWithValue("@Description", Description.Text);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.ExecuteNonQuery();

                MessageBox.Show("SUCCESSFULLY INSERTED");
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                guna2TextBox1.Text = ""; // Clear guna2TextBox1 textbox
                Quantity.Text = ""; // Clear Quantity textbox
            }
        }



        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from Products ", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            guna2DataGridView1.DataSource = dt;
        }


        private void DELETE_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    con.Open();
                    int productId;

                    if (!int.TryParse(guna2TextBox1.Text, out productId))
                    {
                        MessageBox.Show("Product ID must be a valid integer.");
                        return;
                    }

                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@ProductID", guna2TextBox1.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("SUCCESSFULLY DELETED");
                    BindData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    guna2TextBox1.Text = ""; // Clear guna2TextBox1 textbox
                    Quantity.Text = ""; // Clear Quantity textbox
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }



        private void UPDATE_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    con.Open();
                    int productId;
                    decimal price;
                    int quantity;

                    if (!int.TryParse(guna2TextBox1.Text, out productId))
                    {
                        MessageBox.Show("Product ID must be a valid integer.");
                        return;
                    }

                    if (!decimal.TryParse(PricetEXTbox.Text, out price))
                    {
                        MessageBox.Show("Price must be a valid decimal number.");
                        return;
                    }

                    if (!int.TryParse(Quantity.Text, out quantity))
                    {
                        MessageBox.Show("Quantity must be a valid integer.");
                        return;
                    }

                    string query = "UPDATE Products SET Category = @Category, ItemName = @ItemName, Description = @Description, Price = @Price, Quantity = @Quantity WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Category", Category.Text);
                    command.Parameters.AddWithValue("@ItemName", Item_Name.Text);
                    command.Parameters.AddWithValue("@Description", Description.Text);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.ExecuteNonQuery();

                    MessageBox.Show("SUCCESSFULLY UPDATED");
                    BindData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    guna2TextBox1.Text = ""; // Clear guna2TextBox1 textbox
                    Quantity.Text = ""; // Clear Quantity textbox
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update.");
            }
        }



        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            this.Hide();
            logIn.Show();
        }

        private void PricetEXTbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Quantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void RESET_Click(object sender, EventArgs e)
        {
            PricetEXTbox.Text = string.Empty;
            Description.Text = string.Empty;
            Category.Text = string.Empty;
            Quantity.Text= string.Empty;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            CheckReview n = new CheckReview();
            n.Show();
            this.Hide();
        }
    }
}
