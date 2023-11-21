namespace GitExtensions.TaskManger.Domain;

internal record Backlog : Item
{
    public Backlog(DateTime created) : base(ItemType.Backlog, created) { }
    public Backlog() : base(ItemType.Backlog) { }
}
