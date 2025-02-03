using Microsoft.Extensions.Configuration;

using LinksApi.Data;
using LinksApi.Data.Models;
using LinksApi.Exceptions;
using LinksApi.Requests;
using LinksApi.Responses;

namespace LinksApi;

class LinksService : ILinksService
{
    public LinksService(IConfiguration config, ILinksRepository linksRepository)
    {
        _shortUrlSettings = new ShortUrlSettingsOptions();
        config.GetSection(ShortUrlSettingsOptions.ShortUrlSettings).Bind(_shortUrlSettings);

        _linksRepository = linksRepository;
    }

    public async Task<ShortLinkResponse> ShortenLinkAsync(ShortLinkRequest request)
    {
        // check if the link already exists
        var record = await _linksRepository.GetLinkFromLongUrlAsync(request.LongUrl);

        if (record is null)
        {
            var shortLink = GenerateShortLink(request.LongUrl);
            record = await _linksRepository.CreateShortLinkAsync(shortLink, request.LongUrl);
        }

        return MapShortLinkResponse(record);
    }

    public async Task<RetrieveUrlResponse> RetrieveUrlAsync(RetrieveUrlRequest request)
    {
        var record = await _linksRepository.GetLinkFromShortLinkAsync(request.ShortLink)
            ?? throw new ShortLinkNotFoundException(string.Format("shortLink not found: {0}", request.ShortLink));

        return MapRetrieveUrlResponse(record);
    }

    private string GenerateShortLink(string longUrl)
    {
        var encoded = Base62Encoder.EncodeAsBase62String(longUrl);

        // Trim encoded to desired length and assemble full shortlink.
        return $"{_shortUrlSettings.HttpScheme}://{_shortUrlSettings.Domain}/{encoded[.._shortUrlSettings.PathLength]}";
    }

    private static ShortLinkResponse MapShortLinkResponse(Link record) => new() { ShortLink = record.ShortLink };
    private static RetrieveUrlResponse MapRetrieveUrlResponse(Link record) => new() { LongUrl = record.LongUrl };

    private readonly ShortUrlSettingsOptions _shortUrlSettings;    
    private readonly ILinksRepository _linksRepository;

}
