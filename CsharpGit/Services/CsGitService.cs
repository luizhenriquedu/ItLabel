using CSharpGit.Exceptions;

namespace CSharpGit.Services;
/// <summary>
/// CsGit Repository Manage Class
/// </summary>
public static class CsGitService
{
    /// <summary>
    /// Initialize Cs Git repository
    /// </summary>
    public static async ValueTask Initialize()
    {
        var csgitDir = Directory.CreateDirectory(".csgit");
        Directory.CreateDirectory(".csgit/objects/info");
        Directory.CreateDirectory(".csgit/objects/pack");
        Directory.CreateDirectory(".csgit/refs/heads");
        Directory.CreateDirectory(".csgit/refs/tags");
        Environment.SetEnvironmentVariable("PROJECT_CSGIT_DIR",csgitDir.FullName);
        var head = File.Open(".csgit/HEAD", FileMode.Create);
        head.Close();
        var main = File.Open(".csgit/refs/heads/main", FileMode.Create);
        main.Close();
        await File.WriteAllTextAsync(".csgit/HEAD", "ref: refs/heads/main");
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
        var directory = GetCsGitDirectory(Environment.CurrentDirectory);
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

    public static string? GetCsGitDirectory(string startPath)
    {
        var currentDirectory = new DirectoryInfo(startPath);

        while (currentDirectory.Parent != null)
        {
            if (Directory.Exists(Path.Combine(currentDirectory.FullName, ".csgit")))
                return Path.Combine(currentDirectory.FullName, ".csgit");
                
            currentDirectory = currentDirectory.Parent;
        }

        return null;
    }
}