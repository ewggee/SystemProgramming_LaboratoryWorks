namespace Lab12;

class Program
{
    const string FILE = "file.txt";

    static void Main()
    {
        var maxSum = 0;
        // Минимальная разница в парах
        var minPairDifference = int.MaxValue;

        using var sr = new StreamReader(FILE);
        
        string line = sr.ReadLine()!;
        while(line != null)
        {
            var pair = line.Split(' ');
            var firstNumber = int.Parse(pair.First());
            var secondNumber = int.Parse(pair.Last());

            // Добавление наибольшего числа из пары к общей максимальной сумме
            maxSum += Math.Max(firstNumber, secondNumber);

            // Нахождение минимальной разницы в парах
            if ((firstNumber - secondNumber) % 3 != 0)
                minPairDifference = Math.Abs(firstNumber - secondNumber);

            line = sr.ReadLine()!;
        }

        // Если максимальная сумма делится на 3 - вычесть из неё минимальную разницу в парах
        if (maxSum % 3 == 0)
            maxSum -= minPairDifference;

        Console.WriteLine(maxSum);
    }
}
