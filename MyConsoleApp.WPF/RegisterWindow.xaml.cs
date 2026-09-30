using MyConsoleApp.Core.Services;
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
    /// Lógica de interacción para RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        string nameString;
        string emailString;
        string passwordString;
        bool visiblePassword = false;
        UserService userService = new UserService();
        public RegisterWindow()
        {
            InitializeComponent();
        }



        public void ShowPassword(object sender, RoutedEventArgs e)
        {
            visiblePassword = !visiblePassword;

            if (visiblePassword)
            {
                passwordVisibleText.Text = passwordText.Password;
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


        public void Register(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();

            emailString = emailText.Text;
            nameString = nameText.Text;
            if (visiblePassword)
            {
                passwordString = passwordVisibleText.Text;
            }
            else
            {
                passwordString = passwordText.Password;
            }

            if (string.IsNullOrEmpty(nameString))
            {
                MessageBox.Show("Name field can't be empty");
                return;
            }

            if (string.IsNullOrEmpty(emailString))
            {
                MessageBox.Show("Email field can't be empty");
                return;
            }

            if (string.IsNullOrEmpty(passwordString))
            {
                MessageBox.Show("Pasword field can't be empty");
                return;
            }




            if (userService.Exist(emailString)) 
            {
                MessageBox.Show("This Email is already registered");
                return;
            }


            MessageBox.Show(nameString + "/" + emailString + "/" + passwordString);



        }
    }
}
