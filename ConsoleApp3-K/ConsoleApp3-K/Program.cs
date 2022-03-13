using System;

namespace ConsoleApp3_K
{
    class Program
    {
        public static int ReadAnotherNumber()
        {
            int result = 0;
            bool isChanged = false;
            bool isNegative = false;
            for (bool isAnotherNumber = true; isAnotherNumber == true;)
            {
                int ascii = Console.Read();
                if (ascii == 32 || ascii == 45 || ascii == 10 || ascii == 13)
                {
                    if (isChanged)
                    {
                        isAnotherNumber = false;
                        continue;
                    }
                    else
                    {
                        if (ascii == 45) isNegative = true;
                        continue;
                    }
                }

                int number = ascii - '0';

                if (number > 9 || number < 0)
                {
                    throw new FormatException();
                }
                else
                {
                    isChanged = true;
                    result = result * 10 + number;
                }
            }
            if (isChanged)
            {
                if (isNegative)
                {
                    return result * -1;
                }
                else
                {
                    return result;
                }
            }
            else
            {
                throw new FormatException();
            }
        }

        static bool IsPerfect(int number)
        {
            if (number < 0) number *= -1;
            int sum = 0;
            for(int i = 1; i <= (number / 2) + 1; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                }
            }

            if (number == sum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool IsSquare(int number)
        {
            if (number < 0) return false;
            double sqrt = Math.Sqrt(number);
            if (sqrt % 1 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool IsCubic(int number)
        {
            if (number < 0)
            {
                number *= -1;
            }

            double cbrt = Math.Cbrt(number);
            if (cbrt % 1 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                int number = ReadAnotherNumber();            
                if (IsPerfect(number))
                {
                    Console.Write("P");
                }
                if (IsSquare(number))
                {
                    Console.Write("C");
                }
                if (IsCubic(number))
                {
                    Console.Write("K");
                }               
            }
            catch (FormatException)
            {
                //Console.WriteLine("Error: format error");
            }
        }
    }
}
