using GitExTaskManger.Utils;

namespace GitExTaskManger.Domain;

public enum ItemType
{
    [Plural("Issues")]
    Issue = 0,
    Backlog = 1,
}
