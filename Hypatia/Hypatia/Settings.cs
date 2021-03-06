﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    [Serializable]
    class Settings
    {
        //public List<Book> Books { get; set; }

        //public List<Video> Videos { get; set; }

        //public List<Journal> Journals { get; set; }

        public List<Item> Items { get; set; }

        public List<User> Users { get; set; }

        public List<Loan> Loans { get; set; }

        //public int LastBookID { get; set; }

        //public int LastVideoID { get; set; }

        //public int LastJournalID { get; set; }

        public int LastItemID { get; set; }

        public int LastUserID { get; set; }

        public int LastLoanID { get; set; }

        //public Settings(List<Book> books, List<Video> videos, List<Journal> journals, List<User> users, List<Loan> loans)
        public Settings(List<Item> items, List<User> users, List<Loan> loans)
        {
            //Books = books;
            //Videos = videos;
            //Journals = journals;
            if (users == null)
            {
                //Console.WriteLine("here it is null");
            }
            else
            {
                //Console.WriteLine("it is NOT NULL !!! and count is: "+users.Count);
            }
            Items = items;
            Users = users;
            Loans = loans;

            
            //LastID = Books.ElementAt(Books.Count - 1).ItemID;

            
        }

        public void Sanitize()
        {
            

            if (Items == null)
            {
                Items = new List<Item>();
                LastItemID = 10000;
            }
            else if (Items.Count==0)
            {
                LastItemID = 10000;
            }
            else
            {
                LastItemID = Items.ElementAt(Items.Count - 1).ItemID;
            }

            if (Users==null)
            {
                Users = new List<User>();
                LastUserID = 500;
            }
            else if (Users.Count==0)
            {
                LastUserID = 500;
            }
            else
            {
                LastUserID = Users.ElementAt(Users.Count - 1).UserID;
            }

            if (Loans == null)
            {
                //Console.WriteLine("mpika loans == null");
                Loans = new List<Loan>();
                LastLoanID = 3000;
            }
            else if (Loans.Count == 0)
            {
                //Console.WriteLine("mpika loans.count==0");
                LastLoanID = 3000;
            }
            else
            {
                LastLoanID = Loans.ElementAt(Loans.Count - 1).LoanID;
            }

            
        }
    }
}
