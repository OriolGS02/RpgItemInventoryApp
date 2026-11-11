using MyConsoleApp.Core.Models;
using MyConsoleApp.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para AdministratorInventoryWindow.xaml
    /// </summary>
    ///
    
    public partial class AdministratorInventoryWindow : Window
    {
        Item itemcopy;
        InventoryService inventoryService;
        RadioButton rd;
        public AdministratorInventoryWindow()
        {
            InitializeComponent();

            inventoryService = new InventoryService();

            inventoryGrid.ItemsSource = inventoryService.GetItems();

            allCheck.IsChecked = true;

        }

        public void AdminItemList_SelectionChanges(object sender, EventArgs e)
        {
            Item? selectedItem = inventoryGrid.SelectedItem as Item;
            if (selectedItem != null)
            {
                itemcopy = new Item 
                {
                    Id= selectedItem.Id,
                    Name=selectedItem.Name,
                    Type=selectedItem.Type,
                    Description=selectedItem.Description,
                    Quantity=selectedItem.Quantity,
                };

                
            }

            MessageBox.Show(itemcopy.Id + "/" + itemcopy.Name);
        }

        public void Edit_Button(object sender, RoutedEventArgs e) 
        {
            if (itemcopy == null) 
            {
                MessageBox.Show("Select Item to Edit");
                return;
            }

            ItemManagerWindow editItemWindow = new ItemManagerWindow(itemcopy,inventoryService);
            editItemWindow.Owner = this;
            bool? result=editItemWindow.ShowDialog();
            if (result == true) 
            {
                inventoryGrid.Items.Refresh();
            }
           
        }

        private void AdminWindow_Closing(object sender, CancelEventArgs e)
        {
            inventoryService.SaveData();
        }


        private void ItemType_Checked(object sender, RoutedEventArgs e) 
        {
            RadioButton temprd = (RadioButton)sender;
            if (rd == null || rd != temprd)
            {
                rd = temprd;

                switch (rd.Name) 
                {
                    case "weaponCheck":
                        inventoryGrid.ItemsSource = inventoryService.GetItemsByType(ItemType.Weapon);
                        break;
                    case "consumableCheck":
                        inventoryGrid.ItemsSource = inventoryService.GetItemsByType(ItemType.Consumable);
                        break;
                    case "armorCheck":
                        inventoryGrid.ItemsSource = inventoryService.GetItemsByType(ItemType.Armor);
                        break;
                    default:
                        inventoryGrid.ItemsSource = inventoryService.GetItems();
                        break;
                }

                inventoryGrid.Items.Refresh();

            }
           

           


        }




    }
}
