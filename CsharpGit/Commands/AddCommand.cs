using CSharpGit.Exceptions;
using CSharpGit.Services;

namespace CSharpGit.Commands;

public class AddCommand
{
    public static async ValueTask Add(FileInfo path)
    {
        if (!Directory.Exists(".csgit"))
            throw new RepositoryNotFoundException("Repository not found");
        var content = await File.ReadAllTextAsync(path.FullName);
        var hash = ShaHashService.GenerateHash(content);

        var compressedBlobContent = await BlobCompressorService.CompressToBlob(path.FullName);
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        var hash2CharPath = hash.Substring(0, 2);
        var hashPathContinue = hash.Substring(2, hash.Length - 2);

        var dir = CsGitService.CreateObjectHashDirectory(hash2CharPath);
        
       await File.WriteAllBytesAsync(Path.Combine(dir, hashPathContinue), compressedBlobContent);

       var buffer = $"{hash} {path.FullName}\n";
       await File.AppendAllTextAsync(Path.Combine(directory, "index"), buffer);
    }
}