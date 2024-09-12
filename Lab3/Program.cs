public class Program
{
    /// <summary>
    /// Максимальное количество выступающих пар.
    /// </summary>
    const int MAX_PAIRS = 16;

    public static void Main()
    {
        // Словарь для хранения количества голосов за каждую пару
        Dictionary<int, int> votes = new();

        for (int i = 1; i <= MAX_PAIRS; i++)
            votes.Add(i, 0);

        int countSMS;

        #region Ввод количества сообщений
        while (true)
        {
            Console.Write("Введите количество SMS-сообщений: ");

            if (!int.TryParse(Console.ReadLine(), out var n))
                continue;

            countSMS = n;
            break;
        }
        #endregion

        #region Случайное заполнение команд голосами
        for (int i = 0; i < countSMS; i++)
        {
            var random = new Random();

            var pairNumber = random.Next(1, MAX_PAIRS + 1);

            if (votes.ContainsKey(pairNumber)) 
                votes[pairNumber]++;
            else 
                votes.Add(pairNumber, 1);
        }
        #endregion

        // Сортировка по количеству голосов в порядке убывания
        var sortedVotes = votes.OrderByDescending(pair => pair.Value).ToDictionary();

        Console.WriteLine("\nРезультаты голосования");
        Console.WriteLine("№ пары\t| Голоса");
        foreach (var pair in sortedVotes)
        {
            Console.WriteLine($"{pair.Key}\t- {pair.Value}");
        }
    }
}