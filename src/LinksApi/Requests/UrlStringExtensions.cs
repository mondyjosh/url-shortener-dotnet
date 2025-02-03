namespace LinksApi.Requests;

static class UrlStringExtensions
{
    public static bool IsValidUrl(this string input) =>
        Uri.TryCreate(input, UriKind.Absolute, out Uri? uriResult)
        && (uriResult.Scheme.Equals(Uri.UriSchemeHttp) || uriResult.Scheme.Equals(Uri.UriSchemeHttps));
}