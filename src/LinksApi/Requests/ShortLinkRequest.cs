using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LinksApi.Requests;

/// <summary>
/// Request to shorten a long URL.
/// </summary>
public class ShortLinkRequest
{
    public ShortLinkRequest() { }

    [SetsRequiredMembers]
    public ShortLinkRequest(string longUrl)
    {
        LongUrl = longUrl;
        Validate();
    }

    /// <summary>
    /// The long URL used to create a short link.
    /// </summary>
    [Required]
    [JsonPropertyName("long_url")]
    [Url(ErrorMessage = "long_url is not a valid URI format.")]
    public required string LongUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!LongUrl.IsValidUrl())
        {
            yield return new ValidationResult($"{nameof(LongUrl)} is not a valid URI format.", [nameof(LongUrl)]);
        }
    }

    private void Validate()
    {
        var validationContext = new ValidationContext(this);
        Validator.ValidateObject(this, validationContext, validateAllProperties: true);
    }
}