using System.Text.RegularExpressions;

namespace Lab6;

class Program
{
    const string FILE_PATH = "Текст.txt";

    static void Main()
    {
        try
        {
            using var sr = new StreamReader(FILE_PATH);

            var text = sr.ReadToEnd();

            // Без пробелов
            var withoutSpaceCount =
                text.Where(s => !char.IsWhiteSpace(s)).Count();

            // Слова
            var wordsCount =
                Regex.Matches(text, @"\b\w+\b").Count;

            // Абзацы
            var paragraphsCount =
                text.Where(s => s == '\t').Count();

            // Страницы
            var pagesCount = 
                text.Where(s => s == '\u000A').Count();

            var result =
                $"Все символы: {text.Length}\n" +
                $"Символы без пробелов: {withoutSpaceCount}\n" +
                $"Слова: {wordsCount}\n" +
                $"Абзацы: {paragraphsCount}\n" +
                $"Страницы: {pagesCount}";

            Console.WriteLine(result);

            // Получение имени файла
            var filename = FILE_PATH
                .Substring(FILE_PATH.LastIndexOf('\\') + 1)
                .Replace(".txt", string.Empty);

            // Запись в файл .svc
            File.WriteAllText(filename + ".svc", result);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
