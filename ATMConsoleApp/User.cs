using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATMConsoleApp
{
    public class User
    {
        private int cardId;
        private int pin;
        private double balance;

        public User(int cardId, int pin, double balance)
        {
            this.CardId = cardId;
            this.Pin = pin;
            this.Balance = balance;
        }
        public int CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }
        public int Pin
        {
            get { return pin; }
            set { pin = value; }
        }
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
    }
}
