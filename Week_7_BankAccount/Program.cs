namespace Week_7_BankAccount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8;

            BankAccount account = new();
            string input;

            Console.WriteLine("*** Bankrekening ***");

            do
            {
                Console.Write("\nGeef het bedrag voor storting (+ voor storting, - voor afhaling): ");
                input = Console.ReadLine();

                decimal.TryParse(input, out decimal amount);

                if (amount <= 0)
                {
                    account.Withdrawal(amount);
                }
                else
                {
                    account.Deposit(amount);
                }
                Console.WriteLine($"\nSaldo: {account.CurrentBalance:c}");
            } while (!input.ToUpper().Equals("STOP"));
        }
    }
}
