namespace Lab3;

public class Program
{
    /// <summary>
    /// Максимальное количество выступающих пар.
    /// </summary>
    const int MAX_PAIRS = 16;

    public static void Main()
    {
        // Словарь для хранения количества голосов за каждую пару
        Dictionary<int, int> votes = [];

        for (int i = 1; i <= MAX_PAIRS; i++)
            votes.Add(i, 0);

        uint countSMS;

        #region Ввод количества сообщений
        while (true)
        {
            Console.Write("Введите количество SMS-сообщений: ");

            if (!uint.TryParse(Console.ReadLine(), out var n))
                continue;

            countSMS = n;
            break;
        }
        #endregion

        #region Случайное заполнение команд голосами
        var random = new Random();
        int pairNumber;
        for (uint i = 0; i < countSMS; i++)
        {
            pairNumber = random.Next(1, MAX_PAIRS + 1);

            if (votes.ContainsKey(pairNumber))
                votes[pairNumber]++;
            else
                votes.Add(pairNumber, 1);
        }
        #endregion

        // Сортировка по количеству голосов в порядке убывания
        var sortedVotes = votes.OrderByDescending(pair => pair.Value).ToDictionary();

        Console.WriteLine(
            "\nРезультаты голосования\n" +
            "№ пары\t| Голоса"
        );

        foreach (var pair in sortedVotes)
            Console.WriteLine($"{pair.Key}\t- {pair.Value}");
    }
}