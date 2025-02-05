using CSharpGit.Exceptions;
using CSharpGit.Services;

namespace CSharpGit.Utils;

/// <summary>
/// Useful index utilities
/// </summary>

public static class IndexUtil
{
    /// <summary>
    /// Reads the index file
    /// </summary>
    /// <returns>A string array with the index file lines</returns>
    public static async Task<string[]> ReadIndexFile()
    {
        var dir = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        return await File.ReadAllLinesAsync(dir+"/index");
    }

    /// <summary>
    /// Clears the index file
    /// </summary>
    /// <exception cref="RepositoryNotFoundException">Thrown if repository is not found</exception>
    public static async ValueTask ClearIndexFile()
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        if(directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        await File.WriteAllTextAsync(directory+"/index", String.Empty);
    }
}