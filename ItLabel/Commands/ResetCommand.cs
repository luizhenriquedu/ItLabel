using ItLabel.Services;
using ItLabel.Utils;

namespace ItLabel.Commands;

public static class ResetCommand
{
    public static async ValueTask ResetToHead()
    {
        var headCommit = await ReferenceUtil.ReadReference();
        await Task.CompletedTask;
    }
}