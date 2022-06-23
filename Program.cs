using System;
using static System.Console;
using System.Text;

namespace LangtonsAnt
{
    public class Program
    {
        private static readonly int sleep = 0;

        public static int antX { get; private set; }
        public static int antY { get; private set; }
        public static int direction { get; private set; }

        private static void Main(string[] args)
        {
            ReadKey();
            int fieldWidth = 480;
            int fieldHeight = 168;
            CursorVisible = false;
            WindowHeight = fieldHeight;
            WindowWidth = fieldWidth;
            BufferWidth = fieldWidth;
            BufferHeight = fieldHeight;

            Random random = new Random();
            Cell[,] map = new Cell[fieldWidth, fieldHeight];

            SetCursorPosition(0, 0);
            var grayBG = new StringBuilder();
            BackgroundColor = ConsoleColor.Gray;
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (random.Next(100) < 1)
                    {
                        //BackgroundColor = ConsoleColor.Black;
                        //grayBG.Append('#');
                        map[j, i] = Cell.Black;
                        //BackgroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        //BackgroundColor = ConsoleColor.Gray;
                        //grayBG.Append(' ');
                        map[j, i] = Cell.White;
                    }
                    //WriteLine(grayBG);
                }
            }

            //Don't spawn ANT near the border 33% gap
            antX = random.Next(fieldWidth / 3, (fieldWidth - (fieldWidth / 3)));
            antY = random.Next(fieldHeight / 3, (fieldHeight - (fieldHeight / 3)));

            direction = random.Next(0, 4);
            DrawAnt(fieldWidth, fieldHeight);
            for (int i = 0; i < 5000000; i++)
            {
                if (map[antX, antY] == Cell.Black || map[antX, antY] == Cell.Empty)
                {
                    direction = (direction + 1) % 4;
                    map[antX, antY] = Cell.White;
                    DrawWhiteBG();
                    MoveAnt();
                    DrawAnt(fieldWidth, fieldHeight);
                }
                else
                {
                    direction = (direction + 3) % 4;
                    map[antX, antY] = Cell.Black;
                    DrawBlackBG();
                    MoveAnt();
                    DrawAnt(fieldWidth, fieldHeight);
                }
            }

            ReadKey();
        }

        public static void MoveAnt()
        {
            if (direction == 0) antY--;
            if (direction == 1) antX++;
            if (direction == 2) antY++;
            if (direction == 3) antX--;
        }

        public static void DrawBlackBG()
        {
            var randomBG = new Random();
            SetCursorPosition(antX, antY);
            if (randomBG.Next(7) == 1)
                BackgroundColor = ConsoleColor.Black;
            else if (randomBG.Next(7) == 2)
                BackgroundColor = ConsoleColor.Red;
            else if (randomBG.Next(7) == 3)
                BackgroundColor = ConsoleColor.Green;
            else if (randomBG.Next(7) == 4)
                BackgroundColor = ConsoleColor.Blue;
            else if (randomBG.Next(7) == 5)
                BackgroundColor = ConsoleColor.Yellow;
            else if (randomBG.Next(7) == 6)
                BackgroundColor = ConsoleColor.Magenta;
            Write(" ");
        }

        public static void DrawWhiteBG()
        {
            var randomBG = new Random();
            SetCursorPosition(antX, antY);
            if (randomBG.Next(7) == 1)
                BackgroundColor = ConsoleColor.White;
            else if (randomBG.Next(7) == 2)
                BackgroundColor = ConsoleColor.Red;
            else if (randomBG.Next(7) == 3)
                BackgroundColor = ConsoleColor.Green;
            else if (randomBG.Next(7) == 4)
                BackgroundColor = ConsoleColor.Blue;
            else if (randomBG.Next(7) == 5)
                BackgroundColor = ConsoleColor.Yellow;
            else if (randomBG.Next(7) == 6)
                BackgroundColor = ConsoleColor.Magenta;
            Write(" ");
        }

        public static void DrawAnt(int fieldWidth, int fieldHeight)
        {
            if (antX < 2)
                Program.antX = fieldWidth - 2;
            if (antX > fieldWidth - 2)
                Program.antX = 2;
            if (antY < 2)
                Program.antY = fieldHeight - 2;
            if (antY > fieldHeight - 2)
                Program.antY = 2;

            //SetCursorPosition(antX, antY);
            //ForegroundColor = ConsoleColor.Red;
            //Task.Run(() => Beep(1000, 50));
            //Write("+");
            //Thread.Sleep(sleep);
        }

        public enum Cell
        {
            Black,
            White,
            Empty
        }
    }
}