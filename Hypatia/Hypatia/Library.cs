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

        public Library(string filenamePath, bool newLibrary)
        {
            FilenamePath = filenamePath;

            MaxNumberOfLoans = 5;
            
            // SaveSettings saves new settings - LoadSettings SANITIZES the object.

            if (newLibrary)
            {
                SaveSettings();
                LoadSettings();
            }
            else
            {
                LoadSettings();
            }
            

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
                            Loan theLoan = Loans.Find(x => x.ItemLoaned.ItemID == b.ItemID);
                            if (theLoan != null)
                            {
                                name = theLoan.UserLoaning.Name;
                            }

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
                        string name="";
                        if (AtLeastOneVideoLoaned()) {
                            Loan theLoan = Loans.Find(x => x.ItemLoaned.ItemID == v.ItemID);
                            if (theLoan != null) {
                                name = theLoan.UserLoaning.Name;
                            }
                            
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
                            Loan theLoan = Loans.Find(x => x.ItemLoaned.ItemID == j.ItemID);
                            if (theLoan != null)
                            {
                                name = theLoan.UserLoaning.Name;
                            }

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
            Console.WriteLine("loading settings ...");

            try
            {
                LibrarySettings = BinarySerialization.ReadFromBinaryFile<Settings>(FilenamePath);
                Console.WriteLine("loading settings -- try statement executed ");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("loading settings -- catch statement executed ");
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
                    Console.WriteLine("Let's construct a virtual date for testing purposes... (assuming year = 2018)");
                    Console.Write("Give me day (no checks for invalid input): ");
                    int day = Convert.ToInt16(Console.ReadLine());
                    Console.Write("Give me month (give me 1-12, no checks for invalid input): ");
                    int month = Convert.ToInt16(Console.ReadLine());
                    
                    Console.WriteLine("OK " + CurrentUser.Name + ", here is \"" + theItem.Title + "\". Enjoy!");
                    theItem.OnLoan = true;
                    CurrentUser.LoanItem();
                    Loans.Add(new Loan(new DateTime(2018, month, day), theItem, CurrentUser));
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
            var currentUserLoans = Loans.FindAll(x => x.UserLoaning.UserID == CurrentUser.UserID);
            
            if (currentUserLoans.Count != 0)
            {
                // sort, just for the sake of sorting ... not really applicable here. 
                // with sorted List, it will show earlier loans first. 
                // without - it shows loans on the order they were made (and since user enters custom dates, this is really random). 
                currentUserLoans.Sort((x, y) => DateTime.Compare(x.DateLoaned, y.DateLoaned));

                Console.Write("Currently loaned items by ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(CurrentUser.Name);
                Console.ResetColor();

                // make sure to print euro sign correctly (also based on default font used in system's console...)
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                foreach (Loan l in currentUserLoans)
                {
                    //Console.WriteLine();

                    // IMPORTANT - casting -> (int) truncates, while ConvertToInt would round.  I prefer number to be truncated.. 
                    int days = (int)(DateTime.Now - l.DateLoaned).TotalDays;
                    Console.Write(l.ItemLoaned.ToString()+ " \"" + l.ItemLoaned.Title + "\"" + " borrowed on: " + l.DateLoaned.ToShortDateString()+ " --- ");

                    if (l.ItemLoaned.GetType() == typeof(Book))
                    {
                        if (days>28)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("ITEM OVERDUE BY: " + (days - 28) + " days - FINE: "+CalculateFine(l)+ "€");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" DAYS LEFT: " + (28 - days) + " days.");
                        }
                    }

                    if (l.ItemLoaned.GetType() == typeof(Journal))
                    {
                        if (days > 14)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("ITEM OVERDUE BY: " + (days - 14) + " days - FINE: " + CalculateFine(l) +"€");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" DAYS LEFT: " + (14 - days) + " days.");   
                        }
                    }

                    if (l.ItemLoaned.GetType() == typeof(Video))
                    {
                        if (days > 7)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            
                            Console.Write("ITEM OVERDUE BY: " + (days - 7) + " days - FINE: " + CalculateFine(l) + "€");
                            
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" DAYS LEFT: " + (7 - days) + " days.");
                        }
                    }
                    Console.ResetColor();
                    Console.WriteLine();
                }
                Console.OutputEncoding = System.Text.Encoding.Default;
            }
            else
            {
                Console.WriteLine("\nNo items loaned to user: "+CurrentUser.Name);
            }

            
        }

        public void PrintOverdueLoans()
        {
            if (Loans.Count>0)
            {
                foreach (Loan l in Loans)
                {
                    //Console.WriteLine();

                    // IMPORTANT - casting -> (int) truncates, while ConvertToInt would round.  I prefer number to be truncated.. 
                    int days = (int)(DateTime.Now - l.DateLoaned).TotalDays;
                    

                    if (l.ItemLoaned.GetType() == typeof(Book))
                    {
                        if (days > 28)
                        {
                            PrettyPrintLateLoan(l, days-28);
                        }
                       
                    }

                    if (l.ItemLoaned.GetType() == typeof(Journal))
                    {
                        if (days > 14)
                        {
                            PrettyPrintLateLoan(l, days-14);
                        }
                        
                    }

                    if (l.ItemLoaned.GetType() == typeof(Video))
                    {
                        if (days > 7)
                        {
                            PrettyPrintLateLoan(l, days-7);

                        }
                        
                    }
                    Console.ResetColor();
                    Console.WriteLine();
                }
                Console.OutputEncoding = System.Text.Encoding.Default;
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

        private void PrettyPrintLateLoan(Loan l, int days)
        {
            

            Console.Write(l.ItemLoaned.ToString());
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" "+l.ItemLoaned.Title);
            Console.ResetColor();
            Console.Write(" [ITEM ID=" + l.ItemLoaned.ItemID + "], borrowed on ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(l.DateLoaned.ToShortDateString());
            Console.ResetColor();
            Console.Write(" by ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(l.UserLoaning.Name);
            Console.ResetColor();
            Console.Write(" [USER ID="+l.UserLoaning.UserID+"] \nis ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(days + " days late ");
            Console.ResetColor();
            Console.Write("with a fine, as of today (" + DateTime.Now.ToShortDateString()+") equal to ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write(CalculateFine(l) + "€");

            //Console.ResetColor();
            //Console.OutputEncoding = System.Text.Encoding.Default;

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

        private int CalculateFine(Loan late)
        {
            // casting in order to truncate - Convert.ToInt would round.. 
            int days = (int)(DateTime.Now - late.DateLoaned).TotalDays;

            if (late.ItemLoaned.GetType() == typeof(Book))
            {
                return (days - 28);
            }
            else if (late.ItemLoaned.GetType() == typeof(Journal))
            {
                return (days - 14) * 3;
            }
            else
            {
                return (days - 7) * 5;
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

