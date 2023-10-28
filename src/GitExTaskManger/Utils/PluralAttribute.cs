namespace GitExTaskManger.Utils;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
internal class PluralAttribute : Attribute
{
    public PluralAttribute(string pluralName) => PluralName = pluralName;

    public string PluralName { get; }
}