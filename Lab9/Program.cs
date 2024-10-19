using System.Diagnostics;
using System.Net;

namespace Lab9;

class Program
{
    const string DOWNLOADS = @"C:\Users\Evgeniy\Downloads";
    const string EXPLRORER = "explorer.exe";

    static void Main()
    {
        while (true)
        {
            Console.Write("Введите URL: ");
            string url = Console.ReadLine()!;

            Task.Run(() => DownloadAsync(url));
        }
    }

    static async Task DownloadAsync(string url)
    {
        try
        {
            using WebClient client = new();

            var fileName = Path.GetFileName(url);
            var filePath = Path.Combine(DOWNLOADS, fileName);

            await client.DownloadFileTaskAsync(url, filePath);

            Process.Start(EXPLRORER, filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
        }
    }
}