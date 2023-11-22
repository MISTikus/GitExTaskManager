using GitExtensions.TaskManger.Domain;

namespace GitExtensions.TaskManger.Controls;
internal partial class ItemForm : Form
{
    private readonly FormActionType actionType;
    private readonly Item item;
    private TextBox addCommentBox;
    private bool changed;

    public Item Item { get; private set; }

    public ItemForm() => InitializeComponent();
    public ItemForm(FormActionType actionType, Item item) : this()
    {
        this.actionType = actionType;
        this.item = item;

        TitleBox.Text = item.Title;
        DescriptionBox.Text = item.Description;

        CommentsPanel.Controls.Clear();
        CommentsPanel.Controls.AddRange(item.Comments?.Select(x => CreateCommentControl(x.Value, x.Key)).ToArray());
    }

    #region EventHandlers
    private void AddComment_Click(object sender, EventArgs e)
    {
        this.item.AddComment(DateTime.Now, CommentBox.Text);
        CommentsPanel.Controls.Add(CreateCommentControl(this.item.Comments.Last().Value, this.item.Comments.Last().Key));
        ApplyChanged();
        CommentBox.Text = "";
    }

    private void TitleBox_TextChanged(object sender, EventArgs e)
    {
        ApplyChanged();
        this.item.ChangeTitle(TitleBox.Text);
    }

    private void DescriptionBox_TextChanged(object sender, EventArgs e)
    {
        ApplyChanged();
        this.item.ChangeDescription(DescriptionBox.Text);
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        Item = this.item;
    }

    private void CancelButton_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;
    #endregion EventHandlers

    private void ApplyChanged()
    {
        this.changed = true;
        OkButton.Enabled = this.changed;
    }

    private static Control CreateCommentControl(string comment, DateTime added)
    {
        var panel = new Panel { Dock = DockStyle.Top, Height = 80, AutoSize = true, BorderStyle = BorderStyle.FixedSingle };
        panel.Controls.Add(new Label { Text = comment, Dock = DockStyle.Fill, AutoSize = true });
        var dateLabel = new Label { Text = added.ToString("u"), Dock = DockStyle.Top };
        dateLabel.Font = new(dateLabel.Font.FontFamily, 7, FontStyle.Underline);
        panel.Controls.Add(dateLabel);
        panel.Controls.Add(new Label { Text = "", Dock = DockStyle.Bottom });
        return panel;
    }
}
