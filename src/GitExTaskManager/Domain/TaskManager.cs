using GitExtensions.TaskManager.Extensions;
using GitExtensions.TaskManager.Services;
using GitExtensions.TaskManager.Utils;

namespace GitExtensions.TaskManager.Domain;

internal class TaskManager : ITaskManager
{
    private readonly IFileProvider fileProvider;
    private readonly ISerializer serializer;
    private List<Issue> issues = new();
    private List<Backlog> backlogs = new();
    private List<Epic> epics = new();
    private List<Issue> resolvedIssues = new();
    private List<Backlog> resolvedBacklogs = new();

    public event EventHandler<Item> ItemAdded;
    public event EventHandler<Item> ItemChanged;
    public event EventHandler<Item> ItemRemoved;
    public event EventHandler<Item> ItemResolved;

    public TaskManager(IFileProvider fileProvider, ISerializer serializer)
    {
        this.fileProvider = fileProvider;
        this.serializer = serializer;

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

    public Epic[] GetEpics() => epics.ToArray();

    public async Task ResolveAsync(Item item)
    {
        item.Resolve();

        var body = this.serializer.Serialize(item);
        await fileProvider.DeleteAsync(item, null, default);
        await fileProvider.SaveAsync(item, "resolved", body, default);

        ItemResolved?.Invoke(this, item);
    }

    public async Task RemoveAsync(Item item)
    {
        await fileProvider.DeleteAsync(item, item.State == ItemState.Resolved ? "resolved" : null, default);

        ItemRemoved?.Invoke(this, item);
    }

    public async Task SaveAsync(Item item)
    {
        var isChanged = item.IsChanged();

        if (item.Type == ItemType.Epic && item is Epic epic)
        {
            await SaveEpic(epic);
        }
        else
        {
            item.Saved();

            var body = this.serializer.Serialize(item);
            await fileProvider.SaveAsync(item, null, body, default);
        }
        if (isChanged)
            ItemChanged?.Invoke(this, item);
        else
            ItemAdded?.Invoke(this, item);
    }

    private Task SaveEpic(Epic item)
    {
        var folder = fileProvider.GetEpicFolder(item.Title);
        if (item.IsChanged())
        {
            var originalFolder = item.GetInitializedTitle();
            fileProvider.RenameFolder(originalFolder, folder);
        }
        else
        {
            fileProvider.CreateFolder(folder);
        }

        item.Saved();

        return Task.CompletedTask;
    }

    public async Task ReloadAsync()
    {
        this.issues = await LoadAsync<Issue>(ItemType.Issue);
        this.backlogs = await LoadAsync<Backlog>(ItemType.Backlog);
        this.epics = LoadEpic();
        this.resolvedIssues = await LoadAsync<Issue>(ItemType.Issue, true).ConfigureAwait(false);
        this.resolvedBacklogs = await LoadAsync<Backlog>(ItemType.Backlog, true).ConfigureAwait(false);
    }

    private async Task<List<T>> LoadAsync<T>(ItemType itemType, bool resolved = false) where T : Item
    {
        var data = await fileProvider
            .GetListAsync(itemType, resolved ? "resolved" : null, default).ConfigureAwait(false);
        return CrutchForLineEndings(data.Select(this.serializer.Deserialize<T>).ToList());
    }

    private List<Epic> LoadEpic()
    {
        var data = fileProvider.GetFolders();
        return data.Select(x => new Epic(x.name, x.created)).ToList();
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

internal interface ITaskManager
{
    event EventHandler<Item> ItemAdded;
    event EventHandler<Item> ItemChanged;
    event EventHandler<Item> ItemRemoved;
    event EventHandler<Item> ItemResolved;

    Issue[] GetIssues(bool includeResolved);
    Backlog[] GetBacklogs(bool includeResolved);
    Epic[] GetEpics();

    Task ReloadAsync();

    Task SaveAsync(Item item);
    Task ResolveAsync(Item item);
    Task RemoveAsync(Item item);
}