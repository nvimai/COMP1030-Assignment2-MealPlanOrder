using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace MealPlanOrder
{
    public partial class MealPlanOrder : Form
    {
        // The format currency
        static CultureInfo cultureInfo = new CultureInfo("en-CA");

        //The object class for the dish
        public class DishInfo
        {
            // Name of the Dish
            public string name { get; set; }

            // The price of the Dish
            public decimal price { get; set; }

            // The type of the dish: Breakfast, Lunch, Dinner, ...
            public string type { get; set; }

            // The quantity of the dish
            public int quantity { get; set; }
            
            public void inputInfo(string name, decimal price, string type = "", int quantity = 1)
            {
                this.name = name;
                this.price = price;
                this.type = type;
                this.quantity = quantity;
            }

            public string outputInfo()
            {
                return name + " - " + string.Format(cultureInfo, "{0:C}", price);
            }
        }

        // List kind of meal
        public List<string> kindOfMeal = new List<string>() { "breakfast", "lunch", "dinner" };

        // Tax
        public static decimal taxPercentage = 0;
        
        // The list of breakfast meal
        public List<DishInfo> breakfastMealList = new List<DishInfo>();
        
        // The list of lunch meal
        public List<DishInfo> lunchMealList = new List<DishInfo>();

        // The list of dinner meal
        public List<DishInfo> dinnerMealList = new List<DishInfo>();

        // Launch the form
        public MealPlanOrder()
        {
            InitializeComponent();

            // Setup the list of meals for the menu
            setMenu();
        }

        // Calculate the Tax
        public decimal TaxCalculator(decimal subTotal)
        {
            // Set the Tax Percentage to 13%
            taxPercentage = 13;
            return (subTotal * taxPercentage / 100);
        }
        
        //Set the menu
        public void setMenu()
        {
            // Breakfast
            breakfastMealList.Add(new DishInfo() { name = "Bagel", price = 3.95M , type = kindOfMeal[0], quantity = 10 });
            breakfastMealList.Add(new DishInfo() { name = "Vegetarian Special", price = 10.95M, type = kindOfMeal[0], quantity = 10 });
            breakfastMealList.Add(new DishInfo() { name = "Protein Platter", price = 11.95M, type = kindOfMeal[0], quantity = 10 });

            // Display the menu into the list
            foreach (var temp in breakfastMealList)
            {
                cbbBreakfast.Items.Add(temp.outputInfo());
            }

            // Lunch
            lunchMealList.Add(new DishInfo() { name = "Bagel", price = 3.95M, type = kindOfMeal[1], quantity = 10 });
            lunchMealList.Add(new DishInfo() { name = "Vegetarian Special", price = 10.95M, type = kindOfMeal[1], quantity = 10 });
            lunchMealList.Add(new DishInfo() { name = "Protein Platter", price = 11.95M, type = kindOfMeal[1], quantity = 10 });

            // Display the menu into the list
            foreach (var temp in lunchMealList)
            {
                cbbLunch.Items.Add(temp.outputInfo());
            }

            // Dinner
            dinnerMealList.Add(new DishInfo() { name = "Bagel", price = 3.95M, type = kindOfMeal[2], quantity = 10 });
            dinnerMealList.Add(new DishInfo() { name = "Vegetarian Special", price = 10.95M, type = kindOfMeal[2], quantity = 10 });
            dinnerMealList.Add(new DishInfo() { name = "Protein Platter", price = 11.95M, type = kindOfMeal[2], quantity = 10 });

            // Display the menu into the list
            foreach (var temp in dinnerMealList)
            {
                cbbDinner.Items.Add(temp.outputInfo());
            }
        }

        // Quantity checking
        public static bool IsValidQuantity(string stringTemp)
        {
            try
            {
                // Declare the number temp and Convert the input value
                // If the input value is not a number will goto the catch and return false
                decimal temp = Convert.ToInt32(stringTemp);

                // If the temp is a number and a valid quantity then return true
                if(temp >= 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnOrderNow_Click(object sender, EventArgs e)
        {
            // Get the list of meals from guest by the order
            List<DishInfo> guestOrderList = new List<DishInfo>();
            guestOrderList.Clear();

            // Check whether the meal was selected or not
            lblBreakfastTypeReport.Text = "";
            if (cbbBreakfast.SelectedIndex < 0)
            {
                lblBreakfastTypeReport.Text = "Invalid Meal Selection.";
                lblBreakfastTypeReport.ForeColor = Color.Red;
                return;
            }

            // Add the dish into the Guest Order List
            guestOrderList.Add(breakfastMealList[cbbBreakfast.SelectedIndex]);
            
            // Check number of the Breakfast Quantity
            lblBreakfastQuanReport.Text = "";
            if (!IsValidQuantity(txtBreakfastQuantity.Text))
            {
                lblBreakfastQuanReport.Text = "Invalid number!";
                lblBreakfastQuanReport.ForeColor = Color.Red;
                return;
            }

            // Set the quantity of the ordered dish into the Guest Order List
            guestOrderList.Last().quantity = Convert.ToInt32(txtBreakfastQuantity.Text);

            // Check whether the meal was selected or not
            lblLunchTypeReport.Text = "";
            if (cbbLunch.SelectedIndex < 0)
            {
                lblLunchTypeReport.Text = "Invalid Meal Selection.";
                lblLunchTypeReport.ForeColor = Color.Red;
                return;
            }

            // Add the dish into the Guest Order List
            guestOrderList.Add(lunchMealList[cbbLunch.SelectedIndex]);
            
            // Check number of the Lunch Quantity
            lblLunchQuanReport.Text = "";
            if (!IsValidQuantity(txtLunchQuantity.Text))
            {
                lblLunchQuanReport.Text = "Invalid number!";
                lblLunchQuanReport.ForeColor = Color.Red;
                return;
            }

            // Set the quantity of the ordered dish into the Guest Order List
            guestOrderList.Last().quantity = Convert.ToInt32(txtLunchQuantity.Text);

            // Check whether the meal was selected or not
            lblDinnerTypeReport.Text = "";
            if (cbbDinner.SelectedIndex < 0)
            {
                lblDinnerTypeReport.Text = "Invalid Meal Selection.";
                lblDinnerTypeReport.ForeColor = Color.Red;
                return;
            }

            // Add the dish into the Guest Order List
            guestOrderList.Add(dinnerMealList[cbbDinner.SelectedIndex]);
            
            // Check number of the Dinner Quantity
            lblDinnerQuanReport.Text = "";
            if (!IsValidQuantity(txtDinnerQuantity.Text))
            {
                lblDinnerQuanReport.Text = "Invalid number!";
                lblDinnerQuanReport.ForeColor = Color.Red;
                return;
            }

            // Set the quantity of the ordered dish into the Guest Order List
            guestOrderList.Last().quantity = Convert.ToInt32(txtDinnerQuantity.Text);

            // Declare the Sub Total
            decimal subTotal = 0;
            
            // Calculate the subTotal
            for (int i = 0; i < guestOrderList.Count; i++)
            {
                subTotal += guestOrderList[i].price * guestOrderList[i].quantity;
            }

            // Display the Sub Total with the right format
            lblSubTotal.Text = string.Format(cultureInfo, "{0:C}",subTotal);

            // Calculate the Tax
            decimal tax = TaxCalculator(subTotal);

            // Display the Tax with the right format
            lblTax.Text = string.Format(cultureInfo, "{0:C}", tax);

            // Calculate the Total
            decimal total = subTotal + tax;

            // Display the Total with the right format
            lblTotal.Text = string.Format(cultureInfo, "{0:C}", total);
                
        }
    }
}
