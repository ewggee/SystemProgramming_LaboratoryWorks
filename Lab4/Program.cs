namespace Lab4;

class Program
{
    static void Main()
    {
        #region Ввод чисел
        int first;
        while (true)
        {
            Console.Write("- Число 1: ");

            if (!int.TryParse(Console.ReadLine(), out var parsedNumber))
                continue;

            first = parsedNumber;
            break;
        }

        int second;
        while (true)
        {
            Console.Write("- Число 2: ");

            if (!int.TryParse(Console.ReadLine(), out var parsedNumber))
                continue;

            second = parsedNumber;
            break;
        }
        #endregion

        // Обычное умножение
        int usual = first * second;

        #region Установка флагов, перевод отрицательных чисел в положительные
        bool isFirstNeg = false;
        bool isSecondNeg = false;
        if (first < 0)
        {
            isFirstNeg = true;
            first = Math.Abs(first);
        }
        if (second < 0)
        {
            isSecondNeg = true;
            second = Math.Abs(second);
        }
        #endregion

        int recursive = RecursiveMultiplication(first, second);
        int binary = BinaryMultiplication(first, second);

        #region Инверсия ответов, если установлены флаги отрицательных чисел
        if (isFirstNeg)
        {
            recursive = 0 - recursive;
            binary = 0 - binary;
        }
        if (isSecondNeg)
        {
            recursive = 0 - recursive;
            binary = 0 - binary;
        }
        #endregion

        Console.WriteLine(
            $"Рекурсией:\t{recursive}\n" +
            $"Двоичное:\t{binary}\n" +
            $"Обычное:\t{usual}"    
        );
    }

    /// <summary>
    /// Рекурсивное умножение.
    /// </summary>
    static int RecursiveMultiplication(int a, int b)
    {
        if (b == 0) return 0;

        return a + RecursiveMultiplication(a, --b);
    }

    /// <summary>
    /// Двоичное умножение.
    /// </summary>
    static int BinaryMultiplication(int a, int b)
    {
        var result = 0;
        while (b > 0)
        {
            if ((b & 1) == 1) // Проверка нечетности b
                result += a;

            a <<= 1; // Удвоение a
            b >>= 1; // Деление b на 2
        }
        return result;
    }
}