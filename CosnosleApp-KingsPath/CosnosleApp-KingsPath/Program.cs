using System;
using System.Collections;
using System.IO;

namespace ConsoleApp_KingsPath
{
    class Position
    {
        int x_;
        int y_;
        int n_;

        public Position(int x, int y, int n)
        {
            x_ = x;
            y_ = y;
            n_ = n;
        }

        public int getX()
        {
            return x_;
        }

        public int getY()
        {
            return y_;
        }

        public int getN()
        {
            return n_;
        }
    }

    public class MyQueue<T>
    {
        private class Node
        {
            public Node(Node next, T value)
            {
                Next = next;
                Value = value;
            }

            public Node Next { get; internal set; }
            public T Value { get; }
        }

        private Node m_Head;
        private Node m_Tail;
        private int n = 0;

        public void Enqueue(T item)
        {
            n++;
            Node node = new Node(null, item);

            if (m_Tail == null || m_Head == null)
            {
                m_Head = node;
                m_Tail = node;
            }
            else
            {
                m_Tail.Next = node;
                m_Tail = node;
            }
        }

        public bool TryDequeue(out T item)
        {
            if (m_Head == null)
            {
                item = default(T);
                return false;
            }

            item = m_Head.Value;
            m_Head = m_Head.Next;

            return true;
        }

        public int getN()
        {
            return n;
        }
    }

    class Program
    {
        static string[,] grid;
        static int[,] tGrid;
        static int[] start = new int[2];
        static int[] finish = new int[2];
        static int width;
        static int height;

        public static string ReadAnotherValue()
        {
            string result = "";
            for (bool isAnotherNumber = true; isAnotherNumber == true;)
            {
                int ascii = Console.Read();
                if (ascii == ' ' || ascii == 10 || ascii == 13)
                {
                    continue;
                }

                isAnotherNumber = false;     
                result = (char)ascii + "";                
            }

            return result;
        }

        static int findKingsPath()
        {
            MyQueue<Position> queue = new MyQueue<Position>();
            queue.Enqueue(new Position(start[0], start[1], 1));

            while (true)
            {

                Position pos;
                if (!queue.TryDequeue(out pos))
                {
                    break;
                }

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int x = pos.getX() + i;
                        int y = pos.getY() + j;
                        int n = pos.getN();
                        bool xCondition = (0 <= x) && (x < width);
                        bool yCondition = (0 <= y) && (y < height);
                        if (xCondition && yCondition)
                        {
                            if (tGrid[x, y] == -1 || tGrid[x, y] > n)
                            {
                                if (grid[x, y] == "." || grid[x, y] == "C")
                                {
                                    tGrid[x, y] = n;
                                    queue.Enqueue(new Position(x, y, n + 1));
                                }
                            }
                        }
                    }
                }
            }
            return tGrid[finish[0], finish[1]];
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("./sachovnice.txt");
            for (int l = 0; l < lines.Length; l++)
            {
                if (l == 0)
                {
                    Int32.TryParse(lines[l], out height);
                }
                else if (l == 1)
                {
                    Int32.TryParse(lines[l], out width);
                    grid = new string[width, height];
                    tGrid = new int[width, height];
                }
                else if (l <= height + 1)
                {
                    char[] arr = lines[l].ToCharArray();
                    for (int i = 0; i < width; i++)
                    {
                        string letter = (char)arr[i] + "";
                        grid[i, l - 2] = letter;
                        if (letter == "C")
                        {
                            finish[0] = i;
                            finish[1] = l - 2;
                        }
                        if (letter == "S")
                        {
                            start[0] = i;
                            start[1] = l - 2;
                        }
                        tGrid[i, l - 2] = -1;
                    }
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine(findKingsPath());
        }
    }
}


