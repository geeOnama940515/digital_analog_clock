using System;
using System.Threading;

class DigitalAnalogClock
{
    static void Main()
    {
        Console.CursorVisible = false;
        Console.Title = "Digital and Analog Clock";

        while (true)
        {
            Console.Clear();
            DateTime currentTime = DateTime.Now;

            // Digital Clock
            DrawDigitalClock(currentTime);

            // Analog Clock
            DrawAnalogClock(currentTime);

            Thread.Sleep(1000);
            Console.SetCursorPosition(0, 0);
        }
    }

    static void DrawDigitalClock(DateTime time)
    {
        string digitalTime = time.ToString("HH:mm:ss");
        string digitalDate = time.ToString("dddd, MMMM dd, yyyy");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(20, 5);
        Console.WriteLine("DIGITAL CLOCK");
        Console.SetCursorPosition(20, 7);
        Console.WriteLine($"{digitalTime}");
        Console.SetCursorPosition(20, 9);
        Console.WriteLine($"{digitalDate}");
        Console.ResetColor();
    }

    static void DrawAnalogClock(DateTime time)
    {
        int centerX = 60;
        int centerY = 15;
        int radius = 10;

        // Draw clock circle
        Console.ForegroundColor = ConsoleColor.White;
        DrawCircle(centerX, centerY, radius);

        // Draw hour marks
        for (int i = 0; i < 12; i++)
        {
            double angle = i * 30 * Math.PI / 180;
            int x = (int)(centerX + (radius - 1) * Math.Sin(angle));
            int y = (int)(centerY - (radius - 1) * Math.Cos(angle));
            Console.SetCursorPosition(x, y);
            Console.Write("•");
        }

        // Hour hand
        double hourAngle = (time.Hour % 12 + time.Minute / 60.0) * 30 * Math.PI / 180;
        DrawHand(centerX, centerY, hourAngle, 4, ConsoleColor.Yellow,time.Hour);

        // Minute hand
        double minuteAngle = (time.Minute + time.Second / 60.0) * 6 * Math.PI / 180;
        DrawHand(centerX, centerY, minuteAngle, 7, ConsoleColor.Cyan,time.Minute);

        // Second hand
        double secondAngle = time.Second * 6 * Math.PI / 180;
        DrawHand(centerX, centerY, secondAngle, 8, ConsoleColor.Red,time.Second);
    }

    static void DrawCircle(int centerX, int centerY, int radius)
    {
        for (double angle = 0; angle < 360; angle += 5)
        {
            double radian = angle * Math.PI / 180;
            int x = (int)(centerX + radius * Math.Cos(radian));
            int y = (int)(centerY + radius * Math.Sin(radian));
            Console.SetCursorPosition(x, y);
            Console.Write(".");
        }
    }

    static void DrawHand(int centerX, int centerY, double angle, int length, ConsoleColor color,int time)
    {
        Console.ForegroundColor = color;
        int endX = (int)(centerX + length * Math.Sin(angle));
        int endY = (int)(centerY - length * Math.Cos(angle));

        // Bresenham's line algorithm
        DrawLine(centerX, centerY, endX, endY,time);
        Console.ResetColor();
    }

    static void DrawLine(int x0, int y0, int x1, int y1,int time)
    {
        int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
        int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
        int err = dx + dy, e2;

        while (true)
        {
            Console.SetCursorPosition(x0, y0);
            Console.Write(time.ToString());

            if (x0 == x1 && y0 == y1) break;
            e2 = 2 * err;
            if (e2 >= dy)
            {
                err += dy;
                x0 += sx;
            }
            if (e2 <= dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }
}