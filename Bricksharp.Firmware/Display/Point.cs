using System;

namespace Bricksharp.Firmware.Display
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point (int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public static Point operator+(Point lhs, Point rhs)
        {
            return new Point(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Point operator-(Point lhs, Point rhs)
        {
            return new Point(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Point operator*(Point p, int factor)
        {
            return new Point(p.X * factor, p.Y * factor);
        }

        public static Point operator*(Point p, double factor)
        {
            return new Point((int)Math.Round(p.X * factor), (int)Math.Round (p.Y * factor));
        }
    }
}