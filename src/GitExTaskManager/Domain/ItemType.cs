using GitExtensions.TaskManager.Utils;

namespace GitExtensions.TaskManager.Domain;

public enum ItemType
{
    [Plural("Issues")]
    Issue = 0,
    Backlog = 1,
    [Plural("Epics")]
    Epic = 2
}