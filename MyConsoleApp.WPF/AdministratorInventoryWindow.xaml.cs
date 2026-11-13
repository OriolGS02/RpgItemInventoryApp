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
        RadioButton rd_Type;
        RadioButton rd_availability;


        ItemType? sortingType = null;
        bool? sortingavailable = null;
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
                    Id = selectedItem.Id,
                    Name = selectedItem.Name,
                    Type = selectedItem.Type,
                    Description = selectedItem.Description,
                    Quantity = selectedItem.Quantity,
                };


            }

            MessageBox.Show(itemcopy.Id + "/" + itemcopy.Name);
        }

        public void ItemManagement_Buttton(object sender, RoutedEventArgs e)
        {
            Button clickedbutton = (Button)sender;
            ItemManagerWindow editItemWindow = new ItemManagerWindow();
            if (clickedbutton.Name == "editButton")
            {
                if (itemcopy == null)
                {
                    MessageBox.Show("Select Item to Edit");
                    return;
                }


                editItemWindow = new ItemManagerWindow(itemcopy, inventoryService);
            }
            else
            {
                editItemWindow = new ItemManagerWindow(inventoryService);
            }



            editItemWindow.Owner = this;
            bool? result = editItemWindow.ShowDialog();
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
            if (rd_Type == null || rd_Type != temprd)
            {
                rd_Type = temprd;

                switch (rd_Type.Name)
                {
                    case "weaponCheck":
                        sortingType = ItemType.Weapon;
                        break;
                    case "consumableCheck":
                        sortingType = ItemType.Consumable;
                        break;
                    case "armorCheck":
                        sortingType = ItemType.Armor;
                        break;
                    default:
                        sortingType = null;
                        break;
                }

                inventoryGrid.ItemsSource = inventoryService.FilterByTypeAndQuantity(sortingType,sortingavailable);
                inventoryGrid.Items.Refresh();

            }


        }

        private void ItemAvailable_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton temprd = (RadioButton)sender;
            if (rd_availability == null || rd_availability != temprd)
            {
                rd_availability = temprd;

                switch (rd_availability.Name)
                {
                    case "onStockCheck":
                        sortingavailable = true;
                        break;
                    case "outStockCheck":
                        sortingavailable = false;
                        break;
                    case "allStock":
                        sortingavailable = null;
                        break;
                    default:
                        sortingavailable=true;
                        break;
                }
                inventoryGrid.ItemsSource = inventoryService.FilterByTypeAndQuantity(sortingType, sortingavailable);
                inventoryGrid.Items.Refresh();
            }            
        }         
    }
}
