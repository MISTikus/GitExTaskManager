using GitExTaskManger.Domain;
using GitExTaskManger.Utils;
using System.Reflection;

namespace GitExTaskManger.Controls;
internal class ToDoMenuItem : ToolStripMenuItem
{
    private readonly ItemType type;
    private readonly ITaskManger taskManger;

    internal ToDoMenuItem(ItemType type, ITaskManger taskManger)
    {
        this.type = type;
        this.taskManger = taskManger;
        Text = "&" + GetPluralName();
    }

    private string GetPluralName()
    {
        var enumType = typeof(ItemType);
        var typeName = this.type.ToString();
        var memberInfos = enumType.GetMember(typeName);
        var enumValueMemberInfo = memberInfos.Single(m => m.DeclaringType == enumType);
        var valueAttributes = enumValueMemberInfo.GetCustomAttribute<PluralAttribute>(true);
        return valueAttributes?.PluralName ?? typeName;
    }

    protected override void OnClick(EventArgs e)
    {
        var form = new ToDoListForm(type, taskManger);
        form.Show();

        base.OnClick(e);
    }
}