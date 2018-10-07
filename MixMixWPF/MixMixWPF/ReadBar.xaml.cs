using MixMixWPF.BarServiceReference;
using MixMixWPF.LocationServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ReadBar.xaml
    /// </summary>
    public partial class ReadBar : Window
    {
        BarServiceClient barService = new BarServiceClient("barServiceTcp");
        LocationServiceClient locationService = new LocationServiceClient("locationServiceTcp");
        MainWindow mainWindow;
        Bar readBar;
        List<LocationServiceReference.Zipcode> zipcodes = new List<LocationServiceReference.Zipcode>();

        public ReadBar(object sender, int barId)
        {
            InitializeComponent();
            //Main window ref
            mainWindow = (MainWindow)sender;

            //Find bar
            readBar = barService.Find(barId);
            textbox_name.Text = readBar.Name;
            textbox_username.Text = readBar.Username;
            textbox_address.Text = readBar.Address.AddressName;
            textbox_email.Text = readBar.Email;
            textbox_phone.Text = readBar.PhoneNumber;
            textblock_city.Text = readBar.Address.Zipcode.CityName;

            fillCountryComboBox();
            fillZipComboBox();

            combobox_countries.SelectionChanged += combobox_countries_SelectionChanged;
            combobox_zipcodes.SelectionChanged += combobox_zipcodes_SelectionChanged;


        }

        private void button_cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Show();

        }

        #warning Fix Ambiguous references for Address (currently using dummy data)
        private void button_update_click(object sender, RoutedEventArgs e)
        {
            //Address local creation
            LocationServiceReference.Address address = new LocationServiceReference.Address();
            address.Id = readBar.Address.Id;
            address.AddressName = textbox_address.Text;
            int zipcodeId = Int32.Parse(combobox_zipcodes.SelectedValue.ToString());
            //Save address to db and return with Id
            LocationServiceReference.Address barAddress = locationService.updateAddress(address, zipcodeId);

            //Here we "transfer" the LocationServiceReference.Address object to a BarServiceReference.Address object :S
            BarServiceReference.Address referenceAddress = new BarServiceReference.Address();
            referenceAddress.Id = barAddress.Id;

            //BAR
            string name = textbox_name.Text;
            string phone = textbox_phone.Text;
            string email = textbox_email.Text;
            string username = textbox_username.Text;

            bool success = barService.Update(readBar.ID, name, referenceAddress, phone, email, username);

            if (success)
            {
                MessageBox.Show("Your bar was updated");
                this.Close();
                mainWindow.Show();
                mainWindow.show_bars();
            }
        }

        private void button_delete_click(object sender, RoutedEventArgs e)
        {
            bool barDeleted = barService.Delete(readBar.ID);
            bool addressDeleted = locationService.deleteAddress(readBar.Address.Id);
            if (barDeleted && addressDeleted)
            {
                MessageBox.Show("Your bar was deleted");
                this.Close();
                mainWindow.Show();
                mainWindow.show_bars();
            }
        }

        private void combobox_countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Clear fields that has to do with location information
            clearLocationFields();

            int countryId = Int32.Parse(combobox_countries.SelectedValue.ToString());
            zipcodes = locationService.getZipcodesById(countryId).ToList(); //find ud af hvordan man ikke behøver at være explicit
            combobox_zipcodes.DataContext = zipcodes;

            this.combobox_zipcodes.SelectedValuePath = "Key";
            this.combobox_zipcodes.DisplayMemberPath = "Value";

            foreach (var item in zipcodes)
            {
                this.combobox_zipcodes.Items.Add(new KeyValuePair<int, string>(item.Id, item.Zip));
            }
        }

        private void combobox_zipcodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.combobox_zipcodes.Items.Count > 0)
            {
                int zipcodeId = Int32.Parse(combobox_zipcodes.SelectedValue.ToString());
                for (int i = 0; i < zipcodes.Count; i++)
                {
                    if (zipcodes[i].Id == zipcodeId)
                    {
                        textblock_city.Text = zipcodes[i].CityName;
                        break;
                    }
                }
            }
        }

        private void fillCountryComboBox()
        {
            List<LocationServiceReference.Country> countries = locationService.getCountries().ToList();
            combobox_countries.DataContext = countries;

            this.combobox_countries.SelectedValuePath = "Key";
            this.combobox_countries.DisplayMemberPath = "Value";

            foreach (var country in countries)
            {
                this.combobox_countries.Items.Add(new KeyValuePair<int, string>(country.Id, country.CountryName)); //Lav countryName om til name bare ffs
            }

            combobox_countries.SelectedValue = readBar.Address.Zipcode.Country.Id;
        }

        private void fillZipComboBox()
        {
            List<LocationServiceReference.Zipcode> zipcodes = locationService.getZipcodesById(readBar.Address.Zipcode.Country.Id).ToList();
            combobox_zipcodes.DataContext = zipcodes;

            this.combobox_zipcodes.SelectedValuePath = "Key";
            this.combobox_zipcodes.DisplayMemberPath = "Value";

            foreach (var city in zipcodes)
            {
                this.combobox_zipcodes.Items.Add(new KeyValuePair<int, string>(city.Id, city.Zip));
            }

            combobox_zipcodes.SelectedValue = readBar.Address.Zipcode.Id;
        }
        private void clearLocationFields()
        {
            this.combobox_zipcodes.Items.Clear();
            this.textbox_address.Clear();
            this.textblock_city.Text = "";
        }

        //TODO linje igen inden release.
        //Sørg for at vindue altid er øverst hvis det er aktiveret
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Activate();
            //this.Topmost = true; -- Udkommenteret for debugging purposes
        }
    }
}
