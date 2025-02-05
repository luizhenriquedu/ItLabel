using CSharpGit.Exceptions;
using CSharpGit.Services;

namespace CSharpGit.Utils;

public class IndexUtil
{
    public static async Task<string[]> ReadIndexFile(string dir)
    {
        return await File.ReadAllLinesAsync(dir+"/index");
    }

    public static async ValueTask ClearIndexFile()
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        if(directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        await File.WriteAllTextAsync(directory+"/index", String.Empty);
    }
}