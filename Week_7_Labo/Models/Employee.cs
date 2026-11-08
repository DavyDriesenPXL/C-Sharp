using System;
using System.Collections.Generic;
using System.Text;

namespace Week_7_Labo.Models
{
    internal class Employee
    {
		private string _firstName;
		private string _lastName;
		private DateTime _birthDate;
		private decimal _salary;

		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public int Age
        {
           
            get { int age = DateTime.Today.Year - BirthDate.Year;
                return age;
            }
        }

        public Employee(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public Employee()
        {
            
        }

        public void IncreaseSalary(int percentage)
        {
            Salary = Salary * (1 + percentage / 100m);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} - {Age} - {Salary:c}";
        }
    }
}
