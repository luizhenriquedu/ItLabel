using System.ComponentModel;
using System.Text;
using CSharpGit.Exceptions;
using CSharpGit.Utils;

namespace CSharpGit.Services;
/// <summary>
/// Tree manager class
/// </summary>
public static class TreeService
{

    public static async Task<string> BuildCommitTree(string commitSha)
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);

        var commitFolder = commitSha.Substring(0, 2);
        var commitFile = commitSha.Substring(2, commitSha.Length - 2);
        // caralho que dificil
        var commitContent = await File.ReadAllLinesAsync($"{directory}/objects/{commitFolder}/{commitFile}");
        var splitedCommitContent = commitContent[0].Split(" ");
        return "";
    }
    /// <summary>
    /// Write tree
    /// </summary>
    /// <param name="name">tree folder name (probably its just root)</param>
    /// <param name="tree">the tree index object</param>
    /// <returns>The tree hash</returns>
    public static async Task<string> WriteTree(string name, Dictionary<string,object> tree)
    {
        string hash = ShaHashService.GenerateHash(Encoding.ASCII.GetBytes(DateTimeOffset.Now.ToString()+name));;
        foreach (var element in tree)
        {
            if (element.Value is Dictionary<string, object> subdir)
            {
                var dirSha = await WriteTree(element.Key, subdir);
                var buffer = $"tree {dirSha} {element.Key}\n";
                var bufferByte = Encoding.UTF8.GetBytes(buffer);
                var compressedBuffer = CsGitBlobService.CreateCsGitBlob(bufferByte);
                await ObjectService.WriteObject(hash, compressedBuffer);
            }
            else
            {
                var buffer = $"blob {element.Value} {element.Key}\n";
                var bufferByte = Encoding.UTF8.GetBytes(buffer);
                var compressedBuffer = CsGitBlobService.CreateCsGitBlob(bufferByte);
                await ObjectService.WriteObject(hash, compressedBuffer);
            }
        }

        return hash;
    }
    /// <summary>
    /// prints a tree
    /// </summary>
    /// <param name="tree">the tree object to print</param>
    /// <param name="indent">just whitespace</param>
    public static void PrintTree(Dictionary<string,object> tree, string indent)
    {
       
        foreach (var key in tree.Keys)
        {
            if (tree[key] is string sha) //vsfd
                Console.WriteLine($"{indent}{key}: {sha}");
            else if (tree[key] is Dictionary<string, object> subTree)
            {
                Console.WriteLine($"{indent}{key}/");
                PrintTree(subTree, indent + "  ");
            }
            
        }
    }
    /// <summary>
    ///     Reads the index file and builds the tree object
    /// </summary>
    /// <returns>Returns a dictionary representing the file tree</returns>
    /// <exception cref="NothingToCommitException"></exception>
    public static Dictionary<string,object> IndexTree(string[] lines)
    {
        var tree = new Dictionary<string, object>();
        
        foreach (var line in lines)
        {
            var splitLine = line.Split(" ");
            var pathSegments = splitLine[1].Split("/");
            var sha = splitLine[0];
            var current = tree;
            for (var i = 0; i < pathSegments.Length; i++)
            {
                var segment = pathSegments[i];
                
                if (i == pathSegments.Length - 1)
                    current[segment] = sha;
                
                else
                {
                    if (!current.ContainsKey(segment))
                        current[segment] = new Dictionary<string, object>();
                    current = (Dictionary<string,object>)current[segment];
                }
            }
        }
        return tree;
    }
}