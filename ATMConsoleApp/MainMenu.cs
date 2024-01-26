using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMConsoleApp
{
    public class MainMenu
    {
        public int menuInput;
        public void Menu()
        {

            DataBaseIO dataBaseIO = new DataBaseIO();

            Console.WriteLine("\n\n***Main Menu***\n1 - Check Balance\n2 - Withdraw Money\n3 - Change Pin\n4- Exit");
            menuInput = Convert.ToInt32(Console.ReadLine());

            switch(menuInput)
            {
                case 1:
                    Console.WriteLine("\nCheck Balance...");
                    dataBaseIO.CheckBalance();
                    dataBaseIO.AnotherService();
                    break;
                case 2:
                    Console.WriteLine("\nWithdraw Money...");
                    dataBaseIO.WithdrawMoney();
                    dataBaseIO.AnotherService();
                    break;
                case 3:
                    Console.WriteLine("\nChange Pin...");
                    dataBaseIO.ChangePin();
                    dataBaseIO.AnotherService();
                    break;
                case 4:
                    Console.WriteLine("\nEjecting Card...");
                    Environment.Exit(0);
                    break;
            }
            
        }
    }
}
