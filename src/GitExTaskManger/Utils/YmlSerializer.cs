using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GitExtensions.TaskManger.Utils;

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
            .Build();
    }

    public string Serialize<TModel>(TModel model) => this.serializer.Serialize(model);

    public TModel Deserialize<TModel>(string serialized) => this.deserializer.Deserialize<TModel>(serialized);
}