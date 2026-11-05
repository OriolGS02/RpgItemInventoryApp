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
        public EditItemWindow(Item selectedItem)
        {
            InitializeComponent();
            item= selectedItem;

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


       
    }
}
