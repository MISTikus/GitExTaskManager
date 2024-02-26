using GitExtensions.TaskManager.Domain;

namespace GitExtensions.TaskManager.Services;

internal class FileProvider : IFileProvider
{
    private const int titleLength = 15;
    private readonly string rootPath;
    private readonly string fileExt;
    private static readonly string[] defaultFolders = new[] { "backlog", "issue" };

    public FileProvider(string rootPath, string fileExt)
    {
        this.rootPath = rootPath;
        this.fileExt = fileExt;

    }

    public async Task SaveAsync(Item item, string subfolder, string body, CancellationToken cancellation)
    {
        cancellation.ThrowIfCancellationRequested();

        var folder = GetFolderName(item.Type, item.Title, subfolder ?? "");
        var fileName = GetFileName(item);
        var oldFileName = GetOldFileName(item);

        var path = Path.Combine(rootPath, folder, fileName);

        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        await File.WriteAllTextAsync(path, body, cancellation);

        if (oldFileName != fileName)
        {
            var oldPath = Path.Combine(rootPath, folder, oldFileName);
            File.Delete(oldPath);
        }
    }

    public Task DeleteAsync(Item item, string subfolder, CancellationToken cancellation)
    {
        cancellation.ThrowIfCancellationRequested();

        var folder = GetFolderName(item.Type, item.Title, subfolder ?? "");
        var fileName = GetFileName(item);
        if (item.Type == ItemType.Epic)
        {
            Directory.Delete(Path.Combine(rootPath, folder), true);
        }
        else
        {
            var path = Path.Combine(rootPath, folder, fileName);
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                return Task.CompletedTask;
            File.Delete(path);
        }
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<string>> GetListAsync(ItemType itemType, string subfolder, CancellationToken cancellation)
    {
        cancellation.ThrowIfCancellationRequested();

        var path = GetFolderName(itemType, null, subfolder ?? "");
        var info = new DirectoryInfo(Path.Combine(rootPath, path));
        if (!info.Exists)
            return Enumerable.Empty<string>();

        var files = info.EnumerateFiles();
        var tasks = files.Select(async f => await File.ReadAllTextAsync(f.FullName, cancellation)).ToArray();
        await Task.WhenAll(tasks);

        return tasks.Select(x => x.Result).ToList();
    }

    public IEnumerable<(string name, DateTime created)> GetFolders(string path = null)
    {
        var info = new DirectoryInfo(Path.Combine(rootPath, path ?? ""));
        try
        {
            var folders = info.EnumerateDirectories();
            return folders.Where(x => !defaultFolders.Contains(x.Name)).Select(x => (x.Name, x.CreationTime)).ToList();
        }
        catch (DirectoryNotFoundException)
        {
            return Array.Empty<(string name, DateTime created)>();
        }
        catch (Exception e)
        {
            return Array.Empty<(string name, DateTime created)>();
        }
    }

    public void CreateFolder(string folder)
    {
        var path = Path.Combine(rootPath, folder);
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
    }

    public void RenameFolder(string originalFolder, string folder)
    {
        var originalPath = Path.Combine(rootPath, originalFolder);
        var newPath = Path.Combine(rootPath, folder);

        if (Directory.Exists(Path.GetDirectoryName(originalPath)))
            Directory.Move(originalPath, newPath);
        else if (!Directory.Exists(newPath))
            Directory.CreateDirectory(Path.GetDirectoryName(newPath));
        else
            throw new InvalidOperationException($"Can't rename {originalFolder} to {folder}");
    }

    private string GetFileName(Item item) => $"{item.Title
            .Replace(' ', '_')
            [..(item.Title.Length >= titleLength ? titleLength : item.Title.Length)]}_{item.Created:yyMMddhhmmss}.{this.fileExt}";

    private string GetOldFileName(Item item) => $"{item.GetOldTitle()
            .Replace(' ', '_')
            [..(item.GetOldTitle().Length >= titleLength ? titleLength : item.GetOldTitle().Length)]}_{item.Created:yyMMddhhmmss}.{this.fileExt}";

    private static string GetFolderName(ItemType type, string title, params string[] subfolders)
        => type == ItemType.Epic
        ? Path.Combine(subfolders.Prepend(title).ToArray())
        : Path.Combine(subfolders.Prepend(type.ToString().ToLower()).ToArray());

    public string GetEpicFolder(string folder) => folder;
}

internal interface IFileProvider
{
    Task SaveAsync(Item item, string subfolder, string body, CancellationToken cancellation);
    Task DeleteAsync(Item item, string subfolder, CancellationToken cancellation);
    Task<IEnumerable<string>> GetListAsync(ItemType itemType, string subfolder, CancellationToken cancellation);
    IEnumerable<(string name, DateTime created)> GetFolders(string path = null);
    void CreateFolder(string folder);
    void RenameFolder(string originalFolder, string folder);

    string GetEpicFolder(string folder);
}