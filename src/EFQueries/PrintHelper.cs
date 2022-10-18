namespace EFQueries;

public static class PrintHelper
{
    public static void PrintSeparator(string message)
    {
        Console.WriteLine();
        Console.WriteLine("#################################################################################");
        Console.WriteLine(message);
        Console.WriteLine();
    }

    public static void Print<T>(T value)
    {
        var json = JsonSerializer.Serialize<T>(value, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        Console.WriteLine(json);
    }
}
