using System.Text;
using CSharpGit.Exceptions;
using CSharpGit.Services;

namespace CSharpGit.Commands;

public static class AddCommand
{
    public static async ValueTask Add(FileInfo path)
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        
        var parentDirectory = new DirectoryInfo(directory).Parent;
        var content = await File.ReadAllTextAsync(path.FullName);

        var fileContentBytes = Encoding.ASCII.GetBytes(content);
        
        var hash = ShaHashService.GenerateHash(fileContentBytes);
        
        var compressedBlobContent = CsGitBlobService.CreateCsGitBlob(fileContentBytes);
        
        var rootRelative = Path.GetRelativePath(parentDirectory!.FullName, Environment.CurrentDirectory);
        await ObjectService.WriteObject(hash, compressedBlobContent);
        
        var buffer = $"{hash} {Path.GetRelativePath(parentDirectory!.FullName, path.FullName)}\n";
        await File.AppendAllTextAsync(Path.Combine(directory, "index"), buffer);
    }
}