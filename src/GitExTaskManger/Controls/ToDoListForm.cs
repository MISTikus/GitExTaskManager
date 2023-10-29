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
            this.taskManger.Add(addForm.Item);
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        var addForm = new ItemForm(FormActionType.Edit, this.taskManger.GetIssues(showResolved)[Selected]);
        var result = addForm.ShowDialog();
        if (result == DialogResult.OK)
            this.taskManger.Add(addForm.Item);
    }

    private void ResolveButton_Click(object sender, EventArgs e)
    {
        if (Ask($"Are you sure you want to resolve item?\r\n{this.taskManger.GetIssues(showResolved)[Selected].Title}"))
            this.taskManger.Resolve(this.taskManger.GetIssues(showResolved)[Selected]);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        if (Ask($"Are you sure you want to remove item?\r\n{this.taskManger.GetIssues(showResolved)[Selected].Title}"))
            this.taskManger.Remove(this.taskManger.GetIssues(showResolved)[Selected]);
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
    #endregion EventHandlers

    #region Private methods
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
