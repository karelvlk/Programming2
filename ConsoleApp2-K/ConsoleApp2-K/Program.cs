using System;

namespace ConsoleApp2_K
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
                if ((ascii == 32 || ascii == 45) && !isChanged)
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
                    if (!isChanged && isNegative) number *= -1;
                    isChanged = true;
                    result = result * 10 + number;
                }
            }
            if (isChanged)
            {
                return result;
            }
            else
            {
                throw new FormatException();
            }
        }
        static void Main(string[] args)
        {
            try
            {
                int lastNumber = ReadAnotherNumber();               
                int highest = lastNumber;
                int secondHighest = 0;
                bool isSecondNumber = false;
                while (lastNumber != -1) {
                    lastNumber = ReadAnotherNumber();
                    if (lastNumber == -1) continue;
                    if (lastNumber >= highest)
                    {
                        secondHighest = highest;
                        isSecondNumber = true;
                        highest = lastNumber;
                    }
                    else if (isSecondNumber)
                    {
                        if (lastNumber >= secondHighest)
                        {
                            secondHighest = lastNumber;
                        }
                        
                    } 
                    else if (!isSecondNumber)
                    {
                        isSecondNumber = true;
                        secondHighest = lastNumber;
                    }
                }          

                if (isSecondNumber)
                {
                    Console.WriteLine(secondHighest);
                }          
            } 
            catch (FormatException)
            {
                //Console.WriteLine("Error: input numbers are in wrong format");
            }
        }
    }
}
