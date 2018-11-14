using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MealPlanOrder
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Get the inputted username and password
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            //Validate the username and password
            if (validateLogin(username, password)== false)
            {
                //If either username or password is wrong: Show the message and return;
                lblReport.Text = "Incorrect username or password. Please try again!";
                lblReport.ForeColor = Color.Red;
                return;
            }

            //If both username and password are correct: Call "Calculate" Form and show it as a Dialog
            lblReport.Text = "The username and password are correct. Keep going!";
            lblReport.ForeColor = Color.Green;
            MealPlanOrder mealOrderForm = new MealPlanOrder();
            mealOrderForm.ShowDialog();
        }
        //Validate the login information
        private bool validateLogin(string username, string password)
        {
            try
            {
                //The right username and password
                string username_org = "nvimai";
                string password_org = "123456";

                //Compare the inputted username and password with the original ones
                if(username == username_org && password == password_org)
                    return true;
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
