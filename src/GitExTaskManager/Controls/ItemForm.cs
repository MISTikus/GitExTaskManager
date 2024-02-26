using GitExtensions.TaskManager.Domain;

namespace GitExtensions.TaskManager.Controls;
internal partial class ItemForm : Form
{
    private readonly FormActionType actionType;
    private readonly Item item;
    private bool changed;

    public Item Item { get; private set; }

    public ItemForm() => InitializeComponent();
    public ItemForm(FormActionType actionType, Item item) : this()
    {
        this.actionType = actionType;
        this.item = item;

        if (item.Type == ItemType.Epic)
        {
            commentsGroupBox.Hide();
            descriptionLabel.Hide();
            descriptionBox.Hide();
            mainBox.Dock = DockStyle.Fill;
            Height -= Height / 2;
        }

        Text = item.Type.ToString();
        titleBox.Text = item.Title;
        descriptionBox.Text = item.Description;

        commentsPanel.Controls.Clear();
        commentsPanel.Controls.AddRange(item.Comments?.Select(x => CreateCommentControl(x.Value, x.Key)).ToArray());
    }

    #region EventHandlers
    private void AddComment_Click(object sender, EventArgs e)
    {
        this.item.AddComment(DateTime.Now, commentBox.Text);
        commentsPanel.Controls.Add(CreateCommentControl(this.item.Comments.Last().Value, this.item.Comments.Last().Key));
        ApplyChanged();
        commentBox.Text = "";
    }

    private void TitleBox_TextChanged(object sender, EventArgs e)
    {
        ApplyChanged();
        this.item.ChangeTitle(titleBox.Text);
    }

    private void DescriptionBox_TextChanged(object sender, EventArgs e)
    {
        ApplyChanged();
        this.item.ChangeDescription(descriptionBox.Text);
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
        okButton.Enabled = this.changed;
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
