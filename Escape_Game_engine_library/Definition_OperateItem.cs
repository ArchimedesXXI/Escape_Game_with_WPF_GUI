using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Game_engine_library
{
    public class Definition_OperateItem
    {
        #region Constructors
        /// <summary>
        /// A constructor used when you want to specify everything about the "operate Item" interaction.
        /// </summary>
        public Definition_OperateItem(Item inputItem, bool canBeOperated, List<Item> outcomeItems, 
                                        string messageOnOperateAttempt, bool actionCanBeRepeated)
        {
            this.inputItem = inputItem;
            this.canBeOperated = canBeOperated;
            this.outcomeItems = outcomeItems;
            this.messageOnOperateAttempt = messageOnOperateAttempt;
            this.actionCanBeRepeated = actionCanBeRepeated;
            this.hasBeenTriedBefore = false;
        }

        /// <summary>
        /// A constructor useful when concerning an Item, that CANNOT be operated,
        /// however you  want to specify a unique message for such user attempt.
        /// </summary>
        public Definition_OperateItem (Item inputItem, string messageOnUseAttempt, bool actionCanBeRepeated = true) 
            : this(inputItem, false, new List<Item>() { }, messageOnUseAttempt, actionCanBeRepeated)
        { }
        
        /// <summary>
        /// A constructor useful when concerning an Item, that CANNOT be operated, with a default message.
        /// </summary>
        public Definition_OperateItem(Item inputItem) : this(inputItem, false, new List<Item>() { },
            string.Format("Sorry, you cannot operate {0}. It doesn't do anything.", inputItem.Name), true)
        { }
        
        /// <summary>
        /// A constructor useful when concerning an Item, that CAN be operated,
        /// but you  want to specify ONLY the outcome, leaving the standard message for that Item interaction.
        /// </summary>
        public Definition_OperateItem (Item inputItem, List<Item> outcomeItems, bool actionCanBeRepeated = false) 
            : this (inputItem, true, outcomeItems,
            string.Format("You managed to operate {0}. The resulting items appeared in your location.", inputItem.Name), 
            actionCanBeRepeated)
        { }
        #endregion

        #region Properties
        public Item inputItem { get; private set; }
        public bool canBeOperated { get; private set; }
        public List<Item> outcomeItems { get; private set; }
        public string messageOnOperateAttempt { get; private set; }
        public bool actionCanBeRepeated { get; private set; }
        public bool hasBeenTriedBefore { get; set; }
        #endregion
    }
}
