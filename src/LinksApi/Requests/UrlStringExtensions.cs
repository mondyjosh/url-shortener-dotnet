namespace LinksApi.Requests;

static class UrlStringExtensions
{
    /// <summary>
    /// Determines if an input string is a valid HTTP/HTTPS URI format.
    /// </summary>
    /// <param name="input">The input string</param>
    /// <returns>True if input is a valid HTTP/HTTPS URI format, otherwise false.</returns>
    public static bool IsValidUrl(this string input) =>
        Uri.TryCreate(input, UriKind.Absolute, out Uri? uriResult)
        && (uriResult.Scheme.Equals(Uri.UriSchemeHttp) || uriResult.Scheme.Equals(Uri.UriSchemeHttps));
}