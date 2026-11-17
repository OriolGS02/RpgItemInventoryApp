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
    /// Lógica de interacción para AdministratorUsersWindow.xaml
    /// </summary>
    public partial class AdministratorUsersWindow : Window
    {
        InventoryService inventoryService;

        public AdministratorUsersWindow()
        {
            InitializeComponent();
            inventoryService = new InventoryService();

        }
    }
}
