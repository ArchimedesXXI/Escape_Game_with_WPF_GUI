using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Escape_Game_engine_library
{
    public class Game_Control_Center
    {
        public Game_Control_Center()
        {
            // Instantiating Main_character instance
            this.Player = new Main_character();
            
            // Assigning a starting location for the player:
            this.PlayersLocation = garden;

            #region Defining what items can be used with what, and what are the effects
            /// A List of Definition_UseItemWithItem objects. 
            /// Each object in this list forms a definiton, of what should be the result of using one Item with another Item (in the game).
            /// The definition must contain exact instances of both of interacting Items. 
            /// The definintion might contain: 
            ///     the message to be displayed as a result of interaction, 
            ///     and a List of Items that is the result of the interaction.   
            this.AllDefinitions_UseItemWithItem = new List<Definition_UseItemWithItem>() 
            {
                new Definition_UseItemWithItem 
                    (swimmingPool, poolCleaningNet, true, new List<Item>() { swimmingPool, poolCleaningNet, smallPlasticBall }, string.Format("You fished a {0} out of the water.", smallPlasticBall.Name), false),
                new Definition_UseItemWithItem 
                    (grassyLawn, rake, new List<Item>() { grassyLawn, rake, largeKey }),
                new Definition_UseItemWithItem 
                    (pocketKnife, leatherSuitcase, true, new List<Item>() { leatherSuitcase, pocketKnife, smallMetalKey }, string.Format("You cut open the {0} and found a {1}.", leatherSuitcase.Name, smallMetalKey.Name), false),
                new Definition_UseItemWithItem 
                    (smallMetalKey, cupboard, new List<Item>() { unlockedCupboard }, false),
                new Definition_UseItemWithItem 
                    (wrench, gardenHose, true, new List<Item>() { disconnectedGardenHose }, string.Format("You unscrewed the {0} with the {1}.", gardenHose.Name, wrench.Name), false),
                new Definition_UseItemWithItem 
                    (pocketKnife, smallPlasticBall, true, new List<Item>() { mediumKey, pocketKnife }, string.Format("You managed to open the {0} with the {1} and found in it a {2}.", smallPlasticBall.Name, pocketKnife.Name, mediumKey.Name), false),
                new Definition_UseItemWithItem 
                    (mediumKey, secretStorageCompartment, true, new List<Item>() { secretStorageCompartment, electricalWaterPump }, string.Format("You unlocked the {0} and found in it an {1}.", secretStorageCompartment.Name, electricalWaterPump.Name), false),
                new Definition_UseItemWithItem 
                    (disconnectedGardenHose, electricalWaterPump, new List<Item>() { electricalWaterPumpWithHose }, false),        
                new Definition_UseItemWithItem 
                    (electricalWaterPump, swimmingPool, string.Format("The {0} has no tube. You can't pump water with it.", electricalWaterPump.Name), true),
                new Definition_UseItemWithItem 
                    (electricalWaterPumpWithHose, swimmingPool, true, new List<Item>() { swimmingPool, hiddenDoor }, string.Format("You pumped the water out of the {0}. In the bottom of the {0} you notice a {1}.", swimmingPool.Name, hiddenDoor.Name), false),
                new Definition_UseItemWithItem 
                    (hiddenDoor, largeKey, true, new List<Item>() { mysteriousTunnel }, string.Format("You unlocked the {0}. Behind it lies a {1}.", hiddenDoor.Name, mysteriousTunnel.Name), false),
                
                //TODO: to expand the game - define new "use item with item" type interacions here 
            };
            #endregion

            #region Defining what items can be operated, and what are the effects
            /// A List of Definition_OperateItem objects. 
            /// Each object in this list forms a definiton, of what should be the result of "operating an Item" (in the game).
            /// The definition must contain the exact instance of Item to be operated on. 
            /// The definintion might contain: 
            ///     the message to be displayed as a result of operating attempt, 
            ///     and a List of Items that are the result of operating action.   
            this.AllDefinitions_OperateItem = new List<Definition_OperateItem>()
            {            
                new Definition_OperateItem 
                    (leatherSuitcase, string.Format("You tried to open the {0}, but you can't seem to open it.", leatherSuitcase.Name)),
                new Definition_OperateItem 
                    (cupboard, string.Format("You tried to open the {0}. It is locked.", cupboard.Name)),
                new Definition_OperateItem 
                    (bookshelf, true, new List<Item>() { secretStorageCompartment }, string.Format("You pulled the {0} and revealed a {1}.", bookshelf.Name, secretStorageCompartment.Name), false),
                new Definition_OperateItem 
                    (secretStorageCompartment, string.Format("You tried to open the {0}, but it is locked.", secretStorageCompartment.Name)),
                new Definition_OperateItem 
                    (unlockedCupboard, true, new List<Item>() { unlockedCupboard, wrench }, string.Format("You opened the {0} and found a {1} inside.", unlockedCupboard.Name, wrench.Name), false),
                new Definition_OperateItem 
                    (gardenHose, string.Format("The {0} is screwed on to the wall. You won't unscrew it with your bare hands.", gardenHose.Name)),
                new Definition_OperateItem 
                    (electricalWaterPump, string.Format("The {0} has a buit-in battery. I suppose you could pump water with it, but it has no pipe.", electricalWaterPump.Name)),
                new Definition_OperateItem 
                    (swimmingPool, string.Format("Nice idea, but I don't have time for a swim just now.")),
                new Definition_OperateItem 
                    (smallPlasticBall, string.Format("Upon closer inspection you notice, that the {0} seems to be composed of two halves, however you are unable to open it with your bare hands.", smallPlasticBall.Name)),
                new Definition_OperateItem 
                    (hiddenDoor, string.Format("The {0} is locked. No way will I get it open without some tools.", hiddenDoor.Name)),
                new Definition_OperateItem 
                    (mysteriousTunnel, string.Format(" * Hooray! * Congratulations! *   You discovered the long lost... car keys ;)   You solved the mystery of the Villa. Now it's time to relax on the sunbed ;)")),

                //TODO: define new "operate item" type interacions here 
            };
            #endregion

        }

        #region declaring fields and properties to be set in constructor
        public Main_character Player { get; private set; }
        public Location PlayersLocation { get; private set; }
        private List<Definition_UseItemWithItem> AllDefinitions_UseItemWithItem;
        private List<Definition_OperateItem> AllDefinitions_OperateItem;
        #endregion



        #region Istantiating Item istances
        // Instantiating Item istances, which are ment to be:
        //   * with the player on start-up
        private Item pocketKnife = new Item("pocket knife", true);
        private Item plasticComb = new Item("plastic comb", true);
        private Item packOfKleneex = new Item("pack of kleneex", true);
        //   * by the swimming pool on start-up
        private Item swimmingPool = new Item("swimming pool", false);
        private Item poolCleaningNet = new Item("pool cleaning net", true);
        private Item sunbed = new Item("sunbed", false);
        //   * in the garden on start-up
        private Item grassyLawn = new Item("grassy lawn", false);
        private Item gardenHose = new Item("garden hose", false);
        private Item gardenChair = new Item("garden chair", false);
        private Item rake = new Item("rake", true);
        //   * in the kitchen on start-up
        private Item cupboard = new Item("cupboard", false);
        //   * in the library on start-up
        private Item antiqueArmchair = new Item("antique armchair", false);
        private Item bookshelf = new Item("bookshelf", false);
        private Item leatherSuitcase = new Item("leather suitcase", true);

        // Instantiating Item istances, which are the efect of using Items on each other: 
        private Item smallPlasticBall = new Item("small plastic ball", true);
        private Item smallMetalKey = new Item("small metal key", true);
        private Item disconnectedGardenHose = new Item("disconnected garden hose", true);
        private Item unlockedCupboard = new Item("unlocked cupboard", false);
        private Item secretStorageCompartment = new Item("secret storage compartment", false);
        private Item mediumKey = new Item("medium key", true);
        private Item wrench = new Item("wrench", true);
        private Item electricalWaterPump = new Item("electrical water pump", true);
        private Item largeKey = new Item("large key", true);
        private Item hiddenDoor = new Item("hidden door", false);
        private Item electricalWaterPumpWithHose = new Item("electrical water pump with hose", true);
        private Item mysteriousTunnel = new Item("mysterious tunnel", false);
        #endregion

        #region Instantiating Location instances
        // Instantiating Location instances:
        public Location kitchen = new Location("the kitchen");
        public Location library = new Location("the library");
        public Location swimmingPoolArea = new Location("the swimming pool area");
        public Location garden = new Location("the garden");
        #endregion

        #region Filling Locations and Character's inventory with initial Items on game startup
        /// <summary>
        /// This method is to be called before the game starts. It filles (adds) Item instances to Location instances
        /// and to the Main_character instance, that are ment to be there, when the game starts.
        /// </summary>
        public void FillLocationAndCharacterWithInitialItemsOnGameStartup()
        {
            // Adding Item instances to Location instances (filling Locations' content with their initial Item objects) 
            swimmingPoolArea.Content = new List<Item>() { swimmingPool, poolCleaningNet, sunbed }; 
            garden.Content = new List<Item>() { grassyLawn, gardenHose, gardenChair, rake };
            kitchen.Content = new List<Item>() { cupboard };
            library.Content = new List<Item>() { bookshelf, antiqueArmchair, leatherSuitcase };
            
            // Adding Item instances to the Main_character instance (filling Main_character's inventory with initial Item objects) 
            Player.Inventory = new List<Item>() { packOfKleneex, plasticComb, pocketKnife };

        }
        #endregion



        #region operate Item functionality
        /// <summary>
        /// This method returns a Definition_OperateItem object (instance), which defines the effects of "operation" attempt
        /// on Item passed in as argument.
        /// If "operation" attempt was not defined for given Item, it returns a new Definition_OperateItem instance, 
        /// which is build using a constructor (with default settings) for Item that can't be operated on in the game. 
        /// </summary>
        private Definition_OperateItem GetInstanceOfDefinitionOfOperate(Item inputItem)
        {
            foreach (Definition_OperateItem definition in AllDefinitions_OperateItem)
            {
                if (definition.inputItem == inputItem)
                {
                    return definition;
                }
            }
            return new Definition_OperateItem(inputItem);
        }


        /// <summary>
        /// This method manages all the logic related to OPERATING ITEM game functionality.
        /// </summary>
        /// <param name="userInput">string input from user input text-box</param>
        /// <returns>string message to be displayed to the user</returns>
        public string OperateItem(string userInput)
        {
            string message = "";

            if (userInput == "")
            {
                message = "Please, type in the name of item and then press \"operate...\"";
            }
            else if (!PlayersLocation.GetLocationContentAsListOfString().Contains(userInput)
                    && !Player.GetCharactersInventoryAsListOfString().Contains(userInput))
            {
                message = string.Format("Item named \"{0}\" is neither in your inventory nor in your current location!", userInput);
            }
            else
            {
                Debug.Assert(PlayersLocation.GetLocationContentAsListOfString().Contains(userInput) || Player.GetCharactersInventoryAsListOfString().Contains(userInput));
                Item item;

                if (IsItemInstanceWithGivenNamePresent(userInput, Player.Inventory))
                    item = ReturnItemInstanceWithGivenName(userInput, Player.Inventory);
                else
                    item = ReturnItemInstanceWithGivenName(userInput, PlayersLocation.Content);
               
                Debug.Assert(PlayersLocation.Content.Contains(item) || Player.Inventory.Contains(item));

                Definition_OperateItem definition = GetInstanceOfDefinitionOfOperate(item);
                message = definition.messageOnOperateAttempt;
                if (definition.hasBeenTriedBefore && !definition.actionCanBeRepeated)
                {
                    message = "You've already done this. Try something new.";
                }
                else if ((definition.canBeOperated && definition.actionCanBeRepeated) 
                        || (definition.canBeOperated && !definition.hasBeenTriedBefore))
                {
                    Debug.Assert(definition.actionCanBeRepeated || !definition.hasBeenTriedBefore);

                    if (Player.Inventory.Contains(item))
                        Player.LeaveChosenItem(item);
                    else if (PlayersLocation.Content.Contains(item))
                        PlayersLocation.TakeItemFromLocation(item);

                    foreach (Item outcomeItem in definition.outcomeItems)
                    {
                        PlayersLocation.AddItemToLocation(outcomeItem);
                    }
                    definition.hasBeenTriedBefore = true;
                }
            }
            return message;
        }
        #endregion

        #region use Item with Item functionality
        /// <summary>
        /// This method returns a Definition_UseItemWithItem object which defines the interacion 
        /// between Items passed in as arguments.
        /// If an interaction was not defined for given Items, it returns a new Definition_UseItemWithItem instance, 
        /// which is build using a constructor for Items that are not interacting in the game. 
        /// </summary>
        private Definition_UseItemWithItem GetInstanceOfDefinitionOfUse(Item inputItem1, Item inputItem2)
        {
            foreach (Definition_UseItemWithItem definition in AllDefinitions_UseItemWithItem)
            {
                if ((definition.inputItem1 == inputItem1 && definition.inputItem2 == inputItem2) 
                    || (definition.inputItem1 == inputItem2 && definition.inputItem2 == inputItem1))
                {
                    return definition;
                }
            }
            var result = new Definition_UseItemWithItem(inputItem1, inputItem2);
            return result;
        }

        
        /// <summary>
        /// This method manages all the logic related to USING ITEM WITH ITEM game functionality.
        /// </summary>
        /// <param name="input1">string input from 1-st text-box</param>
        /// <param name="input2">string input from 2-nd text-box</param>
        /// <returns>string message to be displayed to the user</returns>
        public string UseItem(string input1, string input2)
        {
            string message = "";

            if (input1 == "" || input2 == "")
            {
                message = "Please, type in names of both items and then press \"use...\"";
            }
            else if (!PlayersLocation.GetLocationContentAsListOfString().Contains(input1)
                    && !Player.GetCharactersInventoryAsListOfString().Contains(input1))
            {
                message = string.Format("Item named \"{0}\" is neither in your inventory nor in your current location!", input1);
            }
            else if (!PlayersLocation.GetLocationContentAsListOfString().Contains(input2)
                    && !Player.GetCharactersInventoryAsListOfString().Contains(input2))
            {
                message = string.Format("Item named \"{0}\" is neither in your inventory nor in your current location!", input2);
            }
            else
            {
                Debug.Assert(PlayersLocation.GetLocationContentAsListOfString().Contains(input1) || Player.GetCharactersInventoryAsListOfString().Contains(input1));
                Debug.Assert(PlayersLocation.GetLocationContentAsListOfString().Contains(input2) || Player.GetCharactersInventoryAsListOfString().Contains(input2));
                Item item1;
                Item item2;
                
                if (IsItemInstanceWithGivenNamePresent(input1, Player.Inventory))
                    item1 = ReturnItemInstanceWithGivenName(input1, Player.Inventory);
                else
                    item1 = ReturnItemInstanceWithGivenName(input1, PlayersLocation.Content);
                if (IsItemInstanceWithGivenNamePresent(input2, Player.Inventory))
                    item2 = ReturnItemInstanceWithGivenName(input2, Player.Inventory);
                else
                    item2 = ReturnItemInstanceWithGivenName(input2, PlayersLocation.Content);

                Debug.Assert(PlayersLocation.Content.Contains(item1) || Player.Inventory.Contains(item1));
                Debug.Assert(PlayersLocation.Content.Contains(item2) || Player.Inventory.Contains(item2));

                Definition_UseItemWithItem definition = GetInstanceOfDefinitionOfUse(item1, item2);
                message = definition.messageOnUseAttempt;
                if (definition.hasBeenTriedBefore && !definition.actionCanBeRepeated)
                {
                    message = "You've already done this. Try something new.";
                }
                else if ((definition.canBeUsedTogether && definition.actionCanBeRepeated) 
                    || (definition.canBeUsedTogether && !definition.hasBeenTriedBefore))
                {
                    Debug.Assert(definition.actionCanBeRepeated || !definition.hasBeenTriedBefore);

                    if (Player.Inventory.Contains(item1))
                        Player.LeaveChosenItem(item1);
                    else if (PlayersLocation.Content.Contains(item1))
                        PlayersLocation.TakeItemFromLocation(item1);
                    if (Player.Inventory.Contains(item2))
                        Player.LeaveChosenItem(item2);
                    else if (PlayersLocation.Content.Contains(item2))
                        PlayersLocation.TakeItemFromLocation(item2);

                    foreach (Item item in definition.outcomeItems)
                    {
                        PlayersLocation.AddItemToLocation(item);
                    }
                    definition.hasBeenTriedBefore = true;
                }
            }
            return message;
        }
        #endregion

        #region pick up Item functionality
        /// <summary>
        /// This method manages all the logic related to PICKING UP ITEM game functionality.
        /// </summary>
        /// <returns>string message to be displayed to the user</returns>
        public string PickUpItem(string userInput)
        {
            string message = "";

            if (userInput == "")
            {
                message = "Please, type in the name of the item and then press \"pick up...\"";
            }
            else if (!PlayersLocation.GetLocationContentAsListOfString().Contains(userInput))
            {
                message = string.Format("There's no item named \"{0}\" in your current location!", userInput);
            }
            else
            {
                foreach (Item item in PlayersLocation.Content)
                {
                    if (item.Name == userInput && item.CanItBeMooved)
                    {
                        PlayersLocation.TakeItemFromLocation(item);
                        Player.PickUpItem(item);
                        message = string.Format("You picked up {0}.", item.Name);
                        break;
                    }
                    else if (item.Name == userInput && !item.CanItBeMooved)
                    {
                        message = string.Format("Sorry, {0} cannot be moved!", item.Name);
                        break;
                    }
                }
            }
            return message;
        }
        #endregion

        #region leave Item functionality
        /// <summary>
        /// This method manages all the logic related to LEAVING ITEM game functionality.
        /// </summary>
        /// <returns>string message to be displayed to the user</returns>
        public string LeaveItem(string userInput)
        {
            string message = "";

            if (userInput == "")
            {
                message = "Please, type in the name of the item and then press \"leave...\"";
            }
            else if (!Player.GetCharactersInventoryAsListOfString().Contains(userInput))
            {
                message = string.Format("You don't have an item named \"{0}\" with you!", userInput);
            }
            else
            {
                foreach (Item item in Player.Inventory)
                {
                    if (item.Name == userInput)
                    {
                        PlayersLocation.AddItemToLocation(item);
                        Player.LeaveChosenItem(item);
                        message = string.Format("You left {0} in {1}.", item.Name, PlayersLocation.Name);
                        break;
                    }
                }
            }
            return message;
        }
        #endregion

        #region Go to location functionality
        public void GoTo(Location theLocation)
        {
            this.PlayersLocation = theLocation;
        }
        #endregion



        #region Looking for Item instances with a given name, Returning Item instances with a given name
        /// <summary>
        /// Return true if an instance of class Item, which Name matches theName, is present in a given list.
        /// </summary>
        private bool IsItemInstanceWithGivenNamePresent(string theName, List<Item> list)
        {
            foreach (Item item in list)
            {
                if (item.Name == theName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return an actual instance of class Item, which Name matches given argument, from a given List of Items.  
        /// </summary>
        private Item ReturnItemInstanceWithGivenName(string theName, List<Item> list)
        {
            foreach (Item item in list)
            {
                if (item.Name == theName)
                {
                    return item;
                }
            }
            return null;
        }
        #endregion

    }
}
