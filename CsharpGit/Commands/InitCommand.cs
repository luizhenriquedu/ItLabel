using System.Diagnostics;
using CSharpGit.Exceptions;
using CSharpGit.Services;

namespace CSharpGit.Commands;
///<summary>
///  Init CLI Command
///</summary>
public static class InitCommand
{
    /// <summary>
    ///     Init method
    /// </summary>
    /// <exception cref="RepositoryAlreadyInitiatedException"></exception>
    public static async ValueTask Init()
    {
        if (CsGitService.GetCsGitDirectory(Environment.CurrentDirectory) != null)
            throw new RepositoryAlreadyInitiatedException("Repository already initiated");
        await CsGitService.Initialize();
        Console.WriteLine($"Initiated empty CsGit repository in {Environment.CurrentDirectory}");
    }
}