using GitUIPluginInterfaces;
using System.Text;

namespace GitExTaskManger.Services;
internal class GitSolutionFileProvider : ISolutionFileProvider
{
    private readonly string rootPath;
    private readonly IExecutable executor;

    public GitSolutionFileProvider(string rootPath, IExecutable executor)
    {
        this.rootPath = rootPath;
        this.executor = executor;
    }

    public async Task<IReadOnlyCollection<string>> GetListAsync(bool isTopLevelSearchOnly, bool includeWorkspaces)
    {
        var command = "ls-files -cz *.sln" + (includeWorkspaces ? " *.code-workspace" : "");
        var process = executor.Start(command, redirectOutput: true, outputEncoding: Encoding.Default);
        var output = await process.StandardOutput.ReadToEndAsync();

        var result = output.Split(new[] { '\0' }, System.StringSplitOptions.RemoveEmptyEntries)
            .Where(x => !isTopLevelSearchOnly || !x.Contains('/'))
            .Select(x => Path.Combine(rootPath, x))
            .Where(File.Exists)
            .ToArray();

        return result;
    }
}

internal interface ISolutionFileProvider
{
}