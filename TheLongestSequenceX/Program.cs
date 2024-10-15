using System.Net;

namespace TheLongestSequenceX;

public class Program
{
    const string URL = "https://inf-ege.sdamgia.ru/doc/inf/zadanie24/24_demo.txt";

    static void Main()
    {
        WebClient client = new();
        Stream stream = client.OpenRead(URL);

        StreamReader reader = new(stream);

        var currentLength = 0;
        var maxLength = 0;

        int readChar;
        while ((readChar = reader.Read()) != -1)
        {
            if ((char)readChar == 'X')
            {
                currentLength++;
            }
            else
            {
                maxLength = Math.Max(maxLength, currentLength);
                currentLength = 0;
            }
        }

        maxLength = Math.Max(maxLength, currentLength);
        Console.WriteLine($"Самая длинная последовательность X: {maxLength}");
    }
}
