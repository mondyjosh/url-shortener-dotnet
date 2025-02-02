using LinksApi.Requests;

namespace LinksApi;

class LinksService : ILinksService
{
    public async Task<string> ShortenLinkAsync(ShortenLinkRequest request)
    {
        return await Task.FromResult($"TODO: Shorten link - {request.LongUrl}");
    }

    public async Task<string> RetrieveLinkAsync(RetrieveLinkRequest request)
    {
        return await Task.FromResult($"TODO: Retrieve link - {request.ShortUrl}");
    }
}
