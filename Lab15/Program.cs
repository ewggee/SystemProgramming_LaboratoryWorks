namespace Lab15;

class Program
{
    static void Main()
    {
        Console.Write(" Начало: ");
        string[] start = Console.ReadLine()!.Split(' ');
        var startHours = int.Parse(start[0]);
        var startMinutes = int.Parse(start[1]);

        Console.Write("  Конец: ");
        string[] end = Console.ReadLine()!.Split(' ');
        var endHours = int.Parse(end[0]);
        var endMinutes = int.Parse(end[1]);


        var result = CalculateStrikes(startHours, startMinutes, endHours, endMinutes);
        Console.WriteLine($" Результат: {result}");
    }

    static int CalculateStrikes(int startHours, int startMinutes, int endHours, int endMinutes)
    {
        var Hours12Strikes = new int[12];
        for (int i = 0; i < 12; i++) 
            Hours12Strikes[i] = i + 1;

        var Hours48Strikes = new int[48];
        Array.Copy(Hours12Strikes, Hours48Strikes, 12);
        Array.Copy(Hours12Strikes, 0, Hours48Strikes, 12, 12);
        Array.Copy(Hours12Strikes, 0, Hours48Strikes, 24, 12);
        Array.Copy(Hours12Strikes, 0, Hours48Strikes, 36, 12);

        int hoursDefference;
        if (endHours > startHours) hoursDefference = endHours - startHours;
        else if (endHours < startHours) hoursDefference = (endHours + 24) - startHours;
        else hoursDefference = endMinutes > startMinutes ? 0 : 24; // endHours == startHours 

        var hoursStrikes = new int[hoursDefference];
        Array.Copy(Hours48Strikes, startHours, hoursStrikes, 0, hoursDefference);
        
        var hoursSum = 0;
        for (int i = 0; i < hoursStrikes.Length; i++) 
            hoursSum += hoursStrikes[i];

        int middleHoursStrikes;
        if (startMinutes < 30 && endMinutes > 30) middleHoursStrikes = hoursStrikes.Length + 1;
        else if (startMinutes < 30 && endMinutes < 30) middleHoursStrikes = hoursStrikes.Length;
        else if (startMinutes > 30 && endMinutes < 30) middleHoursStrikes = hoursStrikes.Length - 1;
        else middleHoursStrikes = hoursStrikes.Length;

        return hoursSum + middleHoursStrikes;
    }
}

