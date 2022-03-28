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
                int n = readAnotherNumber();
                int[] array = new int[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = readAnotherNumber();
                }

                int pivotIdx = -1;
                int currentIdx = n - 1;
                while (pivotIdx == -1 && currentIdx > 0)
                {
                    if (array[currentIdx] > array[currentIdx - 1])
                    {
                        pivotIdx = currentIdx - 1;
                    }
                    else
                    {
                        currentIdx -= 1;
                    }
                }
                if (pivotIdx == -1)
                {
                    Console.WriteLine("NEEXISTUJE");
                    return;
                }


                int toBeSwitchedIdx = -1;
                currentIdx = n - 1;
                while (toBeSwitchedIdx == -1)
                {
                    if (array[currentIdx] > array[pivotIdx])
                    {
                        toBeSwitchedIdx = currentIdx;
                    }
                    else
                    {
                        currentIdx -= 1;
                    }
                }


                int pivot = array[pivotIdx];
                array[pivotIdx] = array[toBeSwitchedIdx];
                array[toBeSwitchedIdx] = pivot;

                for (int i = 0; i < pivotIdx + 1; i++)
                {
                    Console.Write(array[i]);
                    Console.Write(" ");
                }
                
                for (int j = n-1; j > pivotIdx; j--)
                {
                    Console.Write(array[j]);
                    if (pivotIdx != j - 1)
                    {
                        Console.Write(" ");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("NELZE");
            }
        }
    }
}
