using System;

namespace ConsoleApp_Prefix
{
    class Program
    {
        static int ReadAnother()
        {
            bool isThereAnotherNum = true;
            int numberResult = 0;
            int oper = 0;
            bool isChanged = false;
            bool isNegative = false;
            while (isThereAnotherNum == true)
            {
                int ascii = Console.Read();
                if (ascii == ' ' || ascii == 10 || ascii == 13)
                {
                    if (isChanged)
                    {
                        isThereAnotherNum = false;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (ascii >= '0' && ascii <= '9')
                    {
                        isChanged = true;
                        numberResult = numberResult * 10 + (ascii - '0');
                        continue;
                    }
                    else
                    {
                        if (ascii == '+' || ascii == '-' || ascii == '*' || ascii == '/')
                        {
                            isChanged = true;
                            isThereAnotherNum = false;
                            oper = ascii;
                            break;
                        }
                    }
                }
            }

            if (!isChanged)
            {
                throw new Exception();
            }
            else
            {
                int r;
                switch (oper)
                {
                    case '+':
                        r = ReadAnother() + ReadAnother();
                        return r;
                        break;
                    case '-':
                        r = ReadAnother() - ReadAnother();
                        return r;
                        break;
                    case '*':
                        r = ReadAnother() * ReadAnother();
                        return r;
                    case '/':
                        int exp1 = ReadAnother();
                        int exp2 = ReadAnother();
                        if (exp2 == 0)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            return exp1 / exp2;
                        }
                        break;
                    default:
                        return numberResult;
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine(ReadAnother());
            }
            catch (Exception)
            {
                Console.WriteLine("CHYBA");
            }
        }
    }
}
