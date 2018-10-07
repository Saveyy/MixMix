using MixMixWPF.BarServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MixMixWPF
{
    /// <summary>
    /// Interaction logic for StockDetails.xaml
    /// </summary>
    public partial class StockDetails : Window
    {
        private List<Stock> StocksToUpdate { get; set; } = new List<Stock>();
        private BarServiceClient barService = new BarServiceClient("barServiceTcp");
        private MainWindow mainwindow;
        private Bar bar;

        public StockDetails(object sender, int barId)
        {
            InitializeComponent();
            bar = barService.Find(barId);
            mainwindow = (MainWindow)sender;

            //Create initial lists
            List<Stock> barStocks = barService.GetAllStocks(bar.ID).ToList();
            List<Ingredient> allIngredients = barService.GetAllIngredients().ToList();
            Show_Stocks(barStocks);
            Show_Potential_Stocks(barStocks, allIngredients);
        }

        //Hjælpemetoder
        private void Show_Stocks(List<Stock> stocks)
        {
            List<Measurement> measurements = barService.GetAllMeasurements().ToList();

            //ALCOHOL foreach
            foreach (var stock in stocks.Where(x => x.Ingredient.Alch > 0))
            {
                //Textbox with Ingredient name
                TextBox textbox1 = new TextBox();
                textbox1.Text = stock.Ingredient.Name;
                textbox1.Padding = new Thickness(2);
                textbox1.Margin = new Thickness(0, 0, 0, 5);
                textbox1.IsReadOnly = true;
                alc_ingredient.Children.Add(textbox1);

                //Combox with Ingredient name
                ComboBox combo = new ComboBox();
                combo.DataContext = measurements;
                combo.Margin = new Thickness(0, 0, 0, 5);
                combo.Name = "measurement" + stock.Ingredient.Id;

                combo.SelectedValuePath = "Key";
                combo.DisplayMemberPath = "Value";

                foreach (var measurement in measurements)
                {
                    combo.Items.Add(new KeyValuePair<int, string>(measurement.Id, measurement.Type));
                }
                alc_measurement.Children.Add(combo);

                //Finds the "cl" option and selects it
                Measurement m = measurements.Where(x => x.Type == "cl").First();
                combo.SelectedValue = m.Id;

                //Textbox with quantity
                TextBox textbox = new TextBox();
                textbox.Name = "ing" + stock.Ingredient.Id;
                textbox.Text = stock.Quantity.ToString();
                textbox.Padding = new Thickness(2);
                textbox.Margin = new Thickness(0, 0, 0, 5);
                alc_quantity.Children.Add(textbox);
                textbox.TextChanged += textbox_ing_TextChanged;
            }

            //SOFT DRINKS foreach
            foreach (var stock in stocks.Where(x => x.Ingredient.Alch == 0 && x.Ingredient.Measurable == true))
            {
                //Textbox with Ingredient name
                TextBox textbox1 = new TextBox();
                textbox1.Text = stock.Ingredient.Name;
                textbox1.Padding = new Thickness(2);
                textbox1.Margin = new Thickness(0, 0, 0, 5);
                textbox1.IsReadOnly = true;
                soda_ingredient.Children.Add(textbox1);

                //Combox with Ingredient name
                ComboBox combo = new ComboBox();
                combo.DataContext = measurements;
                combo.Margin = new Thickness(0, 0, 0, 5);
                combo.Name = "measurement" + stock.Ingredient.Id;

                combo.SelectedValuePath = "Key";
                combo.DisplayMemberPath = "Value";

                foreach (var measurement in measurements)
                {
                    combo.Items.Add(new KeyValuePair<int, string>(measurement.Id, measurement.Type));
                }
                soda_measurement.Children.Add(combo);

                //Finds the "cl" option and selects it
                Measurement m = measurements.Where(x => x.Type == "cl").First();
                combo.SelectedValue = m.Id;

                //Textbox with quantity
                TextBox textbox = new TextBox();
                textbox.Name = "ing" + stock.Ingredient.Id;
                textbox.Text = stock.Quantity.ToString();
                textbox.Padding = new Thickness(2);
                textbox.Margin = new Thickness(0, 0, 0, 5);
                soda_quantity.Children.Add(textbox);
                textbox.TextChanged += textbox_ing_TextChanged;
            }
            //MISCELLANEOUS foreach
            foreach (var stock in stocks.Where(x => x.Ingredient.Measurable == false))
            {
                //Textbox with Ingredient name
                TextBox textbox1 = new TextBox();
                textbox1.Text = stock.Ingredient.Name;
                textbox1.Padding = new Thickness(2);
                textbox1.Margin = new Thickness(0, 0, 0, 5);
                textbox1.IsReadOnly = true;
                misc_ingredient.Children.Add(textbox1);

                //Textbox with quantity
                TextBox textbox = new TextBox();
                textbox.Name = "Ing" + stock.Ingredient.Id;
                textbox.Text = stock.Quantity.ToString();
                textbox.Padding = new Thickness(2);
                textbox.Margin = new Thickness(0, 0, 0, 5);
                misc_quantity.Children.Add(textbox);
                textbox.TextChanged += textbox_ing_TextChanged;
            }
        }

        //Knapper
        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.Activate();
            this.Close();
        }

        //TODO Update doesnt calculate with proportions
        private void button_updatestock_Click(object sender, RoutedEventArgs e)
        {
            if (StocksToUpdate.Count == 0)
            {
                MessageBox.Show("Der blev ikke registreret nogle ændringer i lageret.");
            }
            else
            {
                bool result = barService.UpdateStock(StocksToUpdate.ToArray(), bar.ID);
                if (result)
                {
                    MessageBox.Show("Lageret blev opdateret!");
                    mainwindow.Activate();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Noget gik galt");
                }
            }
        }

        /// <summary>
        /// Event that is triggered when a quantity has been modidified.
        /// The event will save information about the given Stock being modified
        /// and will save these information to the internal collaction "StocksToBeUpdated".
        /// </summary>
        /// <param name="sender">The object that was changed - TextBox</param>
        /// <param name="e"></param>
        private void textbox_ing_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Convert the quantity from the string of the name of the TextBox - 
            // Can't do simple Convert.toint32(Textbox.name) - As an empty box will
            // cause an exception to be thrown - This simple converts empty box into the value 0
            int quantity = string.IsNullOrEmpty((sender as TextBox).Text) ?
                    0 : Convert.ToInt32((sender as TextBox).Text);
            // Strip the Box.Name of the ing part to the Ingredient id is left
            int IngredientID = Convert.ToInt32((sender as TextBox).Name.ToLower().Replace("ing", ""));

            //Find ingredient
            Ingredient ingredient = barService.FindIngredient(IngredientID);

            //Find measurementId
            string searchCriteria1 = "measurement" + IngredientID;
            ComboBox measurementComboBox = LogicalTreeHelper.FindLogicalNode(InStockGrid, searchCriteria1) as ComboBox;
            int measurementId = Convert.ToInt32(measurementComboBox.SelectedValue.ToString());
            Measurement measurement = barService.FindMeasurement(measurementId);

            // Use search pattern to look through collection
            bool isInCollection = false;
            int indexer = 0;
            // Check all stocks in Internal list of stocks to be updated 
            // to see if the stock is already there to avoid the same 
            // Stock being added multible times
            while (indexer < StocksToUpdate.Count && !isInCollection)
            {
                StocksToUpdate[indexer].Ingredient = ingredient;
                // if the stock is already there modify that stocks quantity with the new value
                if (StocksToUpdate[indexer].Ingredient.Id == IngredientID)
                {
                    //Break while loop with isInCollection, as the object has been found in the collection
                    isInCollection = true;
                    //Modify the objects quantity with the new quantity
                    if (StocksToUpdate[indexer].Ingredient.Measurable)
                    {
                        StocksToUpdate[indexer].Quantity = quantity * measurement.Proportion;
                    } 
                    else
                    {
                        StocksToUpdate[indexer].Quantity = quantity;
                    }
                    
                }

                indexer++;
            }
            // If the stock wasn't in the List already, create new stock and add it to the 
            // internal list of Stocks to be updated
            if (!isInCollection)
            {
                Stock s = new Stock();

                s.Ingredient = ingredient;
                if (s.Ingredient.Measurable)
                {
                    s.Quantity = quantity * measurement.Proportion;
                }
                else
                {
                    s.Quantity = quantity;
                }
                

                StocksToUpdate.Add(s);
            }

        }

        private void Show_Potential_Stocks(List<Stock> stocks, List<Ingredient> ingredients)
        {
            ingredients.RemoveAll(x => stocks.Any(y => y.Ingredient.Id == x.Id));
            List<Measurement> measurements = barService.GetAllMeasurements().ToList();

            //ALCOHOL foreach
            foreach (var ingredient in ingredients.Where(x => x.Alch > 0))
            {
                //Textbox with Ingredient name
                TextBox textbox1 = new TextBox();
                textbox1.Text = ingredient.Name;
                textbox1.Padding = new Thickness(2);
                textbox1.Margin = new Thickness(0, 0, 0, 5);
                textbox1.IsReadOnly = true;
                textbox1.Name = "name" + ingredient.Id;
                alc_add_ingredient.Children.Add(textbox1);

                //Combox with Ingredient name
                ComboBox combo = new ComboBox();
                combo.DataContext = measurements;
                combo.Margin = new Thickness(0, 0, 0, 5);
                combo.Name = "measurement" + ingredient.Id;

                combo.SelectedValuePath = "Key";
                combo.DisplayMemberPath = "Value";

                foreach (var measurement in measurements)
                {
                    combo.Items.Add(new KeyValuePair<int, string>(measurement.Id, measurement.Type));
                }
                alc_add_measurement.Children.Add(combo);

                //Finds the "cl" option and selects it
                Measurement m = measurements.Where(x => x.Type == "cl").First();
                combo.SelectedValue = m.Id;

                //Textbox with quantity
                TextBox textbox = new TextBox();
                textbox.Name = "quantity" + ingredient.Id;
                textbox.Padding = new Thickness(2);
                textbox.Margin = new Thickness(0, 0, 0, 5);
                alc_add_quantity.Children.Add(textbox);

                //Add ingredient button
                Button button = new Button();
                button.Content = "+";
                button.Padding = new Thickness(2);
                button.Margin = new Thickness(0, 0, 0, 5);
                button.Name = "ing" + ingredient.Id;
                alc_add_button.Children.Add(button);
                button.Click += addStock;

            }

            //SOFT DRINKS foreach
            foreach (var ingredient in ingredients.Where(x => x.Alch == 0 && x.Measurable == true))
            {
                //Textbox with Ingredient name
                TextBox textbox1 = new TextBox();
                textbox1.Text = ingredient.Name;
                textbox1.Padding = new Thickness(2);
                textbox1.Margin = new Thickness(0, 0, 0, 5);
                textbox1.IsReadOnly = true;
                textbox1.Name = "name" + ingredient.Id;
                soda_add_ingredient.Children.Add(textbox1);

                //Combox with Ingredient name
                ComboBox combo = new ComboBox();
                combo.DataContext = measurements;
                combo.Margin = new Thickness(0, 0, 0, 5);
                combo.Name = "measurement" + ingredient.Id;

                combo.SelectedValuePath = "Key";
                combo.DisplayMemberPath = "Value";

                foreach (var measurement in measurements)
                {
                    combo.Items.Add(new KeyValuePair<int, string>(measurement.Id, measurement.Type));
                }
                soda_add_measurement.Children.Add(combo);

                //Finds the "cl" option and selects it
                Measurement m = measurements.Where(x => x.Type == "cl").First();
                combo.SelectedValue = m.Id;

                //Textbox with quantity
                TextBox textbox = new TextBox();
                textbox.Name = "quantity" + ingredient.Id;
                textbox.Padding = new Thickness(2);
                textbox.Margin = new Thickness(0, 0, 0, 5);
                soda_add_quantity.Children.Add(textbox);

                //Add ingredient button
                Button button = new Button();
                button.Content = "+";
                button.Padding = new Thickness(2);
                button.Margin = new Thickness(0, 0, 0, 5);
                button.Name = "ing" + ingredient.Id;
                soda_add_button.Children.Add(button);
                button.Click += addStock;
            }
            //MISCELLANEOUS foreach
            foreach (var ingredient in ingredients.Where(x => x.Measurable == false))
            {
                //Textbox with Ingredient name
                TextBox textbox1 = new TextBox();
                textbox1.Text = ingredient.Name;
                textbox1.Padding = new Thickness(2);
                textbox1.Margin = new Thickness(0, 0, 0, 5);
                textbox1.IsReadOnly = true;
                textbox1.Name = "name" + ingredient.Id;
                misc_add_ingredient.Children.Add(textbox1);

                //Textbox with quantity
                TextBox textbox = new TextBox();
                textbox.Name = "quantity" + ingredient.Id;
                textbox.Padding = new Thickness(2);
                textbox.Margin = new Thickness(0, 0, 0, 5);
                misc_add_quantity.Children.Add(textbox);

                //Add ingredient button
                Button button = new Button();
                button.Content = "+";
                button.Padding = new Thickness(2);
                button.Margin = new Thickness(0, 0, 0, 5);
                button.Name = "ing" + ingredient.Id;
                misc_add_button.Children.Add(button);
                button.Click += addStock;
            }
        }

        private void addStock(object sender, RoutedEventArgs e)
        {
            //Find IngredientId via the sender (as a button)s name 
            int ingredientID = Convert.ToInt32((sender as Button).Name.ToLower().Replace("ing", ""));

            //Find ingredient for later use
            Ingredient ingredient = barService.FindIngredient(ingredientID);

            //Create searchCriteria, which is then used in getting the quantityField with the right ingredientId
            string searchCriteria = "quantity" + ingredientID;
            TextBox quantityTextBox = LogicalTreeHelper.FindLogicalNode(AddStockGrid, searchCriteria) as TextBox;
            int quantity = Convert.ToInt32(quantityTextBox.Text);

            //If its measureble we make the stock with measurements calculations
            if (ingredient.Measurable)
            {
                //Find measurementId
                string searchCriteria1 = "measurement" + ingredientID;
                ComboBox measurementComboBox = LogicalTreeHelper.FindLogicalNode(AddStockGrid, searchCriteria1) as ComboBox;
                int measurementId = Convert.ToInt32(measurementComboBox.SelectedValue.ToString());

                //Create the stock belonging to the current bar
                barService.CreateStock(bar.ID, quantity, ingredient, measurementId);
            }
            else
            {
                //Create the stock belonging to the current bar
                barService.CreateNonMeasurableStock(bar.ID, quantity, ingredient);
            }

            //Refresh both lists
            clearStocksList();
            clearIngredientsList();
            List<Stock> stocks = barService.GetAllStocks(bar.ID).ToList();
            List<Ingredient> ingredients = barService.GetAllIngredients().ToList();
            Show_Stocks(stocks);
            Show_Potential_Stocks(stocks, ingredients);
        }

        //This makes the UI very slow with many fields, maybe look at DataGrid or other controls
        private void clearIngredientsList()
        {
            //Clear alcohol
            alc_add_ingredient.Children.Clear();
            alc_add_quantity.Children.Clear();
            alc_add_measurement.Children.Clear();
            alc_add_button.Children.Clear();

            //Clear soda
            soda_add_ingredient.Children.Clear();
            soda_add_quantity.Children.Clear();
            soda_add_measurement.Children.Clear();
            soda_add_button.Children.Clear();

            //Clear misc
            misc_add_ingredient.Children.Clear();
            misc_add_quantity.Children.Clear();
            misc_add_button.Children.Clear();
        }

        private void clearStocksList()
        {
            //Clear alcohol
            alc_ingredient.Children.Clear();
            alc_quantity.Children.Clear();
            alc_measurement.Children.Clear();

            //Clear sodas
            soda_ingredient.Children.Clear();
            soda_quantity.Children.Clear();
            soda_measurement.Children.Clear();

            //Clear misc
            misc_ingredient.Children.Clear();
            misc_quantity.Children.Clear();
        }

        private void searchStocks_Button_Click(object sender, RoutedEventArgs e)
        {
            string searchString = searchStocks.Text.ToLower();
            if (searchString == "")
            {
                MessageBox.Show("Please type something before searching");
            }
            else
            {
                List<Stock> stocks = barService.GetAllStocks(bar.ID).Where(x => x.Ingredient.Name.ToLower().Contains(searchString)).ToList();
                clearStocksList();
                Show_Stocks(stocks);
                searchStocks.Text = "";
            }
        }

        private void searchIngredients_Button_Click(object sender, RoutedEventArgs e)
        {
            string searchString = searchIngredients.Text.ToLower();
            if (searchString == "")
            {
                MessageBox.Show("Please type something before searching");
            }
            else
            {
                List<Ingredient> ingredients = barService.GetAllIngredients().Where(x => x.Name.ToLower().Contains(searchString)).ToList();
                List<Stock> stocks = barService.GetAllStocks(bar.ID).ToList();
                clearIngredientsList();
                Show_Potential_Stocks(stocks, ingredients);
                searchIngredients.Text = "";
            }

        }

        private void restoreIngredientsList_Button_Click(object sender, RoutedEventArgs e)
        {
            clearIngredientsList();
            List<Stock> stocks = barService.GetAllStocks(bar.ID).ToList();
            List<Ingredient> ingredients = barService.GetAllIngredients().ToList();
            Show_Potential_Stocks(stocks, ingredients);
        }

        private void restoreStocksList_Button_Click(object sender, RoutedEventArgs e)
        {
            clearStocksList();
            List<Stock> stocks = barService.GetAllStocks(bar.ID).ToList();
            Show_Stocks(stocks);
        }
    }
}