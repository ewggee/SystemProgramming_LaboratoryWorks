namespace Helpers
{
    public class MessageInfo
    {
        public readonly string? UserName;
        public readonly string? Message;
        public readonly ConsoleColor UserColor;

        public MessageInfo(
            string? userName,
            string? message,
            ConsoleColor userColor)
        {
            UserName = userName;
            Message = message;
            UserColor = userColor;
        }
    }
}
