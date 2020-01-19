using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace SSDProject
{
    class Program
    {
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        private static Random random = new Random();
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            String[] menu = new String[] { "Admin Login", "Admin Sign Up (Testing Purposes Only)", "Login", "Sign Up"};

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
                    MemberLogin();
                    break;
                case "4":
                    SignUpMembers();                 
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

            if (Regex.IsMatch(username, "^[A-z,0-9]{1,35}") && Regex.IsMatch(password, "^[A-z,0-9]{1,35}"))
            {
                List<Admin> records = ReadAdmins();
                Admin admin = records.Find(a => a.Username == username);
                if (ValidatePassword(password, admin.Password))
                {
                    Menu("Admin");
                }
                else
                {
                    Console.WriteLine("Invaild Username or Password try again");
                    AdminLogin();
                }
            }
            else
            {
                Console.WriteLine("Please enter a vaild username/password");
                MainMenu();
            }
        }
        static void AdminSignUp()
        {
            List<Admin> records = new List<Admin>();

            Console.WriteLine("Please Enter a Username");
            string username = Console.ReadLine();

            Console.WriteLine("Please Enter a Password");
            string password = Console.ReadLine();
            if (Regex.IsMatch(username, "^[A-z,0-9]{1,35}") && Regex.IsMatch(password, "^[A-z,0-9]{1,35}"))
            {
                password = CreateHash(password);
                records.Add(new Admin(random.Next(), username, password));

                WriteAdminsToFile(records);
            }
            else
            {
                Console.WriteLine("Plese Enter a vaild username/password");
            }
            MainMenu();
        }
        static void MemberLogin()
        {
            Console.WriteLine("Please Enter your Username");
            string username = Console.ReadLine();

            Console.WriteLine("Please Enter your Password");
            string password = Console.ReadLine();

            if (Regex.IsMatch(username, "^[A-z,0-9]{1,35}") && Regex.IsMatch(password, "^[A-z,0-9]{1,35}"))
            {
                List<Member> records = ReadMembers();
                Member member = records.Find(m => m.Username == username);

                if (ValidatePassword(password, member.Password))
                {
                    Menu("Member");
                }
                else
                {
                    Console.WriteLine("Invaild Username or Password try again");
                    MemberLogin();
                }
            }
            else
            {
                Console.WriteLine("Please enter a vaild username/password");
            }    
        }
        static void SignUpMembers()
        {
            List<Member> records = new List<Member>();
            Console.WriteLine("Please Enter a Username");
            string username= Console.ReadLine();

            Console.WriteLine("Please Enter a Password");
            string password = Console.ReadLine();

            if (Regex.IsMatch(username, "^[A-z,0-9]{1,35}") && Regex.IsMatch(password, "^[A-z,0-9]{1,35}"))
            {
                password = CreateHash(password);
                records.Add(new Member(random.Next(), username, password));

                WriteMembersToFile(records);
            }
            else
            {
                Console.WriteLine("Please enter a vaild username/password");
            }
            
            MainMenu();
        }
        static void Menu(string privilege)
        {
            if (privilege == "Admin")
            {
                String[] menu = new String[] { "Create new Fitness Plan", "Read Fitness Plans", "Update a Fitness Plan", "Delete a Fitness Plan" };

                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine("{0}.{1}", i + 1, menu[i]);
                }
                Console.WriteLine("Enter your option");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        CreateFitnessPlan(privilege);
                        break;
                    case "2":
                        ReadFitnessPlans(privilege);
                        break;
                    case "3":
                        UpdateFitnessPlans(privilege);
                        break;
                    case "4":
                        DeletePlan(privilege);
                        break;
                    default:
                        Console.WriteLine("Please enter a vaild number");
                        Menu(privilege);
                        break;
                }
            }
            else
            {
                String[] menu = new String[] { "Read Fitness Plan","Logout"};
                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine("{0}.{1}", i + 1, menu[i]);
                }
                Console.WriteLine("Enter your option");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ReadFitnessPlans(privilege);
                        break;
                    case "2":
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("Please enter a vaild number");
                        Menu(privilege);
                        break;
                }
            }   
        }
        static void CreateFitnessPlan(string priv)
        {
            int runLength = 0;
            int numPushUps = 0;
            int numSquats = 0;
            List <FitnessPlan> records= new List<FitnessPlan>();
            
            Console.WriteLine("Enter the length of the run");
            while (!int.TryParse(Console.ReadLine(), out runLength))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
                Console.WriteLine("Enter the length of the run:");
            }

            Console.WriteLine("Enter the number of push ups");
            while (!int.TryParse(Console.ReadLine(), out numPushUps))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
                Console.WriteLine("Enter the number of push ups:");
            }

            Console.WriteLine("Enter the number of squats");
            while (!int.TryParse(Console.ReadLine(), out numPushUps))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
                Console.WriteLine("Enter the number of squats:");
            }

            records.Add(new FitnessPlan(random.Next(),runLength, numPushUps, numSquats));

            WritePlansToFile(records);
            Menu(priv);
        }
        static void ReadFitnessPlans(string priv)
        {
            List<FitnessPlan> records = ReadPlans();
            foreach (var r in records)
            {
                Console.WriteLine(r.ToString());
            }
            Menu(priv);
        }
        static void UpdateFitnessPlans(string priv)
        {
            List<FitnessPlan> records = ReadPlans();
            int lengthOfRun=0;
            int numberOfPushUps=0;
            int numberOfSquats=0;
            int id=0;

            Console.WriteLine("Enter the ID of the plan you wish to update");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
                Console.WriteLine("Enter the ID of the plan you wish to update:");
            }

            if (records.Find(fp => fp.Id == id) != null)
            {
                FitnessPlan oldPlan =records.Find(fp => fp.Id == id);

                Console.WriteLine("Enter the length of the new run currently:{0}", oldPlan.LengthOfRun);
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Please Enter a valid numerical value!");
                    Console.WriteLine("Enter the length of the new run currently:{0}", oldPlan.LengthOfRun);
                }

                Console.WriteLine("Enter the number of push ups currently:{0}", oldPlan.NumberOfPushUps);
                while (!int.TryParse(Console.ReadLine(), out numberOfPushUps))
                {
                    Console.WriteLine("Please Enter a valid numerical value!");
                    Console.WriteLine("Enter the number of push ups currently:{0}", oldPlan.NumberOfPushUps);
                }

                Console.WriteLine("Enter the number of squats currently:{0}", oldPlan.NumberOfSquats);
                while (!int.TryParse(Console.ReadLine(), out numberOfSquats))
                {
                    Console.WriteLine("Please Enter a valid numerical value!");
                    Console.WriteLine("Enter the number of squats currently:{0}", oldPlan.NumberOfSquats);
                }

                FitnessPlan newPlan = new FitnessPlan(oldPlan.Id,lengthOfRun,numberOfPushUps,numberOfSquats);

                records.Remove(oldPlan);
                records.Add(newPlan);
            }
            else
            {
                Console.WriteLine("Plan not found try agian");
                Menu(priv);
            }
           
            OverWritePlansToFile(records);
            Menu(priv);
        }
        static void DeletePlan(string priv)
        {
            var records = ReadPlans();
            int id = 0;
            
            Console.WriteLine("Enter the ID of the plan you wish to delete");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
                Console.WriteLine("Enter the ID of the plan you wish to delete");
            }
            if (records.Find(fp => fp.Id == id) != null)
            {
                FitnessPlan entry = records.Find(fp => fp.Id == id);
                records.Remove(entry);
                OverWritePlansToFile(records);
            }
            else
            {
                Console.WriteLine("Plan not found please try again");
            }
                
            Menu(priv);
        }
        static void WriteMembersToFile(List<Member> data)
        {
            FileStream fs = new FileStream("../Users.csv", FileMode.Append);

            Console.WriteLine("File opened");

            var writer = new StreamWriter(fs);
            foreach(dynamic entry in data)
            {
                writer.WriteLine(entry.ToFileFormat());
            }
            Console.WriteLine("Data wrote to file");
            writer.Close();
            fs.Close();
        }
        static void WriteAdminsToFile(List<Admin> data)
        {
            FileStream fs = new FileStream("../Admins.csv", FileMode.Append);

            Console.WriteLine("File opened");

            var writer = new StreamWriter(fs);
            foreach (dynamic entry in data)
            {
                writer.WriteLine(entry.ToFileFormat());
            }
            Console.WriteLine("Data wrote to file");
            writer.Close();
            fs.Close();
        }
        static void WritePlansToFile(List<FitnessPlan> data)
        {
            FileStream fs = new FileStream("../Plans.csv", FileMode.Append);

            Console.WriteLine("File opened");

            var writer = new StreamWriter(fs);
            foreach (dynamic entry in data)
            {
                writer.WriteLine(entry.ToFileFormat());
            }
            Console.WriteLine("Data wrote to file");
            writer.Close();
            fs.Close();
        }
        static void OverWritePlansToFile(List<FitnessPlan> data)
        {
            FileStream fs = new FileStream("../Plans.csv", FileMode.Create);

            Console.WriteLine("File opened");

            var writer = new StreamWriter(fs);
            if (data.Count != 0) {
                foreach (dynamic entry in data)
                {
                    writer.WriteLine(entry.ToFileFormat());
                    Console.WriteLine("Data overwrote to file");
                }
            }
            else
            {
                writer.WriteLine();
            }
            writer.Close();
            fs.Close();
        }
        static List<Member> ReadMembers()
        {
            List<Member> data = new List<Member>();
            string path = "../Users.csv";
            if (File.Exists(path) && new System.IO.FileInfo(path).Length != 0)
            {
                FileStream fs = new FileStream("../Users.csv", FileMode.OpenOrCreate);
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        int id = int.Parse(values[0]);
                        string username = values[1];
                        string password = values[2];

                        Member m = new Member(id, username, password);
                        data.Add(m);
                    }
                }
                fs.Close();
                return data;
            }
            else
            {
                return data;
            }
        }
        static List<Admin> ReadAdmins()
        {
            List<Admin> data = new List<Admin>();
            string path = "../Admins.csv";
            if (File.Exists(path) && new System.IO.FileInfo(path).Length != 0)
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        int id = int.Parse(values[0]);
                        string username = values[1];
                        string password = values[2];

                        Admin a = new Admin(id, username, password);
                        data.Add(a);
                    }
                }
                fs.Close();
                return data;
            }
            else
            {
                return data;
            }
           
        }
        static List<FitnessPlan> ReadPlans()
        {
            List<FitnessPlan> data = new List<FitnessPlan>();
            string path = "../Plans.csv";
            if (File.Exists(path) && new System.IO.FileInfo(path).Length!=0) {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        int id = int.Parse(values[0]);
                        DateTime planDate = DateTime.Parse(values[1]);
                        int lengthOfRun = int.Parse(values[2]);
                        int numberOfPushUps = int.Parse(values[3]);
                        int numberOfSquats = int.Parse(values[4]);

                        FitnessPlan fp = new FitnessPlan(id, planDate, lengthOfRun, numberOfPushUps, numberOfSquats);
                        data.Add(fp);
                    }
                    fs.Close();
                }
                return data;
            }
            else
            {
                Console.WriteLine("There are no entries yet");
                return data;
            }
            
        }
       
        public static string CreateHash(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            GC.Collect();
            return Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }
        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }
        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };
            return pbkdf2.GetBytes(outputBytes);
        }
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}
