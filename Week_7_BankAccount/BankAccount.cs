using System;
using System.Collections.Generic;
using System.Text;

namespace Week_7_BankAccount
{
    internal class BankAccount
    {
        public BankAccount()
        {
            
        }

        public decimal CurrentBalance { get; private 
                set; }

        public void Deposit(decimal amount)
        {
            CurrentBalance += amount;
        }

        public void Withdrawal(decimal amount)
        {
            CurrentBalance -= Math.Abs(amount);
        }
    }
}
