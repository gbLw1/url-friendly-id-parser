using BenchmarkDotNet.Attributes;

namespace UrlFriendlyID;

[MemoryDiagnoser(displayGenColumns: false)]
public class GuidParserBenchmarks
{
    private static readonly Guid IdGuid = Guid.Parse("25580a1d-136a-4676-98e3-7a36bae0cf98");
    private static readonly string IdString = "HQpYJWoTdkaY43o2uuDPmA";

    [Benchmark]
    public string TestToStringFromGuid()
    {
        return GuidParser.ToStringFromGuid(IdGuid);
    }

    [Benchmark]
    public Guid TestToGuidFromString()
    {
        return GuidParser.ToGuidFromString(IdString);
    }
}
