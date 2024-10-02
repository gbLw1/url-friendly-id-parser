using BenchmarkDotNet.Running;

namespace UrlFriendlyID;

class Program
{
    static void Main()
    {
        Benchy(); // dotnet run -c release
        // Run(); // dotnet run
    }

    static void Benchy() => BenchmarkRunner.Run<GuidParserBenchmarks>();

    static void Run()
    {
        var id = Guid.NewGuid();
        Console.WriteLine($"Original GUID: {id}");

        var UrlFriendlyID = GuidParser.ToStringFromGuid(id);
        var UrlFriendlyIDOptimized = GuidParser.ToStringFromGuidOptimized(id);
        Console.WriteLine($"URL Friendly ID: {UrlFriendlyID}");
        Console.WriteLine($"URL Friendly ID (Optimized): {UrlFriendlyIDOptimized}");

        var parsedId = GuidParser.ToGuidFromString(UrlFriendlyID);
        var parsedIdOptimized = GuidParser.ToGuidFromStringOptimized(UrlFriendlyID);
        Console.WriteLine($"Parsed GUID: {parsedId}");
        Console.WriteLine($"Parsed GUID (Optimized): {parsedIdOptimized}");
    }
}