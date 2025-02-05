using System.Diagnostics;
using ItLabel.Exceptions;
using ItLabel.Services;

namespace ItLabel.Commands;
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
        if (ItLabelDirectoryService.GetItLabelDirectory(Environment.CurrentDirectory) != null)
            throw new RepositoryAlreadyInitiatedException("Repository already initiated");
        await ItLabelDirectoryService.Initialize();
        Console.WriteLine($"Initiated empty CsGit repository in {Environment.CurrentDirectory}");
    }
}