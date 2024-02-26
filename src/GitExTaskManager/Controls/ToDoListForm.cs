using GitExtensions.TaskManager.Domain;
using GitExtensions.TaskManager.Utils;

namespace GitExtensions.TaskManager.Controls;
internal partial class ToDoListForm : Form
{
    private readonly Epic item;
    private readonly TaskManagerFactory taskManagerFactory;

    private bool changed;
    private bool isInitializing;

    public ToDoListForm(ItemType type)
    {
        InitializeComponent();
        Text = type.GetPluralName();
    }

    public ToDoListForm(ItemType type, TaskManagerFactory taskManagerFactory) : this(type)
    {
        issuesListControl.Initialize(type, taskManagerFactory);
        splitContainer.Panel2.Hide();
        splitContainer.SplitterDistance = splitContainer.Height;
        this.epicPanel.Hide();
    }
    public ToDoListForm(Epic item, TaskManagerFactory taskManagerFactory) : this(item.Type)
    {
        isInitializing = true;

        this.item = item;
        this.taskManagerFactory = taskManagerFactory;
        issuesListControl.Initialize(this.item, ItemType.Issue, taskManagerFactory);
        backlogListControl.Initialize(this.item, ItemType.Backlog, taskManagerFactory);

        epicTitleBox.Text = item.Title;
        changed = false;
        ApplyChanged();

        isInitializing = false;
    }

    private void EpicSaveButton_Click(object sender, EventArgs e)
    {
        var taskManager = this.taskManagerFactory.CreateTaskManager(null);
        taskManager.SaveAsync(this.item).ConfigureAwait(false);
        changed = false;
        ApplyChanged();
    }

    private void EpicCancelButton_Click(object sender, EventArgs e)
    {
        this.item.RevertChanges();
        epicTitleBox.Text = item.Title;
        changed = false;
        ApplyChanged();
    }

    private void EpicTitleBox_TextChanged(object sender, EventArgs e)
    {
        if (isInitializing)
            return;
        this.changed = true;
        ApplyChanged();
        this.item.ChangeTitle(epicTitleBox.Text);
    }

    private void ApplyChanged()
    {
        epicSaveButton.Enabled = this.changed;
        epicCancelButton.Enabled = this.changed;
    }
}
