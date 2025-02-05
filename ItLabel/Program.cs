using System.CommandLine;
using ItLabel.Commands;

namespace ItLabel;
/// <summary>
///     Program class
/// </summary>
public class Program
{
    /// <summary>
    ///     Start program method
    /// </summary>
    /// <param name="args"></param>
    /// <returns>Returns a Task int</returns>
    public static async Task<int> Main(string[] args)
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
        var resetHead = new Command("reset", "resets branch to a commit");
        var resetToHead = new Command("HEAD", "resets to the head commit");
        
        
        resetHead.AddCommand(resetToHead);
            
        addCommand.AddOption(addCommandOption);
        commitCommand.AddOption(commitCommandOption);
        
        initCommand.SetHandler(async () => await InitCommand.Init());
        addCommand.SetHandler(async (file) => await AddCommand.Add(file), addCommandOption);
        commitCommand.SetHandler(async (message) => await CommitCommand.Commit(message), commitCommandOption);
        resetToHead.SetHandler(async () => await ResetCommand.ResetToHead());
        
        
        rootCommand.AddCommand(initCommand);
        rootCommand.AddCommand(addCommand);
        rootCommand.AddCommand(commitCommand);
        rootCommand.AddCommand(resetHead);
        return await rootCommand.InvokeAsync(args);
    }
}
