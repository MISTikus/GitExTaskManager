namespace GitExtensions.TaskManger.Domain;

internal abstract record Item
{
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

    internal void ChangeTitle(string title)
    {
        if (Title == title)
            return;
        this.title = title;
        State = ItemState.Changed;
    }

    internal void ChangeDescription(string description)
    {
        if (Description == description)
            return;
        this.description = description;
        State = ItemState.Changed;
    }

    internal void Resolve()
    {
        if (State == ItemState.Resolved)
            return;
        State = ItemState.Resolved;
    }
}
