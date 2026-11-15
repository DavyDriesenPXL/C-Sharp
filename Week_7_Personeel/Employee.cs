using System;
using System.Collections.Generic;
using System.Text;

namespace Week_7_Personeel
{
    internal class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public char Sex { get; set; }
        public int Score { get; set; }
        public int StartYear { get; set; }

        public int YearsOfService 
        {
            get { return DateTime.Now.Year - StartYear; }
            
        }
        public string SexDescription 
        { 
            get
            {   if (Sex == 'M') return "Male";
                else if (Sex == 'V') return "Female";
                else return "Other";
            } 
        }

        public decimal Bonus { get { return CalculateBonus(); } }

        public Employee()
        {
            FirstName = "";
            LastName = "";
            Sex = 'M';
            StartYear = 2000;
        }

        public Employee(string lastName, string firstName, char sex, int score, int startYear)
        {
            LastName = lastName;
            FirstName = firstName;
            Sex = sex;
            Score = score;
            StartYear = startYear;
        }

        public void IncreaseScore()
        {
            Score++;
        }
        
        public void DecreaseScore()
        {
            Score--;
        }

        public string ShowSummary()
        {
            return $"Voornaam: {FirstName}\nNaam: {LastName}\nGeslacht: {SexDescription}\nBonus: {Bonus}\nJaren in dienst: {YearsOfService}";
        }

        private decimal CalculateBonus()
        {
            decimal bonusAmount = 500m;

            bonusAmount += YearsOfService * 20;

            if(Score < 5)
            {
                bonusAmount = bonusAmount / 2;
            }
            else if(Score == 7 || Score == 8)
            {
                bonusAmount *= 1.5m;
            }
            else if(Score == 9 || Score == 10)
            {
                bonusAmount *= 2m;
            }

            return bonusAmount;
        }
    }
}
