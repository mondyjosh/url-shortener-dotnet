using LinksApi.Requests;
using LinksApi.Responses;

namespace LinksApi;

// TODO: Document
public interface ILinksService
{
    public Task<ShortLinkResponse> ShortenLinkAsync(ShortLinkRequest request);

    public Task<RetrieveUrlResponse> RetrieveUrlAsync(RetrieveUrlRequest request);
}
