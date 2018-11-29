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

        //public List<Item> Items { get; set; }

        public User CurrentUser { get; set; }

        public List<Book> Books { get; set; }

        public List<Loan> Loans { get; set; }

        public Settings LibrarySettings { get; set; }

        public string FilenamePath { get; set; }
        //public List<Video> Videos { get; set; }

        //public List<Journal> Journals { get; set; }

        public List<User> Users { get; set; }

        //public List<Loan> Loans { get; set; }

        public Library(string filenamePath)
        {
            FilenamePath = filenamePath;

            // make sure to load settings upon initializing the library object. 
            // will fail otherwise ... (if file not found etc.). 

            LoadSettings();


            //Books = new List<Book>();   // Books intentionally not initialized (commented out)
            //int lastid = Item.staticID;

            //LibrarySettings = new Settings(Books, lastid);


        }


        public void ShowAllItems()
        {
            Console.WriteLine("will show all items");

        }

        public void ShowAllUsers()
        {
            foreach (User u in Users)
            {
                Console.WriteLine("User "+u.Name+" with UserID: ["+u.UserID+"]");
            }
        }

        public void ShowAllBooks()
        {
            foreach (Book b in Books)
            {

                /*
                 *  Χρησιμοποιώ το conditional operator '?:' - λέγεται και ternary operator. 
                 *  Έχει τη γενική μορφή: ' condition ? consequence : alternative '
                 *  
                 *  που σημαίνει 'Αν (condition==TRUE) κάνε το consequence, αλλιώς κάνε το alternative. 
                 *  
                 *  Ουσιαστικά το χρησιμοποιώ έτσι ώστε να γράψω τη λέξη 'On LOAN' αν είναι δανεισμένο σε κάποιον 
                 *  (αν δηλαδή η OnLoan ιδιότητα είναι TRUE) αλλιώς να γράψω τη λέξη 'AVAILABLE' αν η OnLoan ιδιότητα είναι FALSE. 
                 */
                Console.WriteLine("ID: [" + b.ItemID + "], \"" + b.Title + "\"" + " by " + b.Author + " --- " + (b.OnLoan ? "On LOAN" : "AVAILABLE"));
            }
        }

        public void SaveSettings()
        {
            //string path = "C:\\Users\\tkogias\\Desktop\\Git\\Hypatia\\Hypatia\\hypatia.bks";
            

            LibrarySettings = new Settings(Books, Users, Loans);

            BinarySerialization.WriteToBinaryFile<Settings>(FilenamePath, LibrarySettings, false);

            //BinarySerialization.WriteToBinaryFile<List<Book>>(path, Books, false);
        }

        public void LoadSettings()
        {
            //Books = BinarySerialization.ReadFromBinaryFile<List<Book>>(path);
            Console.WriteLine("trexei to loadSettings");

            LibrarySettings = BinarySerialization.ReadFromBinaryFile<Settings>(FilenamePath);
            LibrarySettings.Sanitize();

            Books = LibrarySettings.Books;
            Users = LibrarySettings.Users;
            Loans = LibrarySettings.Loans;

            User.staticUserID = (LibrarySettings.LastUserID) + 1;
            Item.staticID = (LibrarySettings.LastItemID)+1;
            Loan.staticLoanID = (LibrarySettings.LastLoanID) + 1;
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
                        Books.Add(new Book(title, author));
                        Console.WriteLine("OK - Book created successfully");
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

            Book theBook = Books.Find(x => x.ItemID == itemId);

            if (theBook.OnLoan)
            {
                Console.WriteLine("I am sorry mr. " + CurrentUser.Name + " but \"" + theBook.Title + "\" (ID=" + theBook.ItemID + ") is currently on loan");
            }
            else
            {
                Console.WriteLine("OK " + CurrentUser.Name + ", here is \"" + theBook.Title + "\". Enjoy!");
                theBook.OnLoan = true;
                Loans.Add(new Loan(DateTime.Now, theBook, CurrentUser));
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


    }
}


/*
 [Serializable]
public class Person
{
    public string Name { get; set; }
    public int Age = 20;
    public Address HomeAddress { get; set;}
    private string _thisWillGetWrittenToTheFileToo = "even though it is a private variable.";
 
    [NonSerialized]
    public string ThisWillNotBeWrittenToTheFile = "because of the [NonSerialized] attribute.";
}
 
[Serializable]
public class Address
{
    public string StreetAddress { get; set; }
    public string City { get; set; }
}
 
// And then in some function.
Person person = new Person() { Name = "Dan", Age = 30; HomeAddress = new Address() { StreetAddress = "123 My St", City = "Regina" }};
List<Person> people = GetListOfPeople();
BinarySerialization.WriteToBinaryFile<Person>("C:\person.bin", person);
BinarySerialization.WriteToBinaryFile<List<People>>("C:\people.bin", people);
 
// Then in some other function.
Person person = BinarySerialization.ReadFromBinaryFile<Person>("C:\person.bin");
List<Person> people = BinarySerialization.ReadFromBinaryFile<List<Person>>("C:\people.bin");
    
    */
