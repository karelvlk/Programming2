using System;

namespace ConsoleApp5_K
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

        static void FindCombs(int n, int curr, int lastStep)
        {
            for(int i = 1; i < n-curr; i++)
            {
                if (curr + 1 == n)
                {
                    FindCombs(n, curr - lastStep, 1);
                }
                else
                {
                    FindCombs(n, curr + 1, i);
                }
               
            }
            Console.Write(lastStep);
        }

        static void Main(string[] args)
        {
            try
            {
                int n = ReadAnotherNumber();
                FindCombs(n, 0, 0);
            }
            catch (FormatException)
            {

            }
        }
    }
}
