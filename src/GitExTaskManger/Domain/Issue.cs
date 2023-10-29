namespace GitExTaskManger.Domain;
internal class Issue : Item
{
    public Issue(DateTime created) : base(ItemType.Issue, created) { }

    public Issue() : base(ItemType.Issue) { }
}
