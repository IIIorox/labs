namespace lab4
{
    static class ShapeCalculator
    {
        public static double CalculateArea(double radius) => Math.PI * radius * radius;

        public static double CalculateArea(double side1, double side2) => side1 * side2;
        /*
         * Так как для площади прямоугольника требуется 2 аргумента, как и для треугольника, то назвал метод по по-другому.
         * А просто поменять тип данных аргументов это тупо
         */
        public static double CalculateTriangleArea(double _base, double height) => 0.5 * height * _base;

        public static void PrintInfo(double radius) => Console.WriteLine($"Circle with radius {radius}");

        public static void PrintInfo(double side1, double side2) => Console.WriteLine($"Rectangle {side1} x {side2}");
        // Аналогично
        public static void PrintTriangleInfo(double _base, double height) => Console.WriteLine($"Triangle (base {_base}, height {height})");
    }
    public class Bootstrap
    {
        public static void Main(string[] args)
        {
            ShapeCalculator.PrintInfo(20);
            ShapeCalculator.PrintInfo(10, 15);
            ShapeCalculator.PrintTriangleInfo(10, 4);
        }
    }
}