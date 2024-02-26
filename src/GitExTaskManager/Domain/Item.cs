namespace GitExtensions.TaskManager.Domain;

internal abstract record Item
{
    private bool isChanged;
    private string oldTitle;
    private string title;
    private string description;

    protected Item(ItemType type) : this(type, DateTime.Now) { }
    protected Item(ItemType type, DateTime created)
    {
        Comments = new();
        Type = type;
        Created = created;
    }

    public ItemType Type { get; init; }
    public DateTime Created { get; init; }
    public string Title { get => this.title; init => this.title = value; }
    public string Description { get => this.description; init => this.description = value; }
    public Dictionary<DateTime, string> Comments { get; protected set; } // protected is a crutch for deserializer
    public ItemState State { get; protected set; } = ItemState.Created;

    internal bool IsChanged() => this.isChanged;
    public string GetOldTitle() => this.oldTitle ?? this.title;

    internal void ChangeTitle(string title)
    {
        if (Title == title)
            return;
        this.oldTitle = this.title;
        this.title = title;
        if (State != ItemState.Created)
            isChanged = true;
    }

    internal void ChangeDescription(string description)
    {
        if (Description == description)
            return;
        this.description = description;
        if (State != ItemState.Created)
            isChanged = true;
    }

    internal void AddComment(DateTime dateStamp, string text)
    {
        Comments.Add(dateStamp, text);
        if (State != ItemState.Created)
            isChanged = true;
    }

    internal void Resolve()
    {
        if (State == ItemState.Resolved)
            return;
        State = ItemState.Resolved;
    }

    internal void Saved()
    {
        isChanged = false;
        if (State != ItemState.Saved)
            State = ItemState.Saved;
    }
}
