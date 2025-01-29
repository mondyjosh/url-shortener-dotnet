using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortenerApi.Controllers;

/// <summary>
/// Endpoints related to link management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    /// <summary>
    /// Initializes a new instances of the <see cref="LinksController"/> class.
    /// </summary>
    public LinksController(ILogger<LinksController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Creates a short link.
    /// </summary>
    /// <param name="link">The original link to shorten.</param>
    /// <returns>A shortened link representing the original long URL.</returns>
    /// <response code="200">Returns a shortened link representing the original URL.</response>
    /// <response code="400">If the link is null or not a valid URL.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public string GetShortLink([Required] string link)
    {
        // TODO: Service to shorten link
        return $"TODO: Shorten {link}";
    }

    /// <summary>
    /// Retrieve original URL from short link.
    /// </summary>
    /// <param name="shortLink">The shortened link.</param>
    /// <returns>The original long URL.</returns>
    /// <response code="200">Returns the original long URL.</response>
    /// <response code="400">If the short link is null or not a valid URL.</response>
    /// <response code="404">If the short link is not found.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(MediaTypeNames.Application.Json)]
    public string GetOriginalLink([Required] string shortLink)
    {
        // TODO: Service to get original link from shortLink
        return $"TODO: Get original link for {shortLink}";
    }

    private readonly ILogger<LinksController> _logger;
}
