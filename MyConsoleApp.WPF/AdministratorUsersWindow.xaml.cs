using MyConsoleApp.Core.Models;
using MyConsoleApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para AdministratorUsersWindow.xaml
    /// </summary>
    public partial class AdministratorUsersWindow : Window
    {
        UserService userService;
        User usercopy;
        RadioButton rd_Role;
        UserType? usertype;

        User adminUser;
        public AdministratorUsersWindow()
        {
            InitializeComponent();
            userService = new UserService();

            usersGrid.ItemsSource = userService.GetUsers();

            allCheck.IsChecked = true;
        }
        public void AdminUserList_SelectionChanges(object sender, EventArgs e)
        {
            User? selectedItem = usersGrid.SelectedItem as User;
            if (selectedItem != null)
            {

                usercopy = new User
                {
                    Id = selectedItem.Id,
                    Name = selectedItem.Name,
                    Email = selectedItem.Email,
                    PasswordHash = selectedItem.PasswordHash,

                };

            }

            MessageBox.Show(selectedItem.Id + "/" + selectedItem.Name);
        }

        private void ItemType_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton temprd = (RadioButton)sender;
            if (rd_Role == null || rd_Role != temprd)
            {
                rd_Role = temprd;

                switch (rd_Role.Name)
                {
                    case "adminCheck":
                        usertype = UserType.Admin;
                        break;
                    case "userCheck":
                        usertype = UserType.User; 
                        break;
                    default:
                        usertype = null;
                        break;
                }

                usersGrid.ItemsSource = userService.FilterByRole(usertype);


            }


        }
    }
}
