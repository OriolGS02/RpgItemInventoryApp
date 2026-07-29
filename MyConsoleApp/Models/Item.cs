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



        public Item(string name,ItemType type,string description) 
        { 
            Name = name;
            Type = type;
            Description = description;
        }
        public Item() 
        { 
              
        }

        public override string ToString()
        {
            return $"Item Id: {Id}.\n"+
            $"Item Name: {Name}.\n"+
            $"Item Type: {Type}.\n"+
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
