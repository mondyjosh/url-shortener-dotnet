namespace LinksApi;

public class ShortLinkConfigurationOptions
{
    public const string ShortLinkConfiguration = nameof(ShortLinkConfiguration);

    public string HttpScheme { get; set; } = "https";
    public string Domain { get; set; } = string.Empty;
    public int PathLength { get; set; } = default;
}