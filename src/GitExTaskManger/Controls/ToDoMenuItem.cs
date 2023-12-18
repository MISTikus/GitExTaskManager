using GitExtensions.TaskManger.Domain;
using GitExtensions.TaskManger.Utils;

namespace GitExtensions.TaskManger.Controls;

internal class ToDoMenuItem : ToolStripMenuItem
{
    private readonly ItemType type;
    private readonly ITaskManger taskManger;

    internal ToDoMenuItem(ItemType type, ITaskManger taskManger)
    {
        this.type = type;
        this.taskManger = taskManger;
        Text = "&" + this.type.GetPluralName();
    }

    protected override void OnClick(EventArgs e)
    {
        var form = new ToDoListForm(type, taskManger);
        form.Show();

        base.OnClick(e);
    }
}