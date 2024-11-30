namespace Lab16;

class Program
{
    static void Main()
    {
        Console.Write("Количество выпиленных клеток: ");
        var N = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Координаты:");
        var cells = new List<(int Y, int X)>();
        for (int i = 0; i < N; i++)
        {
            int[] coordinate = 
                Console.ReadLine()!
                .Split()
                .Select(int.Parse)
                .ToArray();

            cells.Add((coordinate[0], coordinate[1]));
        }

        var perimeter = cells.Count * 4;
        foreach (var (Y, X) in cells)
        {
            if (cells.Contains((Y - 1, X))) perimeter--; // Сверху
            if (cells.Contains((Y + 1, X))) perimeter--; // Снизу
            if (cells.Contains((Y, X - 1))) perimeter--; // Слева
            if (cells.Contains((Y, X + 1))) perimeter--; // Справа
        }

        Console.WriteLine($"Периметр: {perimeter}");
    }
}