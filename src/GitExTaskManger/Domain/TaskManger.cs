using GitExTaskManger.Extensions;
using GitExTaskManger.Services;
using GitExTaskManger.Utils;

namespace GitExTaskManger.Domain;

internal class TaskManger : ITaskManger
{
    private const string baseFolder = ".tasks";
    private readonly IFileProvider fileProvider;
    private readonly ISerializer serializer;
    private List<Issue> issues = new();

    public TaskManger(IFileProvider fileProvider, ISerializer serializer)
    {
        this.fileProvider = fileProvider;
        this.serializer = serializer;
        //Task.Run(ReloadAsync).ConfigureAwait(false);
    }

    public Issue[] GetIssues(bool includeResolved) => issues.WhereIf(!includeResolved, x => x.State != ItemState.Resolved).ToArray();
    public async Task ResolveAsync(Item issue) => throw new NotImplementedException();
    public async Task RemoveAsync(Item issue) => throw new NotImplementedException();

    public async Task AddAsync(Item item)
    {
        var folder = $"{baseFolder}/{GetFolderName(item.Type)}";
        var fileName = GetFileName(item.Title);
        var body = this.serializer.Serialize(item);
        await fileProvider.CreateAsync($"{folder}/{fileName}", body, default);
    }

    public async Task ReloadAsync()
    {
        this.issues = await LoadAsync<Issue>(ItemType.Issue);
    }

    private static string GetFolderName(ItemType type) => type.ToString().ToLower();
    private static string GetFileName(string title) => $"{title
            .Replace(' ', '_')
            [..(title.Length >= 5 ? 5 : title.Length)]}_{Guid.NewGuid().ToString("N")[..5]}.gtm";

    private async Task<List<T>> LoadAsync<T>(ItemType issue)
    {
        var data = await fileProvider.GetListAsync($"{baseFolder}/{GetFolderName(issue)}", default).ConfigureAwait(false);
        return data.Select(this.serializer.Deserialize<T>).ToList();
    }
}

internal interface ITaskManger
{
    Issue[] GetIssues(bool includeResolved);
    Task ReloadAsync();

    Task AddAsync(Item item);
    Task ResolveAsync(Item issue);
    Task RemoveAsync(Item issue);
}