using LinksApi.Data;
using LinksApi.Data.Models;
using LinksApi.Exceptions;
using LinksApi.Requests;
using LinksApi.Responses;
using Npgsql.Replication;

namespace LinksApi;

class LinksService(ILinksRepository linksRepository) : ILinksService
{
    public async Task<ShortLinkResponse> ShortenLinkAsync(ShortLinkRequest request)
    {
        var record = await _linksRepository.GetLinkFromLongUrlAsync(request.LongUrl);
        // TODO_CRITERIA: one which shortens the URL into a brief alphanumeric string.

        if (record is null)
        {
            var shortLink = EncodeLongUrl(request.LongUrl);
            record = await _linksRepository.CreateShortLinkAsync(shortLink);
        }

        return MapShortLinkResponse(record);
    }

    public async Task<RetrieveUrlResponse> RetrieveUrlAsync(RetrieveUrlRequest request)
    {
        // // TODO_CRITERIA: one which expands the string into the original URL. 
        // var record = await _linksRepository.GetLinkFromShortLinkAsync(request.ShortLink);

        // // TODO_CRITERIA: If no such URL exists, it should return an error.

        // if (record is null)
        // {
        //     throw new ShortLinkNotFoundException("");
        // }

        // ----------------------------------------------------
        // TODO: Validate if request.ShortLink is a URL

        var record = await _linksRepository.GetLinkFromShortLinkAsync(request.ShortLink)
            ?? throw new ShortLinkNotFoundException(string.Format("shortLink not found: {0}", request.ShortLink));

        return MapRetrieveUrlResponse(record);
    }

    private static string EncodeLongUrl(string longUrl)
    {
        // Right now it's just a substring, but we'll get something more clever in a bit.
        // I'd really prefer the solution to be static
        return $"go.to/{longUrl[..6]}";
    }

    private static ShortLinkResponse MapShortLinkResponse(Link record) => new() { ShortLink = record.ShortLink };
    private static RetrieveUrlResponse MapRetrieveUrlResponse(Link record) => new() { LongUrl = record.LongUrl };

    private readonly ILinksRepository _linksRepository = linksRepository;
}
