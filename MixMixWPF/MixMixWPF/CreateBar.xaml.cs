using MixMixWPF.LocationServiceReference;
using MixMixWPF.BarServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Text.RegularExpressions;
using RestSharp;
using System.Timers;
using Newtonsoft.Json;

namespace MixMixWPF
{
    /// <summary>
    /// Interaction logic for CreateBar.xaml
    /// </summary>
    public partial class CreateBar : Window
    {
        private System.Timers.Timer timer = new System.Timers.Timer();
        private RestClient client = new RestClient("https://dawa.aws.dk/");
        BarServiceClient barService = new BarServiceClient("barServiceTcp");
        LocationServiceClient locationService = new LocationServiceClient("locationServiceTcp");
        MainWindow mainWindow;

        List<LocationServiceReference.Zipcode> zipcodes = new List<LocationServiceReference.Zipcode>();
        public CreateBar(object sender)
        {
            InitializeComponent();
            mainWindow = (MainWindow)sender;
            timer.Interval = 500;
            timer.Elapsed += Timer_Elapsed;
        }

#warning Fix Ambiguous reference -
        //TODO Kald ikke LocationService, men lad barService gøre det hele
        private void button_create_click(object sender, RoutedEventArgs e)
        {
            Adresse AddressObject = (Adresse)combobox_address.SelectedItem;
            string addressName = (AddressObject.adgangsadresse.vejstykke.navn + " " + AddressObject.adgangsadresse.husnr + " " + AddressObject.etage + " " + AddressObject.dør).Trim();

            if (ValidateEmail() && ValidatePhone())
            {
                //int zipcodeId = Int32.Parse(combobox_zipcodes.SelectedValue.ToString());
                LocationServiceReference.Address address = locationService.createAddress(addressName, Convert.ToInt32(AddressObject.adgangsadresse.postnummer.nr)); //skal være en BarServiceReference.Address
                string barName = textbox_name.Text;
                string phone = textbox_phone.Text;
                string email = textbox_email.Text;
                string username = textbox_username.Text;
                string password = passwordbox_password.Password;

                //Here we "transfer" the LocationServiceReference.Address object to a BarServiceReference.Address object :S
                BarServiceReference.Address referenceAddress = new BarServiceReference.Address();
                referenceAddress.Id = address.Id;

                Bar bar = barService.Create(1, barName, referenceAddress, phone, email, username, password);

                if (bar != null)
                {
                    MessageBox.Show("Your bar was created");
                    this.Close();
                    mainWindow.Show();
                    mainWindow.show_bars();
                }
            }
        }

        //Nøjes med kun at kunne vælge Danmark.

        //private void combobox_countries_Loaded(object sender, RoutedEventArgs e)
        //{
        //    List<LocationServiceReference.Country> countries = locationService.getCountries().ToList();
        //    combobox_countries.DataContext = countries;

        //    this.combobox_countries.SelectedValuePath = "Key";
        //    this.combobox_countries.DisplayMemberPath = "Value";

        //    foreach (var country in countries)
        //    {
        //        this.combobox_countries.Items.Add(new KeyValuePair<int, string>(country.Id, country.CountryName)); //Lav countryName om til name bare ffs
        //    }
        //}

        //private void combobox_countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //Clear fields that has to do with location information
        //    clearLocationFields();

        //    int countryId = Int32.Parse(textblock_country.ToString());
        //    zipcodes = locationService.getZipcodesById(countryId).ToList();
        //    combobox_zipcodes.DataContext = zipcodes;

        //    this.combobox_zipcodes.SelectedValuePath = "Key";
        //    this.combobox_zipcodes.DisplayMemberPath = "Value";

        //    foreach (var item in zipcodes)
        //    {
        //        this.combobox_zipcodes.Items.Add(new KeyValuePair<int, string>(item.Id, item.Zip));
        //    }
        //}

        //private void combobox_zipcodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int zipcodeId = Int32.Parse(combobox_zipcodes.SelectedValue.ToString());
        //    for (int i = 0; i < zipcodes.Count; i++)
        //    {
        //        if (zipcodes[i].Id == zipcodeId)
        //        {
        //            textblock_city.Text = zipcodes[i].CityName;
        //            break;
        //        }
        //    }
        //}

        private void button_cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        //Textbox logik
        //Navn
        private void textbox_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_name.Text == "Navn")
            {
                textbox_name.Clear();
            }
        }

        private void textbox_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_name.Text.Length < 1)
            {
                textbox_name.Text = "Navn";
            }
        }

        //Brugernavn
        private void textbox_username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_username.Text == "Brugernavn")
            {
                textbox_username.Clear();
            }
        }

        private void textbox_username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_username.Text.Length < 1)
            {
                textbox_username.Text = "Brugernavn";
            }
        }

        //Email
        private void textbox_email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_email.Text == "Email")
            {
                textbox_email.Clear();
            }
        }

        private void textbox_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_email.Text.Length < 1)
            {
                textbox_email.Text = "Email";
            }
        }

        private bool ValidateEmail()
        {
            string email = textbox_email.Text;
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            //Regexexp: http://www.rhyous.com/2010/06/15/regular-expressions-in-cincluding-a-new-comprehensive-email-pattern/
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Emailen er ikke valid");
                return false;
            }
        }

        //Telefon
        private void textbox_phone_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_phone.Text == "Tlf. nr.")
            {
                textbox_phone.Clear();
            }
        }

        private void textbox_phone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textbox_phone.Text.Length < 1)
            {
                textbox_phone.Text = "Tlf. nr.";
            }
        }

        private bool ValidatePhone()
        {
            string phone = textbox_phone.Text;
            Regex regex = new Regex(@"^([+]+[0-9]{2})?\d{8}$");
            // Regex: Telefonnummer skal være 8 tal, men kan godt have landekode med formatet +xx
            Match match = regex.Match(phone);
            if (match.Success)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Telefonnummeret er ikke validt");
                return false;
            }
        }

        //Adresse

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            RestRequest request = new RestRequest("adresser", Method.GET);
            this.Dispatcher.Invoke(() =>
            {
                request.AddQueryParameter("q", combobox_address.Text);
            });
            IRestResponse response = client.Execute(request);
            List<Adresse> addressList = JsonConvert.DeserializeObject<List<Adresse>>(response.Content);
            this.Dispatcher.Invoke(() =>
            {
                combobox_address.ItemsSource = addressList;
            });
        }

        private void combobox_address_KeyDown(object sender, KeyEventArgs e)
        {
            timer.Stop();
            combobox_address.IsDropDownOpen = true;
        }

        private void combobox_address_KeyUp(object sender, KeyEventArgs e)
        {
            timer.Start();
        }

        //TODO indkommenter linje igen inden release.
        //Sørg for at vindue altid er øverst hvis det er aktiveret
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Activate();
            //this.Topmost = true; -- Udkommenteret for debugging purpose.
        }

        //private void clearLocationFields()
        //{
        //    this.combobox_zipcodes.Items.Clear();
        //    this.combobox_address.Items.Clear();
        //    this.combobox_city.Items.Clear();
        //}
    }
}
