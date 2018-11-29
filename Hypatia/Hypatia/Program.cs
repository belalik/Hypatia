using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    class Program
    {
        //aegeanLibrary.SaveBooks("Z:\\hypatia\\hypatia.bks"); 
        // "\\\\syros-fs.aegean.gr\\Syros_Staff\\tkogias\\hypatia\\hypatia.save"
        static string home = "\\\\syros-fs.aegean.gr\\Syros_Staff\\tkogias\\hypatia\\hypatia.save";
        static string uni = "Z:\\hypatia\\hypatia.save";

        static Library aegeanLibrary = new Library(home);

        public static void Main(string[] args)
        {
            
            MainMenu();
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
                        Console.WriteLine("[2] choice 2");
                        Console.WriteLine("[3] choice 3");
                        Console.WriteLine("Back to Main Menu");
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

                        case 8:
                              
                            aegeanLibrary.SaveSettings();
                            Console.WriteLine("Settings saved successfully");
                            break;

                        case 9:
                            
                            
                            aegeanLibrary.LoadSettings();
                            Console.WriteLine("Settings loaded successfully");
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

                            break;

                        case 2:
                            // show all books
                            aegeanLibrary.ShowAllBooks();
                            //PressAnyKey();
                            break;

                        case 3:
                            
                            break;

                        case 4:
                            //Environment.Exit(0);
                            Console.WriteLine("Showing all Journals");
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
                            // Do Stuff
                            break;

                        case 3:

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

        /*
        static void CreateItem()
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
                        aegeanLibrary.Books.Add(new Book(title, author));
                        break;

                }
                
            }
            else
            {
                Console.WriteLine("Incorrect choice");
            }

            Console.Clear();
            MainMenu();
        }
        */
    }


}
