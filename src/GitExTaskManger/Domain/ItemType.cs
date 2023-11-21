using GitExtensions.TaskManger.Utils;

namespace GitExtensions.TaskManger.Domain;

public enum ItemType
{
    [Plural("Issues")]
    Issue = 0,
    Backlog = 1,
}
