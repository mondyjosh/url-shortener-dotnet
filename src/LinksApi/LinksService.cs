using LinksApi.Data;
using LinksApi.Data.Models;
using LinksApi.Requests;
using LinksApi.Responses;

namespace LinksApi;

class LinksService(ILinksRepository linksRepository) : ILinksService
{
    public async Task<ShortLinkResponse> ShortenLinkAsync(ShortLinkRequest request)
    {
        // TODO: Check that the request.LongUrl is valid.
        var record = await _linksRepository.GetLinkFromLongUrlAsync(request.LongUrl);
        // TODO_CRITERIA: one which shortens the URL into a brief alphanumeric string.

        if (record is null)
        {
            var shortLink = TransformLink(request.LongUrl);
            record = await _linksRepository.CreateShortLinkAsync(shortLink);
        }

        return MapShortLinkResponse(record);
    }

    public async Task<RetrieveUrlResponse> RetrieveUrlAsync(RetrieveUrlRequest request)
    {
        // TODO_CRITERIA: one which expands the string into the original URL. 
        var record = await _linksRepository.GetLinkFromShortLinkAsync(request.ShortLink);

        // TODO_CRITERIA: If no such URL exists, it should return an error.

        return MapRetrieveUrlResponse(record);
    }

    private static string TransformLink(string longUrl)
    {
        // Right now it's just a substring, but we'll get something more clever in a bit.
        // I'd really prefer the solution to be static
        return $"go.to/{longUrl[..6]}";
    }


    private static ShortLinkResponse MapShortLinkResponse(Link record) => new() { ShortLink = record.ShortLink };
    private static RetrieveUrlResponse MapRetrieveUrlResponse(Link record) => new() { LongUrl = record.LongUrl };

    private readonly ILinksRepository _linksRepository = linksRepository;
}
