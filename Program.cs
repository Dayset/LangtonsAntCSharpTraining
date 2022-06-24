using System;
using static System.Console;
using System.Text;
using System.Threading;

namespace LangtonsAnt
{
    public class Program
    {
        private static readonly int sleep = 0;  //With 0 ant is crazy!

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
            BackgroundColor = ConsoleColor.DarkGray;
            grayBG.Append(' ', fieldWidth);
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (random.Next(1000) < 0) //0 - disable 1 -enable
                    {
                        //This generates black cells
                        //With chance of 0.1% but not drawing on map
                        //And looks ugly due to WriteLine(GrayBG)
                        SetCursorPosition(j, i);
                        Write("#");
                        map[j, i] = Cell.Black;
                    }
                    else
                        map[j, i] = Cell.White;
                }
                WriteLine(grayBG);
            }

            //Don't spawn ANT near the border 33% gap
            antX = random.Next(fieldWidth / 3, (fieldWidth - (fieldWidth / 3)));
            antY = random.Next(fieldHeight / 3, (fieldHeight - (fieldHeight / 3)));

            direction = random.Next(0, 4);
            DrawAnt(fieldWidth, fieldHeight);

            //Little stupidity, i just want to have a count of steps.
            for (int i = 0; i < 50000000; i++)
            {
                Thread.Sleep(sleep);
                if (map[antX, antY] == Cell.Black)
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
            SetCursorPosition(antX, antY);
            BackgroundColor = ConsoleColor.Black;
            /* Colorizer
            var randomBG = new Random();
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
            */
            Write(" ");
        }

        public static void DrawWhiteBG()
        {
            SetCursorPosition(antX, antY);
            BackgroundColor = ConsoleColor.White;
            /* Colorizer
            var randomBG = new Random();
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
            */
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
        }

        public enum Cell
        {
            Black,
            White
        }
    }
}