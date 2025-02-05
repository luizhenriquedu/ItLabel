using System.Text;
using ItLabel.Exceptions;
using ItLabel.Services;

namespace ItLabel.Commands;

public static class AddCommand
{
    public static async ValueTask Add(FileInfo path)
    {
        var directory = ItLabelDirectoryService.GetItLabelDirectory(Environment.CurrentDirectory);
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        
        var parentDirectory = new DirectoryInfo(directory).Parent;
        var content = await File.ReadAllTextAsync(path.FullName);

        var fileContentBytes = Encoding.ASCII.GetBytes(content);
        
        var hash = ShaHashService.GenerateHash(fileContentBytes);
        
        var compressedBlobContent = ItLabelBlobService.CreateCsGitBlob(fileContentBytes);
        
        var rootRelative = Path.GetRelativePath(parentDirectory!.FullName, Environment.CurrentDirectory);
        await ObjectService.WriteObject(hash, compressedBlobContent);
        
        var buffer = $"{hash} {Path.GetRelativePath(parentDirectory!.FullName, path.FullName)}\n";
        await File.AppendAllTextAsync(Path.Combine(directory, "index"), buffer);
    }
}