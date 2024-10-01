namespace UrlFriendlyID;

public static class GuidParser
{
    public static string ToStringFromGuid(Guid id)
    {
        return Convert.ToBase64String(id.ToByteArray())
        .Replace("/", "-")
        .Replace("+", "_")
        .Replace("=", string.Empty);
    }

    public static Guid ToGuidFromString(string id)
    {
        var base64Id = Convert.FromBase64String(id
        .Replace("-", "/")
        .Replace("_", "+")
        + "==");

        return new Guid(base64Id);
    }

    private const char EqualsChar = '=';
    private const char HypenChar = '-';
    private const char UnderscoreChar = '_';
    private const char SlashChar = '/';
    private const char PlusChar = '+';

    public static Guid ToGuidFromStringOptimized(ReadOnlySpan<char> id)
    {
        // allocate 24 bytes for the base64 chars
        // 22 bytes for the base64 chars and 2 bytes for the padding
        Span<char> base64Chars = stackalloc char[24];

        // decode base64url to base64
        for (int i = 0; i < 22; ++i)
        {
            base64Chars[i] = id[i] switch
            {
                HypenChar => SlashChar,
                UnderscoreChar => PlusChar,
                _ => id[i]
            };
        }

        base64Chars[22] = EqualsChar;
        base64Chars[23] = EqualsChar;

        // allocate 16 bytes for the Guid
        // 1 byte for each 2 base64 chars
        Span<byte> idBytes = stackalloc byte[16];

        // try to convert base64 chars to bytes so we can create a Guid
        Convert.TryFromBase64Chars(base64Chars, idBytes, out _);

        return new Guid(idBytes);
    }
}
