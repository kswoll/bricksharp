namespace Bricksharp.Firmware.Display
{
    public struct Rectangle
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }

        public Rectangle(Point p1, Point p2) : this()
        {
            P1 = p1;
            P2 = p2;
        }

        public static Rectangle operator+(Rectangle r, Point p)
        {
            return new Rectangle(r.P1 + p, r.P2 + p);
        }

        public static Rectangle operator-(Rectangle r, Point p)
        {
            return new Rectangle(r.P1 - p, r.P2 - p);
        }
    }
}