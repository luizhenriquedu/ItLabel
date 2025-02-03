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
        var initCommand = new Command("init", "Init local repository");
        initCommand.SetHandler(async () => await InitCommand.Init());
        
        rootCommand.AddCommand(initCommand);

        return await rootCommand.InvokeAsync(args);
    }
}
