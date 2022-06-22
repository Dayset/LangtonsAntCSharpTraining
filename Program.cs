using System;
using static System.Console;
using System.Text;
using System.Threading;

namespace LangtonsAnt
{
    public class Program
    {
        public static int antX { get; private set; }
        public static int antY { get; private set; }

        private static void Main(string[] args)
        {
            int fieldWidth = 60;
            int fieldHeight = 60;
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
            }

            antX = random.Next(fieldWidth);
            antY = random.Next(fieldHeight);

            //if (map[antX, antY] == Cell.Empty)
            map[antX, antY] = Cell.Black;

            DrawAnt();

            DrawWhiteBG();

            DrawAnt();

            int direction = random.Next(3);
            if (direction == 0) antY--;
            if (direction == 1) antY++;
            if (direction == 2) antX--;
            if (direction == 3) antX++;

            ReadKey();
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
            Write("+");
            Thread.Sleep(1000);
        }

        public enum Cell
        {
            Empty,
            Black,
            White
        }
    }
}