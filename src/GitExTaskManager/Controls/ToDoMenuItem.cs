using GitExtensions.TaskManager.Domain;
using GitExtensions.TaskManager.Utils;

namespace GitExtensions.TaskManager.Controls;

internal class ToDoMenuItem : ToolStripMenuItem
{
    private readonly ItemType type;
    private readonly TaskManagerFactory taskManagerFactory;

    internal ToDoMenuItem(ItemType type, TaskManagerFactory taskManagerFactory)
    {
        this.type = type;
        this.taskManagerFactory = taskManagerFactory;
        Text = "&" + this.type.GetPluralName();
    }

    protected override void OnClick(EventArgs e)
    {
        var form = new ToDoListForm(type, taskManagerFactory);
        form.Show();

        base.OnClick(e);
    }
}