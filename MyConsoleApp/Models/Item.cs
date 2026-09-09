using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp.Classes
{
    internal class Item
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }



        public Item(int id,string name, ItemType type, int quantity, string description)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            Quantity = quantity;
        }
        public Item() 
        { 
              
        }

        public override string ToString()
        {
            return $"Item Id: {Id}.\n"+
            $"Item Name: {Name}.\n"+
            $"Item Type: {Type}.\n"+
            $"Item Quanity: {Quantity}.\n"+
            $"Item Descritpion: {Description}.";
        }
      
    }

    enum ItemType 
    {
        Weapon,
        Armor,
        Consumable,


    }

    
}
