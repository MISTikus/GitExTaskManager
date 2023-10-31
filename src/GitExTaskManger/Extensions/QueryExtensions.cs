using System.Linq.Expressions;

namespace GitExTaskManger.Extensions;
internal static class QueryExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate)
        => condition ? query.Where(predicate) : query;
}
