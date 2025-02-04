using System.Security.Cryptography;
using System.Text;

namespace CSharpGit.Services;

public class ShaHashService
{
    public static string GenerateHash(string buffer)
    {
        var sha = SHA256.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(buffer);
        byte[] hashBytes = sha.ComputeHash(bytes);

        StringBuilder sb = new();
        
        foreach(var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}