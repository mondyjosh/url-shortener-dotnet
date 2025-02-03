namespace LinksApi;

public class ShortUrlSettingsOptions
{
    public const string ShortUrlSettings = "ShortUrlSettings";

    public string HttpScheme { get; set; } = "https";
    public string Domain { get; set; } = string.Empty;
    public int PathLength { get; set; } = default;
}