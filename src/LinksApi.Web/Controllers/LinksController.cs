using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using LinksApi.Exceptions;
using LinksApi.Requests;

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
    /// <param name="request">
    /// The request object that supplies the long URL to shorten.
    /// </param>
    /// <returns>A shortened link representing the original long URL.</returns>
    /// <response code="200">Returns a shortened link representing the original URL.</response>
    /// <response code="400">If the link is null or not a valid URL.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> ShortenLinkAsync([Required] ShortLinkRequest request)
    {
        try
        {
            var response = await _linksService.ShortenLinkAsync(request);

            return Ok(response);
        }
        catch (UriFormatException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves an original URL from short link.
    /// </summary>
    /// <param name="request">
    /// The request object that supplies the shortened link used to retrieve the original URL.
    /// </param>
    /// <returns>The original long URL.</returns>
    /// <response code="200">Returns the original URL.</response>
    /// <response code="400">If the short link is null or not a valid URL.</response>
    /// <response code="404">If the short link is not found.</response>
    [HttpGet("{**shortLink}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> RetrieveUrlAsync([Required][FromRoute] RetrieveUrlRequest request)
    {
        try
        {
            var response = await _linksService.RetrieveUrlAsync(request);

            return Ok(response);
        }
        catch (ShortLinkNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UriFormatException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private readonly ILinksService _linksService = linksService;
}
