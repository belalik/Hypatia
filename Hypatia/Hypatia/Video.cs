using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable]

    class Video : Item
    {
        public int Duration { get; set; }

        public Video(string title, int duration)
            : base(title)
        {

            Duration = duration;
        }
    }
}
