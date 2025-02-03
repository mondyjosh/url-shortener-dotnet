using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LinksApi.Requests;

/// <summary>
/// Request to retrieve a long URL.
/// </summary>
public class RetrieveUrlRequest : IValidatableObject
{
    public RetrieveUrlRequest() { }

    [SetsRequiredMembers]
    public RetrieveUrlRequest(string shortLink)
    {
        ShortLink = shortLink;
        Validate();
    }

    /// <summary>
    /// The short link used to search for an existing long URL.
    /// </summary>
    [Required]
    [JsonPropertyName("short_link")]
    [Url(ErrorMessage = "short_link is not a valid URI format.")]
    public required string ShortLink { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!ShortLink.IsValidUrl())
        {
            yield return new ValidationResult($"{nameof(ShortLink)} is not a valid URI format.", [nameof(ShortLink)]);
        }
    }

    private void Validate()
    {
        var validationContext = new ValidationContext(this);
        Validator.ValidateObject(this, validationContext, validateAllProperties: true);
    }
}