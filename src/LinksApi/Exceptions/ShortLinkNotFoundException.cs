namespace LinksApi.Exceptions;

[Serializable]
public class ShortLinkNotFoundException : Exception
{
    public ShortLinkNotFoundException() { }
    public ShortLinkNotFoundException(string message) : base(message) { }
    public ShortLinkNotFoundException(string message, Exception inner) : base(message, inner) { }
}