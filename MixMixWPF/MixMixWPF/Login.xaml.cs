using MixMixWPF.ManagerServiceReference;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ManagerServiceClient managerService = new ManagerServiceClient("managerServiceTcp");

        public Login()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = textbox_username.Text;
                string password = passwordbox_password.Password;
                Manager manager = managerService.Login(username, password);
                if (manager == null)
                {
                    MessageBoxResult result = MessageBox.Show("Forkert brugernavn eller password");
                }
                else
                {
                    MainWindow mainWindow = new MainWindow(manager);
                    mainWindow.Show();
                    this.Close();
                }
            }
            //TODO remove Catchblock before release - OR don't show the Exception
            catch (Exception ex)
            {
                MessageBoxResult information = MessageBox.Show("UPS ! Ser ud til at Hosten ikke er tilgængelig - Vi har sat praktikanten Frode til at stirre intenst på den, til den virker igen !\n\n" + ex.Message);
                //MessageBoxResult result = MessageBox.Show(ex.Message);
            }

        }
        private void button_help_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult information = MessageBox.Show("Inspirational Quote of today :\nEnjoy the good times because something terrible is probably about to happen");

        }

        private void button_create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void passwordbox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordbox_password.Clear();
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
