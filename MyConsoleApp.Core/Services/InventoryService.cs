using MyConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyConsoleApp.Core.Services
{
   
    public class InventoryService
    {
        List<Item> items = new List<Item>();
        //string filepath = "data.json";
        string filepath = @"C:\VisualStudioC#Projects\MyConsoleApp\Data\data.json";

        static int nextId = 0;

        public InventoryService() { LoadData(); } 
        public InventoryService(string fileName)
        {
            filepath= fileName;
            LoadData();
        } 
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
        public List<Item> GetAvailableItems()
        {
            List<Item> itemsList = items.FindAll(it => it.Quantity >0);
            return itemsList;
        }
        public List<Item> GetItemsByType(ItemType type)
        {
            List<Item> itemsList = items.FindAll(it => it.Type == type);
            return itemsList;
        }
        public List<Item> GetItemsWithName(string name)
        {
            List<Item> itemsList = items.FindAll(it => it.Name.Contains(name,StringComparison.OrdinalIgnoreCase));
            return itemsList;
        }


        public Item GetItemById(int id)
        {
           Item item= items.Find(it => it.Id == id);
           return item;
        }
        public Item GetItemByName(string name)
        {
           Item item= items.FirstOrDefault(it => it.Name.Equals(name,StringComparison.OrdinalIgnoreCase));
           return item;
        }

        public List<Item> SortedByName(bool ascending) 
        {
            List<Item> sortedList;
            if (ascending)
            {
                sortedList = GetItems().OrderBy(i => i.Name).ToList();
            }
            else 
            {
                sortedList = GetItems().OrderByDescending(i => i.Name).ToList();
            }
               
            return sortedList;
        }

        public List<Item> SortedByType(bool ascending)
        {
            List<Item> sortedList;
            if (ascending)
            {
                sortedList = GetItems().OrderBy(i => i.Type).ToList();
            }
            else
            {
                sortedList = GetItems().OrderByDescending(i => i.Type).ToList();
            }
            return sortedList;
        }

        public List<Item> SortedByQuantity(bool ascending)
        {
            List<Item> sortedList;
            if (ascending)
            {
                sortedList = GetItems().OrderBy(i => i.Quantity).ToList();
            }
            else
            {
                sortedList = GetItems().OrderByDescending(i => i.Quantity).ToList();
            }
            return sortedList;
        }

        public bool Exist(string name) 
        {
           
            return GetItemByName(name) != null;
        }
        public bool Exist(int id) 
        {
            return GetItemById(id) != null;
        }
        



        public void RemoveItem(Item item) 
        {
            items.Remove(item);
        }

        public void UpdateItem(int index,Item updatedItem) 
        {
            items[index].Name = updatedItem.Name;
            items[index].Description = updatedItem.Description;
        }
        public void UpdateItemQuantity(int index, int quantity) 
        {


            items[index].Quantity=quantity;

            
        }

        public int GetItemIndex(Item item) 
        {
           
            return items.IndexOf(item);
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

            try
            {
                string data = File.ReadAllText(filepath);
                items = JsonSerializer.Deserialize<List<Item>>(data);

                nextId = items.Any() ?
                items.Max(i => i.Id) + 1
                : 0;
            }
            catch (JsonException jex) 
            {
                Console.WriteLine("Something went wrong trying to read the JSON file.");
                nextId = 0;
            }

            
            
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
