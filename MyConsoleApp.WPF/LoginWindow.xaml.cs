using MyConsoleApp.Core.Models;
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
            emailString = emailText.Text;
            if (visiblePassword)
            {
                passwordString = passwordVisibleText.Text;
            }
            else
            {
                passwordString = passwordText.Password;
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
            MessageBox.Show(emailString + " " + passwordString);

            //login userservice call
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


        public void RegisterLink(object sender, RoutedEventArgs e) 
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

       
    }

    
}
