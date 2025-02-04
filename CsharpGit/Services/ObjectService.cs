using CSharpGit.Exceptions;

namespace CSharpGit.Services;

public class ObjectService
{
    public static async ValueTask WriteObject(string hash, byte[] compressedBlobContent)
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        
        var hash2CharPath = hash.Substring(0, 2);
        var hashPathContinue = hash.Substring(2, hash.Length - 2); 
        
        var dir = CsGitService.CreateObjectHashDirectory(hash2CharPath);
        await File.WriteAllBytesAsync(Path.Combine(dir, hashPathContinue), compressedBlobContent);
    }
}