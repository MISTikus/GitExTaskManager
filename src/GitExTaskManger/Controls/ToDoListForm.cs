using GitExTaskManger.Domain;

namespace GitExTaskManger.Controls;
public partial class ToDoListForm : Form
{
    private readonly ItemType type;

    public ToDoListForm(ItemType type) : this() => this.type = type;
    public ToDoListForm()
    {
        InitializeComponent();
        toDoList.Columns.AddRange(GetColumnsByType());
    }

    private ColumnHeader[] GetColumnsByType() => type switch
    {
        _ => new ColumnHeader[]
        {
            new ColumnHeader("Title"),
            new ColumnHeader("Created")
        },
    };
}
