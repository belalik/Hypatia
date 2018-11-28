using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable] class Item
    {
        private static int itemId = 10000;

        public Item(string title)
        {
            Title = title;

            // When I first create an Item, I consider it to be available (onLoan = false). 
            OnLoan = false;

            ItemID = itemId++;
        }

        public string Title { get; set; }

        public bool OnLoan { get; set; }

        public int ItemID { get; set; }

    }
}
