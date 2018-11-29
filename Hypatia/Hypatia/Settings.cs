using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable]
    class Settings
    {
        public List<Book> Books { get; set; }

        public List<User> Users { get; set; }

        public List<Loan> Loans { get; set; }

        public int LastItemID { get; set; }

        public int LastUserID { get; set; }

        public int LastLoanID { get; set; }

        public Settings(List<Book> books, List<User> users, List<Loan> loans)
        {
            Books = books;
            Users = users;
            Loans = loans;

            
            //LastID = Books.ElementAt(Books.Count - 1).ItemID;

            
        }

        public void Sanitize()
        {
            if (Books != null)
            {
                //Console.WriteLine("mpika sto if");
                LastItemID = Books.ElementAt(Books.Count - 1).ItemID;
            }
            else
            {
                //Console.WriteLine("mpika sto else");
                Books = new List<Book>();
                LastItemID = 10000;
            }

            if (Users != null)
            {
                //Console.WriteLine("mpika sto if");
                LastUserID = Users.ElementAt(Users.Count - 1).UserID;
            }
            else
            {
                //Console.WriteLine("mpika sto else");
                Users = new List<User>();
                LastUserID = 500;
            }

            if (Loans != null)
            {
                LastLoanID = Loans.ElementAt(Loans.Count - 1).LoanID;
            }
            else
            {
                Loans = new List<Loan>();
                LastLoanID = 3000;
            }
        }
    }
}
