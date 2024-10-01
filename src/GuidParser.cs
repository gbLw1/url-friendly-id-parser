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
}
