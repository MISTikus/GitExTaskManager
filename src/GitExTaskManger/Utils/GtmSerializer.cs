using System.Collections;
using System.Reflection;
using System.Text;

namespace GitExTaskManger.Utils;

internal class GtmSerializer : ISerializer
{
    private const string propertyFormat = "[{0}]";
    private const string keyValueFormat = "{0}={1}";
    private const string objectFormat = "{{\n{0}\n}}";
    private const string keyValueSeparator = "=";
    private const char objectStart = '{';
    private const char objectEnd = '}';
    private const char propStart = '[';

    private readonly GtmSerializerOptions options;

    public GtmSerializer(GtmSerializerOptions options) => this.options = options;

    #region Serialize
    public string Serialize<TModel>(TModel model) => SerializeObject(typeof(TModel), model);

    private string SerializeObject(Type type, object value, int depth = 0)
    {
        if (type.IsClass && !type.Assembly.GetName().Name.StartsWith("System."))
        {
            var builder = new StringBuilder();
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propValue = prop.GetValue(value);
                if (propValue == null)
                    continue;

                builder.Append(string.Format(propertyFormat, prop.Name));
                builder.Append(options.LineBreak);
                var stringValue = Serialize(prop.PropertyType, propValue, depth + 1);
                builder.Append(stringValue);
                builder.Append(options.LineBreak);
                builder.Append(options.LineBreak);
            }
            return builder.ToString();
        }
        return Serialize(type, value, depth);
    }

    private string Serialize(Type type, object value, int depth = 0)
    {
        if (typeof(IDictionary).IsAssignableFrom(type))
        {
            var list = new List<string>();
            var dict = (IDictionary)value;
            foreach (var key in dict.Keys)
            {
                var keyString = Serialize(type.GetGenericArguments()[0], key, depth);
                var valueString = SerializeObject(type.GetGenericArguments()[1], dict[key], depth);
                if (valueString.Contains(options.LineBreak))
                {
                    var indent = string.Concat(Enumerable.Repeat(options.Indent, depth));
                    list.Add(string.Format(keyValueFormat,
                        keyString,
                        string.Format(objectFormat, indent + valueString.Replace(options.LineBreak, options.LineBreak + indent))));
                }
                else
                {
                    list.Add(string.Format(keyValueFormat, keyString, valueString));
                }
            }
            return string.Join(options.LineBreak, list);
        }
        else if (type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type))
        {
            var enumerableType = typeof(IEnumerable<>);
            var valueEnumerableType = type.GetInterfaces()
                .First(x => x.IsGenericType && x == enumerableType.MakeGenericType(x.GetGenericArguments()));
            var list = new List<string>();
            foreach (var item in (IEnumerable)value)
            {
                list.Add(Serialize(valueEnumerableType.GetGenericArguments()[0], item, depth));
            }
            return string.Join(options.LineBreak, list);
        }
        else if (options.Converters.ContainsKey(type))
        {
            return options.Converters[type].serializer(value);
        }
        else
        {
            return value?.ToString() ?? "";
        }
    }
    #endregion Serialize

    #region Deserialize
    public TModel Deserialize<TModel>(string serialized)
    {
        var type = typeof(TModel);
        var lines = Split(serialized);
        return (TModel)Deserialize(type, lines);
    }

    private object Deserialize(Type type, (int i, string l)[] lines)
    {
        var values = new Dictionary<string, object>();
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.l.StartsWith(propStart))
            {
                var propName = line.l[1..^1];
                if (lines.Length > i + 1)
                {
                    var valueLines = GetLinesBeforeNextProp(lines, i + 1);
                    values[propName] = ConvertValue(type, propName, valueLines);
                    i += valueLines.Length;
                }
                else
                {
                    values.Add(propName, null);
                }
            }
        }
        return Instantiate(type, values);
    }

    private static object Instantiate(Type type, Dictionary<string, object> values)
    {
        var ctors = type.GetConstructors();
        var ctor = ctors.OrderBy(x => x.GetParameters().Length).FirstOrDefault();
        object instance = null;
        if (ctor is null)
            instance = Activator.CreateInstance(type);
        else
        {
            var ctorValues = ctor.GetParameters()
                .Select(x => values.ContainsKey(x.Name) ? values[x.Name] : null).ToArray();
            instance = Activator.CreateInstance(type, ctorValues);
        }

        foreach (var prop in type.GetProperties())
        {
            if (!values.TryGetValue(prop.Name, out var value) || value is null)
                continue;

            if (!prop.CanWrite && typeof(IDictionary).IsAssignableFrom(prop.PropertyType))
            {
                var dict = (IDictionary)prop.GetValue(instance);
                var source = (IDictionary)value;
                foreach (var key in source.Keys)
                    dict.Add(key, source[key]);
                continue;
            }

            if (prop.CanWrite)
                prop.SetValue(instance, value);
            else
                GetSetMethodOnDeclaringType(prop)?.Invoke(instance, new[] { value });
        }

        return instance;
    }

    private static (int i, string l)[] GetLinesBeforeNextProp((int i, string l)[] lines, int startIndex)
    {
        var result = new List<(int i, string l)>();
        var objectStarted = false;
        for (var i = startIndex; i < lines.Length; i++)
        {
            if (lines[i].l.EndsWith(objectStart))
                objectStarted = true;
            if (lines[i].l.EndsWith(objectEnd))
                objectStarted = false;
            if (lines[i].l.StartsWith(propStart) && !objectStarted)
                return result.ToArray();
            result.Add(lines[i]);
        }
        return result.ToArray();
    }

    private static string[] GetLinesBeforeObjectEnds((int i, string l)[] lines, int startIndex)
    {
        var result = new List<string>();
        for (var i = startIndex; i < lines.Length; i++)
        {
            if (lines[i].l.StartsWith(objectEnd))
                return result.ToArray();
            result.Add(lines[i].l);
        }
        return result.ToArray();
    }

    private object ConvertValue(Type type, string propName, (int i, string l)[] lines)
    {
        var joined = string.Join(options.LineBreak, lines.Select(x => x.l));

        var prop = type.GetProperty(propName);
        if (prop is null)
            return null;
        if (options.Converters.TryGetValue(prop.PropertyType, out var converter))
            return converter.deserializer(joined);
        if (prop.PropertyType.IsEnum)
            return Enum.Parse(prop.PropertyType, joined);

        if (typeof(IDictionary).IsAssignableFrom(prop.PropertyType))
        {
            var instance = (IDictionary)Activator.CreateInstance(prop.PropertyType);
            for (var i = 0; i < lines.Length; i++)
            {
                var (_, line) = lines[i];
                if (line.EndsWith(objectEnd))
                    continue;
                var split = line.Split(keyValueSeparator);
                if (split[1].EndsWith(objectStart) && lines.Length > i + 1)
                {
                    var values = GetLinesBeforeObjectEnds(lines, i + 1);
                    split[1] = string.Join(options.LineBreak, values);
                    i += values.Length;
                }

                try
                {
                    var key = Convert.ChangeType(split[0], prop.PropertyType.GenericTypeArguments[0]);

                    var valueType = prop.PropertyType.GenericTypeArguments[1];
                    var value = valueType.IsClass && !valueType.Namespace.StartsWith("System")
                        ? Deserialize(prop.PropertyType.GenericTypeArguments[1], Split(split[1]))
                        : Convert.ChangeType(split[1], valueType);
                    instance.Add(key, value);
                }
                catch (InvalidCastException e)
                {
                    throw new NotImplementedException();
                }
            }
            return instance;
        }
        if (prop.PropertyType.IsArray)
        {
            var elementType = prop.PropertyType.GetElementType();
            var instance = Array.CreateInstance(elementType, lines.Length);
            for (var i = 0; i < lines.Length; i++)
            {
                var value = Convert.ChangeType(lines[i].l, elementType);
                ((IList)instance)[i] = value;
            }
            return instance;
        }
        if (typeof(IList).IsAssignableFrom(prop.PropertyType))
        {
            var enumerableType = typeof(IEnumerable<>);
            var valueEnumerableType = prop.PropertyType.GetInterfaces()
                .First(x => x.IsGenericType && x == enumerableType.MakeGenericType(x.GetGenericArguments()));
            var addMethod = prop.PropertyType.GetMethod("Add");

            var instance = Activator.CreateInstance(prop.PropertyType);
            for (var i = 0; i < lines.Length; i++)
            {
                var value = Convert.ChangeType(lines[i].l, valueEnumerableType.GenericTypeArguments[0]);
                addMethod.Invoke(instance, new[] { value });
            }
            return instance;
        }
        return Convert.ChangeType(joined, prop.PropertyType);
    }

    private (int i, string l)[] Split(string value)
    {
        var i = 0;
        return value
            .Split(options.LineBreak, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => (i++, x))
            .ToArray();
    }

    private static MethodInfo GetSetMethodOnDeclaringType(PropertyInfo propertyInfo)
    {
        var methodInfo = propertyInfo.GetSetMethod(true);
        return methodInfo ?? propertyInfo
            .DeclaringType
            .GetProperty(propertyInfo.Name)
            .GetSetMethod(true);
    }
    #endregion Deserialize
}

public record GtmSerializerOptions(string LineBreak = "\r\n", string Indent = "\t")
{
    public Dictionary<Type, (Func<object, string> serializer, Func<string, object> deserializer)> Converters { get; } = new();
}

internal interface ISerializer
{
    TModel Deserialize<TModel>(string serialized);
    string Serialize<TModel>(TModel model);
}