using System;
using System.Threading;

namespace ConsoleApp_NonTicTacToe
{
    class Reader
    {
        public virtual string ReadLine()
        {
            return "";
        }
    }
    class ConsoleReader : Reader
    {
        public override string ReadLine()
        {
            return Console.ReadLine();
        }
    }

    class GridCreator
    {
        string[,] grid = new string[3,3];
        int Xs = 0;
        int Os = 0;
        public void CreateNewGrid(string[] lines)
        {
            Xs = 0;
            Os = 0; 

            if (lines.Length > 0)
            {
                for (int h = 0; h < 3; h++)
                {
                    for (int w = 0; w < 3; w++)
                    {
                        string letter = lines[h][w].ToString();
                        if (letter == "X") Xs++;
                        if (letter == "O") Os++;
                        grid[w, h] = letter;
                        
                    }
                }
            } 
            else
            {
                for (int h = 0; h < 3; h++)
                {
                    for (int w = 0; w < 3; w++)
                    {
                        grid[w, h] = ".";
                    }
                }
            }
        }

        public string[,] GetGrid()
        {
            return this.grid;
        }

        public void SetLetterToPosition(int[] position, string letter)
        {
            if (letter == "X") Xs++;
            if (letter == "O") Os++;
            this.grid[position[0], position[1]] = letter;
        }

        public int GetMaxRounds()
        {
            return 9 - Xs - Os;
        }

        public string WhosTurn()
        {
            if ((Xs - Os) == 0)
            {
                return "X";
            } else
            {
                return "O";
            }
        }

        public string GetLetterOnPosition(int[] position)
        {
            return this.grid[position[0], position[1]];
        }
    }

    class GridLogic
    {
        public string WhosTrio(string[,] grid) //returns "N" if there is no trio or "X"/"O" if some player has trio
        {
            //horizontal trios
            for (int width = 0; width < 3; width++)
            {
                if (grid[width, 0] == grid[width, 1] && grid[width, 1] == grid[width, 2])
                {
                    if (grid[width, 0] != ".")
                    {
                        return grid[width, 0];
                    }
                }
            }

            //horizontal trios
            for (int height = 0; height < 3; height++)
            {
                if (grid[0, height] == grid[1, height] && grid[1, height] == grid[2, height])
                {
                    if (grid[0, height] != ".")
                    {
                        return grid[0, height];
                    }
                }
            }

            //diagonal trios

            if (grid[1, 1] == grid[2, 2] && grid[2, 2] == grid[0, 0])
            {
                if (grid[0, 0] != ".")
                {
                    return grid[0, 0];
                }
            }

            return "N";
        }
        public int GetTrioValue(string[,] grid, string player)
        {
            string trioLetter = this.WhosTrio(grid);
            if (trioLetter == "N") // nobody has trio
            {
                return 0;
            }
            else if (trioLetter == player) // current player has trio => current player LOSS
            {
                return -10;
            }
            else // opponent has trio => current player WIN
            {
                return 10;
            }
        }

        public bool IsFreeSpace(string[,] grid)
        {
            for (int h = 0; h < 3; h++)
            {
                for (int w = 0; w < 3; w++)
                {
                    if (grid[w, h] == ".") return true;
                }
            }

            return false;
        }
        public int ProcessMinimax(string[,] grid, string player,  bool isMax)
        {
            int value = this.GetTrioValue(grid, player);
            if (value != 0) return value;
            if (this.IsFreeSpace(grid)) 
            {
                int best = 10; // initialized that it is always overwritten
                if (isMax) best = -10; // initialized that it is always overwritten
                for (int h = 0; h < 3; h++)
                {
                    for (int w = 0; w < 3; w++)
                    {
                        if (grid[w, h] == ".")
                        {
                            grid[w, h] = player; // make current player move
                            string nextPlayer = "X";
                            if (player == "X") nextPlayer = "O";
                            best = Math.Max(best, this.ProcessMinimax(grid, nextPlayer, !isMax));
                            grid[w, h] = "."; // remove current player move to try another one
                        }
                    }
                }
                return best;
            }
            return 0;
        }

