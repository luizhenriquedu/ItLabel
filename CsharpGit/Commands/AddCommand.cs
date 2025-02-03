using CSharpGit.Exceptions;
using CSharpGit.Utils;

namespace CSharpGit.Commands;

public class AddCommand
{
    public static async ValueTask Add(FileInfo path)
    {
        if (!Directory.Exists(".csgit"))
            throw new RepositoryNotFoundException("Repository not found");

        var hash = ShaHash.GenerateHash(path.FullName);
        Console.WriteLine(hash);
    }
}