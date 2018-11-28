using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable] class Book : Item
    {

        public string Author { get; set; }

        public Book (string title, string author)
            : base (title) {

            Author = author;
        }
        
    }
}
