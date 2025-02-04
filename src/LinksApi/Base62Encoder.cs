using System.Numerics;
using System.Text;

namespace LinksApi;

/// <summary>
/// Base62 encoder for strings.
/// </summary>
/// <remarks>
/// The base-62 encoding scheme uses 62 characters ([0-9A-Za-z]).
/// This is ideal for URL-encoding purposes, because the character set ensures that
/// encoded strings do not contain URL-restricted characters.
/// </remarks>
public static class Base62Encoder
{
    // Many thanks to the folks at https://base62.org/ for providing great sample code
    // for Base62 encoder/decoder in multiple languages!

    /// <summary>
    /// Encodes the input string to base-62 string representation.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The string representation, in base 62, of the input string.</returns>
    public static string EncodeAsBase62String(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);

        return ToBase62String(inputBytes);
    }

    /// <summary>
    /// Converts an array of 8-bit unsigned integers to its equivalent string representation
    /// that is encoded with base-62 digits.
    /// </summary>
    /// <param name="bytes">An array of 8-bit unsigned integers.</param>
    /// <returns>The string representation, in base 62, of the contents of inArray.</returns>
    /// <exception cref="ArgumentNullException">inArray is null.</exception
    public static string ToBase62String(byte[] inArray)
    {
        ArgumentNullException.ThrowIfNull(inArray);

        var value = new BigInteger(inArray);
        var result = new StringBuilder();
        Convert.ToBase64String(inArray);
        while (value > 0)
        {
            value = BigInteger.DivRem(value, 62, out var remainder);
            result.Append(_base62Cipher[(int)remainder]);
        }

        foreach (var b in inArray)
        {
            if (b == 0)
            {
                result.Append(_base62Cipher[0]);
            }
            else
            {
                break;
            }
        }

        var resultArray = result.ToString().ToCharArray();
        Array.Reverse(resultArray);

        return new string(resultArray);
    }

    private readonly static char[] _base62Cipher = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

}