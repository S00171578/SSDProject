using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;

namespace SSDProject
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            String[] menu = new String[] { "Admin Login", "Admin Sign Up (Testing Purposes Only)", "Login", "Sign Up" };

            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, menu[i]);
            }
            Console.WriteLine("Enter your option");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    AdminLogin();
                    break;
                case "2":
                    AdminSignUp();
                    break;
                case "3":
                    Login();
                    break;
                case "4":
                    SignUp();                 
                    break;
                default:
                    Console.WriteLine("Please Enter a vaild option");
                    MainMenu();
                    break;
            }
        }
        static void AdminLogin()
        {
            Console.WriteLine("Please Enter your Username");
            string username = Console.ReadLine();

            Console.WriteLine("Please Enter your Password");
            string password = Console.ReadLine();

            using (var reader = new StreamReader("../Admins.csv"))
            using (var csv = new CsvReader(reader))
            {
                var record = new Admin();
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.EnumerateRecords(record);
                foreach (var r in records)
                {
                    if (r.Username == username && r.Password == password)
                    {
                        Menu(String.Concat(r.GetType()));
                    }
                    else
                    {
                        Console.WriteLine("Invaild Username or Password try again");
                        Login();
                    }
                }
            }
        }
        static void AdminSignUp()
        {
            Console.WriteLine("Please Enter a Username");
            string username = Console.ReadLine();

            Console.WriteLine("Please Enter a Password");
            string password = Console.ReadLine();

            using (var writer = new StreamWriter("../Admins.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(new Admin(username, password));
            }
        }
        static void Login()
        {
            Console.WriteLine("Please Enter your Username");
            string username = Console.ReadLine();

            Console.WriteLine("Please Enter your Password");
            string password = Console.ReadLine();

            using (var reader = new StreamReader("../Users.csv"))
            using (var csv = new CsvReader(reader))
            {
                var record = new Member();
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.EnumerateRecords(record);
                foreach (var r in records)
                {
                    if(r.Username==username && r.Password == password)
                    {
                        Menu(String.Concat(r.GetType()));
                    }
                    else
                    {
                        Console.WriteLine("Invaild Username or Password try again");
                        Login();
                    }
                }
            }
        }
        static void SignUp()
        {
            Console.WriteLine("Please Enter a Username");
            string username= Console.ReadLine();

            Console.WriteLine("Please Enter a Password");
            string password = Console.ReadLine();

            using (var writer = new StreamWriter("../Users.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(new Member(username, password));
            }

        }
        static void Menu(string privilege)
        {
            if (privilege == "Admin")
            {
                String[] menu = new String[] { "Create", "Read", "Update", "Delete" };

                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine("{0}.{1}", i + 1, menu[i]);
                }
                Console.WriteLine("Enter your option");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        Create();
                        break;
                    case "1":
                        Read();
                        break;
                    case "2":
                        Update();
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Please enter a vaild number");
                        Menu(privilege);
                        break;
                }
            }
            else
            {
                String[] menu = new String[] { "Read"};
                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine("{0}.{1}", i + 1, menu[i]);
                }
                Console.WriteLine("Enter your option");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        Create();
                        break;
                    default:
                        Console.WriteLine("Please enter a vaild number");
                        Menu(privilege);
                        break;
                }
            }

            
        }
        static void Create()
        {
            Console.WriteLine("Please enter your text");
            string value = Console.ReadLine();
            System.IO.File.AppendAllText(@"C:\Users\Mark\Documents\WriteText.txt", value);
        }
        static void Read()
        {
            String[] lines = System.IO.File.ReadAllLines(@"C:\Users\Mark\Documents\WriteText.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i, lines[i]);
            }
        }
        static void Update()
        {


        }
    }
}
