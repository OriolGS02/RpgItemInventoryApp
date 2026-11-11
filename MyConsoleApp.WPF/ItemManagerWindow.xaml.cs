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

       


        void SetFields() 
        {
            editedName.Text=item.Name;
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

                editedItem = new Item
                {
                    Id = item.Id,
                    Name = editedName.Text,
                    Type = GetSelectedItemType(),
                    Description = descriptionString.Text,
                    Quantity = (int)quantityInt.Value < 0 ? 0 : (int)quantityInt.Value,
                };

                MessageBox.Show("Edited: " + editedItem.ToString() + "/ Original: " + item.ToString());

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
            else 
            {
                AddItem();
            }

        }

        private void AddItem() 
        {
            //checking if tht parameters are good

        }

        private ItemType GetSelectedItemType() 
        {
            if(weaponType.IsChecked==true)
                return ItemType.Weapon;
            if(consumableType.IsChecked==true)
                return ItemType.Consumable;

            return ItemType.Armor;

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
