using ItLabel.Exceptions;

namespace ItLabel.Services;
/// <summary>
/// CsGit Repository Manage Class
/// </summary>
public static class ItLabelDirectoryService
{
    /// <summary>
    /// Initialize Cs Git repository
    /// </summary>
    public static async ValueTask Initialize()
    {
        var csgitDir = Directory.CreateDirectory(".it");
        Directory.CreateDirectory(".it/objects/info");
        Directory.CreateDirectory(".it/objects/pack");
        Directory.CreateDirectory(".it/refs/heads");
        Directory.CreateDirectory(".it/refs/tags");
        Environment.SetEnvironmentVariable("PROJECT_CSGIT_DIR",csgitDir.FullName);
        var head = File.Open(".it/HEAD", FileMode.Create);
        head.Close();
        var main = File.Open(".it/refs/heads/main", FileMode.Create);
        main.Close();
        await File.WriteAllTextAsync(".it/HEAD", "ref: refs/heads/main");
    }

    /// <summary>
    /// Create object directory
    /// </summary>
    /// <example>
    ///     <code>
    ///         CreateObjectHashDirectory("directory name");
    ///           <br/>
    ///          .csgit<br/>    
    ///              L objects<br/>
    ///                  L directory_name
    ///     </code>
    ///      
    /// </example>
    /// <param name="name">The directory name</param>
    /// <returns></returns>
    /// <exception cref="RepositoryNotFoundException">Not found repository exception</exception>
    public static string CreateObjectHashDirectory(string name)
    {
        var directory = GetItLabelDirectory(Environment.CurrentDirectory);
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        var dir = Directory.CreateDirectory(Path.Combine(directory, "objects", name));
        return dir.FullName;
    }
    /// <summary>
    /// Get .csgit directory
    /// </summary>
    /// <param name="startPath">The path do start the search recursively</param>
    /// <returns>Return the .csgit directory path</returns>

    public static string? GetItLabelDirectory(string startPath)
    {
        var currentDirectory = new DirectoryInfo(startPath);

        while (currentDirectory.Parent != null)
        {
            if (Directory.Exists(Path.Combine(currentDirectory.FullName, ".it")))
                return Path.Combine(currentDirectory.FullName, ".it");
            currentDirectory = currentDirectory.Parent;
        }

        return null;
    }
}