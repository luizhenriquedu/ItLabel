using System.Security.Cryptography;
using System.Text;

namespace CSharpGit.Services;

public class ShaHashService
{
    public static string GenerateHash(string buffer)
    {

        var bytes = Encoding.UTF8.GetBytes(buffer);
        var hashBytes = SHA256.HashData(bytes);

        StringBuilder sb = new();
        
        foreach(var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}