using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

namespace LinksApi.Web.Controllers;

/// <summary>
/// Endpoints related to link management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LinksController(ILinksService linksService) : ControllerBase
{
    /// <summary>
    /// Creates a short link.
    /// </summary>
    /// <param name="link">The URL to shorten.</param>
    /// <returns>A shortened link representing the original long URL.</returns>
    /// <response code="200">Returns a shortened link representing the original URL.</response>
    /// <response code="400">If the link is null or not a valid URL.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> ShortenLinkAsync([Required][FromBody] string link)
    {
        var response = await _linksService.ShortenLinkAsync(link);

        return Ok(response);
    }

    /// <summary>
    /// Retrieve original URL from short link.
    /// </summary>
    /// <param name="shortLink">The shortened link that represents a URL.</param>
    /// <returns>The original URL.</returns>
    /// <response code="200">Returns the original URL.</response>
    /// <response code="400">If the short link is null or not a valid URL.</response>
    /// <response code="404">If the short link is not found.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> RetrieveLinkAsync([Required] string shortLink)
    {
        // TODO: Service to get original link from shortLink
        var response = await _linksService.RetrieveLinkAsync(shortLink);

        return Ok(response);
    }

    private readonly ILinksService _linksService = linksService;
}
