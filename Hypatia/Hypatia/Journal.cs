using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable]
    class Journal : Item
    {
        public string Publisher { get; set; }

        public Journal(string title, string publisher)
            : base(title)
        {

            Publisher = publisher;
        }
    }
}
