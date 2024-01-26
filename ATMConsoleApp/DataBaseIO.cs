using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ATMConsoleApp
{
    class DataBaseIO
    {
        private List<User> users;

        private static int userNumber;

        private int userId;
        private int userPin;
        private int newPin;
        private int Attempts;

        private double withdraw { get; set; }
        

        public static int UserNumber
        {
            get { return userNumber; }
            set { userNumber = value; }
        }
        public void ReadUsers()
        {
            users = new List<User>();
            using (StreamReader sr = new StreamReader(@"C:\Users\jordw\source\repos\ATMConsoleApp\ATMConsoleApp\UserFile.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(' ');

                    int cardId = Convert.ToInt32(data[0]);
                    int pin = Convert.ToInt32(data[1]);
                    double balance = Convert.ToDouble(data[2]);

                    users.Add(new User(cardId, pin, balance));
                }
            }  
        }
        public void WriteUsers()
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\jordw\source\repos\ATMConsoleApp\ATMConsoleApp\UserFile.txt"))
            {
                for (int i = 0; i< users.Count; i++)
                {
                    sw.WriteLine(users[i].CardId + " " + users[i].Pin + " " + users[i].Balance);
                }
            }
        }
        public void Login()
        {

            MainMenu mainMenu = new MainMenu();

            while (Attempts < 3)
            {
                try
                {
                    Console.WriteLine("\nEnter UserID: ");
                    userId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nEnter Pin: ");
                    userPin = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nUser ID is 8 Digits...\nPin is 4 Digits...\nNumbers only...");
                    Login();
                }
                int i = 0;
                UserNumber = -1;

                for (i = 0; i < users.Count; i++)
                {
                    if (userId == users[i].CardId)
                    {
                        UserNumber = i;
                    }
                }
                if (UserNumber == -1)
                {
                    Console.WriteLine("\nIncorrectID");
                    Attempts++;
                }
                else
                {
                    if (userPin == users[UserNumber].Pin)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nIncorrect Pin");
                        Attempts++;
                    }
                }
            }
            if (Attempts >= 3)
            {
                Console.WriteLine("\nToo many attempts... \nEjecting Card...");
            }
            else
            {
                mainMenu.Menu();
            }
        }
        public void WithdrawMoney()
        {
            if (UserNumber == -1)
            {
                Console.WriteLine("\nAn Error has occured\n" + "Ejecting Card... ");
                Environment.Exit(0);

            }
            else
            {
                CheckBalance();

                Console.WriteLine("\nThis machine only carries £10's & £20's\nEnter 0 to cancel Withdraw\nOr\nEnter Withdraw Amount: ");
                try
                {
                    withdraw = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid Input...");
                    WithdrawMoney();
                }
                if (withdraw == 0)
                {
                    AnotherService();
                }
                else
                {
                    ChangeBalance(withdraw);
                }
            }
        }
        public void ChangeBalance(double withdraw)
        {
            ReadUsers();

            if (withdraw < users[UserNumber].Balance && withdraw % 10 == 0)
            {
                users[UserNumber].Balance = users[UserNumber].Balance - withdraw;
                Console.WriteLine("You're New Balance is: " + users[UserNumber].Balance);
                WriteUsers();
            }
            else if (withdraw % 10 != 0)
            {
                Console.WriteLine("\nWithdraw amount must be in segments of £10 or £20's...");
                WithdrawMoney();
            }
            else
            {
                Console.WriteLine("\nAmount cannot be more than current Balance...");
                WithdrawMoney();
            }
            
        }
        public void CheckBalance()
        {
            if (UserNumber == -1)
            {
                Console.WriteLine("\nAn Error has occured\n" + "Ejecting Card... ");
                Environment.Exit(0);
            }
            else
            {
                ReadUsers();
                Console.WriteLine($"\nCurrent Balance: {users[UserNumber].Balance}");
            }
        }
        public void ChangePin()
        {
            if (UserNumber == -1)
            {
                Console.WriteLine("\nAn Error has occured\n" + "Ejecting Card... ");
                Environment.Exit(0);
            }
            else
            {
                ReadUsers();
                
                Console.WriteLine("\nEnter 0 to cancel operation\nOr\nEnter your new pin: ");
                try
                {
                    newPin = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nPin can only be numbers...");
                    ChangePin();
                }
                if (newPin.ToString().Length == 4)
                {
                    users[UserNumber].Pin = newPin;

                    Console.WriteLine("\nYou have changed your pin...");
                    WriteUsers();
                }
                else if (newPin.ToString().Length != 4)
                {
                    if (newPin == 0)
                    {
                        AnotherService();
                    }
                    else
                    {
                        Console.WriteLine("\nPin is only 4 digits");
                        ChangePin();
                    }
                }
            }
        }
        public void AnotherService()
        {
            MainMenu menu = new MainMenu();

            string answer;

            Console.WriteLine("\nWould you like to use another service?" + "\nY = Yes\nN = No");
            
            answer = Console.ReadLine();

            if (answer == "Y")
            {
                Console.WriteLine("\nLoading Menu...\n");
                menu.Menu();
            }
            else if (answer == "N")
            {
                Console.WriteLine("\nEjecting Card...");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("\nEnter a Y or N...\n");
            }
        }
    }
}
