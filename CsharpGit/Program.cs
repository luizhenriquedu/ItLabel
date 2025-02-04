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
        var initCommand = new Command("init", "Init local repository");
        var addCommand = new Command("add", "Add file to tracking");
        
        addCommand.AddOption(addCommandOption);
        
        initCommand.SetHandler(async () => await InitCommand.Init());
        addCommand.SetHandler(async (file) => await AddCommand.Add(file), addCommandOption);
        rootCommand.AddCommand(initCommand);
        rootCommand.AddCommand(addCommand);
        return await rootCommand.InvokeAsync(args);
    }
}
