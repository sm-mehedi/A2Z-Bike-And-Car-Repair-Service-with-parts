using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A2Z_Bike_And_Car_Repair_Service_with_parts
{
    public partial class Payment2 : Form
    {
        public Payment2()
        {
            InitializeComponent();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            BuyerPanel buyerPanel = new BuyerPanel();
            buyerPanel.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payment complete");

            // Reset the text boxes
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
