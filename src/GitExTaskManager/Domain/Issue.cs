﻿namespace GitExtensions.TaskManager.Domain;
internal record Issue : Item
{
    public Issue(DateTime created) : base(ItemType.Issue, created) { }
    public Issue() : base(ItemType.Issue) { }
}
