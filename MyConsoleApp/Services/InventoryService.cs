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

        static int nextId = 0;
        public void AddItem(Item item) 
        {
            item.Id = nextId;
            items.Add(item);
            nextId++;
        }

        public List<Item> GetItems()
        {
            return items;
        }
        public Item GetItemById(int id)
        {
           Item item= items.Find(it => it.Id == id);
           return item;
        }

        public void RemoveItem(Item item) 
        {
            items.Remove(item);
        }




    }
}