        public int[] FindBestMove(string[,] grid, string currentPlayer)
        {
            int bestValue = -int.MaxValue;
            int[] bestMove = { -1, -1 }; //no move 

            for (int h = 0; h < 3; h++)
            {
                for (int w = 0; w < 3; w++)
                {
                    if (grid[w, h] == ".")
                    {
                        grid[w, h] = currentPlayer; // try current player move
                        int val = this.ProcessMinimax(grid, currentPlayer, false);
                        grid[w, h] = "."; // remove current player move to try different one
                        if (val > bestValue)
                        {
                            bestValue = val;
                            bestMove[0] = w;
                            bestMove[1] = h;
                        }
                    }
                }
            }

            return bestMove;
        } 
    }


    class Analyzer
    {
        int numberOfGrids = 0;
        string result = "";
        GridCreator gridCreator = new GridCreator();
        GridLogic gridLogic = new GridLogic();
        public Analyzer(int num, Reader reader)
        {
            this.numberOfGrids = num;
            this.StartAnalyzing();
            this.result = "";
        }

        void StartAnalyzing()
        {
            for (int i = 0; i < this.numberOfGrids; i++)
            {
                this.CreateGrid();
                this.AnalyzeCurrentGrid();
            }
            
            Console.WriteLine(this.result);
        }

        void CreateGrid()
        {
            string[] lines = new string[3];
            for (int i = 0; i < 3;)
            {
                string line = Console.ReadLine();
                if (line != null && line != "")
                {
                    lines[i] = line;
                    i++;
                } 
            }
            gridCreator.CreateNewGrid(lines);
        }

        void AnalyzeCurrentGrid()
        {
            int maxRounds = this.gridCreator.GetMaxRounds();
            for (int i = 0; i < maxRounds; i++)
            {
                int[] nextMove = gridLogic.FindBestMove(this.gridCreator.GetGrid(), this.gridCreator.WhosTurn());
                this.gridCreator.SetLetterToPosition(nextMove, this.gridCreator.WhosTurn());
                string whosTrio = gridLogic.WhosTrio(this.gridCreator.GetGrid());
                if (whosTrio != "N") {
                    if (whosTrio == "X")
                    {
                        result += "O";
                    } 
                    else
                    {
                        result += "X";
                    }
                    return;
                }
            }
            result += "N";
        }
    }

    class Game
    {
        string playerLetter;
        GridCreator gridCreator = new GridCreator();
        GridLogic gridLogic = new GridLogic();
        Printer printer;
        public Game(string playerLetter)
        {
            this.playerLetter = playerLetter;
            this.StartGame();
        }

        void ProcessInput(string line)
        {
            if (line.Length >= 2)
            {
                if (line[0] >= 49 && line[0] <= 51 && line[1] >= 49 && line[1] <= 51) // check if input is in range line[0-1] => 1-3
                {
                    int[] pos = { line[0]-'0'-1, line[1]-'0'-1 };
                    if (gridCreator.GetLetterOnPosition(pos) == ".")
                    {
                        gridCreator.SetLetterToPosition(pos, this.playerLetter);
                        return;
                    }
                }
            }
            Console.WriteLine("ERROR: BAD INPUT!");
        }
        void StartGame()
        {
            string loser = "N";
            gridCreator.CreateNewGrid(new string[0]);
            printer = new ConsolePrinter(gridCreator.GetGrid());
            printer.PrintGameHelper();
            for (int i = 0; i < 9; i++) // process all moves 
            {
                string whosTurn = this.gridCreator.WhosTurn();
                printer.PrintGrid();
                if (whosTurn == this.playerLetter)
                {
                    printer.PrintWhosTurn(whosTurn, false);
                    string input = Console.ReadLine();
                    this.ProcessInput(input);
                } 
                else
                {
                    printer.PrintWhosTurn(whosTurn, true);
                    int[] nextMove = gridLogic.FindBestMove(this.gridCreator.GetGrid(), this.gridCreator.WhosTurn());
                    this.gridCreator.SetLetterToPosition(nextMove, this.gridCreator.WhosTurn());
                    
                }

                string whosTrio = gridLogic.WhosTrio(this.gridCreator.GetGrid());
                if (whosTrio != "N")
                {
                    loser = whosTrio;
                    break;
                }
            }
            this.ProcessEnd(loser);
        }

