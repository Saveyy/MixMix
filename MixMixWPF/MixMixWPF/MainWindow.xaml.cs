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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MixMixWPF.ManagerServiceReference;
using MixMixWPF.BarServiceReference;

namespace MixMixWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager loggedInManager;
        BarServiceClient barService = new BarServiceClient("barServiceTcp");
        public MainWindow(Manager manager)
        {
            InitializeComponent();
            loggedInManager = manager;
            label_username.Content = loggedInManager.Username;
            show_bars();
        }

        private void button_create_click(object sender, RoutedEventArgs e)
        {
            CreateBar bar = new CreateBar(this);
            bar.ShowDialog();
        }

        public void show_bars()
        {
            stackedpanel_bars.Children.Clear();
            List<BarServiceReference.Bar> bars = barService.GetBarsByManagerId(loggedInManager.Id).ToList();

            foreach (var bar in bars)
            {
                //Generer textboxe med barnavne
                TextBox barTextBox = new TextBox();
                barTextBox.Name = "bar_textbox_" + bar.ID;
                barTextBox.Text = bar.Name;
                barTextBox.IsReadOnly = true;
                barTextBox.Margin = new Thickness(0, 0, 0, 5);
                barTextBox.Padding = new Thickness(1);
                stackedpanel_barnames.Children.Add(barTextBox);

                //Generer stamdata knapper til hver bar
                Button barButton = new Button();
                barButton.Name = "bar_button_" + bar.ID;
                barButton.Content = "Rediger stamdata";
                barButton.Margin = new Thickness(0, 0, 0, 5);
                stackedpanel_bars.Children.Add(barButton);
                barButton.Click += new RoutedEventHandler(this.ReadBar);

                //Generer lager knapper til hver bar
                Button stockButton = new Button();
                stockButton.Name = "stock_button_" + bar.ID;
                stockButton.Content = "Rediger lager";
                stockButton.Margin = new Thickness(0, 0, 0, 5);
                stackedpanel_stock.Children.Add(stockButton);
                stockButton.Click += new RoutedEventHandler(this.EditStocks);
            }
        }

        private void ReadBar(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int barId = Int32.Parse(b.Name.Substring(11, b.Name.Length - 11));

            ReadBar bar = new ReadBar(this, barId);
            bar.ShowDialog();
        }

        private void EditStocks(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int barId = Int32.Parse(b.Name.Substring(13, b.Name.Length - 13));

            StockDetails stock = new StockDetails(this, barId);
            stock.ShowDialog();
        }

        private void button_logout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
    }
}
