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
    private List<Issue> resolvedIssues = new();
    private List<Backlog> resolvedBacklogs = new();

    public event EventHandler<Item> ItemAdded;
    public event EventHandler<Item> ItemChanged;
    public event EventHandler<Item> ItemRemoved;
    public event EventHandler<Item> ItemResolved;

    public TaskManger(IFileProvider fileProvider, ISerializer serializer, string fileExt)
    {
        this.fileProvider = fileProvider;
        this.serializer = serializer;
        this.fileExt = fileExt;

        ItemAdded += (s, item) => AddToCache(item);
        ItemChanged += (s, item) => ReplaceInCache(item);
        ItemResolved += (s, item) => ReplaceInCache(item);
        ItemRemoved += (s, item) => RemoveFromCache(item);
    }

    public Issue[] GetIssues(bool includeResolved)
    {
        var result = issues.WhereIf(!includeResolved, x => x.State != ItemState.Resolved).ToArray();
        if (includeResolved)
            result = result.Concat(resolvedIssues).ToArray();
        return result;
    }

    public Backlog[] GetBacklogs(bool includeResolved)
    {
        var result = backlogs.WhereIf(!includeResolved, x => x.State != ItemState.Resolved).ToArray();
        if (includeResolved)
            result = result.Concat(resolvedBacklogs).ToArray();
        return result;
    }

    public async Task ResolveAsync(Item item)
    {
        item.Resolve();

        var sourceFolder = $"{baseFolder}/{GetFolderName(item.Type)}";
        var destinationFolder = $"{baseFolder}/{GetFolderName(item.Type)}/resolved";
        var fileName = GetFileName(item, this.fileExt);
        var body = this.serializer.Serialize(item);
        await fileProvider.DeleteAsync($"{sourceFolder}/{fileName}", default);
        await fileProvider.CreateAsync($"{destinationFolder}/{fileName}", body, default);

        ItemResolved?.Invoke(this, item);
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

        ItemRemoved?.Invoke(this, item);
    }

    public async Task SaveAsync(Item item)
    {
        var isChanged = item.IsChanged();

        item.Saved();

        var folder = $"{baseFolder}/{GetFolderName(item.Type)}";
        var fileName = GetFileName(item, this.fileExt);
        var body = this.serializer.Serialize(item);
        await fileProvider.CreateAsync($"{folder}/{fileName}", body, default);

        if (isChanged)
            ItemChanged?.Invoke(this, item);
        else
            ItemAdded?.Invoke(this, item);
    }

    public async Task ReloadAsync()
    {
        this.issues = await LoadAsync<Issue>(ItemType.Issue);
        this.backlogs = await LoadAsync<Backlog>(ItemType.Backlog);
        this.resolvedIssues = await LoadAsync<Issue>(ItemType.Issue, true).ConfigureAwait(false);
        this.resolvedBacklogs = await LoadAsync<Backlog>(ItemType.Backlog, true).ConfigureAwait(false);
    }

    private static string GetFolderName(ItemType type) => type.ToString().ToLower();
    private static string GetFileName(Item item, string ext) => $"{item.Title
            .Replace(' ', '_')
            [..(item.Title.Length >= titleLength ? titleLength : item.Title.Length)]}_{item.Created:yyMMddhhmmss}.{ext}";

    private async Task<List<T>> LoadAsync<T>(ItemType issue, bool resolved = false) where T : Item
    {
        var data = await fileProvider
            .GetListAsync($"{baseFolder}/{GetFolderName(issue)}{(resolved ? "/resolved" : "")}", default).ConfigureAwait(false);
        return CrutchForLineEndings(data.Select(this.serializer.Deserialize<T>).ToList());
    }

    private void AddToCache<TItem>(TItem item) where TItem : Item
    {
        switch (item.Type)
        {
            case ItemType.Issue when item is Issue issue:
                this.issues.Add(issue);
                break;
            case ItemType.Backlog when item is Backlog backlog:
                this.backlogs.Add(backlog);
                break;
        }
    }

    private void RemoveFromCache(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Issue when item is Issue issue:
                this.issues.Remove(issue);
                break;
            case ItemType.Backlog when item is Backlog backlog:
                this.backlogs.Remove(backlog);
                break;
        }
    }

    private void ReplaceInCache(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Issue when item is Issue issue:
                var issueIndex = this.issues.IndexOf(issue);
                this.issues[issueIndex] = issue;
                break;
            case ItemType.Backlog when item is Backlog backlog:
                var backlogIndex = this.backlogs.IndexOf(backlog);
                this.backlogs[backlogIndex] = backlog;
                break;
        }
    }

    /// <summary>
    /// https://github.com/aaubry/YamlDotNet/issues/867
    /// </summary>
    private static List<T> CrutchForLineEndings<T>(List<T> items) where T : Item
    {
        for (var i = 0; i < items.Count; i++)
        {
            items[i] = items[i] with { Description = items[i].Description?.Replace("\n", "\r\n") };
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
    event EventHandler<Item> ItemAdded;
    event EventHandler<Item> ItemChanged;
    event EventHandler<Item> ItemRemoved;
    event EventHandler<Item> ItemResolved;

    Issue[] GetIssues(bool includeResolved);
    Backlog[] GetBacklogs(bool includeResolved);

    Task ReloadAsync();

    Task SaveAsync(Item item);
    Task ResolveAsync(Item item);
    Task RemoveAsync(Item item);
}