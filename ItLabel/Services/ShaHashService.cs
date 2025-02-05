using System.Security.Cryptography;
using System.Text;

namespace ItLabel.Services;

/// <summary>
///     SHA Hashing Service class
/// </summary>
public static class ShaHashService
{
    /// <summary>
    ///     Generate a hash 
    /// </summary>
    /// <param name="data">Bytes to be hashed</param>
    /// <returns>Returns a hash string that represents the buffer</returns>
    public static string GenerateHash(byte[] data)
    {
        var hashBytes = SHA1.HashData(data);

        StringBuilder sb = new();
        
        foreach(var b in hashBytes)
            sb.Append(b.ToString("x2"));
        
        return sb.ToString();
    }
}