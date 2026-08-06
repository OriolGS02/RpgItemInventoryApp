// See https://aka.ms/new-console-template for more information

using MyConsoleApp.Classes;
using MyConsoleApp.Helpers;
using MyConsoleApp.Services;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;


Menu();
static void Menu()
{
    
    InventoryService inventory= new InventoryService();

    inventory.LoadData();
    bool running = true;

    
    while (running)
    {
        Console.WriteLine("---INVENTORY---");
        Console.WriteLine("1-Add Item");
        Console.WriteLine("2-Remove Item");
        Console.WriteLine("3-Update Item");
        Console.WriteLine("4-Show Inventory");
        Console.WriteLine("5-Find Items");

        Console.WriteLine("Exit-Close");
        string read= Console.ReadLine();
        
        int selection;


        if (read.ToLower() == "exit")
        {
            inventory.SaveData();
            running = false;
        }
        else if (int.TryParse(read, out selection))
        {
            switch (selection)
            {
                case 1:
                    AddItem(inventory);
                    break;
                case 2:
                    RemoveItem(inventory);
                    break;
                case 3:
                    UpdateItem(inventory);
                    break;
                case 4:
                    ShowInventory(inventory);
                    break;
                case 5:
                    FindItem(inventory);
                    break;
            }

        }
        else
        {
            Console.WriteLine("Error--Not A Number");

        }

        
    }

}

