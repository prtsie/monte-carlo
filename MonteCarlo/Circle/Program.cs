using System;

namespace Circle
{
    internal static class Program
    {
        private struct Point(double x, double y)
        {
            public double X { get; set; } = x;

            public double Y { get; set; } = y;
        }

        private const int SleepDuration = 500;
        private const char LoadingChar = '.';
        private const int LoadingCharsCount = 5;
        private static readonly Random Random = new();

        private static void Main()
        {
            while (true)
            {
                var radius = 1.0;
                var lengthLimit = Math.Sqrt(3);
                Console.Write($"Радиус (по умолчанию {radius}) >>> ");
                if (double.TryParse(Console.ReadLine(), out var input) && input > 0)
                {
                    radius = input;
                }
                Console.Write($"Минимальная длина хорды (по умолчанию {Math.Round(lengthLimit, 5)}) >>> ");
                if (double.TryParse(Console.ReadLine(), out input))
                {
                    lengthLimit = input;
                }
                int iterationsCount;
                do
                {
                    Console.Write("Количество итераций >>> ");
                } while (!(int.TryParse(Console.ReadLine(), out iterationsCount)) && iterationsCount <= 0);
                Console.Clear();
                var iterations = Task.Run(DoIterations);
                var waitString = LoadingChar.ToString();
                do
                {
                    Console.Clear();
                    WriteConditions();
                    Console.WriteLine(waitString);
                    if (waitString.Length < LoadingCharsCount)
                    {
                        waitString += LoadingChar;
                    }
                    else
                    {
                        waitString = "";
                    }
                    Thread.Sleep(SleepDuration);
                } while (!iterations.IsCompleted);
                Console.Clear();
                WriteConditions();
                Console.WriteLine($"Вероятность: {(double)iterations.Result / iterationsCount}");
                Console.ReadKey();
                Console.Clear();

                void WriteConditions()
                {
                    Console.WriteLine($"Радиус: {radius}");
                    Console.WriteLine($"Предел длины: {lengthLimit}");
                    Console.WriteLine($"Количество итераций: {iterationsCount}");
                }

                int DoIterations()
                {
                    var count = 0;
                    for (var i = 0; i < iterationsCount; i++)
                    {
                        var first = GeneratePointOnCircle(radius);
                        var second = GeneratePointOnCircle(radius);
                        if (GetLength(first, second) > lengthLimit)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
        }

        private static Point GeneratePointOnCircle(double radius)
        {
            var randomAngle = Random.NextDouble() * Math.PI * 2;
            var (y, x) = Math.SinCos(randomAngle);
            return new Point(x * radius, y * radius);
        }

        private static double GetLength(Point first, Point second)
        {
            var deltaX = Math.Abs(first.X - second.X);
            var deltaY = Math.Abs(first.Y - second.Y);
            return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
        }
    }
}
