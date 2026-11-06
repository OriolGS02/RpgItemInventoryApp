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
    /// Lógica de interacción para EditItemWindow.xaml
    /// </summary>
    /// 
    
    public partial class EditItemWindow : Window
    {
        Item item;
        Item editedItem= new Item();
        InventoryService inventoryService;
        public EditItemWindow(Item selectedItem,InventoryService service)
        {
            InitializeComponent();
            item= selectedItem;
            inventoryService = service;
            SetFields();
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


        void SaveUpdate(object sender, EventArgs e) 
        {
            editedItem = new Item
            {
                Id=item.Id,
                Name=editedName.Text,
                Type=GetSelectedItemType(),
                Description=descriptionString.Text,
                Quantity=(int)quantityInt.Value<0?0:(int)quantityInt.Value,
            };

            MessageBox.Show("Edited: " + editedItem.ToString() + "/ Original: " +item.ToString());

            if (HasChanges(item, editedItem))
            {
                inventoryService.UpdateItem(editedItem);
                inventoryService.UpdateItemQuantity(editedItem,editedItem.Quantity);
                MessageBox.Show("Update Completed");
                this.DialogResult = true;
            }
            else 
            {
                this.Close();
            }

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
