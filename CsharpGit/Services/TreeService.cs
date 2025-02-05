using System.ComponentModel;
using System.Text;
using CSharpGit.Exceptions;
using CSharpGit.Utils;

namespace CSharpGit.Services;

public class TreeService
{
    public static async Task<string> BuildTree(string name, Dictionary<string,object> tree)
    {
        var hash = ShaHashService.GenerateHash(DateTimeOffset.Now.ToString()+name);
        foreach (var element in tree)
        {
            if (element.Value is Dictionary<string, object> subdir)
            {
                var dirSha = await BuildTree(element.Key, subdir);
                var bytes = Encoding.UTF8.GetBytes($"tree {dirSha} {element.Key}");
                await ObjectService.WriteObject(hash, bytes);
            }
            else
            {
                var bytes = Encoding.UTF8.GetBytes($"blob {element.Value} {element.Key}");
                await ObjectService.WriteObject(hash, bytes);
            }
        }

        return hash;
    }
    public static void PrintTree(Dictionary<string,object> tree, string indent)
    {
       
        foreach (var key in tree.Keys)
        {
            if (tree[key] is string sha)
                Console.WriteLine($"{indent}{key}: {sha}");
            else if (tree[key] is Dictionary<string, object> subTree)
            {
                // Se for um diretório, chama recursivamente
                Console.WriteLine($"{indent}{key}/");
                PrintTree(subTree, indent + "  ");
            }
        }
    }
    
    public static async Task<Dictionary<string,object>> IndexTree()
    {
        var directory = CsGitService.GetCsGitDirectory(Environment.CurrentDirectory);
        var lines = await IndexUtil.ReadIndexFile(directory!);
        
        if (lines.Length == 0)
            throw new NothingToCommitException("Nothing to commit");
        
        var tree = new Dictionary<string, object>();
        
        foreach (var line in lines)
        {
            var splitLine = line.Split(" ");
            var pathSegments = splitLine[1].Split("/");
            var sha = splitLine[0];
            var current = tree;
            for (int i = 0; i < pathSegments.Length; i++)
            {
                var segment = pathSegments[i];
                if (i == pathSegments.Length - 1)
                    current[segment] = sha;
                else
                {
                    if (!current.ContainsKey(segment))
                    {
                        current[segment] = new Dictionary<string, object>();
                    }

                    current = (Dictionary<string,object>)current[segment];
                }
            }
        }

        return tree;
    }
}