static void AddItem(InventoryService inventory) 
{
    Item item = new Item();
    while (true)
    {
        Console.WriteLine("Item Creation");
        Console.WriteLine("----------------------------");
        Console.WriteLine("*-Required Field");
        while (true)
        {
            Console.WriteLine("Item Name*:");
            string name = Console.ReadLine();

            if (inventory.Exist(StringHelper.FormatItemName(name))) 
            {
                Console.WriteLine("Item Already Exist");
                continue;
            }

            if (!string.IsNullOrEmpty(name))
            {
                item.Name = StringHelper.FormatItemName(name);//Class created by Me
                break;
            }

            Console.WriteLine("Name can't be NULL");
        }

        while (true)
        {
            Console.WriteLine("Item Type*:");
            foreach (ItemType types in Enum.GetValues(typeof(ItemType)))
            {
                Console.WriteLine($"{(int)types}-{types.ToString()}");
            }

            int type;

            if (int.TryParse(Console.ReadLine(), out type))
            {
                item.Type = (ItemType)type;
                break;
            }

            Console.WriteLine("Invalid Type Of Item.");
        }

        while (true)
        {
            Console.WriteLine("Item Description:");
            string description = Console.ReadLine();
            item.Description = description;
            break;

        }


        Console.WriteLine("Summary:\n");

        Console.WriteLine($"Item Name: {item.Name}");
        Console.WriteLine($"Item Type: {item.Type}");
        Console.WriteLine($"Item Description: {item.Description}");
        Console.WriteLine("-----------------------------------");
        bool save=false;
        string yn="";
        while (!save)
        {           
            Console.WriteLine("Save?");
            Console.WriteLine("Y/N(If no it will restart the creation)");
            yn=Console.ReadLine();
            if (yn.ToLower() == "y" || yn.ToLower() == "n")
            {
                break;
            }
            else
            {
                Console.WriteLine("Error-Put Y or N to save the Item");
            }
        }

        if (yn.ToLower() == "y")
        {
            inventory.AddItem(item);
            break;
        }   
    }
}
static void RemoveItem(InventoryService inventory) 
{
    Console.WriteLine("Inventory:");
    Console.WriteLine("----------------");
    ShowInventory(inventory);
    while (true) 
    {
        Console.WriteLine("Introduce the Id of the item you want to Delete.");
        int id;
        int.TryParse(Console.ReadLine(), out id);
        Item item= inventory.GetItemById(id);
        if ( item == null) 
        {
            Console.WriteLine("Item Not Found, try again.");
            continue;
        }

        inventory.RemoveItem(item);

        ShowInventory(inventory);
        break;


    }
}
static void UpdateItem(InventoryService inventory)
{
    while (true) {
        Item item;
        Item updatedItem;
        int index;

        while (true)
        {
            Console.WriteLine("Introduce the Id of the item you want to Update.");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            item = inventory.GetItemById(id);
            index = inventory.GetItemIndex(item);
            updatedItem = item;
            if (item == null)
            {
                Console.WriteLine("Item Not Found, try again.");
                continue;
            }
            break;

        }
        Console.WriteLine(item.ToString());

        while (true)
        {
            Console.WriteLine("What you want to Update");
            Console.WriteLine("0-Name");
            Console.WriteLine("1-Type");
            Console.WriteLine("2-Description");
            int select;
            if (!int.TryParse(Console.ReadLine(), out select))
            {
                Console.WriteLine("Not a number");
                continue;
            }

            switch (select)
            {
                case 0:
                    while (true)
                    {
                        Console.WriteLine("Item Name:");
                        string name = Console.ReadLine();

                        if (inventory.Exist(StringHelper.FormatItemName(name)))
                        {
                            Console.WriteLine("Item with this Name already exist");
                            continue;
                        }

                        if (!string.IsNullOrEmpty(name))
                        {
                            updatedItem.Name = StringHelper.FormatItemName(name);//Class created by Me
                            break;
                        }
                    }

                    break;


                case 1:
                    break;

                case 2:
                    break;

                default:
                    Console.WriteLine("Incorrect Option");
                    continue;


            }

            Console.WriteLine("----Updated Item----\n");
            Console.WriteLine(updatedItem.ToString() + "\n");


            string ysno;
            while (true)            {
               
                Console.WriteLine("Update other field?");
                Console.WriteLine("Y/N(If no it will exit the update )");
                ysno = Console.ReadLine();
                if (ysno.ToLower() == "y" || ysno.ToLower() == "n")
                {

                    break;
                }
                else
                {
                    Console.WriteLine("Error-Put Y or N to continue or not updating");
                }
            }

            if (ysno.ToLower() == "y")
            {

                continue;
            }
            else 
            {
                break;
            }

            
        }

        bool save = false;
        string yn = "";
        while (!save)
        {
            Console.WriteLine("Save Update?");
            Console.WriteLine("Y/N(If no it will restart the update process)");
            yn = Console.ReadLine();
            if (yn.ToLower() == "y" || yn.ToLower() == "n")
            {

                break;
            }
            else
            {
                Console.WriteLine("Error-Put Y or N to save the Item");
            }
        }

        if (yn.ToLower() == "y")
        {
            
            break;
        }


        inventory.UpdateItem(index,updatedItem);
        break;

    }



}
static void ShowInventory(InventoryService inventory)
{
    List<Item> items = inventory.GetItems();
    for (int i = 0; i < items.Count; i++)
    {
        Console.WriteLine(items[i].ToString());
        Console.WriteLine("\n");
    }


}
static void FindItem(InventoryService inventory) 
{
    bool execute = true;
    while (execute) 
    {
        Console.WriteLine("----Find Items By:----");
        Console.WriteLine("0-ID (Get the Item With matching ID)");
        Console.WriteLine("1-Name (Get the Item With matching Name)");
        Console.WriteLine("2-Type (Get all the Items With matching Type)");
        Console.WriteLine("3-Contains In Name (Get all the Items containing the name)");
        Console.WriteLine("Return-Returns");
        string read = Console.ReadLine();
        int select;
        if (read.ToLower()=="return")
        {
            execute = false;
        }
        else if (!int.TryParse(read, out select))
        {
            Console.WriteLine("Error Not a number.");
        }
        else 
        {
            switch (select) 
            {
                case 0:
                    while (true) {
                        Console.WriteLine("Introduce ID:");                        
                        int id;

                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Item item = inventory.GetItemById(id);

                            if (item == null) 
                            {
                                Console.WriteLine("Item not Found");
                                continue;
                            }

                            Console.WriteLine(item.ToString());
                            Console.WriteLine("\n");     
                            
                            break;
                        }
                        else 
                        {
                            Console.WriteLine("Error not a vaild Id");
                        }                        
                    }
                    break;

                case 1:
                    while (true)
                    {
                        Console.WriteLine("Introduce Name:");
                        string name=Console.ReadLine();

                        Item item = inventory.GetItemByName(name);
                        if (item == null)
                        {
                            Console.WriteLine("Item not Found");
                            continue;
                        }

                        Console.WriteLine(item.ToString());
                        Console.WriteLine("\n");
                        break; 
                    }
                    break;

                case 2:
                    while (true)
                    {
                        Console.WriteLine("Select Type:");
                        foreach (ItemType types in Enum.GetValues(typeof(ItemType)))
                        {
                            Console.WriteLine($"{(int)types}-{types.ToString()}");
                        }

                        int id;

                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            List<Item> items= inventory.GetItemsByType((ItemType)id);
                            if (items == null) 
                            {
                                Console.WriteLine("Items Not Found");
                            }
                            foreach (Item item in items) 
                            {
                                Console.WriteLine(item.ToString());
                            }
                            Console.WriteLine("\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error not a vaild number");
                        }
                    }
                    break;

                case 3:
                    while (true)
                    {
                        Console.WriteLine("Write Item Name:");
                        
                        string name= Console.ReadLine();                       

                        if (name!="")
                        {
                            List<Item> items = inventory.GetItemsWithName(name);
                            
                            if (inventory.GetListSize(items)<1)
                            {
                                Console.WriteLine("Items Not Found");
                                continue;
                            }

                            foreach (Item item in items)
                            {
                                Console.WriteLine(item.ToString());
                            }
                            Console.WriteLine("\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error Blank Space");
                        }
                    }
                    break;
            }
        }
       
    }
}
















