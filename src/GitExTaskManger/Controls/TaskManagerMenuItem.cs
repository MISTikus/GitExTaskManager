using GitExTaskManger.Domain;

namespace GitExTaskManger.Controls;
internal class TaskManagerMenuItem : ToolStripMenuItem
{
    internal TaskManagerMenuItem(ITaskManger taskManger)
    {
        Text = "T&ask manager";
        DropDown.Items.AddRange(CreateBundleItemsAsync(taskManger));
    }

    // ToDo: add badges with elements count
    private static ToolStripItem[] CreateBundleItemsAsync(ITaskManger taskManger) => Enum.GetValues<ItemType>()
        .Select(x => new ToDoMenuItem(x, taskManger))
        .ToArray();
}