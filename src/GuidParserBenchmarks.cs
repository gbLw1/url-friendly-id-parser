using BenchmarkDotNet.Attributes;

namespace UrlFriendlyID;

[MemoryDiagnoser(displayGenColumns: false)]
public class GuidParserBenchmarks
{
    private static readonly Guid IdGuid = Guid.Parse("25580a1d-136a-4676-98e3-7a36bae0cf98");
    private static readonly string IdString = "HQpYJWoTdkaY43o2uuDPmA";

    [Benchmark]
    public string ToString_FromGuid()
    {
        return GuidParser.ToStringFromGuid(IdGuid);
    }

    [Benchmark]
    public Guid ToGuid_FromString()
    {
        return GuidParser.ToGuidFromString(IdString);
    }

    [Benchmark]
    public Guid ToGuid_FromString_Optimized()
    {
        return GuidParser.ToGuidFromStringOptimized(IdString);
    }
}
