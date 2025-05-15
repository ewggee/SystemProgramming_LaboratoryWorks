using Microsoft.Extensions.Logging;

namespace CoordinateQuarter;

class Program
{
    static void Main()
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddFile("logs/logs.txt"));
        var logger = loggerFactory.CreateLogger<Program>();

        Console.Write("Введите количество точек N: ");
        var N = int.Parse(Console.ReadLine()!);

        var quarters = new QuarterManager();

        Console.WriteLine($"Введите координаты {N} точек:");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"  Точка {i + 1}: ");
            string[] coordinates = Console.ReadLine()!.Split();
            var x = int.Parse(coordinates[0]);
            var y = int.Parse(coordinates[1]);

            if (x == 0 || y == 0)
            {
                logger.LogInformation($"Пропущена точка [{x}; {y}], т.к. она лежит на оси 0.");
                continue;
            }

            quarters.ProcessPoint(x, y);
        }

        var bestQuarter = quarters.GetBestQuarter();
        Console.WriteLine("\nРезультаты:");
        Console.WriteLine($"K = {bestQuarter.Number}");
        Console.WriteLine($"M = {bestQuarter.PointsCount}");
        Console.WriteLine($"A = [{bestQuarter.MinPoint.x}; {bestQuarter.MinPoint.y}]");
        Console.WriteLine($"R = {bestQuarter.MinR}");
    }
}