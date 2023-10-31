using GitExTaskManger.Domain;

namespace GitExTaskManger.Controls;
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
        var addForm = new ItemForm(FormActionType.Add, new Issue());
        var result = addForm.ShowDialog();
        if (result == DialogResult.OK)
            this.taskManger.AddAsync(addForm.Item).ConfigureAwait(false);
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        var addForm = new ItemForm(FormActionType.Edit, this.taskManger.GetIssues(showResolved)[Selected]);
        var result = addForm.ShowDialog();
        if (result == DialogResult.OK)
            this.taskManger.AddAsync(addForm.Item).ConfigureAwait(false);
    }

    private void ResolveButton_Click(object sender, EventArgs e)
    {
        if (Ask($"Are you sure you want to resolve item?\r\n{this.taskManger.GetIssues(showResolved)[Selected].Title}"))
            this.taskManger.ResolveAsync(this.taskManger.GetIssues(showResolved)[Selected]).ConfigureAwait(false);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        if (Ask($"Are you sure you want to remove item?\r\n{this.taskManger.GetIssues(showResolved)[Selected].Title}"))
            this.taskManger.RemoveAsync(this.taskManger.GetIssues(showResolved)[Selected]).ConfigureAwait(false);
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
        var items = taskManger.GetIssues(showResolved)
            .Select(x => new ListViewItem(new string[] { x.Title, x.Created.ToString("u") })
            {
                ToolTipText = x.Description
            })
            .ToArray();
        ToDoList.Items.Clear();
        ToDoList.Items.AddRange(items);
    }

    private ColumnHeader[] GetColumnsByType() => type switch
    {
        _ => new ColumnHeader[]
        {
            new ColumnHeader{ Text = "Title", Width = CalculateWidth(80)-10 },
            new ColumnHeader{ Text = "Created", Width = CalculateWidth(20) }
        },
    };

    private int CalculateWidth(int percentage) => (int)(ToDoList.Width * (percentage / 100.0));

    private static bool Ask(string message)
        => MessageBox.Show(message, message, MessageBoxButtons.YesNo) == DialogResult.Yes;
    #endregion Private methods
}
