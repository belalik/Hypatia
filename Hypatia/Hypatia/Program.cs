using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypatia
{
    class Program
    {
        static Library aegeanLibrary = new Library();

        public static void Main(string[] args)
        {
            
            MainMenu();
        }

        public static void MainMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("--------------------");
            Console.WriteLine("[1] Administration Menu");
            Console.WriteLine("[2] User Menu");
            Console.WriteLine("[3] Show students");
            Console.WriteLine("[4] Exit the program");
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
                Console.WriteLine("Incorrect choice");
            }
        }

        public static void SubMenu(int mainMenuChoice)
        {
            switch (mainMenuChoice)
            {
                case 1:
                    Console.WriteLine("Administration Menu");
                    Console.WriteLine("[1] Create Item");
                    Console.WriteLine("[2] Create User");
                    Console.WriteLine("[3] Check Overdue Loans");
                    Console.WriteLine("[4] Notify Overdue Users");
                    Console.WriteLine("[8] Save List of Books to file");
                    Console.WriteLine("[9] Load Books from file");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 1-4\n");
                    break;

                case 2:
                    Console.WriteLine("User Menu");
                    Console.WriteLine("[1] Show all ITEMS");
                    Console.WriteLine("[2] Show all BOOKS");
                    Console.WriteLine("[3] Show all VIDEOS");
                    Console.WriteLine("[4] Show all JOURNALS");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 1-4\n");
                    break;

                case 3:
                    Console.WriteLine("Students");
                    Console.WriteLine("[1] Show all students");
                    Console.WriteLine("[2] Find a student by his matricule");
                    Console.WriteLine("[3] Return Main Menu");
                    Console.WriteLine("[4] Exit the program");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 1-4\n");
                    break;

                case 4:
                    Environment.Exit(0);
                    break;
            }
            string choice = Console.ReadLine();
            int number;
            bool result = Int32.TryParse(choice, out number);
            if (result)
            {
                Action(mainMenuChoice, number);
            }
            else
            {
                Console.WriteLine("Incorrect choice");
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
                            CreateItem();
                            Console.Clear();
                            MainMenu();
                            break;

                        case 2:
                            // Do Stuff
                            break;

                        case 3:
                            Console.Clear();
                            MainMenu();
                            break;

                        case 4:
                            Environment.Exit(0);
                            break;

                        case 8:
                            aegeanLibrary.SaveBooks();
                            break;

                        case 9:
                            aegeanLibrary.LoadBooks();
                            Console.Clear();
                            MainMenu();
                            break;
                    }
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
                            break;

                        case 3:
                            Console.Clear();
                            MainMenu();
                            break;

                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                    break;

                case 3:
                    switch (choice)
                    {
                        case 1:
                            // Do Stuff
                            break;

                        case 2:
                            // Do Stuff
                            break;

                        case 3:
                            Console.Clear();
                            MainMenu();
                            break;

                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                    break;
            }
        }


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
    }


}
