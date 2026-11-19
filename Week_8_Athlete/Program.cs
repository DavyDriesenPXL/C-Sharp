namespace Week_8_Athlete
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static void Main(string[] args)
            {
                //try
                //{
                //    string[] names = new string[5];
                //    names[0] = "PXL";
                //    names[1] = "John";
                //    names[4] = "Jane";
                //    //int number1 = 5;
                //    //int number2 = 0;
                //    //int result = number1 / number2;
                //    Student stud = new Student();
                //    stud.Name = "PXL";
                //    try
                //    {
                //        stud.Result = 5;
                //    }
                //    catch (ArgumentException)
                //    {
                //        stud.Result = 0;
                //    }

                //    foreach (string demo in names)
                //    {
                //        Console.WriteLine(demo);
                //    }

                //    Console.WriteLine($"Student {stud.Name}, {stud.Result}");
                //}
                //catch (DivideByZeroException ex)
                //{
                //    Console.WriteLine("Boekhoudkundige fout.");
                //}
                //catch (IndexOutOfRangeException ex)
                //{
                //    Console.WriteLine("Index buiten bereik: " + ex.Message);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                Console.Write("Hoeveel deelnemers? ");
                int.TryParse(Console.ReadLine(), out int number);

                string[] names = new string[number];
                byte[] times = new byte[names.Length];
                string fastestName = string.Empty;
                int fastestTime = int.MaxValue;
                int idx = 0;
                bool endOfApplication = false;
                bool askInput = true;

                do
                {
                    do //Input Naam
                    {
                        Console.Write("Naam: ");
                        names[idx] = Console.ReadLine();
                        endOfApplication = names[idx].ToUpper().Equals("STOP");
                        askInput = false;
                        if (endOfApplication)
                        {
                            string input;
                            do
                            {
                                Console.Write("Bent u zeker dat u wil stoppen (J/N)? ");
                                input = Console.ReadLine().ToUpper();
                            } while (!input.Equals("J") && !input.Equals("N"));
                            endOfApplication = input.Equals("J");
                            askInput = input.Equals("N");
                        }
                    } while (askInput);

                    if (!endOfApplication)
                    {
                        do //Input looptijd
                        {
                            askInput = false;
                            bool isCorrect = false;
                            do //Input correct getal
                            {
                                Console.Write("Looptijd: ");
                                isCorrect = byte.TryParse(Console.ReadLine(), out times[idx]);
                                if (!isCorrect)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Foutieve ingave.");
                                    Console.ResetColor();
                                }
                            } while (!isCorrect);
                            endOfApplication = times[idx] == 0;
                            if (endOfApplication)
                            {
                                string input;
                                do
                                {
                                    Console.Write("Bent u zeker dat u wil stoppen (J/N)? ");
                                    input = Console.ReadLine().ToUpper();
                                } while (!input.Equals("J") && !input.Equals("N"));
                                endOfApplication = input.Equals("J");
                                askInput = input.Equals("N");
                            }
                        } while (askInput);

                        //Show overview
                        if (times[idx] < fastestTime)
                        {
                            fastestName = names[idx];
                            fastestTime = times[idx];
                        }
                        idx++;
                    }
                } while (!endOfApplication && idx < number);

                for (int i = 0; i < names.Length; i++)
                {
                    Console.WriteLine($"Atleet {names[i]} heeft een tijd van {times[i]}.");
                }
                Console.WriteLine($"De snelste atleet is {fastestName} met {fastestTime} seconden");
            }
        }
    }
}
