using GitExtensions.TaskManger.Extensions;
using GitExtensions.TaskManger.Services;
using GitExtensions.TaskManger.Utils;

namespace GitExtensions.TaskManger.Domain;

internal class TaskManger : ITaskManger
{
    private const int titleLength = 15;
    private const string baseFolder = ".tasks";
    private readonly IFileProvider fileProvider;
    private readonly ISerializer serializer;
    private readonly string fileExt;
    private List<Issue> issues = new();
    private List<Backlog> backlogs = new();

    public TaskManger(IFileProvider fileProvider, ISerializer serializer, string fileExt)
    {
        this.fileProvider = fileProvider;
        this.serializer = serializer;
        this.fileExt = fileExt;
    }

    public Issue[] GetIssues(bool includeResolved)
        => issues.WhereIf(!includeResolved, x => x.State != ItemState.Resolved).ToArray();
    public Backlog[] GetBacklogs(bool includeResolved)
        => backlogs.WhereIf(!includeResolved, x => x.State != ItemState.Resolved).ToArray();

    public async Task ResolveAsync(Item item)
    {
        item.Resolve();

        var sourceFolder = $"{baseFolder}/{GetFolderName(item.Type)}";
        var destinationFolder = $"{baseFolder}/{GetFolderName(item.Type)}/resolved";
        var fileName = GetFileName(item, this.fileExt);
        var body = this.serializer.Serialize(item);
        await fileProvider.DeleteAsync($"{sourceFolder}/{fileName}", default);
        await fileProvider.CreateAsync($"{destinationFolder}/{fileName}", body, default);
    }

    public async Task RemoveAsync(Item item)
    {
        var sourceFolder = item.State switch
        {
            ItemState.Resolved => $"{baseFolder}/{GetFolderName(item.Type)}/resolved",
            _ => $"{baseFolder}/{GetFolderName(item.Type)}"
        };
        var fileName = GetFileName(item, this.fileExt);
        await fileProvider.DeleteAsync($"{sourceFolder}/{fileName}", default);
    }

    public async Task AddAsync(Item item)
    {
        var folder = $"{baseFolder}/{GetFolderName(item.Type)}";
        var fileName = GetFileName(item, this.fileExt);
        var body = this.serializer.Serialize(item);
        await fileProvider.CreateAsync($"{folder}/{fileName}", body, default);
    }

    public async Task ReloadAsync()
    {
        this.issues = await LoadAsync<Issue>(ItemType.Issue);
        this.backlogs = await LoadAsync<Backlog>(ItemType.Backlog);
    }

    private static string GetFolderName(ItemType type) => type.ToString().ToLower();
    private static string GetFileName(Item item, string ext) => $"{item.Title
            .Replace(' ', '_')
            [..(item.Title.Length >= titleLength ? titleLength : item.Title.Length)]}_{item.Created:yyMMddhhmmss}.{ext}";

    private async Task<List<T>> LoadAsync<T>(ItemType issue) where T : Item
    {
        var data = await fileProvider.GetListAsync($"{baseFolder}/{GetFolderName(issue)}", default).ConfigureAwait(false);
        return CrutchForLineEndings(data.Select(this.serializer.Deserialize<T>).ToList());
    }

    /// <summary>
    /// https://github.com/aaubry/YamlDotNet/issues/867
    /// </summary>
    private static List<T> CrutchForLineEndings<T>(List<T> items) where T : Item
    {
        for (var i = 0; i < items.Count; i++)
        {
            items[i] = items[i] with { Description = items[i].Description.Replace("\n", "\r\n") };
            foreach (var key in items[i].Comments.Keys)
            {
                items[i].Comments[key] = items[i].Comments[key].Replace("\n", "\r\n");
            }
        }
        return items;
    }
}

internal interface ITaskManger
{
    Issue[] GetIssues(bool includeResolved);
    Backlog[] GetBacklogs(bool includeResolved);

    Task ReloadAsync();

    Task AddAsync(Item item);
    Task ResolveAsync(Item item);
    Task RemoveAsync(Item item);
}