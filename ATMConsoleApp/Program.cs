using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;

namespace ATMConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBaseIO dataBaseIO = new DataBaseIO();

            dataBaseIO.ReadUsers();

            dataBaseIO.Login();

        }
    }
}
