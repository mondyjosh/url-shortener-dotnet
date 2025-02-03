namespace LinksApi;

public class ShortLinkSettingsOptions
{
    public const string ShortLinkSettings = nameof(ShortLinkSettings);

    public string HttpScheme { get; set; } = "https";
    public string Domain { get; set; } = string.Empty;
    public int PathLength { get; set; } = default;
}