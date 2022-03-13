using System;

namespace ConsoleApp4_K
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
        static void Main(string[] args)
        {
            try
            {
                int n = ReadAnotherNumber();
                int[] array = new int[n];
                int validPositions = 0;
                int highest = 0;
                bool isHighest = false;
                for(int i = 0; i < n; i++)
                {
                    int number = ReadAnotherNumber();
                    if (isHighest)
                    {
                        if (number == highest)
                        {
                            array[validPositions++] = i;
                        } 
                        else if (number > highest)
                        {
                            highest = number;
                            validPositions = 0;
                            array[validPositions++] = i;
                        }                
                    }
                    else
                    {
                        highest = number;
                        array[validPositions++] = i;
                        isHighest = true;
                    }
                }

                Console.WriteLine(highest);
                for(int j = 0; j < validPositions; j++)
                {
                    if (j != 0) Console.Write(" ");
                    Console.Write(array[j]+1);
                }

            }
            catch(FormatException)
            {

            }
        }
    }
}
