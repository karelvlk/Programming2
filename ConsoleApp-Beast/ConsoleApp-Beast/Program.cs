using System;

namespace ConsoleApp_Beast
{
    class Program
    {
        class Reader
        {
            public int ReadNumber()
            {
                int result = 0;
                bool isChanged = false;
                for (bool isAnotherNumber = true; isAnotherNumber == true;)
                {
                    int ascii = Console.Read();
                    if (ascii == 32 || ascii == 10 || ascii == 13)
                    {
                        if (isChanged)
                        {
                            isAnotherNumber = false;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    int number = ascii - '0';

                    isChanged = true;
                    result = result * 10 + number;
                }

                return result;
            }


            public string ReadLetter()
            {
                bool isNoLetter = true;
                string letter = "";
                while (isNoLetter)
                {
                    int ascii = Console.Read();
                    if (ascii == ' ' || ascii == 10 || ascii == 13)
                    {
                        continue;
                    }
                    else
                    {
                        isNoLetter = false;
                        letter = (char)ascii + "";
                    }
                }
                return letter;
            }
        }

        class Beast
        {
            int[] position = new int[2];
            int[] direction = new int[2];

            public void SetPosition(int x, int y)
            {
                position[0] = x;
                position[1] = y;
            }

            public void SetDirection(int x, int y)
            {
                direction[0] = x;
                direction[1] = y;
            }

            public int[] GetPosition()
            {
                return position;
            }

            public int[] GetDirection()
            {
                return direction;
            }
        }

        class GridLoader
        {
            string[,] grid;
            int width;
            int height;
            private readonly Reader reader = new Reader();

            public Beast LoadGrid()
            {
                width = reader.ReadNumber();
                height = reader.ReadNumber();
                grid = new string[width, height];
                Beast beast = new Beast();

                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        string letter = reader.ReadLetter();
                        switch (letter)
                        {
                            case "v":
                                beast.SetPosition(w, h);
                                beast.SetDirection(0, 1);
                                grid[w, h] = ".";
                                break;
                            case ">":
                                beast.SetPosition(w, h);
                                beast.SetDirection(1, 0);
                                grid[w, h] = ".";
                                break;
                            case "^":
                                beast.SetPosition(w, h);
                                beast.SetDirection(0, -1);
                                grid[w, h] = ".";
                                break;
                            case "<":
                                beast.SetPosition(w, h);
                                beast.SetDirection(-1, 0);
                                grid[w, h] = ".";
                                break;
                            default:
                                grid[w, h] = letter;
                                break;
                        }
                    }
                }

                return beast;
            }

            public string[,] GetGrid()
            {
                return grid;
            }

            public int GetWidth()
            {
                return width;
            }

            public int GetHeight()
            {
                return height;
            }
        }

        class Printer
        {
            public void PrintOutput(string[,] grid, int width, int height, Beast beast)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        int[] pos = beast.GetPosition();
                        int[] dir = beast.GetDirection();
                        if (w == pos[0] && h == pos[1])
                        {
                            if (dir[0] == 0 && dir[1] == -1)
                            {
                                Console.Write("^");
                            }
                            else if (dir[0] == -1 && dir[1] == 0)
                            {
                                Console.Write("<");
                            }
                            else if (dir[0] == 0 && dir[1] == 1)
                            {
                                Console.Write("v");
                            }
                            else if (dir[0] == 1 && dir[1] == 0)
                            {
                                Console.Write(">");
                            }
                        }
                        else
                        {
                            Console.Write(grid[w, h]);
                        }
                    }
                    Console.Write("\n");
                }
            }
        }

        class Mover
        {
            private string GetLetter(string[,] grid, int w, int h, int x, int y)
            {
                if ((0 <= x && x < w) && (0 <= y && y < h))
                {
                    return grid[x, y];
                }
                else
                {
                    return "O";
                }
            }


            public void DoOneMove(string[,] grid, Beast beast, int w, int h)
            {
                int[] d = beast.GetDirection();
                int[] dl = TurnLeft(d);
                int[] p = beast.GetPosition();
                int xRB = p[0] - dl[0] - d[0];
                int yRB = p[1] - dl[1] - d[1];
                string rightBack = GetLetter(grid, w, h, xRB, yRB);

                int xRF = p[0] - dl[0] + d[0];
                int yRF = p[1] - dl[1] + d[1];
                string rightForw = GetLetter(grid, w, h, xRF, yRF);

                int xR = p[0] - dl[0];
                int yR = p[1] - dl[1];
                string right = GetLetter(grid, w, h, xR, yR);

                int xF = p[0] + d[0];
                int yF = p[1] + d[1];                
                string forw = GetLetter(grid, w, h, xF, yF);

                if (rightForw == "X" && forw == "." || right == "X" && forw == ".")
                {
                    beast.SetPosition(p[0] + d[0], p[1] + d[1]);
                }
                else if (rightBack == "X" && right == ".")
                {
                    beast.SetDirection(-dl[0], -dl[1]);
                }
                else if (forw == "X")
                {
                    beast.SetDirection(dl[0], dl[1]);
                }
            }
 
            private int[] TurnLeft(int[] dir)
            {
                if (dir[0] == 0 && dir[1] == -1)
                {
                    return new int[] { -1, 0 };
                }
                else if (dir[0] == -1 && dir[1] == 0)
                {
                    return new int[] { 0, 1 };
                }
                else if (dir[0] == 0 && dir[1] == 1)
                {
                    return new int[] { 1, 0 };
                }
                else if (dir[0] == 1 && dir[1] == 0)
                {
                    return new int[] { 0, -1 };
                }
                else
                {
                    return new int[] { dir[0], dir[1] };
                }
            }
        }

        static void Main(string[] args)
        {
            GridLoader loader = new GridLoader();
            Beast beast = loader.LoadGrid();
            Printer printer = new Printer();
            Mover mover = new Mover();
            for (int i = 0; i < 20; i++)
            {
                mover.DoOneMove(loader.GetGrid(), beast, loader.GetWidth(), loader.GetHeight());
                printer.PrintOutput(loader.GetGrid(), loader.GetWidth(), loader.GetHeight(), beast);
            }
        }
    }
}