        void ProcessEnd(string loser)
        {
            this.printer.PrintGrid();
            if (loser == "N")
            {
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("| IT IS TIE! YOU HAVE TO TRY HARDER NEXT TIME!|");
                Console.WriteLine("+---------------------------------------------+");
            } else if (loser == this.playerLetter) {
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("| YOU LOST! YOU HAVE TO TRY HARDER NEXT TIME! |");
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("P.S. hahaha loooser");
                Console.WriteLine("Your Bob xxx");
            } else
            {
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("| YOU WIN! GOOJ JOB U R LEGEND, YOU BEAT BOB! |");
                Console.WriteLine("+---------------------------------------------+");
            }
        }
    }

    class Printer
    {
        string[,] grid;
        public Printer(string[,] grid)
        {
            this.grid = grid;
        }

        public string[,] GetGrid()
        {
            return this.grid;
        }

        public virtual void PrintGameHelper()
        {

        }

        public virtual void PrintGrid()
        {

        }

        public virtual void PrintWhosTurn(string letter, bool isBobs)
        {

        }
    }

    class ConsolePrinter : Printer
    {
        public ConsolePrinter(string[,] grid) : base (grid)
        {
            
        }

        public override void PrintGrid()
        {
            Console.WriteLine();
            for (int h = 0; h < 3; h++)
            {
                for (int w = 0; w < 3; w++)
                {
                    Console.Write(this.GetGrid()[w, h]);
                }
                Console.Write("\n");
            }
            Console.WriteLine();
        }

        public override void PrintGameHelper()
        {
            Console.WriteLine("+---------------------------------------------+");
            Console.WriteLine("|       Welcome to the Non-Tic-Tac-Toe        |");
            Console.WriteLine("+---------------------------------------------+");
            Console.WriteLine("| My name is Bob nad I will play against you  |");
            Console.WriteLine("| How to play:                                |");
            Console.WriteLine("| As soon as it's your turn you should write  |");
            Console.WriteLine("| idx of position you want to set your letter |");
            Console.WriteLine("| Idxes:   11  21  31                         |");
            Console.WriteLine("|          12  22  32                         |");
            Console.WriteLine("|          13  23  33                         |");
            Console.WriteLine("| The winner is the person who force other to |");
            Console.WriteLine("| have 3 it's letters next to each other:     |");
            Console.WriteLine("| horizontaly, verticaly as well as diagonaly |");
            Console.WriteLine("| ENJOY THE GAME!                             |");
            Console.WriteLine("+---------------------------------------------+");
        }

        public override void PrintWhosTurn(string letter, bool isBobs)
        {
            Console.Write("It's ");
            if (isBobs)
            {
                Console.Write("Bob's ");
            }
            else
            {
                Console.Write("your ");
            }
            Console.Write("turn ");
            Console.Write("(");
            Console.Write(letter);
            Console.Write("):");
            if (isBobs) Console.Write(" *thinking* ");
            Console.Write("\n");
        }
    }


    class Program
    {
        private static ConsoleReader reader;
        static public void ProcessControlLine(string line)
        {
            bool isNumber = Int32.TryParse(line, out int num);
            if (isNumber)
            {
                new Analyzer(num, reader);
            }
            else if (line.Length > 0 && (line[0] ==  'X' || line[0] == 'O')) // if control line contains X or O
            {
                new Game(line[0].ToString());
            }
        }
        static void Main(string[] args)
        {
            Program.reader = new ConsoleReader();
            ProcessControlLine(reader.ReadLine());
        }
    }
}
