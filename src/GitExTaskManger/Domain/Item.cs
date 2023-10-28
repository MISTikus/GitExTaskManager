namespace GitExTaskManger.Domain;

internal record Item(
    ItemType Type,
    string Title,
    DateTime Created,
    string Description,
    Dictionary<DateTime, string> Comments);
