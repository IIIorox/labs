namespace lab6
{
    struct Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Point other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }
    }

    struct RgbColor : IEquatable<RgbColor>
    {
        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }

        public RgbColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public byte ToGrayscale()
        {
            return (byte)((Red * 0.3) + (Green * 0.59) + (Blue * 0.11));
        }

        public bool Equals(RgbColor other)
        {
            return Red == other.Red && Green == other.Green && Blue == other.Blue;
        }

        public override bool Equals(object? obj)
        {
            return obj is RgbColor other && Equals(other);
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Red, Green, Blue);
        }

        public override string ToString()
        {
            return $"RGB({Red}, {Green}, {Blue})";
        }
    }

    public class Bootstrap
    {
        public static void Main(string[] args)
        {
            Point p1 = new Point(3, 4);
            Point p2 = new Point(0, 0);
            Console.WriteLine(p1);
            Console.WriteLine($"Distance to {p2}: {p1.DistanceTo(p2)}");

            Console.WriteLine("#################");

            RgbColor color1 = new RgbColor(100, 150, 200);
            RgbColor color2 = new RgbColor(100, 150, 200);
            RgbColor color3 = new RgbColor(0, 0, 0);

            Console.WriteLine(color1);
            Console.WriteLine($"Grayscale: {color1.ToGrayscale()}");
            Console.WriteLine($"Equal? {color1.Equals(color2)}");
            Console.WriteLine($"Equal? {color1.Equals(color3)}");
        }
    }
}