using LinksApi.Requests;

namespace LinksApi;

// TODO: Document
public interface ILinksService
{
    public Task<string> ShortenLinkAsync(ShortenLinkRequest request);

    public Task<string> RetrieveLinkAsync(RetrieveLinkRequest request);
}
