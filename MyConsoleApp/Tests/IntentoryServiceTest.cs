using MyConsoleApp.Classes;
using MyConsoleApp.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp.Tests
{

    [TestFixture]
    internal class IntentoryServiceTest
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

       
    }
}
