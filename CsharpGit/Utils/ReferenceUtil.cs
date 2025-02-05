using CSharpGit.Services;

namespace CSharpGit.Utils;

public class ReferenceUtil
{
    public static async Task<string> ReadReference()
    {
        var dir = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        var currentBranch = await GetCurrentBranch();
        
        
        return await File.ReadAllTextAsync(dir+$"/{currentBranch}");
    }

    public static async ValueTask WriteReference(string branch, string sha)
    {
        var dir = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        await File.WriteAllTextAsync(dir+$"/{branch}", sha);
    }

    public static async Task<string> GetCurrentBranch()
    {
        var dir = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        var headContent = await File.ReadAllTextAsync(dir + "/HEAD");
        return headContent.Split(" ")[1];
    }
    public static async ValueTask UpdateReference(string commitSha)
    {
        var currentBranch = await GetCurrentBranch();   
        await WriteReference(currentBranch, commitSha);
    }
}