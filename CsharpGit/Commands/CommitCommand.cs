using System.Text;
using CSharpGit.Exceptions;
using CSharpGit.Services;
using CSharpGit.Utils;

namespace CSharpGit.Commands;

public class CommitCommand
{

    private static string BuildCommit(string message, string tree)
    {
        var hash = ShaHashService.GenerateHash(DateTimeOffset.Now + "Commiter");
        var buffer = $"tree {tree}\nAuthor: Commiter\n{message}";
        var bytes = Encoding.UTF8.GetBytes(buffer);
        var obj = ObjectService.WriteObject(hash, bytes);

        return hash;
    }
    public static async ValueTask Commit(string? message)
    {
        if (message is null)
            throw new NoMessageToCommitException("No message provided");
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        if(directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        var tree = await TreeService.IndexTree();
        var treeHash = await TreeService.BuildTree("root", tree);
        var hash = BuildCommit(message, treeHash);
        await IndexUtils.ClearIndexFile(directory);
    }
}