using CSharpGit.Exceptions;
using CSharpGit.Services;
using CSharpGit.Utils;

namespace CSharpGit.Commands;

public class CommitCommand
{
    public static async ValueTask Commit(string? message)
    {
        if (message is null)
            throw new NoMessageToCommitException("No message provided");
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        if(directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        var tree = await TreeService.IndexTree();
        await TreeService.BuildTree("root", tree);

        await IndexUtils.ClearIndexFile(directory);
    }
}