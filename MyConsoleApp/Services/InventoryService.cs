using MyConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyConsoleApp.Services
{
   
    internal class InventoryService
    {
        List<Item> items = new List<Item>();
        static string filepath = "data.json";

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
        public List<Item> GetItemsByType(ItemType type)
        {
            List<Item> itemsList = items.FindAll(it => it.Type == type);
            return itemsList;
        }
        public List<Item> GetItemsWithName(string name)
        {
            List<Item> itemsList = items.FindAll(it => it.Name.Contains($"{name}"));
            return itemsList;
        }


        public Item GetItemById(int id)
        {
           Item item= items.Find(it => it.Id == id);
           return item;
        }
        public Item GetItemByName(string name)
        {
           Item item= items.Find(it => it.Name==name);
           return item;
        }



        public void RemoveItem(Item item) 
        {
            items.Remove(item);
        }


       

        public void SaveData()
        {
           
            string jsonitem = JsonSerializer.Serialize(items);
            File.WriteAllText(filepath, jsonitem);
            
        }

        public void LoadData()
        {
            if (!File.Exists(filepath))
                return;
            

            string data= File.ReadAllText(filepath);
            
            items= JsonSerializer.Deserialize<List<Item>>(data);

            nextId = items.Any()?
                items.Max(i=>i.Id)+1
                :0;
            
        }


        public int GetInventorySize() 
        {
            return items.Count; 
        }

        public int GetListSize(List<Item> items) 
        {
            return items.Count;
        }




    }
}
