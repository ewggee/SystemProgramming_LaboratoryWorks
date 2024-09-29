using System.ComponentModel;
using System.Diagnostics;

namespace Lab_5;

class Program
{
    static void Main()
    {
        var validAxtions = new[] { 1, 2, 3 };

        while (true)
        {
            Console.Clear();

            Console.WriteLine(
                "\n1 - Запустить процесс по названию\n" +
                "2 - Закрыть процесс по PID\n" +
                "3 - Вывести все процессы"
            );
            Console.WriteLine(new string('-', 80));

            int action;
            while (true)
            {
                Console.Write("Выберите дейтсвие: ");

                if (!int.TryParse(Console.ReadLine(), out var n))
                    continue;

                if (!validAxtions.Contains(n))
                    continue;

                action = n;
                break;
            }

            switch (action)
            {
                case 1:

                    CreateProcessByName();
                    break;

                case 2:

                    KillProcessByPID();
                    break;

                case 3:

                    DisplayAllProcessInfo();
                    break;
            }
        }
    }

    static void CreateProcessByName()
    {
        string appName = string.Empty;

        Console.Clear();

        Console.Write("Введите название и расширение процесса: ");
        appName = Console.ReadLine()?.Trim()!;
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(appName)
            };

            process.Start();
            Console.WriteLine($"Процесс {appName} запущен");

            Thread.Sleep(3000);
            Process.Start("taskkill", $"/F /T /IM {appName}");

            Console.ReadKey();
        }
        catch(Win32Exception)
        {
            Console.Write($"Не найдено приложений с именем {appName}. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }

    static void KillProcessByPID()
    {
        Console.Clear();

        int pid;
        while (true)
        {
            Console.Write("PID процесса: ");

            if (!int.TryParse(Console.ReadLine(), out var n))
                continue;

            pid = n;
            break;
        }

        try
        {
            if (Process.GetProcessById(pid) == null)
                throw new ArgumentException();

            Process.Start("taskkill", $"/F /T /PID {pid}");

            Console.Write("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        catch (ArgumentException)
        {
            Console.Write($"Не найдено приложений с PID-ом = {pid}. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }

    static void DisplayAllProcessInfo()
    {
        Console.Clear();

        ProcessStartInfo allProcessInfo = new()
        {
            FileName = "cmd.exe",
            Arguments = "/c tasklist",
            RedirectStandardOutput = true
        };

        Process process = new()
        {
            StartInfo = allProcessInfo
        };

        process.Start();

        var result = process.StandardOutput.ReadToEnd();
        Console.WriteLine(result);

        Console.Write("Нажмите любую клавишу для выхода...");

        Console.ReadKey();
    }
}