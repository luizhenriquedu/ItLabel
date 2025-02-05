using System.Text;
using ItLabel.Exceptions;
using ItLabel.Services;
using ItLabel.Utils;

namespace ItLabel.Commands;

public static class CommitCommand
{

    private static async Task<string> BuildCommit(string message, string tree, string lastCommit)
    {
        string lastCommitSha;
        if (lastCommit.Length == 0)
            lastCommitSha = "/NONE/";
        else lastCommitSha = lastCommit;
        var hash = ShaHashService.GenerateHash(Encoding.ASCII.GetBytes(DateTimeOffset.Now + "Commiter"));
        
        var buffer = $"tree {tree}\nAuthor: Commiter\n{message}\nLast commit: {lastCommitSha}";
        var bytes = Encoding.ASCII.GetBytes(buffer);
        var compressedBytes = ItLabelBlobService.CreateCsGitBlob(bytes);
        await ObjectService.WriteObject(hash, compressedBytes);

        return hash;
    }
    public static async ValueTask Commit(string? message)
    {
        if (message is null)
            throw new NoMessageToCommitException("No message provided");
        var lines = await IndexUtil.ReadIndexFile();
        if (lines.Length == 0)
            throw new NothingToCommitException("Nothing to commit");
        var tree =  TreeService.IndexTree(lines);
        TreeService.PrintTree(tree, "");
        var treeHash = await TreeService.WriteTree("root", tree);
        var currentCommit = await ReferenceUtil.ReadReference();
        var hash = await BuildCommit(message, treeHash, currentCommit);
        await ReferenceUtil.UpdateReference(hash);
        await IndexUtil.ClearIndexFile();
    }
}