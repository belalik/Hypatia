using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable]
    class User

    {
        public static int staticUserID;

        public string Name { get; set; }

        public int UserID { get; set; }

        public string Email { get; set; }

        public int Telephone { get; set; }

        public User(string name, string email, int telephone)
        {
            Name = name;
            Email = email;
            Telephone = telephone;

            UserID = staticUserID++;
        }
    }
}
