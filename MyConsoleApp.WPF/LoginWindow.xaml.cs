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

namespace MyConsoleApp.WPF
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        

        string emailString; 
        string passwordString; 
        bool visiblePassword = false;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(emailText.Text+" "+ passwordText.Password);
        }

        public void ShowPassword(object sender, RoutedEventArgs e) 
        {
            

            visiblePassword=!visiblePassword;

            if (visiblePassword) 
            {
                passwordVisibleText.Text =passwordText.Password;
                passwordText.Visibility = Visibility.Collapsed;
                passwordVisibleText.Visibility = Visibility.Visible;

            }
            else 
            {
                passwordText.Password = passwordVisibleText.Text;
                passwordText.Visibility = Visibility.Visible;
                passwordVisibleText.Visibility = Visibility.Collapsed;
            }
        }

       
    }

    
}
