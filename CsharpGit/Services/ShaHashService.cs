using System.Security.Cryptography;
using System.Text;

namespace CSharpGit.Services;

/// <summary>
///     SHA Hashing Service class
/// </summary>
public static class ShaHashService
{
    /// <summary>
    ///     Generate a hash 
    /// </summary>
    /// <param name="buffer">String to be hashed</param>
    /// <returns>Returns a hash string that represents the buffer</returns>
    public static string GenerateHash(string buffer)
    {

        var bytes = Encoding.UTF8.GetBytes(buffer);
        var hashBytes = SHA256.HashData(bytes);

        StringBuilder sb = new();
        
        foreach(var b in hashBytes)
            sb.Append(b.ToString("x2"));
        

        return sb.ToString();
    }
}