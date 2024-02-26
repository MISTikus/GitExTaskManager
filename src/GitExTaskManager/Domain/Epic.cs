namespace GitExtensions.TaskManager.Domain;

internal record Epic : Item
{
    private readonly string originalTitle;

    public Epic(string title, DateTime created) : this(created)
    {
        Title = title;
        this.originalTitle = title;
        State = ItemState.Saved;
    }
    public Epic(DateTime created) : base(ItemType.Epic, created) { }

    public string GetInitializedTitle() => this.originalTitle;
    internal void RevertChanges()
    {
        ChangeTitle(this.originalTitle);
        Saved();
    }
}