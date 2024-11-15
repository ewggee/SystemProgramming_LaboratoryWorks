using Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        const string IPv4 = "";
        const int PORT = 0;

        static string? UserName { get; set; }
        static ConsoleColor UserColor { get; set; } = default;

        static void Main()
        {
            using var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Connect(new IPEndPoint(IPAddress.Parse(IPv4), PORT));

            PrintHelper.PrintColorMessage("Подключение успешно!\n", ConsoleColor.Green);

            #region Ввод имени
            while (true)
            {
                Console.Write("\nВведите ваше имя: ");
                UserName = Console.ReadLine()!;

                if (!string.IsNullOrWhiteSpace(UserName))
                    break;
            }
            #endregion

            #region Выбор цвета
            Console.WriteLine("\nВыберите номер цвета вашего имени");

            var colorNumbers = Enum.GetValues(typeof(ConsoleColor))
                .Cast<int>()
                .Skip(1)    // Пропуск Black цвета.
                .ToArray();

            for (var i = 1; i < colorNumbers.Length; i++)
            {
                Console.Write($"  {i}) ");
                PrintHelper.PrintColorMessage(
                    $"{Enum.GetName(typeof(ConsoleColor), i)}\n",
                    (ConsoleColor)i
                );
            }

            while (true)
            {
                Console.Write("\nЦвет: ");

                if (int.TryParse(Console.ReadLine(), out var colorNumber)
                    && colorNumbers.Contains(colorNumber))
                {
                    UserColor = (ConsoleColor)colorNumber;

                    break;
                }
            }
            #endregion

            PrintHeader();

            var receiveThread = new Thread(() => ReceiveMessages(serverSocket));
            receiveThread.Start();

            // Оповещение сервера о присоединении пользователя к чату.
            SendMessage(
                serverSocket,
                new MessageInfo(UserName, null, UserColor));

            while (true)
            {
                var message = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(message))
                {
                    SendMessage(
                        serverSocket,
                        new MessageInfo(UserName, message, UserColor));
                }
            }
        }

        static void PrintHeader()
        {
            Console.Clear();
            PrintHelper.PrintColorMessage("Вы: ", ConsoleColor.DarkGray);
            PrintHelper.PrintColorMessage(UserName + "\n", UserColor);
            Console.WriteLine(new string('-', 70));
        }

        /// <summary>
        /// Отправка сообщения на сервер.
        /// </summary>
        static void SendMessage(Socket serverSocket, MessageInfo messageInfo)
        {
            var json = JsonConvert.SerializeObject(messageInfo);
            byte[] buffer = Encoding.UTF8.GetBytes(json);

            try
            {
                serverSocket.Send(buffer);
            }
            catch (Exception ex)
            {
                PrintHelper.PrintColorMessage($"Ошибка отправки сообщения: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Приём сообщений от сервера.
        /// </summary>
        static void ReceiveMessages(Socket serverSocket)
        {
            string json = string.Empty;
            while (true)
            {
                var buffer = new byte[1024];
                var bytesReceived = serverSocket.Receive(buffer);

                if (bytesReceived == 0)
                {
                    PrintHelper.PrintColorMessage("Соединение с сервером разорвано.", ConsoleColor.Red);
                    continue;
                }

                try
                {
                    json = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                    var messageInfo = JsonConvert.DeserializeObject<MessageInfo>(json);

                    if (messageInfo.Message == " присоединился к чату.")
                    {
                        Console.Write(" - ");
                        PrintHelper.PrintColorMessage(messageInfo.UserName, messageInfo.UserColor);
                        Console.WriteLine(messageInfo.Message);
                        continue;
                    }

                    PrintHelper.PrintColorMessage($"{messageInfo.UserName}: ", messageInfo.UserColor);
                    Console.WriteLine(messageInfo.Message);
                }
                catch (JsonReaderException)
                {
                    PrintHeader();
                    PrintHelper.PrintColorMessage(json.ToString() ?? string.Empty, ConsoleColor.DarkGray);

                    var lineCount = json.Count(c => c == '\n');
                    Console.WriteLine($"------------------------ Ранние сообщения ({lineCount}) ------------------------");
                }
                catch (Exception ex)
                {
                    PrintHelper.PrintColorMessage($"Ошибка приёма сообщения: {ex.Message}", ConsoleColor.Red);
                }
            }
        }
    }
}
