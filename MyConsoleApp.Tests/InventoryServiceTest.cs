using MyConsoleApp.Core.Models;
using MyConsoleApp.Core.Helpers;
using MyConsoleApp.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp.Tests
{

    [TestFixture]
    internal class InventoryServiceTest
    {
        

        [Test]
        public void TestAddItem_ShouldAddItem() 
        {
            InventoryService inventory = new InventoryService();

            Item item = new Item
            {
                Name = "Sword",
                Type = ItemType.Weapon,
                Description = "Sharp Sword",
                Quantity = 1


            };

            inventory.AddItem(item);    

            Assert.That(inventory.GetItemById(item.Id), Is.Not.Null);
        }

        [Test]
        public void TestRemoveItem_ShouldRemoveItem()
        {
            InventoryService inventory = new InventoryService();

            Item item = new Item
            {
                Name = "Sword",
                Type = ItemType.Weapon,
                Description = "Sharp Sword",
                Quantity = 1

            };

            inventory.AddItem(item);


            inventory.RemoveItem(item);

            Assert.That(inventory.GetItemById(item.Id), Is.Null);
        }

        [Test]
        public void TestUpdateItem_ShouldUpdateItem()
        {
            InventoryService inventory = new InventoryService();

            Item item = new Item
            {
                Name = "Sword",
                Type = ItemType.Weapon,
                Description = "Sharp Sword",
                Quantity = 1

            };


            inventory.AddItem(item);

            Item updatedItem = new Item
            {
                Name = item.Name,
                Type = item.Type,
                Description = "Very Sharp Sword",
                Quantity = item.Quantity,
            };

            inventory.UpdateItem(inventory.GetItemIndex(item),updatedItem);

            Assert.That(item,Is.Not.SameAs(updatedItem));
        }

        [Test]
        public void TestGetItemById_ShouldReturnIdItem() 
        {
            InventoryService inventory = new InventoryService();
            Item item = new Item
            {
                Name = "Sword",
                Type = ItemType.Weapon,
                Description = "Sharp Sword",
                Quantity = 1

            };            

            inventory.AddItem(item);
            


            Item testItem= inventory.GetItemById(item.Id);

            Assert.That(testItem, Is.SameAs(item));

        }

        [Test]
        public void TestGetItemByName_ShouldReturnNameItem()
        {
            InventoryService inventory = new InventoryService();
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 1

            };

            inventory.AddItem(item);



            Item testItem = inventory.GetItemByName(StringHelper.FormatItemName(item.Name));

            Assert.That(testItem, Is.SameAs(item));

        }

        [Test]
        public void TestGetItemsByType_ShouldReturnTypeItems()
        {
            InventoryService inventory = new InventoryService();
            ItemType type = ItemType.Weapon;
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = type,
                Description = "God Sword",
                Quantity = 1

            };
            Item item2 = new Item
            {
                Name = "Potion",
                Type = ItemType.Consumable,
                Description = "Potion",
                Quantity = 1

            };

            inventory.AddItem(item);
            inventory.AddItem(item2);

            

            List<Item> testItem = inventory.GetItemsByType(type);

            Assert.That(testItem, Is.Not.Empty);

            foreach (Item testedItem in testItem) 
            {
                Assert.That(testedItem.Type, Is.EqualTo(type));
            }
            

        }

        [Test]
        public void TestGetItemsByType_ShouldReturnEmptyList()
        {
            InventoryService inventory = new InventoryService();
            ItemType type = ItemType.Weapon;
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = type,
                Description = "God Sword",
                Quantity = 1

            };
            Item item2 = new Item
            {
                Name = "Potion",
                Type = ItemType.Consumable,
                Description = "Potion",
                Quantity = 1

            };

            inventory.AddItem(item);
            inventory.AddItem(item2);            

            List<Item> testItem = inventory.GetItemsByType(ItemType.Armor);


            Assert.That(testItem, Is.Empty);

        }

        [Test]
        public void TestItemExistById_ShouldReturnTrue()
        {
            InventoryService inventory = new InventoryService();
            
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 1
            };            

            inventory.AddItem(item);
            Assert.That(inventory.Exist(item.Id), Is.True);

        }

        [Test]
        public void TestItemExistByName_ShouldReturnTrue()
        {
            InventoryService inventory = new InventoryService();

            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 1

            };


            inventory.AddItem(item);
            Assert.That(inventory.Exist(item.Name), Is.True);

        }

        [Test]
        public void TestSortByName_Ascending_ShouldSortCorrectly() 
        {
            InventoryService inventory = new InventoryService();
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 1

            };
            Item item2 = new Item
            {
                Name = "Potion",
                Type = ItemType.Consumable,
                Description = "Potion",
                Quantity = 1

            };
            Item item3 = new Item
            {
                Name = "Armor Plate",
                Type = ItemType.Armor,
                Description = "Potion",
                Quantity = 1

            };
            inventory.AddItem(item);
            inventory.AddItem(item2);
            inventory.AddItem(item3);

            List<Item> itemlist = inventory.SortedByName(true);


            Assert.That(itemlist[0].Name=="Armor Plate");
            Assert.That(itemlist[1].Name=="Holy Sword");
            Assert.That(itemlist[2].Name=="Potion");

        }

        [Test]
        public void TestSortByName_Descending_ShouldSortCorrectly()
        {
            InventoryService inventory = new InventoryService();
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 1

            };
            Item item2 = new Item
            {
                Name = "Potion",
                Type = ItemType.Consumable,
                Description = "Potion",
                Quantity = 1

            };
            Item item3 = new Item
            {
                Name = "Armor Plate",
                Type = ItemType.Armor,
                Description = "Potion",
                Quantity = 1

            };
            inventory.AddItem(item);
            inventory.AddItem(item2);
            inventory.AddItem(item3);

            List<Item> itemlist = inventory.SortedByName(false);


            Assert.That(itemlist[0].Name == "Potion");
            Assert.That(itemlist[1].Name == "Holy Sword");           
            Assert.That(itemlist[2].Name == "Armor Plate");

        }

        [Test]
        public void TestSortByQuantity_Ascending_ShouldSortCorrectly()
        {
            InventoryService inventory = new InventoryService();
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 3

            };
            Item item2 = new Item
            {
                Name = "Potion",
                Type = ItemType.Consumable,
                Description = "Potion",
                Quantity = 5

            };
            Item item3 = new Item
            {
                Name = "Armor Plate",
                Type = ItemType.Armor,
                Description = "Armor plate",
                Quantity = 1

            };
            inventory.AddItem(item);
            inventory.AddItem(item2);
            inventory.AddItem(item3);

            List<Item> itemlist = inventory.SortedByName(true);
           
            Assert.That(itemlist[0].Name == "Armor Plate");
            Assert.That(itemlist[1].Name == "Holy Sword");
            Assert.That(itemlist[2].Name == "Potion");

        }

        [Test]
        public void TestSortByQuantity_Descending_ShouldSortCorrectly()
        {
            InventoryService inventory = new InventoryService();
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 3

            };
            Item item2 = new Item
            {
                Name = "Potion",
                Type = ItemType.Consumable,
                Description = "Potion",
                Quantity = 5

            };
            Item item3 = new Item
            {
                Name = "Armor Plate",
                Type = ItemType.Armor,
                Description = "Armor Plate",
                Quantity = 1

            };
            inventory.AddItem(item);
            inventory.AddItem(item2);
            inventory.AddItem(item3);

            List<Item> itemlist = inventory.SortedByName(false);

            Assert.That(itemlist[0].Name == "Potion");
            Assert.That(itemlist[1].Name == "Holy Sword");
            Assert.That(itemlist[2].Name == "Armor Plate");


        }

        [Test]
        public void TestPersistence_ShouldCreateFileAndLoadFile() 
        {
            InventoryService inventory = new InventoryService("test_data.json");
            Item item = new Item
            {
                Name = "Holy Sword",
                Type = ItemType.Weapon,
                Description = "God Sword",
                Quantity = 3

            };

            inventory.AddItem(item);

            inventory.SaveData();

            InventoryService inventory2 = new InventoryService("test_data.json");

            inventory2.LoadData();

            Assert.That(inventory2.GetItems(),Is.Not.Empty);
        }



    }
}
