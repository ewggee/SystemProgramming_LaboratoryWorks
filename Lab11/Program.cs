namespace Lab11;

class Program
{
    public static void Main()
    {
        using StreamReader fileReader = new("rows.txt");

        // Кол-во занятых мест
        var N = int.Parse(fileReader.ReadLine()!);

        var rows = new Dictionary<int, List<int>>();
        for (int i = 0; i < N; i++)
        {
            string[] parts = fileReader.ReadLine()!.Split(' ');

            var rowNumber = int.Parse(parts[0]);
            var seatNumber = int.Parse(parts[1]);

            if (!rows.ContainsKey(rowNumber))
            {
                rows.Add(rowNumber, []);
            }
            rows[rowNumber].Add(seatNumber);
        }

        // Максимальный номер ряда
        var maxRow = 0;
        // Минимальный номер места
        var minSeat = 0;

        foreach (var row in rows)
        {
            var currentRowNumber = row.Key;
            var lastSeat = 0;

            foreach (var seat in row.Value)
            {
                if (lastSeat != 0 && seat == lastSeat + 3)
                {
                    var possibleMinSeat = lastSeat + 1;

                    if (currentRowNumber > maxRow || (currentRowNumber == maxRow && possibleMinSeat < minSeat))
                    {
                        maxRow = currentRowNumber;
                        minSeat = possibleMinSeat;
                    }
                }
                lastSeat = seat;
            }
        }

        Console.WriteLine($"Результат: {maxRow} {minSeat}");
    }
}
