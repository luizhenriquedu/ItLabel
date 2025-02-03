using System.Diagnostics;
using CSharpGit.Exceptions;

namespace CSharpGit.Commands;
///<summary>
///  Init CLI Command
///</summary>
public class InitCommand
{
    /// <summary>
    ///     Init method
    /// </summary>
    /// <exception cref="RepositoryAlreadyInitiatedException"></exception>
    public static async ValueTask Init()
    {
        if (Directory.Exists(".csgit")) 
            throw new RepositoryAlreadyInitiatedException("Repository already initiated");
        Directory.CreateDirectory(".csgit");
        Directory.CreateDirectory(".csgit/objects/info");
        Directory.CreateDirectory(".csgit/objects/pack");
        Directory.CreateDirectory(".csgit/refs/heads");
        Directory.CreateDirectory(".csgit/refs/tags");

       var file = File.Open(".csgit/HEAD", FileMode.Create);
       file.Close();
       await File.WriteAllTextAsync(".csgit/HEAD", "ref: refs/heads/main");
       
       Console.WriteLine($"Initiated empty CsGit repository in {Environment.CurrentDirectory}");
    }
}