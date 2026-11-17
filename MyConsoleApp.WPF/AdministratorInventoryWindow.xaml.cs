using MyConsoleApp.Core.Models;
using MyConsoleApp.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            allStock.IsChecked = true;

            this.Closed += (s, e) =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    Debug.WriteLine(
                        $"Remaining window: {window.GetType().Name}, " +
                        $"Visibility: {window.Visibility}"
                    );
                }
            };

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

           // MessageBox.Show(itemcopy.Id + "/" + itemcopy.Name);
        }

        public void ItemManagement_Buttton(object sender, RoutedEventArgs e)
        {
            Button clickedbutton = (Button)sender;
            ItemManagerWindow editItemWindow;
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
                RefreshItems();
            }

            

        }

        private void AdminWindow_Closing(object sender, CancelEventArgs e)
        {
            inventoryService.SaveIfDirty();
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
                
            }            
        }


        private void SearchBar(object sender, TextChangedEventArgs e) 
        {
            TextBox searchBox = (TextBox)sender;
           if (searchBox.Text != "")
            {
                inventoryGrid.ItemsSource = inventoryService.GetItemsWithName(searchBox.Text);
            }
            else 
            {
                inventoryGrid.ItemsSource = inventoryService.GetItems();
            }

            

        }

        private void DeleteItem(object sender, RoutedEventArgs e) 
        {
            Item? selectedItem = inventoryGrid.SelectedItem as Item;
            if (selectedItem == null) 
            {
                MessageBox.Show("Select the Item you want to Delete");
                return;
            }
            MessageBoxResult result=MessageBox.Show(this,"Are you sure you want to delete the Item?", "Caution!!", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {                
                inventoryService.RemoveItem(selectedItem);
                Debug.WriteLine($"Items in service: {inventoryService.GetItems().Count}");

                RefreshItems();

                Debug.WriteLine($"Items in DataGrid: {inventoryGrid.Items.Count}");
            }

            
            
        }

        private void RefreshItems() 
        {
            inventoryGrid.ItemsSource = null;
            inventoryGrid.ItemsSource = inventoryService.GetItems();
        }

       
    }
}
