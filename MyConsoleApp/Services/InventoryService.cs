using MyConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp.Services
{
   
    internal class InventoryService
    {
        List<Item> items = new List<Item>();

        public void AddItem(Item item) 
        {
            items.Add(item);
        }

        public List<Item> GetItems()
        {
            return items;
        }


    }
}
