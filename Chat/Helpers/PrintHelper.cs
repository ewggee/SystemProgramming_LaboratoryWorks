namespace Helpers
{
    public static class PrintHelper
    {
        /// <summary>
        /// Вывод цветного сообщения на консоль.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="color">Цвет текста сообщения.</param>
        public static void PrintColorMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.Write(message);

            Console.ResetColor();
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 18afb3aacd326bee17664b2f96f4b4fbf8c99d65
