using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    class Loan
    {
        public static int staticLoanID;

        public DateTime DateLoaned { get; set; }
        public int LoanID { get; set; }
        public Item ItemLoaned { get; set; }
        public User UserLoaning { get; set; }

        public Loan(DateTime dateloaned, Item itemloaned, User userloaning)
        {
            
            LoanID = staticLoanID;  // auto increment value for LoanID.

            DateLoaned = dateloaned;
            ItemLoaned = itemloaned;
            UserLoaning = userloaning;

        }
    }
}
