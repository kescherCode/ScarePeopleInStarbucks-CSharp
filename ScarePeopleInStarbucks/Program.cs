using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScarePeopleInStarbucks
{
    class Program
    {
        /// <summary>
        /// Initializes some stuff for the console window.
        /// </summary>
        static void InitStuff()
        {
            Console.CursorVisible = false;
            Console.InputEncoding = Console.OutputEncoding = Encoding.ASCII;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.BufferHeight = Console.WindowHeight = Console.LargestWindowHeight / 2;
            Console.BufferWidth = Console.WindowWidth = Console.LargestWindowWidth / 2;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application</param>
        static void Main(string[] args)
        {
            InitStuff();
            List<int> columns = new List<int>();
            int currColumn, currRow;
            int delay = 5;
            Random random = new Random(1337);
            char[] charset = " 01IOio".ToCharArray();
            while (true)
            {
                currRow = 0;
                if (columns.Count >= Console.WindowWidth - 1)
                {
                    columns = new List<int>();
                    Console.Clear();
                }

                do currColumn = random.Next(0, Console.WindowWidth - 1);
                while (columns.Contains(currColumn));
                Console.SetCursorPosition(currColumn, currRow);
                columns.Add(currColumn);

                while (Console.CursorTop < Console.WindowHeight - 1)
                {
                    Thread.Sleep(delay);

                    Console.Write(charset[random.Next(0, charset.Length)]);
                    try { Console.SetCursorPosition(currColumn, currRow); }
                    catch (ArgumentOutOfRangeException) { break; } // End the loop early if the cursor was set outside (common cause: resized window)
                    currRow++;

                    if (Console.KeyAvailable)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.Escape:
                                Environment.Exit(0);
                                break;
                            case ConsoleKey.OemPlus:
                            case ConsoleKey.Add:
                                delay += (delay < 100) ? 5 : 0;
                                break;
                            case ConsoleKey.OemMinus:
                            case ConsoleKey.Subtract:
                                delay -= (delay > 0) ? 5 : 0;
                                break;
                            case ConsoleKey.R:
                                delay = 5;
                                break;
                        }
                    }
                }
            }
        }
    }
}
