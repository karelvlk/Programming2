using System;

namespace ConsoleApp_NonTicTacToe
{
    // Abstract class Reader is created like interface because in the future
    // there can be added more options to read from (from file etc.) without
    // modifying the whole script
    abstract class Reader
    {
        public abstract string ReadLine();

    }

    class ConsoleReader : Reader
    {
        public override string ReadLine()
        {
            return Console.ReadLine();
        }
    }

    class Grid
    {
        int Xs = 0;
        int Os = 0;
        string[,] grid = new string[3,3];

        public void FillFromLines(string[] lines)
        {
            Xs = 0;
            Os = 0;

            if (lines.Length >= 3)
            {
                for (int h = 0; h < 3; h++)
                {
                    if (lines[h].Length >=3)
                    {
                        for (int w = 0; w < 3; w++)
                        {
                            string letter = lines[h][w].ToString();
                            if (letter == "x") Xs++;
                            if (letter == "o") Os++;
                            grid[w, h] = letter;

                        }
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
    

        public int GetXs()
        {
            return this.Xs;
        }

        public int GetOs()
        {
            return this.Os;
        }

        public void SetLetterToPosition(int[] position, string letter, bool temp)
        {
            if (!temp)
            {
                if (letter == "x") Xs++;
                if (letter == "o") Os++;
            }
            this.grid[position[0], position[1]] = letter;
        }

        public string GetLetterOnPosition(int[] position)
        {
            return this.grid[position[0], position[1]];
        }
    }

    class GridLogic
    {
        Grid grid;

        public void SetLetterToPosition(int[] pos, string letter, bool temp)
        {
            this.grid.SetLetterToPosition(pos, letter, temp);
        }

        public int GetMaximumRoundLeft()
        {
            return 9 - grid.GetXs() - grid.GetOs();
        }

        public string GetWhosTurn()
        {
            if (this.grid.GetXs() == grid.GetOs())
            {
                return "x"; // if Xs == Os it means that first player is on move => X
            }
            else
            {
                return "o";
            }
        }
        
        public void SetGrid(Grid grid)
        {
            this.grid = grid;
        }

        string GetPosInfo(int x, int y)
        {
            if (0 <= x && x <= 2 && 0 <= x && x <= 2)
            {
                int[] pos = { x, y };
                return grid.GetLetterOnPosition(pos);
            }
            else
            {
                return "?";
            }
        }
        public string GetWhoWins() //returns "N" if there is no trio or "X"/"O" if some player has trio
        {
            //horizontal trios
            for (int width = 0; width < 3; width++)
            {
                if (GetPosInfo(width, 0) == GetPosInfo(width, 1) && GetPosInfo(width, 1) == GetPosInfo(width, 2))
                {
                    string trioLetter = GetPosInfo(width, 0); // letter that is in trio (three of them next to each other)
                    if (trioLetter != "." && trioLetter != "?")
                    {
                        if (trioLetter == "x")
                        {
                            return "o"; // wins who force other to create trio
                        }
                        else
                        {
                            return "x";
                        }
                    }
                }
            }

            //vertical trios
            for (int height = 0; height < 3; height++)
            {
                if (GetPosInfo(0, height) == GetPosInfo(1, height) && GetPosInfo(1, height) == GetPosInfo(2, height))
                {
                    string trioLetter = GetPosInfo(0, height); // letter that is in trio (three of them next to each other)
                    if (trioLetter != "." && trioLetter != "?")
                    {
                        if (trioLetter == "x")
                        {
                            return "o"; // wins who force other to create trio
                        }
                        else
                        {
                            return "x";
                        }
                    }
                }
            }

            //diagonal trios

            if (GetPosInfo(1, 1) == GetPosInfo(2, 2) && GetPosInfo(2, 2) == GetPosInfo(0, 0))
            {
                string trioLetter = GetPosInfo(0, 0); // letter that is in trio (three of them next to each other)
                if (trioLetter != "." && trioLetter != "?")
                {
                    if (trioLetter == "x")
                    {
                        return "o"; // wins who force other to create trio
                    }
                    else
                    {
                        return "x";
                    }
                }
            }

            // second diagonal
            if (GetPosInfo(0, 2) == GetPosInfo(1, 1) && GetPosInfo(1, 1) == GetPosInfo(2, 0))
            {
                string trioLetter = GetPosInfo(1, 1); // letter that is in trio (three of them next to each other)
                if (trioLetter != "." && trioLetter != "?")
                {
                    if (trioLetter == "x")
                    {
                        return "o"; // wins who force other to create trio
                    }
                    else
                    {
                        return "x";
                    }
                }
            }

            return "N"; // nobody
        }

        public int GetChanceToBeWinner(string player)
        {
            string winner = this.GetWhoWins();
            if (winner == "N") // nobody is winner
            {
                return 0;
            }
            else if (winner == player)
            {
                return 100;
            }
            else
            {
                return -100;
            }
        }

        public bool IsFreeSpace()
        {
            for (int h = 0; h < 3; h++)
            {
                for (int w = 0; w < 3; w++)
                {
                    if (GetPosInfo(w, h) == ".") return true;
                }
            }

            return false;
        }

        public int ProcessMinimax(string player,  bool isMax)
        {
            int value = this.GetChanceToBeWinner(player);
            if (value != 0)
            {
               return value;
            }
            int best = 0;
            if (this.IsFreeSpace()) 
            {
                /*
                int best = 100; // initialized that it is always overwritten
                if (isMax) best = -100; // initialized that it is always overwritten
                for (int h = 0; h < 3; h++)
                {
                    for (int w = 0; w < 3; w++)
                    {
                        if (GetPosInfo(w, h) == ".")
                        {
                            int[] position = { w, h };
                            this.SetLetterToPosition(position, player, true); // make current player move
                            string nextPlayer = "x";
                            if (player == "x") nextPlayer = "o";
                            if (isMax)
                            {
                                best = Math.Max(best, this.ProcessMinimax(nextPlayer, !isMax, depth + 1));
                            }
                            else
                            {
                                best = Math.Min(best, this.ProcessMinimax(nextPlayer, !isMax, depth + 1));
                            }
                            this.SetLetterToPosition(position, ".", true); // remove current player move to try another one
                        }
                    }
                }
                return best;
                */
                if (isMax)
                {
                    best = -100;
                    for (int h = 0; h < 3; h++)
                    {
                        for (int w = 0; w < 3; w++)
                        {
                            if (GetPosInfo(w, h) == ".")
                            {
                                int[] position = { w, h };
                                this.SetLetterToPosition(position, "x", true); // make current player move
                                best = Math.Max(best, this.ProcessMinimax("o", !isMax));
                                this.SetLetterToPosition(position, ".", true); // remove current player move to try another one
                            }
                        }
                    }
                } else
                {
                    best = 100;
                    for (int h = 0; h < 3; h++)
                    {
                        for (int w = 0; w < 3; w++)
                        {
                            if (GetPosInfo(w, h) == ".")
                            {
                                int[] position = { w, h };
                                this.SetLetterToPosition(position, "o", true); // make current player move
                                best = Math.Min(best, this.ProcessMinimax("x", !isMax));
                                this.SetLetterToPosition(position, ".", true); // remove current player move to try another one
                            }
                        }
                    }
                }
            }
            return best;
        }

        public int[] FindBestMove(string currentPlayer)
        {
            int bestValue = -int.MaxValue;
            int[] bestMove = { -1, -1 }; //no move 

            for (int h = 0; h < 3; h++)
            {
                for (int w = 0; w < 3; w++)
                {
                    if (GetPosInfo(w, h) == ".")
                    {
                        int[] position = { w, h };
                        this.SetLetterToPosition(position, currentPlayer, true); // try current player move
                        int val = this.ProcessMinimax(currentPlayer, false);
                        this.SetLetterToPosition(position, ".", true); // remove current player move to try different one
                        if (val >= bestValue)
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
        Reader reader;
        Grid grid = new Grid();
        GridLogic gridLogic = new GridLogic();

        public Analyzer(int num, Reader reader)
        {
            this.numberOfGrids = num;
            this.result = "";
            this.reader = reader;
        }

        public void StartAnalyzing()
        {
            for (int i = 0; i < this.numberOfGrids; i++)
            {
                this.CreateGrid();
                this.gridLogic.SetGrid(grid);
                this.AnalyzeCurrentGrid();
            }
            
            Console.WriteLine(this.result.ToUpper());
        }

        void CreateGrid()
        {
            string[] lines = new string[3];
            for (int i = 0; i < 3;)
            {
                string line = this.reader.ReadLine();
                if (line != null && line != "")
                {
                    lines[i] = line;
                    i++;
                } 
            }
            grid.FillFromLines(lines);
        }

        void AnalyzeCurrentGrid()
        {
            int maxRounds = this.gridLogic.GetMaximumRoundLeft();
            for (int i = 0; i < maxRounds; i++)
            {
                string playerOnTurn = this.gridLogic.GetWhosTurn();
                int[] nextMove = gridLogic.FindBestMove(playerOnTurn);
                this.gridLogic.SetLetterToPosition(nextMove, playerOnTurn, false);
                string winner = this.gridLogic.GetWhoWins();
                if (winner != "N") {
                    result += winner;
                    return;
                }
            }
            result += "N";
        }
    }

    class Game
    {
        string playerLetter;
        Reader reader;
        Grid grid = new Grid();
        GridLogic gridLogic = new GridLogic();
        Printer printer;

        public Game(string playerLetter, Reader reader)
        {
            this.playerLetter = playerLetter;
            this.reader = reader;
        }

        void ProcessInput(string line)
        {
            if (line.Length >= 2)
            {
                int x = line[0] - '0' - 1;
                int y = line[1] - '0' - 1;
                if (0 <= x && x <= 2 && 0 <= y && y <= 2) // checks if input x and y is in range 0-2
                {
                    int[] pos = { x, y };
                    if (grid.GetLetterOnPosition(pos) == ".")
                    {
                        gridLogic.SetLetterToPosition(pos, this.playerLetter, false);
                        return;
                    }
                }
            }
            Console.WriteLine("ERROR: BAD INPUT!");
        }

        public void StartGame()
        {
            string winner = "N";
            grid.FillFromLines(new string[0]);
            gridLogic.SetGrid(this.grid);
            printer = new ConsolePrinter();
            printer.SetGrid(this.grid);
            printer.PrintGameHelper();

            for (int i = 0; i < 9; i++) // process all moves 
            {
                string whosTurn = this.gridLogic.GetWhosTurn();
                printer.PrintGrid();
                if (whosTurn == this.playerLetter)
                {
                    printer.PrintWhosTurn(whosTurn, false);
                    string input = this.reader.ReadLine();
                    this.ProcessInput(input);
                } 
                else
                {
                    printer.PrintWhosTurn(whosTurn, true);
                    int[] nextMove = gridLogic.FindBestMove(whosTurn);
                    this.gridLogic.SetLetterToPosition(nextMove, whosTurn, false);
                    
                }

                winner = gridLogic.GetWhoWins();
                if (winner != "N")
                {
                    break;
                }
            }
            this.ProcessEnd(winner);
        }

        void ProcessEnd(string winner)
        {
            this.printer.PrintGrid();
            if (winner == "N")
            {
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("| IT IS TIE! YOU HAVE TO TRY HARDER NEXT TIME!|");
                Console.WriteLine("+---------------------------------------------+");
            } 
            else if (winner == this.playerLetter) 
            {
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("| YOU WIN! GOOJ JOB U R LEGEND, YOU BEAT BOB! |");
                Console.WriteLine("+---------------------------------------------+");
            }
            else
            {
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("| YOU LOST! YOU HAVE TO TRY HARDER NEXT TIME! |");
                Console.WriteLine("+---------------------------------------------+");
                Console.WriteLine("P.S. hahaha loooser");
                Console.WriteLine("Your Bob xxx");
            }
        }
    }

    // Abstract class Printer is created like interface because in the future
    // there can be added more options to print to (file etc.) without modifying
    // the whole script
    abstract class Printer
    {
        Grid grid;

        public void SetGrid(Grid grid)
        {
            this.grid = grid;
        }

        public string GetLetterOnPosition(int x, int y)
        {
            int[] pos = { x, y };
            return this.grid.GetLetterOnPosition(pos);
        }

        public abstract void PrintGameHelper();

        public abstract void PrintGrid();

        public abstract void PrintWhosTurn(string letter, bool isBobs);

    }

    class ConsolePrinter : Printer
    {
        public override void PrintGrid()
        {
            Console.WriteLine();
            for (int h = 0; h < 3; h++)
            {
                for (int w = 0; w < 3; w++)
                {
                    Console.Write(this.GetLetterOnPosition(w, h));
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
            Console.WriteLine("| My name is Bob and I will play against you  |");
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
        private static Reader reader;
        static public void ProcessControlLine(string line)
        {
            bool isNumber = Int32.TryParse(line, out int num);
            if (isNumber)
            {
                Analyzer analyzer = new Analyzer(num, reader);
                analyzer.StartAnalyzing();
            }
            else if (line.Length > 0 && (line[0] ==  'x' || line[0] == 'o')) // if control line contains X or O
            {
                Game game = new Game(line[0].ToString(), reader); //param: empty [] of line 
                game.StartGame();
            }
        }
        static void Main(string[] args)
        {
            Program.reader = new ConsoleReader();
            ProcessControlLine(reader.ReadLine());
        }
    }
}
