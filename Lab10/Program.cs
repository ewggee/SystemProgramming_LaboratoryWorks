namespace Lab10
{
    class Program
    {
        const int NUMBERS_DIFFER = 10;
        const int MAX_VALUE = 100;
        const int MAX_COUNT_IN_SEQUENCE = 10000;

        static void Main()
        {
            var random = new Random();
            var zerosArray = new int[MAX_COUNT_IN_SEQUENCE];
            var sequence = zerosArray
                .Select(_ => random.Next(1, MAX_VALUE))
                .ToList();

            var maxSumOfSquares = 0;

            for (var i = 0; i < sequence.Count; i++)
            {
                for (var j = i + 10; j < sequence.Count; j++)
                {
                    var currentSum = (int)Math.Pow(sequence[i], 2) + (int)Math.Pow(sequence[j], 2);
                    
                    if (currentSum > maxSumOfSquares)
                        maxSumOfSquares = currentSum;
                }
            }

            Console.WriteLine(
                $"Максимальное произведение: {maxSumOfSquares}"    
            );
        }
    }
}
