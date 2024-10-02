using System.Buffers.Text;
using System.Runtime.InteropServices;
using TraceReloggerLib;

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
    private const byte SlashCharByte = (byte)'/';
    private const char PlusChar = '+';
    private const byte PlusCharByte = (byte)'+';

    public static string ToStringFromGuidOptimized(Guid id)
    {
        Span<byte> idBytes = stackalloc byte[16];
        Span<byte> base64Bytes = stackalloc byte[24];

        // convert Guid to bytes
        MemoryMarshal.TryWrite(destination: idBytes, value: in id);

        // convert bytes to base64 bytes
        Base64.EncodeToUtf8(
            bytes: idBytes,
            utf8: base64Bytes,
            bytesConsumed: out _,
            bytesWritten: out _);

        // 22 bytes because we are returning a string with 22 chars
        // and 2 bytes for padding, which we will ignore
        Span<char> finalChars = stackalloc char[22];

        // convert bytes to base64 chars
        for (int i = 0; i < 22; ++i)
        {
            finalChars[i] = base64Bytes[i] switch
            {
                SlashCharByte => HypenChar,
                PlusCharByte => UnderscoreChar,
                _ => (char)base64Bytes[i]
            };
        }

        return new string(finalChars);
    }

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
