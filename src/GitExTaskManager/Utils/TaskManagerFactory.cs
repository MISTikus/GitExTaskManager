using GitExtensions.TaskManager.Domain;
using GitExtensions.TaskManager.Services;

namespace GitExtensions.TaskManager.Utils;

internal class TaskManagerFactory
{
    private const string baseFolder = ".tasks";
    private readonly ISerializer serializer;
    private readonly Func<string, IFileProvider> fileProviderFactory;

    public TaskManagerFactory(ISerializer serializer, Func<string, IFileProvider> fileProviderFactory)
    {
        this.serializer = serializer;
        this.fileProviderFactory = fileProviderFactory;
    }

    public ITaskManager CreateTaskManager(Epic baseItem)
    {
        return new Domain.TaskManager(this.fileProviderFactory(Path.Combine(baseFolder, baseItem?.Title ?? "")), serializer);
    }
}
