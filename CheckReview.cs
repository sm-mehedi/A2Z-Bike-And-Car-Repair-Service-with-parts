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
    public partial class CheckReview : Form
    {
        public CheckReview()
        {
            InitializeComponent();
        }

        private void CheckReview_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'a2ZDataSet9.Review' table. You can move, or remove it, as needed.
            this.reviewTableAdapter.Fill(this.a2ZDataSet9.Review);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            this.Hide();
            logIn.Show();
        }
    }
}
