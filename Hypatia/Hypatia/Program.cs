using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    class Program
    {

        /*
             * NOTICE the difference in .GetType() == typeOf [answers True only in exact match] 
             * and the 'is' keyword comparison [answers True for exact match OR parent class]
             */

        /*
        Console.WriteLine("Checking if videotest.GetType() == typeOf(Video): "+(videotest.GetType() == typeof(Video)));
        Console.WriteLine("Checking if videotest.GetType() == typeOf(Journal): " + (videotest.GetType() == typeof(Journal)));
        Console.WriteLine("Checking if videotest.GetType() == typeOf(Item): " + (videotest.GetType() == typeof(Item)));

        Console.WriteLine("Checking (videotest is Video): " + (videotest is Video));
        Console.WriteLine("Checking (videotest is Journal): " + (videotest is Journal));
        Console.WriteLine("Checking (videotest is Item): " + (videotest is Item));

        Console.ReadLine();
        */

        static string path = "";
        static string filename = "";
        static string fullPath = "";

        static bool newLibrary = false;

        static Library aegeanLibrary;

        
        

        public static void Main(string[] args)
        {
            

            bool goodToGo = initialize();

            Console.WriteLine("full path is: " + fullPath+ " \ngoodToGo is: "+goodToGo);
            //Console.ReadLine();

            if (goodToGo)
            {
                aegeanLibrary = new Library(fullPath, newLibrary);
                MainMenu();
            }

            else
            {
                Console.WriteLine("Goodbye ...");
            }
            
        }

        public static void MainMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("--------------------");
            Console.WriteLine("[1] Administration Menu");
            Console.WriteLine("[2] Browse Library Contents");
            Console.WriteLine("[3] User Area");
            Console.WriteLine("[4] Exit Program");
            Console.WriteLine("--------------------\n");
            Console.WriteLine("Please select an option from 1-4\n");

            string choice = Console.ReadLine();
            int number;
            bool result = Int32.TryParse(choice, out number);
            if (result)
            {
                Console.Clear();
                SubMenu(number);
            }
            else
            {
                Console.WriteLine("Incorrect choice 1");
            }
        }

        public static void SubMenu(int mainMenuChoice)
        {
            bool invalidLogin = false;
            switch (mainMenuChoice)
            {
                

                case 1:
                    Console.WriteLine("Administration Menu");
                    Console.WriteLine("[1] Create Item");
                    Console.WriteLine("[2] Create User");
                    Console.WriteLine("[3] Show All Users");
                    Console.WriteLine("[4] Notify Overdue Users");
                    Console.WriteLine("[5] Check Overdue Loans");
                    Console.WriteLine("[8] Save ALL settings");
                    Console.WriteLine("[9] Load ALL settings");
                    Console.WriteLine("[0] Return to MAIN MENU");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option\n");
                    break;

                case 2:
                    Console.WriteLine("Browse Contents");
                    Console.WriteLine("[1] Show all ITEMS");
                    Console.WriteLine("[2] Show all BOOKS");
                    Console.WriteLine("[3] Show all VIDEOS");
                    Console.WriteLine("[4] Show all JOURNALS");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option\n");
                    break;

                case 3:
                    if (aegeanLibrary.LoginUser())
                    {
                        Console.WriteLine("Hello " + aegeanLibrary.CurrentUser.Name);

                        Console.WriteLine();
                        Console.WriteLine("[1] Borrow Item");
                        Console.WriteLine("[2] Return Item");
                        Console.WriteLine("[3] Currently Loaned Items");
                        Console.WriteLine("[4] Back to Main Menu");
                        Console.WriteLine("--------------------\n");
                        Console.WriteLine("Please select an option\n");
                        break;
                    }
                    else
                    {
                        invalidLogin = true;
                        Console.WriteLine("Login Attempt unsuccessful. Try again..");
                        break;
                    }

                    

                case 4:
                    Environment.Exit(0);
                    break;
            }
            
            if (invalidLogin)
            {
                PressAnyKey();
                Console.Clear();
                MainMenu();
            }
            else
            {
                string choice = Console.ReadLine();
                int number;
                bool result = Int32.TryParse(choice, out number);
                if (result)
                {
                    Action(mainMenuChoice, number);
                }
                else
                {
                    Console.WriteLine("Incorrect choice 2");
                }
            }

            
        }
        public static void Action(int menu, int choice)
        {
            switch (menu)
            {
                case 1:
                    switch (choice)
                    {
                        case 1:
                            //CreateItem();
                            aegeanLibrary.CreateItem();
                            //PressAnyKey();
                            break;

                        case 2:
                            aegeanLibrary.CreateUser();
                            //PressAnyKey();
                            break;

                        case 3:
                            aegeanLibrary.ShowAllUsers();
                            //PressAnyKey();
                            break;

                        case 4:
                            //Environment.Exit(0);
                            Console.WriteLine("Sending lots of emails ....");
                            break;

                        case 5:
                            //Environment.Exit(0);
                            Console.WriteLine("Print all overdue loans...");
                            aegeanLibrary.PrintOverdueLoans();
                            break;

                        case 8:
                              
                            aegeanLibrary.SaveSettings();
                            Console.WriteLine("Settings saved successfully");
                            break;

                        case 9:
                            
                            aegeanLibrary.LoadSettings();
                            Console.WriteLine("Settings loaded successfully");
                            break;

                        case 0:
                            Console.WriteLine("Returning to main menu...");
                            break;
                    }
                    PressAnyKey();
                    Console.Clear();
                    MainMenu();
                    break;

                case 2:
                    switch (choice)
                    {
                        case 1:
                            // show all items
                            aegeanLibrary.ShowAllItems();
                            break;

                        case 2:
                            // show all books
                            aegeanLibrary.ShowAllBooks();
                            //PressAnyKey();
                            break;

                        case 3:
                            aegeanLibrary.ShowAllVideos();
                            break;

                        case 4:
                            //Environment.Exit(0);
                            aegeanLibrary.ShowAllJournals();
                            break;

                    }
                    PressAnyKey();
                    Console.Clear();
                    MainMenu();
                    break;

                case 3:
                    
                    
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Trying to Borrow Item");
                            aegeanLibrary.LoanItem();
                            break;

                        case 2:
                            Console.WriteLine("Return an Item");
                            aegeanLibrary.Return();
                            break;

                        case 3:
                            
                            aegeanLibrary.CurrentLoans();
                            break;

                        case 4:
                            // logout user
                            break;
                    }
                    
                    
                    PressAnyKey();
                    Console.Clear();
                    MainMenu();
                    break;
            }
        }

        static void PressAnyKey()
        {
            Console.WriteLine("Press any key to go back to Main Menu");
            Console.ReadLine();
        }

        // ugly method - needs refactoring .... 
        static bool initialize()
        {
            Console.WriteLine("Where are you? ");
            Console.WriteLine("1. Home");
            Console.WriteLine("2. University");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();
            int where;
            bool result = Int32.TryParse(choice, out where);

            if (result)
            {
                Console.WriteLine("What do you want to do? ");
                Console.WriteLine("1. New Library");
                Console.WriteLine("2. Load Saved Library");
                Console.Write("Choice: ");

                choice = Console.ReadLine();
                int what;
                result = Int32.TryParse(choice, out what);

                if (result)
                {
                    return createPath(where, what);
                }
                else
                {
                    Console.WriteLine("Incorrect choice - goodbye ... ");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Incorrect choice - goodbye ... ");
                return false;
            }

            //int where = Convert.ToInt16()
            //static string home = "\\\\syros-fs.aegean.gr\\Syros_Staff\\tkogias\\hypatia\\hypatia.save";
            //static string uni = "Z:\\hypatia\\hypatia.save";

        }

        // same here .. VERY ugly method, needs refactoring ... 
        // however, researching gave me this: https://stackoverflow.com/questions/556133/whats-the-in-front-of-a-string-in-c
        // a @ in-front of a string, makes it a 'verbatim' string - anything in the string that would normally be interpreted as an escape sequence is ignored !!!
        static bool createPath(int where, int what)
        {
            if (where==1)
            {
                //path = "\\\\syros-fs.aegean.gr\\Syros_Staff\\tkogias\\hypatia\\";
                path = @"\\syros-fs.aegean.gr\Syros_Staff\tkogias\hypatia\";
            }
            else if (where ==2)
            {
                //path = "Z:\\hypatia\\";
                path = @"Z:\hypatia\";
            }
            else
            {
                return false;
            }

            if (what==1)
            {
                Console.WriteLine("OK - Give me name of new library: ");
                Console.WriteLine("Careful - if it exists, it will be overwritten.. ");
                Console.Write("New Library: ");
                filename = Console.ReadLine()+".save";
                newLibrary = true;
                
            }
            else if (what==2)
            {
                
                // play around with files
                string[] filePaths = System.IO.Directory.GetFiles(@path, "*.save",
                                         System.IO.SearchOption.TopDirectoryOnly);
                Console.WriteLine("Libraries created so far: ");

                for (int i=0; i<filePaths.Length; i++)
                {
                    string file = filePaths[i];
                    Console.WriteLine((i+1)+". "+ System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));
                }

                //foreach (string file in filePaths)
                //{
                    //Console.WriteLine(System.IO.Path.GetFileNameWithoutExtension(file));
                    //Console.WriteLine(file);
                //}

                Console.Write("So, choose which one to load (number): ");
                
                filename = System.IO.Path.GetFileName(filePaths[Convert.ToInt32(Console.ReadLine()) - 1]);
                //filename = Console.ReadLine() + ".save";

            }
            else
            {
                return false;
            }

            fullPath = path + filename;
            return true;
            
        }
    }


}
