using System.Globalization;

namespace RaceTrack;

class Program
{
    static void Main()
    {
        GetConsoleInput(
            message: "Введите количество участков трассы: ", 
            value: out int N);

        GetConsoleInput(
            message: "Введите время прохождения переездов: ", 
            value: out int t);

        var startTimeA = 0;
        var finishTimeB = t;
        for (int i = 0; i < N; i++)
        {
            Console.Write($"A{i}A{i + 1} B{i}B{i + 1}: ");
            string[] parts = Console.ReadLine()!.Split();
            var ai = int.Parse(parts[0]);
            var bi = int.Parse(parts[1]);

            var newTimeA = startTimeA + ai;

            var newTimeB = Math.Min(
                finishTimeB + bi,
                startTimeA + t + bi
            );

            startTimeA = newTimeA;
            finishTimeB = newTimeB;
        }

        finishTimeB = Math.Min(finishTimeB, startTimeA + t);

        Console.WriteLine(finishTimeB);
    }

    static void GetConsoleInput<T>(string message, out T value) where T : struct, IParsable<T>
    {
        while (true)
        {
            Console.Write(message);
            if (T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out value)) return;
        }
    }
}