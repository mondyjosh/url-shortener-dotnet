using Microsoft.Extensions.Configuration;

using LinksApi.Data;
using LinksApi.Data.Models;
using LinksApi.Exceptions;
using LinksApi.Requests;

using Moq;

namespace LinksApi.Tests;

public class LinksServiceTests
{
    public LinksServiceTests()
    {
        var inMemoryConfig = new Dictionary<string, string?>
        {
            {"ConnectionStrings:DefaultConnection", "connectionstring"},
            {"ShortUrlSettings:HttpScheme", HttpScheme},
            {"ShortUrlSettings:Domain", Domain},
            {"ShortUrlSettings:PathLength", PathLength.ToString()},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryConfig)
            .Build();

        _linksRepository = new Mock<ILinksRepository>(MockBehavior.Strict);

        _linksService = new LinksService(configuration, _linksRepository.Object);
    }

    [Fact]
    public async Task ShortenLinkAsync_LinkAlreadyExists_ReturnShortLink()
    {
        var shortLink = "https://test.to/3BX29G";
        var longUrl = "https://www.example.com/ThisLinkExistsAlready";

        _linksRepository
            .Setup(repo => repo.GetLinkFromLongUrlAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new Link
                {
                    ShortLink = shortLink,
                    LongUrl = longUrl
                });

        var expected = shortLink;

        var request = new ShortLinkRequest(longUrl);

        var actual = await _linksService.ShortenLinkAsync(request);

        Assert.Equal(expected, actual.ShortLink);
        _linksRepository.Verify(repo => repo.CreateShortLinkAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ShortenLinkAsync_LinkDoesNotExist_CreateAndReturnShortLink()
    {
        var longUrl = "https://www.example.com/ThisIsANewLink";
        var shortLink = "https://test.to/WqBKDd";

        _linksRepository
            .Setup(repo => repo.GetLinkFromLongUrlAsync(It.IsAny<string>()))
            .ReturnsAsync((Link)null!);

        _linksRepository
            .Setup(repo => repo.CreateShortLinkAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(
                new Link
                {
                    ShortLink = shortLink,
                    LongUrl = longUrl
                });

        var expected = shortLink;

        var request = new ShortLinkRequest(longUrl);

        var actual = await _linksService.ShortenLinkAsync(request);

        Assert.Equal(expected, actual.ShortLink);
    }

    [Fact]
    public async Task RetrieveUrlAsync_ShortLinkFound_ReturnOriginalUrl()
    {
        var shortLink = "https://test.to/BdojTF";
        var longUrl = "https://www.example.com/ThisCameFromAShortLink";

        _linksRepository
            .Setup(repo => repo.GetLinkFromShortLinkAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new Link
                {
                    ShortLink = shortLink,
                    LongUrl = longUrl
                });

        var expected = longUrl;

        var request = new RetrieveUrlRequest(shortLink);

        var actual = await _linksService.RetrieveUrlAsync(request);

        Assert.Equal(expected, actual.LongUrl);
    }

    [Fact]
    public async Task RetrieveUrlAsync_ShortLinkNotFound_ThrowShortLinkNotFoundException()
    {
        var shortLink = "https://test.to/nope";

        _linksRepository
            .Setup(repo => repo.GetLinkFromShortLinkAsync(It.IsAny<string>()))
            .ReturnsAsync((Link)null!);

        var request = new RetrieveUrlRequest(shortLink);

        await Assert.ThrowsAsync<ShortLinkNotFoundException>(() => _linksService.RetrieveUrlAsync(request));
    }

    private readonly Mock<ILinksRepository> _linksRepository;
    private readonly LinksService _linksService;

    private const string HttpScheme = "https";
    private const string Domain = "test.to";
    private const int PathLength = 6;
}