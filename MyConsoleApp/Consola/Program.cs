// See https://aka.ms/new-console-template for more information

using MyConsoleApp.Classes;
using MyConsoleApp.Services;
using System.Diagnostics;


Menu();
static void Menu()
{
    InventoryService inventory= new InventoryService();

    bool running = true;
    while (running)
    {
        Console.WriteLine("---INVENTORY---");
        Console.WriteLine("1-AddItem");
        Console.WriteLine("2-Show Inventory");
        Console.WriteLine("Exit-Close");
        string read= Console.ReadLine();
        
        int selection;


        if (read.ToLower() == "exit")
        {
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
                    ShowInventory(inventory);
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
            if (!string.IsNullOrEmpty(name))
            {
                item.Name = name;
                break;
            }

            Console.WriteLine("Name can't be NULL");
        }

        while (true)
        {
            Console.WriteLine("Item Type*:");
            Console.WriteLine(
                "0-Weapon.\n" +
                "1-Armor.\n" +
                "2-Consumable."
                );
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

static void ShowInventory(InventoryService inventory) 
{
    List<Item> items = inventory.GetItems();
    for (int i = 0; i < items.Count; i++) 
    {
        Console.WriteLine(items[i].ToString());
        Console.WriteLine("\n");
    }


    Console.WriteLine("Continue?");
    Console.ReadKey();
}











