using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyConsoleApp.Core.Services;
using MyConsoleApp.Core.Models;
using System.Collections;

namespace MyConsoleApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InventoryService inventoryService;
        public MainWindow()
        {
            InitializeComponent();
            inventoryService = new InventoryService();     
            
            IEnumerable items = inventoryService.GetItems();

            inventoryList.ItemsSource= items; 
            

            
            
        }


        public void InventoryList_SelectionChanges(object sender, SelectionChangedEventArgs e) 
        {
            Item? selectedItem = inventoryList.SelectedItem as Item;
            if (selectedItem != null)
            {
                
            }
        }
    }
}