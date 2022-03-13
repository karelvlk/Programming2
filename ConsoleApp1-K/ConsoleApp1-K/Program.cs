using System;

namespace ConsoleApp1_K
{
    class Program
    {
        public static int readAnotherNumber()
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

        static void Main(string[] args)
        {
            try
            {
                double n1 = readAnotherNumber();
                Console.WriteLine("N1 >>>");
                Console.WriteLine(n1);
            } 
            catch (FormatException)
            {
                Console.WriteLine("NELZE");
            }
        }
    }
}
