using LinksApi.Data;
using LinksApi.Data.Models;
using LinksApi.Exceptions;
using LinksApi.Requests;
using LinksApi.Responses;

namespace LinksApi;

class LinksService(ILinksRepository linksRepository) : ILinksService
{
    public async Task<ShortLinkResponse> ShortenLinkAsync(ShortLinkRequest request)
    {
        // check if the link already exists
        var record = await _linksRepository.GetLinkFromLongUrlAsync(request.LongUrl);
        // TODO_CRITERIA: one which shortens the URL into a brief alphanumeric string.

        if (record is null)
        {
            var shortLink = GenerateShortLink(request.LongUrl);
            record = await _linksRepository.CreateShortLinkAsync(shortLink, request.LongUrl);
        }

        return MapShortLinkResponse(record);
    }

    public async Task<RetrieveUrlResponse> RetrieveUrlAsync(RetrieveUrlRequest request)
    {
        // // TODO_CRITERIA: one which expands the string into the original URL. 
        // var record = await _linksRepository.GetLinkFromShortLinkAsync(request.ShortLink);

        // // TODO_CRITERIA: If no such URL exists, it should return an error.

        var record = await _linksRepository.GetLinkFromShortLinkAsync(request.ShortLink)
            ?? throw new ShortLinkNotFoundException(string.Format("shortLink not found: {0}", request.ShortLink));

        return MapRetrieveUrlResponse(record);
    }

    private static string GenerateShortLink(string longUrl)
    {
        var encoded = Base62Encoder.EncodeAsBase62String(longUrl);

        // Trim encoded to desired length and assemble full shortlink.
        return $"{HttpScheme}://{Domain}/{encoded[..ShortUrlLength]}";
    }

    private static ShortLinkResponse MapShortLinkResponse(Link record) => new() { ShortLink = record.ShortLink };
    private static RetrieveUrlResponse MapRetrieveUrlResponse(Link record) => new() { LongUrl = record.LongUrl };

    private readonly ILinksRepository _linksRepository = linksRepository;

    private const string HttpScheme = "https"; // TODO: Move to config
    private const string Domain = "short.link"; // TODO: Move to config
    private const int ShortUrlLength = 7; // TODO: Move to config
}
