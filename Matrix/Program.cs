using System;

namespace matrix
{
    class Program
    {
        static int Counter;
        static Random rnd = new Random();

        static int Interval = 100;           // Normal Rain
        static int FullFlow = Interval + 30; // Fast Rain
        static int Blacking = FullFlow + 50; // Displaying the Test Alone

        static ConsoleColor NormalColor = ConsoleColor.DarkGreen;
        static ConsoleColor GlowColor = ConsoleColor.Green;
        static ConsoleColor FancyColor = ConsoleColor.White;

        static String TextInput = "W4k3 Up V0idB04rd Us3r";

        //Randomised Inputs
        static char Chars  
        {
            get
            {
                int t = rnd.Next(10);
                if (t <= 2)
                    return (char)('0' + rnd.Next(10));
                else if (t <= 4)
                    return (char)('a' + rnd.Next(27));
                else if (t <= 6)
                    return (char)('A' + rnd.Next(27));
                else
                    return (char)(rnd.Next(32, 255));
            }
        }
        static void Main()
        {
            Console.ForegroundColor = NormalColor;
            Console.WindowLeft = Console.WindowTop = 0;
            Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;

            int width, height;
            int[] y;
            Initialize(out width, out height, out y);//Setting the Starting Point

            while (true)
            {
                Counter = Counter + 1;
                UpdateAllColumns(width, height, y);
                if (Counter > (3 * Interval))
                    Counter = 0;
            }
        }
        private static void UpdateAllColumns(int width, int height, int[] y)
        {
            int x;
            if (Counter < Interval)
            {
                for (x = 0; x < width; ++x)
                {
                    //Randomly setting up the White Position
                    if (x % 10 == 1)
                        Console.ForegroundColor = FancyColor;
                    else
                        Console.ForegroundColor = GlowColor;
                    Console.SetCursorPosition(x, y[x]);
                    Console.Write(Chars);

                    if (x % 10 == 9)
                        Console.ForegroundColor = FancyColor;
                    else
                        Console.ForegroundColor = NormalColor;
                    int temp = y[x] - 2;
                    Console.SetCursorPosition(x, inScreenYPosition(temp, height));
                    Console.Write(Chars);

                    int temp1 = y[x] - 20;
                    Console.SetCursorPosition(x, inScreenYPosition(temp1, height));
                    Console.Write(' ');
                    y[x] = inScreenYPosition(y[x] + 1, height);
                }
            }
            else if (Counter > Interval && Counter < FullFlow)
            {
                for (x = 0; x < width; ++x)
                {
                    Console.SetCursorPosition(x, y[x]);
                    if (x % 10 == 9)
                        Console.ForegroundColor = FancyColor;
                    else
                        Console.ForegroundColor = NormalColor;

                    Console.Write(Chars);//Printing the Character Always at Fixed position

                    y[x] = inScreenYPosition(y[x] + 1, height);
                }
            }
            else if (Counter > FullFlow)
            {
                for (x = 0; x < width; ++x)
                {
                    Console.SetCursorPosition(x, y[x]);
                    Console.Write(' ');//Slowly blacking out the Screen
                    int temp1 = y[x] - 20;
                    Console.SetCursorPosition(x, inScreenYPosition(temp1, height));
                    Console.Write(' ');
                    if (Counter > FullFlow && Counter < Blacking)// Clearing the entire screen
                    {
                        if (x % 10 == 9)
                            Console.ForegroundColor = FancyColor;
                        else
                            Console.ForegroundColor = NormalColor;
                        int temp = y[x] - 2;
                        Console.SetCursorPosition(x, inScreenYPosition(temp, height));
                        Console.Write(Chars);//The text is always printed
                    }
                    Console.SetCursorPosition(width / 2, height / 2);
                    Console.Write(TextInput);
                    y[x] = inScreenYPosition(y[x] + 1, height);
                }
            }
        }
        public static int inScreenYPosition(int yPosition, int height)
        {
            if (yPosition < 0)//When there is negative value
                return yPosition + height;
            else if (yPosition < height)//Normal
                return yPosition;
            else// When y goes out of screen when autoincremented by 1
                return 0;

        }
        private static void Initialize(out int width, out int height, out int[] y)
        {
            height = Console.WindowHeight;
            width = Console.WindowWidth - 1;
            y = new int[width];
            Console.Clear();

            for (int x = 0; x < width; ++x)//Setting the cursor at random at program startup
            {
                y[x] = rnd.Next(height);
            }
        }
    }
}