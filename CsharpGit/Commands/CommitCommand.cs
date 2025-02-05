using System.Text;
using CSharpGit.Exceptions;
using CSharpGit.Services;
using CSharpGit.Utils;

namespace CSharpGit.Commands;

public class CommitCommand
{

    private static string BuildCommit(string message, string tree, string lastCommit)
    {
        string lastCommitSha;
        if (lastCommit.Length == 0)
            lastCommitSha = "/NONE/";
        else lastCommitSha = lastCommit;
        var hash = ShaHashService.GenerateHash(DateTimeOffset.Now + "Commiter");
        
        var buffer = $"tree {tree}\nAuthor: Commiter\n{message}\nLast commit: {lastCommitSha}";
        var bytes = Encoding.UTF8.GetBytes(buffer);
        var obj = ObjectService.WriteObject(hash, bytes);

        return hash;
    }
    public static async ValueTask Commit(string? message)
    {
        if (message is null)
            throw new NoMessageToCommitException("No message provided");
        
        var tree = await TreeService.IndexTree();
        var treeHash = await TreeService.BuildTree("root", tree);
        var currentCommit = await ReferenceUtil.ReadReference();
        var hash = BuildCommit(message, treeHash, currentCommit);
        await ReferenceUtil.UpdateReference(hash);
        await IndexUtil.ClearIndexFile();
    }
}