using CSharpGit.Utils;

namespace CSharpGit.Commands;

public class ResetCommand
{
    public static async ValueTask ResetToHead()
    {
        var currentBranch = await ReferenceUtil.GetCurrentBranch();
        var headCommit = await ReferenceUtil.ReadReference();
        
        Console.WriteLine(headCommit);
    }
}