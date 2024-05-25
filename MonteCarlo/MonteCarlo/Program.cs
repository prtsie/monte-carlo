namespace MonteCarlo
{
    internal class Program
    {
        static void Main()
        {
            var random = new Random();
            while (true)
            {
                int iterationsCount;
                do
                {
                    Console.Write("Введите количество итераций >>> ");
                } while (!int.TryParse(Console.ReadLine(), out iterationsCount));
                var hits = 0;
                for (int i = 0; i < iterationsCount; i++)
                {
                    var x = random.NextDouble() + 1.0; //from 1 to 2
                    var y = random.NextDouble() / 2 + 0.5; //from 0.5 to 1
                    if (0.5 - 0.25 * (x - 2) <= y && y <= 1 / x)
                    {
                        hits++;
                    }
                }
                var rectArea = 0.5 * 1;
                var figureArea = (double)hits / iterationsCount * rectArea;
                Console.WriteLine($"Примерная площадь фигуры: {figureArea}");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
