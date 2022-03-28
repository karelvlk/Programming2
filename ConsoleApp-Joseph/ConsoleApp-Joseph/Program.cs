
using System;

namespace ConsoleApp1_K
{
    class Program
    {
        static int Space = 2;

        public static int ReadAnotherNumber()
        {
            int result = 0;
            bool isNegative = false;
            for (bool isAnotherNumber = true; isAnotherNumber == true;)
            {
                int ascii = Console.Read();
                if (ascii == 32 || ascii == 45)
                {
                    if (ascii == 45) isNegative = true;
                    continue;
                }

                int number = ascii - '0';

                if (number > 9 || number < 0)
                {
                    isAnotherNumber = false;
                }
                else
                {
                    if (isNegative) number *= -1;
                    result = result * 10 + number;
                }
            }
            return result;
        }

        static int processJoseph(int numberOfPeople)
        {
            int position = 1;
            while (position <= numberOfPeople)
            {
                position *= 2;
            }
            return ((2 * numberOfPeople) - position) + 1;

        }

        static void Main(string[] args)
        {
            int numberOfSoldiers = ReadAnotherNumber();
            if (numberOfSoldiers > 0)
            {
                Console.WriteLine(processJoseph(numberOfSoldiers));
            }
            else
            {
                Console.WriteLine("ERROR");
            }
            
        }
    }
}

