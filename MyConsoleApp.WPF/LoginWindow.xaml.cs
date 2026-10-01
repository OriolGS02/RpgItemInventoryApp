using MyConsoleApp.Core.Models;
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
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        

        string emailString; 
        string passwordString; 
        bool visiblePassword = false;
        UserService userService = new UserService();
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

            if (!emailString.Contains("@")) 
            {
                MessageBox.Show("Email not vaild.");
                return;
            }

            

            //login userservice call
            User loggedUser =userService.UserLogin(emailString,passwordString);

            if (loggedUser != null)
            {
                MessageBox.Show("User Logged");

               
            }
            else 
            {
                MessageBox.Show("User doesn't exist. Revise the credentials or Register if you don't have an account.");
            }
            
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
