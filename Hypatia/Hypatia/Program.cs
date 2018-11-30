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

        static Library aegeanLibrary = new Library(uni);


        //static Video videotest = new Video("test", 120);
        //static Book booktest = aegeanLibrary.Books.ElementAt(0);

        public static void Main(string[] args)
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
