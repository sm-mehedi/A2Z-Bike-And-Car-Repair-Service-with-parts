using System;
using System.Windows.Forms;

namespace A2Z_Bike_And_Car_Repair_Service_with_parts
{
    public partial class SubscriptionForm : Form
    {
        private int userId;

        public SubscriptionForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            this.Hide();
            signup.Show();
        }

        private void Exit_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Buy3_Click(object sender, EventArgs e)
        {
            ShowSubscriptionMessage(3, 450);
        }

        private void Buy6_Click(object sender, EventArgs e)
        {
            ShowSubscriptionMessage(6, 750);
        }

        private void Buy1year_Click(object sender, EventArgs e)
        {
            ShowSubscriptionMessage(12, 1100);
        }
        private void label2_Click(object sender, EventArgs e)
        {
            // Your event handler code here
        }
        private void ShowSubscriptionMessage(int months, int amount)
        {
            DialogResult result = MessageBox.Show($"Pay {amount} TAKA for {months} months subscription?", "Subscription Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                OpenPaymentForm(amount);
            }
        }

        private void OpenPaymentForm(int amount)
        {
            PaymentGateway paymentForm = new PaymentGateway();

            // Subscribe to the PaymentCompleted event
            paymentForm.PaymentCompleted += PaymentForm_PaymentCompleted;

            paymentForm.Show();
            this.Hide();
        }

        private void PaymentForm_PaymentCompleted(object sender, EventArgs e)
        {
            // Payment completed, show the login form or perform other actions
            ShowLoginForm();
        }

        private void ShowLoginForm()
        {
            // Show the login form
            LogIn loginForm = new LogIn();
            loginForm.Show();
        }
    }
}
