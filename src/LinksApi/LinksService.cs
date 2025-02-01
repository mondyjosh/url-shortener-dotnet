namespace LinksApi;

class LinksService : ILinksService
{
    public async Task<string> ShortenLinkAsync(string url)
    {
        return await Task.FromResult($"TODO: Shorten link - {url}");
    }

    public async Task<string> RetrieveLinkAsync(string shortUrl)
    {
        return await Task.FromResult($"TODO: Retrieve link - {shortUrl}");
    }
}
