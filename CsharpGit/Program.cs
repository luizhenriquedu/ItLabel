using System.CommandLine;
using CSharpGit.Commands;

namespace CSharpGit;
/*
 * <summary>
 *      Program class
 * </summary>
 */
public class Program
{
    public async static Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand();

        var addCommandOption = new Option<FileInfo>(
            name: "--file",
            description: "Adds file to the versioning tracking"
            );
        var commitCommandOption = new Option<string>(
            name: "-m",
            description: "Message for the commit"
            );
        var initCommand = new Command("init", "Init local repository");
        var addCommand = new Command("add", "Add file to tracking");
        var commitCommand = new Command("commit", "Commits an alteration");
        
        addCommand.AddOption(addCommandOption);
        commitCommand.AddOption(commitCommandOption);
        
        initCommand.SetHandler(async () => await InitCommand.Init());
        addCommand.SetHandler(async (file) => await AddCommand.Add(file), addCommandOption);
        commitCommand.SetHandler(async (message) => await CommitCommand.Commit(message), commitCommandOption);
        rootCommand.AddCommand(initCommand);
        rootCommand.AddCommand(addCommand);
        rootCommand.AddCommand(commitCommand);
        return await rootCommand.InvokeAsync(args);
    }
}
