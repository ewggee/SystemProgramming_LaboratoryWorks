using Microsoft.Extensions.Logging;

namespace WrongAmountPairsInSequence;

class Program
{
    const string FilePath = "1.txt";
    const string LogsFilePath = "logs/logs.txt";

    static void Main()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddFile(LogsFilePath);

        });
        var logger = loggerFactory.CreateLogger<Program>();

        var numbers = new List<int>();

        var minThreeDigitDivBy7 = int.MinValue;
        var minFourDigit = int.MaxValue;

        try
        {
            using var reader = new StreamReader(FilePath);
            logger.LogInformation($"Чтение из файла: {FilePath}.");

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var number = int.Parse(line);
                numbers.Add(number);

                var absNumber = Math.Abs(number);

                // Нахождение минимального трёхзначного числа, делящегося на 7
                if (absNumber >= 100 && absNumber <= 999
                    && number % 7 == 0
                    && number < minThreeDigitDivBy7)
                {
                    minThreeDigitDivBy7 = number;
                }

                // Нахождение минимального четырёхзначного числа
                if (absNumber >= 1000 && absNumber <= 9999
                    && number < minFourDigit)
                {
                    minFourDigit = number;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return;
        }

        if (minThreeDigitDivBy7 != int.MinValue)
            logger.LogInformation($"Найдено мин. трёхзначное число делящееся на 7: {minThreeDigitDivBy7}.");
        else
        {
            logger.LogError($"Мин. трёхзначное число делящееся на 7 не найдено (minThreeDigitDivBy7: {minThreeDigitDivBy7}).");
            return;
        }

        if (minFourDigit != int.MaxValue)
            logger.LogInformation($"Найдено мин. четырёхзначное число: {minFourDigit}.");
        else
        {
            logger.LogWarning($"Мин. четырёхзначное число не найдено (minFourDigit: {minFourDigit}).");
            return;
        }

        var count = 0;
        var maxSum = int.MaxValue;
        var lastDigitInMinFourDigit = Math.Abs(minFourDigit) % 10;

        for (int i = 1; i < numbers.Count; i++)
        {
            var first = numbers[i - 1];
            var second = numbers[i];

            // Хотя бы одно число меньше мин. трехзначного числа делящегося на 7.
            // Произведение пары чисел заканчивается на ту же цифру,
            // на которую заканчивается мин. четырехзначное число всей последовательности
            if ((first < minThreeDigitDivBy7 || second < minThreeDigitDivBy7)
                && Math.Abs((long)first * second) % 10 == lastDigitInMinFourDigit)
            {
                count++;

                maxSum = Math.Max(maxSum, first + second);
            }
        }

        if (count != 0)
            logger.LogInformation($"Найдено пар чисел удовлетворяющих условию: {count}.");
        else
            logger.LogWarning($"Не найдено пар чисел удовлетворяющих условию.");

        if (maxSum != int.MaxValue)
            logger.LogInformation($"Найдена максимальная сумма пары чисел, удовлетворяющих условию: {maxSum}.");
        else
            logger.LogWarning($"Не найдена максимальная сумма пары чисел, удовлетворяющих условию (maxSum: {maxSum}).");
    }
}