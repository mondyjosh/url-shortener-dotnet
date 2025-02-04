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
        _shortLinkSettings = new ShortLinkSettingsOptions();
        config.GetSection(ShortLinkSettingsOptions.ShortLinkSettings).Bind(_shortLinkSettings);

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

    string GenerateShortLink(string longUrl)
    {
        var encoded = Base62Encoder.EncodeAsBase62String(longUrl);

        // Trim encoded to desired length and assemble full shortlink.
        return $"{_shortLinkSettings.HttpScheme}://{_shortLinkSettings.Domain}/{encoded[.._shortLinkSettings.PathLength]}";
    }

    private static ShortLinkResponse MapShortLinkResponse(Link record) => new() { ShortLink = record.ShortLink, LongUrl = record.LongUrl };
    private static RetrieveUrlResponse MapRetrieveUrlResponse(Link record) => new() { LongUrl = record.LongUrl, ShortLink = record.ShortLink };

    private readonly ShortLinkSettingsOptions _shortLinkSettings;    
    private readonly ILinksRepository _linksRepository;

}
