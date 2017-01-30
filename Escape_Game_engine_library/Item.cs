using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Game_engine_library
{
    public class Item
    {
        #region Constructors

        public Item(string theName, bool canItBeMooved = true)
        {
            this.name = theName;
            this.canItBeMooved = canItBeMooved;
        }

        public Item() : this("unnamed_item") { }

        #endregion

        #region Fields
        private string name;
        private bool canItBeMooved;
        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
        }

        public bool CanItBeMooved
        {
            get { return canItBeMooved; }
        }

        #endregion
    }
}
