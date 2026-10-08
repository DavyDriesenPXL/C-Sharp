using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Week_3
{
    internal class Functies
    {
        public static void Ondernemingsnummer()
        {
            bool isValid = false;
            string ondernemingsnummer;
            do
            {
                Console.Write("Ondernemingsnummer: ");
                ondernemingsnummer = Console.ReadLine();

                if(ondernemingsnummer.Length != 10 || !ondernemingsnummer.All(char.IsDigit))
                {
                    Console.WriteLine("Ondernemingsnummer moet uit exact 10 cijfers bestaan.");
                }
                else
                {
                    isValid = true;
                }
            } while (!isValid);
        }
    }
}
