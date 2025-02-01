namespace LinksApi;

// TODO: Document
public interface ILinksService
{
    public Task<string> ShortenLinkAsync(string url);

    public Task<string> RetrieveLinkAsync(string shortUrl);
}
