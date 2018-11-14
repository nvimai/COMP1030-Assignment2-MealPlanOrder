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
        //The format currency
        static CultureInfo cultureInfo = new CultureInfo("en-CA");

        //The object class for the dish
        public class DishInfo
        {
            public string name { get; set; }
            public decimal prise { get; set; }
            public string type { get; set; }

            // The 
            public void inputInfo(string name, decimal prise)
            {
                this.name = name;
                this.prise = prise;
            }

            public string outputInfo()
            {
                return name + " - " + string.Format(cultureInfo, "{0:C}", prise);
            }
        }

        // List kind of meal
        public List<string> kindOfMeal = new List<string>() { "breakfast", "lunch", "dinner" };

        //Tax
        public static decimal tax = 0;
        
        // The list of breakfast meal
        public List<DishInfo> breakfastMealList = new List<DishInfo>();
        
        // The list of lunch meal
        public List<DishInfo> lunchMealList = new List<DishInfo>();

        // The list of dinner meal
        public List<DishInfo> dinnerMealList = new List<DishInfo>();

        public MealPlanOrder()
        {
            InitializeComponent();

            // Setup the list of meals for the menu
            setMenu();
        }

        //Set tax to 13%
        public void taxSetting()
        {
            tax = 13;
        }
        
        //Set the menu
        public void setMenu()
        {
            // Breakfast
            breakfastMealList.Add(new DishInfo() { name = "Bagel", prise = 3.95M , type = kindOfMeal[0] });
            breakfastMealList.Add(new DishInfo() { name = "Vegetarian Special", prise = 10.95M, type = kindOfMeal[0] });
            breakfastMealList.Add(new DishInfo() { name = "Protein Platter", prise = 11.95M, type = kindOfMeal[0] });

            // Display the menu into the list
            foreach (var temp in breakfastMealList)
            {
                cbbBreakfast.Items.Add(temp.outputInfo());
            }

            // Lunch
            lunchMealList.Add(new DishInfo() { name = "Bagel", prise = 3.95M, type = kindOfMeal[1] });
            lunchMealList.Add(new DishInfo() { name = "Vegetarian Special", prise = 10.95M, type = kindOfMeal[1] });
            lunchMealList.Add(new DishInfo() { name = "Protein Platter", prise = 11.95M, type = kindOfMeal[1] });

            // Display the menu into the list
            foreach (var temp in lunchMealList)
            {
                cbbLunch.Items.Add(temp.outputInfo());
            }

            // Dinner
            dinnerMealList.Add(new DishInfo() { name = "Bagel", prise = 3.95M, type = kindOfMeal[2] });
            dinnerMealList.Add(new DishInfo() { name = "Vegetarian Special", prise = 10.95M, type = kindOfMeal[2] });
            dinnerMealList.Add(new DishInfo() { name = "Protein Platter", prise = 11.95M, type = kindOfMeal[2] });

            // Display the menu into the list
            foreach (var temp in dinnerMealList)
            {
                cbbDinner.Items.Add(temp.outputInfo());
            }
        }

        // validate all user inputs to ensure all meals have a selected option (check that the SelectedIndex property of each ComboBox is greater than -1)
        private bool IsSelected()
        {
            if(cbbBreakfast.SelectedIndex > -1 && cbbLunch.SelectedIndex > -1 && cbbDinner.SelectedIndex > -1)
                return true;
            return false;
        }

        //Numeric checking
        public static bool IsNumber(string stringTemp)
        {
            try
            {
                decimal temp = Convert.ToDecimal(stringTemp);
                return true;
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
            guestOrderList.Add(breakfastMealList[cbbBreakfast.SelectedIndex]);
            
            // Check number of the Breakfast Quantity
            lblBreakfastQuanReport.Text = "";
            if (!IsNumber(txtBreakfastQuantity.Text))
            {
                lblBreakfastQuanReport.Text = "Not a number!";
                lblBreakfastQuanReport.ForeColor = Color.Red;
                return;
            }
            decimal breakfastQuantity = Convert.ToDecimal(txtBreakfastQuantity.Text);

            // Check whether the meal was selected or not
            lblLunchTypeReport.Text = "";
            if (cbbLunch.SelectedIndex < 0)
            {
                lblLunchTypeReport.Text = "Invalid Meal Selection.";
                lblLunchTypeReport.ForeColor = Color.Red;
                return;
            }
            guestOrderList.Add(lunchMealList[cbbLunch.SelectedIndex]);
            
            // Check number of the Lunch Quantity
            lblLunchQuanReport.Text = "";
            if (!IsNumber(txtLunchQuantity.Text))
            {
                lblLunchQuanReport.Text = "Not a number!";
                lblLunchQuanReport.ForeColor = Color.Red;
                return;
            }
            decimal lunchQuantity = Convert.ToDecimal(txtLunchQuantity.Text);

            // Check whether the meal was selected or not
            lblDinnerTypeReport.Text = "";
            if (cbbDinner.SelectedIndex < 0)
            {
                lblDinnerTypeReport.Text = "Invalid Meal Selection.";
                lblDinnerTypeReport.ForeColor = Color.Red;
                return;
            }
            guestOrderList.Add(dinnerMealList[cbbDinner.SelectedIndex]);
            
            // Check number of the Dinner Quantity
            lblDinnerQuanReport.Text = "";
            if (!IsNumber(txtDinnerQuantity.Text))
            {
                lblDinnerQuanReport.Text = "Not a number!";
                lblDinnerQuanReport.ForeColor = Color.Red;
                return;
            }
            decimal dinnerQuantity = Convert.ToDecimal(txtDinnerQuantity.Text);

            // Calculate the subTotal
            decimal subTotal = breakfastQuantity * guestOrderList[0].prise + lunchQuantity * guestOrderList[1].prise + dinnerQuantity * guestOrderList[2].prise;
            lblSubtotal.Text = string.Format(cultureInfo, "{0:C}",subTotal);

            // Set tax
            taxSetting();
            lblTax.Text = Convert.ToString(tax) + "%";

            // Calculate the total
            decimal total = subTotal * (1 + tax / 100);
            lblTotal.Text = string.Format(cultureInfo, "{0:C}", total);
                
            
        }
    }
}
