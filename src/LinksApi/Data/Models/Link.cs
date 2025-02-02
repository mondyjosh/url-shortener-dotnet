namespace LinksApi.Data.Models;

class Link
{
    public int Id { get; set; }
    public required string ShortLink { get; set; }
    public required string LongUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }    
}