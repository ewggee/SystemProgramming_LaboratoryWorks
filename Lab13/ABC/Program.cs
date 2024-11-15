using System.Text.RegularExpressions;

namespace ABC;

class Program
{
    const string FILE_PATH = "fl.txt";
    readonly static string Sequence =
        File.ReadAllText(FILE_PATH)
        .Replace("AB", ".")
        .Replace("CB", ".");

    static void Main()
    {
        var max = Regex
            .Split(Sequence, "\\w")
            .Max().Length;

        Console.WriteLine(max);
    }
}
