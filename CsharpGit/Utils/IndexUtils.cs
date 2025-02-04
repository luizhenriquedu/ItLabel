namespace CSharpGit.Utils;

public class IndexUtils
{
    public static async Task<string[]> ReadIndexFile(string dir)
    {
        return await File.ReadAllLinesAsync(dir+"/index");
    }

    public static async ValueTask ClearIndexFile(string dir)
    {
        await File.WriteAllTextAsync(dir+"/index", String.Empty);
    }
}