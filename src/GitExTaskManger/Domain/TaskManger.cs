using GitExTaskManger.Services;

namespace GitExTaskManger.Domain;

internal class TaskManger : ITaskManger
{
    private const string baseFolder = ".tasks";
    private readonly IFileProvider fileProvider;

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
}

internal interface ITaskManger
{
    void Add(Item item);
}