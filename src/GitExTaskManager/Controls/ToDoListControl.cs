using GitExtensions.TaskManager.Domain;
using GitExtensions.TaskManager.Utils;

namespace GitExtensions.TaskManager.Controls;

internal partial class ToDoListControl : UserControl
{
    private ItemType type;
    private TaskManagerFactory taskManagerFactory;
    private ITaskManager taskManager;

    private bool showResolved = false;
    private bool viewModeOpened = false;
    private int Selected => ToDoList.SelectedIndices[0];

    public ToDoListControl() => InitializeComponent();

    public void Initialize(Epic baseItem, ItemType type, TaskManagerFactory taskManagerFactory)
    {
        this.taskManager = taskManagerFactory.CreateTaskManager(baseItem);
        Initialize(type, taskManagerFactory);
    }
    public void Initialize(ItemType type, TaskManagerFactory taskManagerFactory)
    {
        this.type = type;
        this.taskManagerFactory = taskManagerFactory;
        this.taskManager ??= taskManagerFactory.CreateTaskManager(null);
        ReloadList();

        if (type == ItemType.Epic)
        {
            resolveButton.Hide();
            changeModeButton.Visible = false;
        }

        StatusLabel.Text = "";
        ToDoList.Columns.Clear();
        ToDoList.Items.Clear();

        ToDoList.Columns.AddRange(GetColumnsByType());

        this.taskManager.ItemAdded += (s, item) => AssignItems();
        this.taskManager.ItemRemoved += (s, item) => AssignItems();
        this.taskManager.ItemResolved += (s, item) => AssignItems();
        this.taskManager.ItemChanged += (s, item) => AssignItems();
    }

    #region EventHandlers
    private void AddButton_Click(object sender, EventArgs e)
    {
        ShowFormDialog(false)
            .ContinueWith(r => ReloadList());
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        ShowFormDialog(true)
            .ContinueWith(r => ReloadList());
    }

    private async Task ShowFormDialog(bool isEdit)
    {
        var item = isEdit
            ? GetSelectedItemByType()
            : type switch
            {
                ItemType.Issue => new Issue(),
                ItemType.Backlog => new Backlog(),
                ItemType.Epic => new Epic("New Epic Title", DateTime.Now),
                _ => throw new NotImplementedException()
            };

        if (type == ItemType.Epic)
        {
            var form = new ToDoListForm((Epic)item, taskManagerFactory);
            form.ShowDialog();
        }
        else
        {
            var form = new ItemForm(isEdit ? FormActionType.Edit : FormActionType.Add, item);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
                await this.taskManager.SaveAsync(form.Item).ConfigureAwait(false);
        }
    }

    private void ResolveButton_Click(object sender, EventArgs e)
    {
        var item = GetSelectedItemByType();
        if (Ask($"Are you sure you want to resolve item?\r\n{item.Title}"))
            this.taskManager.ResolveAsync(item).ConfigureAwait(false);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        var item = GetSelectedItemByType();
        if (Ask($"Are you sure you want to completely REMOVE item?\r\n{item.Title}"))
            this.taskManager
                .RemoveAsync(item)
                .ContinueWith(r =>
                {
                    if (r.IsCompleted && item.Type == ItemType.Epic)
                        ReloadList();
                })
                .ConfigureAwait(false);
    }

    private void ShowResolved_Click(object sender, EventArgs e)
    {
        this.showResolved = !this.showResolved;
        showResolvedMenuItem.Checked = this.showResolved;
        AssignItems();
    }

    private void ToDoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isSelected = ToDoList.SelectedItems.Count != 0;
        editButton.Enabled = isSelected;
        removeButton.Enabled = isSelected;
        resolveButton.Enabled = isSelected;
    }

    private void ToDoList_DoubleClick(object sender, EventArgs e)
    {
        var isSelected = ToDoList.SelectedItems.Count != 0;
        if (!isSelected)
            return;
        EditButton_Click(sender, e);
    }

    private void ChangeModeButton_ButtonClick(object sender, EventArgs e)
    {
        if (!viewModeOpened)
            changeModeButton.ShowDropDown();
        else
            changeModeButton.HideDropDown();

        viewModeOpened = !viewModeOpened;
    }

    private void ShowResolvedMenuItem_DropDownClosed(object sender, EventArgs e) => viewModeOpened = false;

    private void ShowResolvedMenuItem_DropDownOpened(object sender, EventArgs e) => viewModeOpened = true;
    #endregion EventHandlers

    #region Private methods
    private void AssignItems()
    {
        Item[] items = type switch
        {
            ItemType.Issue => taskManager.GetIssues(showResolved),
            ItemType.Backlog => taskManager.GetBacklogs(showResolved),
            ItemType.Epic => taskManager.GetEpics(),
            _ => throw new NotImplementedException()
        };
        var viewItems = items
            .Select(x => new ListViewItem(
            new string[] { x.Type.ToString(), x.Title, x.State.ToString(), x.Created.ToString("u") })
            {
                ToolTipText = x.Description,
                BackColor = x.State switch
                {
                    ItemState.Resolved => Color.Plum,
                    _ => BackColor,
                },
            })
            .ToArray();
        ToDoList.Items.Clear();
        ToDoList.Items.AddRange(viewItems);
    }

    private ColumnHeader[] GetColumnsByType() => type switch
    {
        _ => new ColumnHeader[]
        {
            new() { Text = "Type", Width = 0 },
            new() { Text = "Title", Width = CalculateWidth(70)-10 },
            new() { Text = "State", Width = CalculateWidth(10) },
            new() { Text = "Created", Width = CalculateWidth(20) }
        },
    };

    private int CalculateWidth(int percentage) => (int)(ToDoList.Width * (percentage / 100.0));

    private static bool Ask(string message)
        => MessageBox.Show(message, message, MessageBoxButtons.YesNo) == DialogResult.Yes;
    private static void Say(string message)
        => MessageBox.Show(message, message, MessageBoxButtons.OK);

    private Item GetSelectedItemByType() => type switch
    {
        ItemType.Issue => this.taskManager.GetIssues(showResolved)[Selected],
        ItemType.Backlog => this.taskManager.GetBacklogs(showResolved)[Selected],
        ItemType.Epic => this.taskManager.GetEpics()[Selected],
        _ => throw new NotImplementedException(),
    };

    private void ReloadList() => this.taskManager
        .ReloadAsync()
        .ContinueWith(t => AssignItems())
        .ConfigureAwait(false);
    #endregion Private methods
}
