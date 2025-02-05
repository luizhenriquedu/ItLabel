using System.Text;
using CSharpGit.Exceptions;

namespace CSharpGit.Services;
/// <summary>
///     Object manager class
/// </summary>
public static class ObjectService
{
    /// <summary>
    /// Write an object to .csgit/objects
    ///     <example>
    ///         <code>
    ///             var hash = ShaHashService.GenerateHash("my file content");
    ///             var compressedBlobContent = await BlobCompressorService.CompressFileToBlob("my file path");
    ///             ObjectService.WriteObject(hash, compressedBlobContent);
    ///             <br/>
    ///             Output:
    ///             .csgit<br/>
    ///                 L objects<br/>
    ///                     L 40<br/>
    ///                        L c32f39d968d26c3e5b269e2080775c592341ca470cd61b2f43e25b6db17707<br/>
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="compressedBlobContent"></param>
    /// <exception cref="RepositoryNotFoundException"></exception>
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

    public static async ValueTask WriteAppendToObject(string hash, byte[] appendBytes)
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        
        var hash2CharPath = hash.Substring(0, 2);
        var hashPathContinue = hash.Substring(2, hash.Length - 2); 
        
        var dir = CsGitService.CreateObjectHashDirectory(hash2CharPath);
        var bytesToString = Encoding.UTF8.GetString(appendBytes);
        await File.AppendAllTextAsync(Path.Combine(dir, hashPathContinue), bytesToString);
    }
}