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

    }
}
