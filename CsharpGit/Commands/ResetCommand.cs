using CSharpGit.Services;
using CSharpGit.Utils;

namespace CSharpGit.Commands;

public static class ResetCommand
{
    public static async ValueTask ResetToHead()
    {
        var headCommit = await ReferenceUtil.ReadReference();
        await Task.CompletedTask;
    }
}