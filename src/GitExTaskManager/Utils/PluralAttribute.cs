using GitExtensions.TaskManager.Domain;
using System.Reflection;

namespace GitExtensions.TaskManager.Utils;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
internal class PluralAttribute : Attribute
{
    public PluralAttribute(string pluralName) => PluralName = pluralName;

    public string PluralName { get; }
}

internal static class EnumPluralExtensions
{
    public static string GetPluralName<T>(this T type) where T : Enum
    {
        var enumType = typeof(ItemType);
        var typeName = type.ToString();
        var memberInfos = enumType.GetMember(typeName);
        var enumValueMemberInfo = memberInfos.Single(m => m.DeclaringType == enumType);
        var valueAttributes = enumValueMemberInfo.GetCustomAttribute<PluralAttribute>(true);
        return valueAttributes?.PluralName ?? typeName;
    }
}
