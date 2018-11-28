using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{

    [Serializable] class Library
    {

        //public List<Item> Items { get; set; }
        string bookpath = "Z:\\hypatia\\hypatia.bks";

        public List<Book> Books { get; set; }

        //public List<Video> Videos { get; set; }

        //public List<Journal> Journals { get; set; }

        //public List<User> Users { get; set; }

        //public List<Loan> Loans { get; set; }

        public Library()
        {
            //Items = new List<Item>();

            Books = new List<Book>();
        }


        public void ShowAllItems()
        {
            Console.WriteLine("will show all items");
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
                Console.WriteLine("ID: ["+b.ItemID+"], \""+b.Title+"\""+" by "+b.Author+" --- "+(b.OnLoan ? "On LOAN" : "AVAILABLE"));
            }
        }

        public void SaveBooks()
        {
            //string path = "C:\\Users\\tkogias\\Desktop\\Git\\Hypatia\\Hypatia\\hypatia.bks";
            
            

            BinarySerialization.WriteToBinaryFile<List<Book>>(bookpath, Books, false);
        }

        public void LoadBooks()
        {
            Books = BinarySerialization.ReadFromBinaryFile<List<Book>>(bookpath);
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
