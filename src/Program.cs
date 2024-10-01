using BenchmarkDotNet.Running;
using UrlFriendlyID;

BenchmarkRunner.Run<GuidParserBenchmarks>();

// using UrlFriendlyID;

// var id = Guid.NewGuid();
// Console.WriteLine($"Original GUID: {id}");

// var UrlFriendlyID = GuidParser.ToStringFromGuid(id);
// Console.WriteLine($"URL Friendly ID: {UrlFriendlyID}");

// var parsedId = GuidParser.ToGuidFromString(UrlFriendlyID);
// Console.WriteLine($"Parsed GUID: {parsedId}");