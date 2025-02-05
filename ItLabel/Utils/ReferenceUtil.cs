using ItLabel.Services;

namespace ItLabel.Utils;
/// <summary>
/// Useful class with reference utilities
/// </summary>

public static class ReferenceUtil
{
    /// <summary>
    /// Reads the reference of the current branch
    /// </summary>
    /// <returns>The reference content</returns>
    public static async Task<string> ReadReference()
    {
        var dir = ItLabelDirectoryService.GetItLabelDirectory(Environment.CurrentDirectory);
        var currentBranch = await GetCurrentBranch();
        
        return await File.ReadAllTextAsync(dir+$"/{currentBranch}");
    }
    /// <summary>
    /// Writes the reference
    /// </summary>
    /// <param name="branch">The reference branch</param>
    /// <param name="sha">The reference sha content</param>
    private static async ValueTask WriteReference(string branch, string sha)
    {
        var dir = ItLabelDirectoryService.GetItLabelDirectory(Environment.CurrentDirectory);
        await File.WriteAllTextAsync(dir+$"/{branch}", sha);
    }
    /// <summary>
    /// Gets the current branch
    /// </summary>
    /// <returns>Returns the current branch path</returns>
    public static async Task<string> GetCurrentBranch()
    {
        var dir = ItLabelDirectoryService.GetItLabelDirectory(Environment.CurrentDirectory);
        var headContent = await File.ReadAllTextAsync(dir + "/HEAD");
        return headContent.Split(" ")[1];
    }
    /// <summary>
    /// Updates the current branch with the current commit SHA
    /// </summary>
    /// <param name="commitSha">The commit SHA to point</param>
    public static async ValueTask UpdateReference(string commitSha)
    {
        var currentBranch = await GetCurrentBranch();   
        await WriteReference(currentBranch, commitSha);
    }
}