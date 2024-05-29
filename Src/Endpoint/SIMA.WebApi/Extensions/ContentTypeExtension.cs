namespace SIMA.WebApi.Extensions;

public static class ContentTypeExtension
{
    private static readonly Dictionary<string, string> Mappings = new Dictionary<string, string>()
    {
        {"txt","text/plain" },
        {"xls","application/vnd.ms-excel" },
        {"xlsx","application/vnd.ms-excel" },
        {"csv","text/cs" },
        {"xml","text/xml" },
        {"pdf","application/pdf" },
        {"png","image/png" },
        {"jpg","image/jpeg" },
        {"jpeg","image/jpeg" },
    };
    public static string GetContentType(this string extension)
    {
        return Mappings[extension];
    }
}
