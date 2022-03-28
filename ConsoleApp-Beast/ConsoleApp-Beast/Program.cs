using System;

namespace ConsoleApp_Beast
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

    class World
    {
        string[,] grid;
        Beast beast;
        int width;
        int height;
        public World(string[,] grid, Beast beast, int width, int height)
        {
            this.grid = grid;
            this.beast = beast;
            this.width = width;
            this.height = height;
        }

        public string[,] GetGrid()
        {
            return grid;
        }

        public Beast GetBeast()
        {
            return beast;
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

    class WorldLoader
    {
        string[,] grid;
        int width;
        int height;
        private readonly Reader reader = new Reader();

        public World CreateWorld()
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

            return new World(grid, beast, width, height);
        }
    }

    class Printer
    {
        World world;
        public Printer(World world)
        {
            this.world = world;
        }

        public void PrintOutput()
        {
            for (int h = 0; h < world.GetHeight(); h++)
            {
                for (int w = 0; w < world.GetWidth(); w++)
                {
                    int[] pos = world.GetBeast().GetPosition();
                    int[] dir = world.GetBeast().GetDirection();
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
                        Console.Write(world.GetGrid()[w, h]);
                    }
                }
                Console.Write("\n");
            }
        }
    }

    class Mover
    {
        World world;
        public Mover(World world)
        {
            this.world = world;
        }

        private string GetLetter(int x, int y)
        {
            if ((0 <= x && x < world.GetWidth()) && (0 <= y && y < world.GetHeight()))
            {
                return world.GetGrid()[x, y];
            }
            else
            {
                return "O";
            }
        }

        public void DoOneMove()
        {
            int[] d = world.GetBeast().GetDirection();
            int[] dl = TurnLeft(d);
            int[] p = world.GetBeast().GetPosition();
            int xRB = p[0] - dl[0] - d[0];
            int yRB = p[1] - dl[1] - d[1];
            string rightBack = GetLetter(xRB, yRB);

            int xRF = p[0] - dl[0] + d[0];
            int yRF = p[1] - dl[1] + d[1];
            string rightForw = GetLetter(xRF, yRF);

            int xR = p[0] - dl[0];
            int yR = p[1] - dl[1];
            string right = GetLetter(xR, yR);

            int xF = p[0] + d[0];
            int yF = p[1] + d[1];
            string forw = GetLetter(xF, yF);

            if (rightForw == "X" && forw == "." || right == "X" && forw == ".")
            {
                world.GetBeast().SetPosition(p[0] + d[0], p[1] + d[1]);
            }
            else if (rightBack == "X" && right == ".")
            {
                world.GetBeast().SetDirection(-dl[0], -dl[1]);
            }
            else if (forw == "X")
            {
                world.GetBeast().SetDirection(dl[0], dl[1]);
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

    class Program
    {
        static void Main(string[] args)
        {
            WorldLoader loader = new WorldLoader();
            World world = loader.CreateWorld();
            Printer printer = new Printer(world);
            Mover mover = new Mover(world);
            for (int i = 0; i < 20; i++)
            {
                mover.DoOneMove();
                printer.PrintOutput();
            }
        }
    }
}
