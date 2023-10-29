using GitExTaskManger.Services;

namespace GitExTaskManger.Domain;

internal class TaskManger : ITaskManger
{
    private const string baseFolder = ".tasks";
    private readonly IFileProvider fileProvider;
    private readonly List<Issue> issues = new();

    public TaskManger(IFileProvider fileProvider) => this.fileProvider = fileProvider;

    public void Add(Item item)
    {
        var folder = $"{baseFolder}/{GetFolderName(item.Type)}";
        var fileName = GetFileName(item.Title);
        fileProvider.Create($"{folder}/{fileName}");
    }

    private static string GetFolderName(ItemType type) => type.ToString().ToLower();
    private static string GetFileName(string title) => $"{title
            .Replace(' ', '_')
            [..(title.Length >= 5 ? 5 : title.Length)]}_{Guid.NewGuid().ToString("N")[..5]}.gtm";

    public Issue[] GetIssues(bool includeResolved) => issues.Where(x => x.State != ItemState.Resolved).ToArray();
    public void Resolve(Item issue) => throw new NotImplementedException();
    public void Remove(Item issue) => throw new NotImplementedException();
}

internal interface ITaskManger
{
    public Issue[] GetIssues(bool includeResolved);

    void Add(Item item);
    void Resolve(Item issue);
    void Remove(Item issue);
}