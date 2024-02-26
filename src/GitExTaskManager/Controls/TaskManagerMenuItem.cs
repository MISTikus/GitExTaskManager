using GitExtensions.TaskManager.Domain;
using GitExtensions.TaskManager.Utils;

namespace GitExtensions.TaskManager.Controls;
internal class TaskManagerMenuItem : ToolStripMenuItem
{
    private readonly PluginSettings settings;

    internal TaskManagerMenuItem(TaskManagerFactory taskManagerFactory, PluginSettings settings)
    {
        Text = "T&ask manager";
        this.settings = settings;
        DropDown.Items.AddRange(CreateBundleItemsAsync(taskManagerFactory));
    }

    // ToDo: add badges with elements count
    private ToolStripItem[] CreateBundleItemsAsync(TaskManagerFactory taskManagerFactory) => Enum.GetValues<ItemType>()
        .Where(x => this.settings.UseEpics || x != ItemType.Epic)
        .Select(x => new ToDoMenuItem(x, taskManagerFactory))
        .ToArray();
}