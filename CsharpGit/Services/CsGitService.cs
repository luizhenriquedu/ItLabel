using CSharpGit.Exceptions;

namespace CSharpGit.Services;

public class CsGitService
{
    public static async ValueTask Initialize()
    {
        var csgitDir = Directory.CreateDirectory(".csgit");
        Directory.CreateDirectory(".csgit/objects/info");
        Directory.CreateDirectory(".csgit/objects/pack");
        Directory.CreateDirectory(".csgit/refs/heads");
        Directory.CreateDirectory(".csgit/refs/tags");
        Environment.SetEnvironmentVariable("PROJECT_CSGIT_DIR",csgitDir.FullName);
        var file = File.Open(".csgit/HEAD", FileMode.Create);
        file.Close();
        await File.WriteAllTextAsync(".csgit/HEAD", "ref: refs/heads/main");
    }

    public static string CreateObjectHashDirectory(string shortHash)
    {
        var directory = GetCsGitDirectory(Environment.CurrentDirectory);
        if (directory is null)
            throw new RepositoryNotFoundException("Repository not found");
        var dir = Directory.CreateDirectory(Path.Combine(directory, "objects", shortHash));
        return dir.FullName;
    }
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