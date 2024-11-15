namespace QRW123;

class Program
{
    const string FILE_PATH = "fl.txt";
    readonly static string Sequence = File.ReadAllText(FILE_PATH);

    static void Main()
    {
        var max = 0;
        var current = 0;
        for (int i = 1; i < Sequence.Length; i++)
        {
            if (char.IsLetter(Sequence[i]) != char.IsLetter(Sequence[i - 1]))
            {
                current++;
            }
            else
            {
                max = Math.Max(max, current);
                current = 0;
            }
        }

        Console.WriteLine(Math.Max(max, current));
    }
}
