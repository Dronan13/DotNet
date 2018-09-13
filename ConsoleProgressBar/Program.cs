using System;
using System.Threading;

namespace ConsoleProgressBar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is progress:");

            int total = 100;

            for(int i=1; i<=total; i++)
            {
                drawTextProgressBar(i, total);
                Thread.Sleep(100);
            }
            
        }

        private static void drawTextProgressBar(int progress, int total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;
            
            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.CursorLeft = position++;
            Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorLeft = position++;
            Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString()+"    "); //blanks at the end remove any excess
        }
    }
}
