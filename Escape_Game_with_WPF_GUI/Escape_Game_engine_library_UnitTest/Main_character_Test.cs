using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Escape_Game_engine_library;

namespace Escape_Game_engine_library_UnitTest
{
    [TestClass]
    public class Main_character_Test
    {
        [TestMethod]
        public void Test_PickUpItem_OneAddedItemIsPresentInTheInventory()
        {
            Main_character test_character = new Main_character();
            Item test_item_0 = new Item();
            test_character.PickUpItem(test_item_0);
            Assert.IsTrue(test_character.Inventory.Contains(test_item_0));
        }

        [TestMethod]
        public void Test_PickUpItem_TwoAddedItemsArePresentInTheInventory()
        {
            Main_character test_character = new Main_character();
            Item test_item_0 = new Item();
            Item test_item_1 = new Item();
            test_character.PickUpItem(test_item_0);
            test_character.PickUpItem(test_item_1);
            Assert.IsTrue(test_character.Inventory.Contains(test_item_0) 
                && test_character.Inventory.Contains(test_item_1));
        }

        [TestMethod]
        public void Test_PickUpItem_OneAddedItemIsPresentTheOtherIsNotAdded()
        {
            Main_character test_character = new Main_character();
            Item test_item_0 = new Item();
            Item test_item_1 = new Item();
            test_character.PickUpItem(test_item_0);
            Assert.IsTrue(test_character.Inventory.Contains(test_item_0)
                && !test_character.Inventory.Contains(test_item_1));
        }

        [TestMethod]
        public void Test_LeaveItemFromSlot_DroppedItemIsTheSameAfterDroppingIt()
        {
            Main_character test_character = new Main_character();
            Item test_item_0 = new Item();
            Item test_item_1 = new Item();
            test_character.PickUpItem(test_item_0);
            test_character.PickUpItem(test_item_1);
            Assert.AreSame(test_item_0, test_character.LeaveItemFromSlot(0));
        }

        [TestMethod]
        public void Test_LeaveItemFromSlot_TwoItemsAddedOneIsStillInside()
        {
            Main_character test_character = new Main_character();
            Item test_item_0 = new Item();
            Item test_item_1 = new Item();
            test_character.PickUpItem(test_item_0);
            test_character.PickUpItem(test_item_1);
            test_character.LeaveItemFromSlot(0);
            Assert.IsTrue(test_character.Inventory.Contains(test_item_1));
        }

        [TestMethod]
        public void Test_LeaveItemFromSlot_TwoItemsAddedOneHasBeenRemoved()
        {
            Main_character test_character = new Main_character();
            Item test_item_0 = new Item();
            Item test_item_1 = new Item();
            test_character.PickUpItem(test_item_0);
            test_character.PickUpItem(test_item_1);
            test_character.LeaveItemFromSlot(0);
            Assert.IsFalse(test_character.Inventory.Contains(test_item_0));
        }
    
        [TestMethod]
        public void Test_LeaveChosenItem_TwoItemsAddedOneHasBeenRemoved()
        {
            Main_character test_character = new Main_character();
            Item test_item_0 = new Item();
            Item test_item_1 = new Item();
            test_character.PickUpItem(test_item_0);
            test_character.PickUpItem(test_item_1);
            test_character.LeaveChosenItem(test_item_1);
            Assert.IsFalse(test_character.Inventory.Contains(test_item_1));
            Assert.IsTrue(test_character.Inventory.Contains(test_item_0));
        }

        [TestMethod]
        public void Test_Item_CanItBeMoovedDefaultValueIsTrue()
        {
            Item test_item_0 = new Item();
            Item test_item_1 = new Item("noname");
            Assert.IsTrue(test_item_0.CanItBeMooved);
            Assert.IsTrue(test_item_1.CanItBeMooved);
        }

        [TestMethod]
        public void Test_Item_NameDefaultValueEqualsExpectedDefault()
        {
            Item test_item_0 = new Item();
            Item test_item_1 = new Item("noname");
            Assert.AreEqual("unnamed_item", test_item_0.Name);
            Assert.AreEqual("noname", test_item_1.Name);

        }

    }
}
