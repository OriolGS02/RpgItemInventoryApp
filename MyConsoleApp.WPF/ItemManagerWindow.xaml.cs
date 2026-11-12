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
using Xceed.Wpf.Toolkit.Primitives;

namespace MyConsoleApp.WPF
{
    /// <summary>
    /// Lógica de interacción para ItemManagerWindow.xaml
    /// </summary>
    /// 

    public partial class ItemManagerWindow : Window
    {
        Item item;
        Item editedItem= new Item();
        InventoryService inventoryService;

        bool UpdateMode;
        public ItemManagerWindow(Item selectedItem,InventoryService service)
        {
            InitializeComponent();
            item= selectedItem;
            inventoryService = service;  
            UpdateMode = true;
            title.Content = "Edit Item";
            SetFields(); 
            
            
        }

        public ItemManagerWindow(InventoryService service) 
        {
            InitializeComponent();
           
            inventoryService = service;
            UpdateMode = false;

            title.Content = "Add Item";


        }
        public ItemManagerWindow() { } 




        void SetFields() 
        {
            nameString.Text=item.Name;
            switch (item.Type) 
            {
                case ItemType.Weapon:
                    weaponType.IsChecked = true; 
                    break;
                case ItemType.Consumable:
                    consumableType.IsChecked = true;
                    break;
                case ItemType.Armor:
                    armorType.IsChecked = true;
                    break;
            }
            descriptionString.Text=item.Description;

            if (item.Quantity < 0) 
            {
                quantityInt.Value = 0;
            }

            quantityInt.Value=item.Quantity;

            editedItem.Id = item.Id;
        }


        void Save(object sender, EventArgs e) 
        {
            
            if (UpdateMode)
            {

                EditItem();
            }
            else 
            {
                AddItem();
            }

        }
        private void EditItem() 
        {
            if (!CheckParameters()) 
            {
                return;
            }




            editedItem = new Item
            {
                Id = item.Id,
                Name = nameString.Text,
                Type = (ItemType)GetSelectedItemType(),
                Description = descriptionString.Text,
                Quantity = (int)quantityInt.Value < 0 ? 0 : (int)quantityInt.Value,
            };


            

           if (HasChanges(item, editedItem))
            {
                inventoryService.UpdateItem(editedItem);
                inventoryService.UpdateItemQuantity(editedItem, editedItem.Quantity);
                MessageBox.Show("Update Completed");
                this.DialogResult = true;
            }
            else
            {
                this.Close();
            }
        }

        private void AddItem() 
        {
            //checking if tht parameters are good

            if (!CheckParameters())
            {                
                return;
            }

            if (!CheckQuantity()) 
            {
                MessageBox.Show("Quantity can't be Zero to add an item");
                return;
            }

            Item newItem = new Item
            {
                Name = nameString.Text,
                Type = (ItemType)GetSelectedItemType(),
                Description = descriptionString.Text,
                Quantity = (int)quantityInt.Value < 0 ? 0 : (int)quantityInt.Value

            };

            inventoryService.AddItem(newItem);
            MessageBox.Show("Item Added");
            this.DialogResult = true;

            



        }

        private bool CheckParameters() 
        {
            if (nameString.Text == "")
            {
                MessageBox.Show("Name empty");
                return false;
            }
            if (GetSelectedItemType() == null) 
            {
                MessageBox.Show("Item type not selected");
                return false;
            }
            if (descriptionString.Text == "")
            {
                MessageBox.Show("Description empty");
                return false;
            }
            return true;

        }

        private bool CheckQuantity() 
        {
            if ((quantityInt.Value < 1)) 
            {
                return false;
            }
            return true;

        }

        private ItemType? GetSelectedItemType() 
        {
            if(weaponType.IsChecked==true)
                return ItemType.Weapon;
            if(consumableType.IsChecked==true)
                return ItemType.Consumable;
            if(armorType.IsChecked==true)
                return ItemType.Armor;

            return null;

            

        }
        private bool HasChanges(Item original, Item edited)
        {
            return original.Name != edited.Name ||
                   original.Type != edited.Type ||
                   original.Quantity != edited.Quantity ||
                   original.Description != edited.Description;
        }



    }
}
