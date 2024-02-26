using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GitExtensions.TaskManager.Utils;

internal class YmlSerializer : ISerializer
{
    private readonly YamlDotNet.Serialization.ISerializer serializer;
    private readonly IDeserializer deserializer;

    public YmlSerializer()
    {
        this.serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        this.deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
    }

    public string Serialize<TModel>(TModel model) => this.serializer.Serialize(model);

    public TModel Deserialize<TModel>(string serialized) => this.deserializer.Deserialize<TModel>(serialized);
}
internal interface ISerializer
{
    TModel Deserialize<TModel>(string serialized);
    string Serialize<TModel>(TModel model);
}