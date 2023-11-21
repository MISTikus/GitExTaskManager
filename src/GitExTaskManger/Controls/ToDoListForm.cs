using GitExtensions.TaskManger.Domain;

namespace GitExtensions.TaskManger.Controls;
internal partial class ToDoListForm : Form
{
    private readonly ItemType type;
    private readonly ITaskManger taskManger;

    private bool showResolved = false;
    private int Selected => ToDoList.SelectedIndices[0];

    public ToDoListForm() => InitializeComponent();
    public ToDoListForm(ItemType type, ITaskManger taskManger) : this()
    {
        this.type = type;
        this.taskManger = taskManger;
        taskManger.ReloadAsync()
            .ContinueWith(t => AssignItems())
            .ConfigureAwait(false);

        StatusLabel.Text = "";
        ToDoList.Columns.Clear();
        ToDoList.Items.Clear();

        ToDoList.Columns.AddRange(GetColumnsByType());
    }

    #region EventHandlers
    private void AddButton_Click(object sender, EventArgs e)
    {
        var addForm = new ItemForm(FormActionType.Add, type switch
        {
            ItemType.Issue => new Issue(),
            ItemType.Backlog => new Backlog(),
            _ => throw new NotImplementedException()
        });
        var result = addForm.ShowDialog();
        if (result == DialogResult.OK)
            this.taskManger.AddAsync(addForm.Item).ConfigureAwait(false);
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        var addForm = new ItemForm(FormActionType.Edit, GetSelectedItemByType());
        var result = addForm.ShowDialog();
        if (result == DialogResult.OK)
            this.taskManger.AddAsync(addForm.Item).ConfigureAwait(false);
    }

    private void ResolveButton_Click(object sender, EventArgs e)
    {
        var item = GetSelectedItemByType();
        if (Ask($"Are you sure you want to resolve item?\r\n{item.Title}"))
            this.taskManger.ResolveAsync(item).ConfigureAwait(false);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        var item = GetSelectedItemByType();
        if (Ask($"Are you sure you want to completely REMOVE item?\r\n{item.Title}"))
            this.taskManger.RemoveAsync(item).ConfigureAwait(false);
    }

    private void ShowResolved_Click(object sender, EventArgs e)
        => this.showResolved = !this.showResolved;

    private void ToDoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isSelected = ToDoList.SelectedItems.Count != 0;
        EditButton.Enabled = isSelected;
        RemoveButton.Enabled = isSelected;
        ResolveButton.Enabled = isSelected;
    }

    private void ToDoList_DoubleClick(object sender, EventArgs e)
    {
        var isSelected = ToDoList.SelectedItems.Count != 0;
        if (!isSelected)
            return;
        EditButton_Click(sender, e);
    }
    #endregion EventHandlers

    #region Private methods
    private void AssignItems()
    {
        Item[] items = type switch
        {
            ItemType.Issue => taskManger.GetIssues(showResolved),
            ItemType.Backlog => taskManger.GetBacklogs(showResolved),
            _ => throw new NotImplementedException()
        };
        var viewItems = items.Select(x => new ListViewItem(new string[] { x.Title, x.Created.ToString("u") })
        {
            ToolTipText = x.Description
        })
        .ToArray();
        ToDoList.Items.Clear();
        ToDoList.Items.AddRange(viewItems);
    }

    private ColumnHeader[] GetColumnsByType() => type switch
    {
        _ => new ColumnHeader[]
        {
            new() { Text = "Title", Width = CalculateWidth(80)-10 },
            new() { Text = "Created", Width = CalculateWidth(20) }
        },
    };

    private int CalculateWidth(int percentage) => (int)(ToDoList.Width * (percentage / 100.0));

    private static bool Ask(string message)
        => MessageBox.Show(message, message, MessageBoxButtons.YesNo) == DialogResult.Yes;

    private Item GetSelectedItemByType() => type switch
    {
        ItemType.Issue => this.taskManger.GetIssues(showResolved)[Selected],
        ItemType.Backlog => this.taskManger.GetBacklogs(showResolved)[Selected],
        _ => throw new NotImplementedException(),
    };
    #endregion Private methods
}
