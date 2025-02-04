using CSharpGit.Exceptions;
using CSharpGit.Services;

namespace CSharpGit.Commands;

public class AddCommand
{
    public static async ValueTask Add(FileInfo path)
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        var parentDirectory = new DirectoryInfo(directory).Parent;
        var content = await File.ReadAllTextAsync(path.FullName);
        var hash = ShaHashService.GenerateHash(content);
        
        var compressedBlobContent = await BlobCompressorService.CompressFileToBlob(path.FullName);

        await ObjectService.WriteObject(hash, compressedBlobContent);
        
        var buffer = $"{hash} {Path.GetRelativePath(parentDirectory.FullName, path.FullName)}\n";
        await File.AppendAllTextAsync(Path.Combine(directory, "index"), buffer);
    }
}