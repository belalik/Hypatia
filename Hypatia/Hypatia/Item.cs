using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable] class Item
    {
        public static int staticID;

        
        
        public Item(string title)
        {
            Title = title;

            // When I first create an Item, I consider it to be available (onLoan = false). 
            OnLoan = false;

            ItemID = staticID++;
        }

        public string Title { get; set; }

        public bool OnLoan { get; set; }

        public int ItemID { get; private set; }

        
    }
}
