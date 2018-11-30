using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{

    [Serializable]
    class Library
    {

        public List<Item> Items { get; set; }

        public User CurrentUser { get; set; }

        //public List<Book> Books { get; set; }

        //public List<Video> Videos { get; set; }

        //public List<Journal> Journals { get; set; }

        public List<Loan> Loans { get; set; }

        public Settings LibrarySettings { get; set; }

        public int MaxNumberOfLoans { get; private set; }

        public string FilenamePath { get; set; }

        public List<User> Users { get; set; }

        //public List<Loan> Loans { get; set; }

        public Library(string filenamePath)
        {
            FilenamePath = filenamePath;

            MaxNumberOfLoans = 5;
            
            // This creates AND SANITIZES a Settings Object.
            LoadSettings();

            Items = LibrarySettings.Items;
            Users = LibrarySettings.Users;
            Loans = LibrarySettings.Loans;

            User.staticUserID = (LibrarySettings.LastUserID) + 1;
            Item.staticID = (LibrarySettings.LastItemID) + 1;
            Loan.staticLoanID = (LibrarySettings.LastLoanID) + 1;

            //Instantiate();
            //Individuate();

            //Books = new List<Book>();   // Books intentionally not initialized (commented out)
            //int lastid = Item.staticID;

            //LibrarySettings = new Settings(Books, lastid);


        }


        public void ShowAllItems()
        {

            List<Book> books = Items.OfType<Book>().ToList();

            List<Video> videos = Items.OfType<Video>().ToList();

            List<Journal> journals = Items.OfType<Journal>().ToList();

            foreach (Book b in books)
            {
                Console.WriteLine("BOOK - ID: [" + b.ItemID + "], \"" + b.Title + "\"" + " by " + b.Author + " --- " + (b.OnLoan ? "On LOAN" : "AVAILABLE"));
            }
            foreach (Video v in videos)
            {
                Console.WriteLine("VIDEO - ID: [" + v.ItemID + "], \"" + v.Title + "\"" +
                            " with a duration of:  " + v.Duration + " min --- " +
                            (v.OnLoan ? "On LOAN " : "AVAILABLE"));
            }
            foreach (Journal j in journals)
            {
                Console.WriteLine("JOURNAL - ID: [" + j.ItemID + "], \"" + j.Title + "\"" + " published by:  " + j.Publisher + " --- " + (j.OnLoan ? "On LOAN" : "AVAILABLE"));
            }

            Console.WriteLine();
            Console.WriteLine("Total of "+Items.OfType<Book>().Count()+" books, "+ Items.OfType<Video>().Count() + " videos and "+ Items.OfType<Journal>().Count() + " journals");
           

        }

        public void ShowAllUsers()
        {
            foreach (User u in Users)
            {
                Console.WriteLine("User "+u.Name+" with UserID: ["+u.UserID+"]");
            }
        }

        // REFACTOR THIS - WILL BE ONLY ONE GENERIC FUNCTION THAT ACCEPTS AN INCOMING ARGUMENT OF WHAT TYPE TO SEARCH FOR. 
        public void ShowAllBooks()
        {

            List<Book> books = Items.OfType<Book>().ToList();

            if (books != null)
            {
                if (books.Count == 0)
                {
                    Console.WriteLine("No videos in our library yet.");
                }
                else
                {
                    foreach (Book b in books)
                    {
                        string name = "";
                        if (AtLeastOneBookLoaned())
                        {
                            name = Loans.Find(x => x.ItemLoaned.ItemID == b.ItemID).UserLoaning.Name;
                        }


                        Console.WriteLine("ID: [" + b.ItemID + "], \"" + b.Title + "\"" +
                            " by:  " + b.Author + " min --- " +
                            (b.OnLoan ? "On LOAN by " + name : "AVAILABLE"));


                        // var currentLoans = Loans.FindAll(x => x.UserLoaning.UserID == CurrentUser.UserID);
                    }
                }
            }
            //foreach (Item i in Items)
            //{

            //    /*
            //     *  Χρησιμοποιώ το conditional operator '?:' - λέγεται και ternary operator. 
            //     *  Έχει τη γενική μορφή: ' condition ? consequence : alternative '
            //     *  
            //     *  που σημαίνει 'Αν (condition==TRUE) κάνε το consequence, αλλιώς κάνε το alternative. 
            //     *  
            //     *  Ουσιαστικά το χρησιμοποιώ έτσι ώστε να γράψω τη λέξη 'On LOAN' αν είναι δανεισμένο σε κάποιον 
            //     *  (αν δηλαδή η OnLoan ιδιότητα είναι TRUE) αλλιώς να γράψω τη λέξη 'AVAILABLE' αν η OnLoan ιδιότητα είναι FALSE. 
            //     */
            //    if (i.GetType() == typeof(Book))
            //    {
            //        Console.WriteLine("ID: [" + i.ItemID + "], \"" + i.Title + "\"" + " by " + ((Book)i).Author + " --- " + (i.OnLoan ? "On LOAN" : "AVAILABLE"));
            //    }

            //}
        }

        public void ShowAllVideos()
        {

            List<Video> videos = Items.OfType<Video>().ToList();

            if (videos != null)
            {
                if (videos.Count == 0)
                {
                    Console.WriteLine("No videos in our library yet.");
                }
                else
                {
                    foreach (Video v in videos)
                    {
                        string name = "";
                        if (AtLeastOneVideoLoaned()) {
                            name = Loans.Find(x => x.ItemLoaned.ItemID == v.ItemID).UserLoaning.Name;
                        }
                        

                        Console.WriteLine("ID: [" + v.ItemID + "], \"" + v.Title + "\"" + 
                            " with a duration of:  " + v.Duration + " min --- " + 
                            (v.OnLoan ? "On LOAN by "+name : "AVAILABLE"));

                        
                            // var currentLoans = Loans.FindAll(x => x.UserLoaning.UserID == CurrentUser.UserID);
                    }
                }
            }
            /*
             * 
             * To parakatw menei edw mono gia reference ... 
             * 
             * 
            foreach (Item i in Items)
            {

              
                if (i.GetType() == typeof(Video))
                {
                    Console.WriteLine("ID: [" + i.ItemID + "], \"" + i.Title + "\"" + " with a duration of:  " + ((Video)i).Duration + " min --- " + (i.OnLoan ? "On LOAN" : "AVAILABLE"));
                }

            }
            */
        }

        public void ShowAllJournals()
        {
            List<Journal> journals = Items.OfType<Journal>().ToList();

            if (journals != null)
            {
                if (journals.Count == 0)
                {
                    Console.WriteLine("No Journals in our library yet.");
                }
                else
                {
                    foreach (Journal j in journals)
                    {
                        string name = "";
                        if (AtLeastOneJournalLoaned())
                        {
                            name = Loans.Find(x => x.ItemLoaned.ItemID == j.ItemID).UserLoaning.Name;
                        }


                        Console.WriteLine("ID: [" + j.ItemID + "], \"" + j.Title + "\"" +
                            " publisher:  " + j.Publisher + " --- " +
                            (j.OnLoan ? "On LOAN by " + name : "AVAILABLE"));


                    }
                }
            }
        }
        

        public void SaveSettings()
        {

            

            LibrarySettings = new Settings(Items, Users, Loans);

            BinarySerialization.WriteToBinaryFile<Settings>(FilenamePath, LibrarySettings, false);

            
        }

        public void LoadSettings()
        {
            //Books = BinarySerialization.ReadFromBinaryFile<List<Book>>(path);
            Console.WriteLine("trexei to loadSettings");

            try
            {
                LibrarySettings = BinarySerialization.ReadFromBinaryFile<Settings>(FilenamePath);
                Console.WriteLine("perasa ok to try");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("mpika sto catch");
                LibrarySettings = new Settings(Items, Users, Loans);
            }
            
            LibrarySettings.Sanitize();
            
        }

        public void CreateUser()
        {
            Console.WriteLine();
            Console.Write("Give me Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Give me Email Address");
            string email = Console.ReadLine();

            Console.WriteLine("Give me Telephone Number");
            int phone = Convert.ToInt32(Console.ReadLine());

            Users.Add(new User(name, email, phone));
            Console.WriteLine("OK - User created successfully");

        }

        public void CreateItem()
        {

            Console.WriteLine();

            Console.WriteLine("Creating new Item");
            Console.WriteLine("[1] Create new BOOK");
            Console.WriteLine("[2] Create new VIDEO");
            Console.WriteLine("[3] Create new JOURNAL");


            string choice = Console.ReadLine();
            int number;
            bool result = Int32.TryParse(choice, out number);
            if (result)
            {
                Console.WriteLine();
                Console.Write("Give me Title: ");
                string title = Console.ReadLine();

                switch (number)
                {
                    // in case of BOOK
                    case 1:
                        Console.Write("Give me Author: ");
                        string author = Console.ReadLine();
                        //Books.Add(new Book(title, author));
                        Items.Add(new Book(title, author));
                        Console.WriteLine("OK - Book created successfully");
                        break;

                    case 2:
                        Console.WriteLine("Give me video duration (in min): ");
                        int duration = Convert.ToInt32(Console.ReadLine());
                        Items.Add(new Video(title, duration));
                        Console.WriteLine("OK - Video created successfully");
                        break;

                    case 3:
                        Console.WriteLine("Give me Publisher: ");
                        string publisher = Console.ReadLine();
                        Items.Add(new Journal(title, publisher));
                        Console.WriteLine("Journal created successfully");
                        break;

                }

            }
            else
            {
                Console.WriteLine("Incorrect choice");
            }

        }

        
        public void LoanItem()
        {
            Console.Write("Give me Item ID: ");
            int itemId = Convert.ToInt32(Console.ReadLine());

            Item theItem = Items.Find(x => x.ItemID == itemId);

            if (theItem.OnLoan)
            {
                Console.WriteLine("I am sorry mr. " + CurrentUser.Name + " but \"" + theItem.Title + "\" (ID=" + theItem.ItemID + ") is currently on loan");
            }
            else
            {
                if (CurrentUser.NumberOfItemsLoaned>=MaxNumberOfLoans)
                {
                    Console.WriteLine("Sorry "+CurrentUser.Name+" but you already have "+MaxNumberOfLoans+" loans");
                }
                else
                {
                    Console.WriteLine("OK " + CurrentUser.Name + ", here is \"" + theItem.Title + "\". Enjoy!");
                    theItem.OnLoan = true;
                    CurrentUser.LoanItem();
                    Loans.Add(new Loan(DateTime.Now, theItem, CurrentUser));
                }
                
            }
        }
        
        public void Return()
            
        {
            Console.WriteLine(CurrentUser.Name+", give me Item ID please: ");
            int itemID = Convert.ToInt32(Console.ReadLine());

            Loan theLoan = Loans.Find(x => x.ItemLoaned.ItemID == itemID);

            if (theLoan != null)
            {
                if (CurrentUser.UserID != theLoan.UserLoaning.UserID)
                {
                    Console.WriteLine("I am sorry "+CurrentUser.Name+", but "+theLoan.UserLoaning.Name+" "+"should return the item himself/herself");
                }
                else
                {
                    Console.WriteLine("OK - Item with ID=" + theLoan.ItemLoaned.ItemID + " is returned to the library.");
                    theLoan.ItemLoaned.OnLoan = false;
                    Loans.Remove(theLoan);
                    CurrentUser.ReturnItem();
                }
                
            }
            else
            {
                Console.WriteLine("*********  There is no such loan for item: " + itemID);
            }
            
        }
        public bool LoginUser()
        {
            Console.WriteLine("Please Enter Your UserID: ");
            int loginId = Convert.ToInt32(Console.ReadLine());

            User theUser = Users.Find(x => x.UserID == loginId);
            if (theUser != null)
            {
                CurrentUser = theUser;
                return true;
            }
            else
            {
                return false;
            }
            //Loan theLoan = Loans.Find(x => x.ItemToBeLoaned.ID == item.ID);
        }

        // todo: needs refactoring
        public void CurrentLoans()
        {

            // this doesn't work - leave it here for further research (how to use 'where')
            //IEnumerable<Loan> currentLoans = Loans.Where(x => x.UserLoaning.UserID == CurrentUser.UserID);
            var currentLoans = Loans.FindAll(x => x.UserLoaning.UserID == CurrentUser.UserID);
            
            if (currentLoans.Count != 0)
            {
                Console.WriteLine("Currently loaned items by "+CurrentUser.Name);
                foreach (Loan l in Loans)
                {
                    Console.WriteLine("\"" + l.ItemLoaned.Title + "\"" + " borrowed on: " + l.DateLoaned.ToShortDateString());
                }
            }
            else
            {
                Console.WriteLine("\nNo loans found.");
            }

            
        }


        private bool AtLeastOneVideoLoaned()
        {
            // Item theItem = Items.Find(x => x.ItemID == itemId);
            // if (i.GetType() == typeof(Journal))
            //Loan atLeastOne = Loans.Find(x => x.ItemLoaned.GetType() == typeof(Video));

            if (Loans.Find(x => x.ItemLoaned.GetType() == typeof(Video)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool AtLeastOneBookLoaned()
        {
            if (Loans.Find(x => x.ItemLoaned.GetType() == typeof(Book)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool AtLeastOneJournalLoaned()
        {
            if (Loans.Find(x => x.ItemLoaned.GetType() == typeof(Journal)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
        private void Instantiate()
        {
            if (Books == null)
            {
                Books = new List<Book>();
            }
            
            if (Videos == null)
            {
                Videos = new List<Video>();
            }
            
            if (Journals == null)
            {
                Journals = new List<Journal>();
            }
            
        }
        */

        /*
    private void Individuate()
    {
        foreach (Item i in Items)
        {
            //videotest.GetType() == typeOf(Video)
            if (i.GetType() == typeof(Book))
            {
                Console.WriteLine("mpainw edw sto individuate - #1");
                Books.Add((Book)i);
            }

            if (i.GetType() == typeof(Video))
            {
                Videos.Add((Video)i);
            }

            if (i.GetType() == typeof(Journal))
            {
                Journals.Add((Journal)i);
            }

        }
    }
    */
    }
}

