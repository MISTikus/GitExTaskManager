namespace GitExtensions.TaskManger.Services;
internal class FileProvider : IFileProvider
{
    private readonly string rootPath;

    public FileProvider(string rootPath)
    {
        this.rootPath = rootPath;
    }

    public async Task CreateAsync(string fileName, string body, CancellationToken cancellation)
    {
        cancellation.ThrowIfCancellationRequested();

        var path = Path.Combine(rootPath, fileName);
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        await File.WriteAllTextAsync(path, body, cancellation);
    }

    public Task DeleteAsync(string fileName, CancellationToken cancellation)
    {
        cancellation.ThrowIfCancellationRequested();

        var path = Path.Combine(rootPath, fileName);
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            return Task.CompletedTask;
        File.Delete(path);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<string>> GetListAsync(string path, CancellationToken cancellation)
    {
        cancellation.ThrowIfCancellationRequested();

        var info = new DirectoryInfo(Path.Combine(rootPath, path));
        if (!info.Exists)
            return Enumerable.Empty<string>();

        var files = info.EnumerateFiles();
        var tasks = files.Select(async f => await File.ReadAllTextAsync(f.FullName, cancellation)).ToArray();
        await Task.WhenAll(tasks);

        return tasks.Select(x => x.Result).ToList();
    }
}

internal interface IFileProvider
{
    Task CreateAsync(string fileName, string body, CancellationToken cancellation);
    Task DeleteAsync(string fileName, CancellationToken cancellation);
    Task<IEnumerable<string>> GetListAsync(string path, CancellationToken cancellation);
}