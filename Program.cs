using System;
using static System.Console;
using System.Text;
using System.Threading;

namespace LangtonsAnt
{
    public class Program
    {
        private static readonly int sleep = 2;

        public static int antX { get; private set; }
        public static int antY { get; private set; }
        public static int direction { get; private set; }

        private static void Main(string[] args)
        {
            int fieldWidth = 100;
            int fieldHeight = 100;
            CursorVisible = false;
            WindowHeight = fieldHeight;
            WindowWidth = fieldWidth;
            BufferWidth = fieldWidth;
            BufferHeight = fieldHeight;

            Random random = new Random();
            Cell[,] map = new Cell[fieldWidth, fieldHeight];

            SetCursorPosition(0, 0);
            var grayBG = new StringBuilder();
            for (int i = 0; i < fieldHeight; i++)
            {
                BackgroundColor = ConsoleColor.Gray;
                grayBG.Append(' ', fieldWidth);
                WriteLine(grayBG);
                for (int j = 0; j < fieldWidth; j++)
                {
                    map[i, j] = Cell.White;
                }
            }

            //Don't spawn ANT near the border 33% gap
            antX = random.Next(fieldWidth / 3, (fieldWidth - (fieldWidth / 3)));
            antY = random.Next(fieldHeight / 3, (fieldHeight - (fieldHeight / 3)));

            direction = random.Next(3);
            DrawAnt();
            for (int i = 0; i < 50000; i++)
            {
                if (map[antX, antY] == Cell.Black)
                {
                    direction = (direction + 1) % 4;
                    map[antX, antY] = Cell.White;
                    DrawWhiteBG();
                    MoveAnt();
                    DrawAnt();
                }
                else
                {
                    direction = (direction + 3) % 4;
                    map[antX, antY] = Cell.Black;
                    DrawBlackBG();
                    MoveAnt();
                    DrawAnt();
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
            SetCursorPosition(antX, antY);
            BackgroundColor = ConsoleColor.Black;
            Write(" ");
        }

        public static void DrawWhiteBG()
        {
            SetCursorPosition(antX, antY);
            BackgroundColor = ConsoleColor.White;
            Write(" ");
        }

        public static void DrawAnt()
        {
            SetCursorPosition(antX, antY);
            ForegroundColor = ConsoleColor.Red;
            //Task.Run(() => Beep(1000, 50));
            Write("+");
            Thread.Sleep(sleep);
        }

        public enum Cell
        {
            Black,
            White
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}