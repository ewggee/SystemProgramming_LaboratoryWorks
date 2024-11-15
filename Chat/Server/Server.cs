using Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Server
    {
        readonly static StringBuilder ChatHistory = new();
        readonly static List<Socket> ClientSockets = [];

        static void Main()
        {
            var port = 8080;

            TcpListener serverSocket = new(IPAddress.Any, port);
            serverSocket.Start();

            PrintHelper.PrintColorMessage($"Чат запущен: ", ConsoleColor.Green);
            Console.WriteLine(DateTime.Now);
            PrintHelper.PrintColorMessage(new string('-', 70), ConsoleColor.DarkGray);
            Console.WriteLine();

            while (true)
            {
                Socket clientSocket = serverSocket.AcceptSocket();

                var clientThread = new Thread(() => HandleClient(clientSocket));
                clientThread.Start();
            }
        }

        /// <summary>
        /// Обработка клиента.
        /// </summary>
        static void HandleClient(Socket clientSocket)
        {
            while (true)
            {
                var json = ReceiveMessage(clientSocket);

                if (json == null)
                {
                    ClientSockets.Remove(clientSocket);
                    break;
                }

                var messageInfo = JsonConvert.DeserializeObject<MessageInfo>(json);
                if (messageInfo == null)
                    continue;

                if (string.IsNullOrWhiteSpace(messageInfo.Message))
                {
                    Console.Write(" - ");
                    PrintHelper.PrintColorMessage(messageInfo.UserName, messageInfo.UserColor);
                    Console.WriteLine(" присоединился к чату.");

                    ChatHistory.AppendLine($" - {messageInfo.UserName} присоединился к чату.");

                    var joinNotify = new MessageInfo(messageInfo.UserName, $" присоединился к чату.", messageInfo.UserColor);
                    BroadcastMessage(clientSocket, joinNotify);

                    SendChatHistory(clientSocket);

                    continue;
                }

                PrintHelper.PrintColorMessage($"{messageInfo.UserName}: ", messageInfo.UserColor);
                Console.WriteLine(messageInfo.Message);

                ChatHistory.AppendLine($"{messageInfo.UserName}: {messageInfo.Message}");

                BroadcastMessage(clientSocket, messageInfo);
            }
        }

        /// <summary>
        /// Получение сообщение от клиента.
        /// </summary>
        static string? ReceiveMessage(Socket clientSocket)
        {
            if (!ClientSockets.Contains(clientSocket))
                ClientSockets.Add(clientSocket);

            try
            {
                var buffer = new byte[1024];
                var bytesReceived = clientSocket.Receive(buffer);

                if (bytesReceived == 0)
                {
                    return null; // Соединение закрыто
                }

                return Encoding.UTF8.GetString(buffer, 0, bytesReceived);
            }
            catch (Exception)
            {
                return null; // Ошибка соединения
            }
        }

        /// <summary>
        /// Отправка сообщения всем подключенным клиентам.
        /// </summary>
        static void BroadcastMessage(Socket senderSocket, MessageInfo messageInfo)
        {
            var json = JsonConvert.SerializeObject(messageInfo);

            byte[] buffer = Encoding.UTF8.GetBytes(json);

            foreach (Socket clientSocket in ClientSockets)
            {
                if (clientSocket != senderSocket)
                {
                    try
                    {
                        clientSocket.Send(buffer);
                    }
                    catch (SocketException)
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Отправка новому участнику истории чата.
        /// </summary>
        static void SendChatHistory(Socket clientSocket)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(ChatHistory.ToString());

            try
            {
                clientSocket.Send(buffer);
            }
            catch (SocketException)
            {
                return;
            }
        }
    }
}