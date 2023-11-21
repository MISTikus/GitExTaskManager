using FluentAssertions;
using GitExtensions.TaskManger.Domain;
using GitExtensions.TaskManger.Utils;

namespace GitExTaskManager.UnitTests.Utils;

public class GtmSerializerTests
{
    private readonly GtmSerializer sut;
    private readonly GtmSerializerOptions options;

    public GtmSerializerTests()
    {
        this.options = new();
        this.sut = new(this.options);
    }

    [Fact]
    public void Deserialize_Should_Restore_Serialized_Issue()
    {
        // Arrange
        var model = new Issue
        {
            Title = "Test",
            Description = "some\ndesc",
            Created = DateTime.Today.AddHours(10).AddMinutes(10).AddSeconds(10),
        };
        model.Comments.Add(DateTime.Today.AddHours(10).AddMinutes(10).AddSeconds(10), "some\ncomment");
        model.Resolve();

        // Act
        var serialized = this.sut.Serialize(model);
        var restored = this.sut.Deserialize<Issue>(serialized);

        // Assert
        restored.Should().BeEquivalentTo(model);
    }

    [Fact]
    public void Deserialize_Should_Restore_Serialized_Object()
    {
        // Arrange
        this.options.Converters.Add(typeof(Sample.SubSample), (
            new Func<object, string>((o) => this.sut.Serialize((Sample.SubSample)o)),
            (s) => new Sample.SubSample()));

        var model = new Sample
        {
            SingleLineValue = "SomeValue",
            IntValue = 100500,
            EnumValue = EnumSample.Value1,
            MultiLineValue = "Some\r\nValue\nLines",
            ArrayValue = new[] { "some", "array" },
            ListValue = new() { "some", "array" },
            DateValue = DateTime.Today.AddHours(10).AddMinutes(10).AddSeconds(10),
            KeyedChildren = new()
            {
                ["some"] = new() { SingleLineValue = "Child" },
                ["other"] = new() { MultiLineValue = "Some\nOther Child" },
            },
            AnotherSingleLineValue = "SomeValue",
        };

        // Act
        var serialized = this.sut.Serialize(model);
        var restored = this.sut.Deserialize<Sample>(serialized);

        // Assert
        model.MultiLineValue = model.MultiLineValue.Replace("\r\n", "\n");
        restored.Should().BeEquivalentTo(model);
    }

    private class Sample
    {
        public string? SingleLineValue { get; set; }
        public string? MultiLineValue { get; set; }
        public EnumSample EnumValue { get; set; }
        public DateTime DateValue { get; set; }
        public int IntValue { get; set; }
        public string[]? ArrayValue { get; set; }
        public List<string>? ListValue { get; set; }
        public Dictionary<string, SubSample>? KeyedChildren { get; set; }
        public string? AnotherSingleLineValue { get; set; }

        public class SubSample
        {
            public string? SingleLineValue { get; set; }
            public string? MultiLineValue { get; set; }
        }
    }

    private enum EnumSample
    {
        Default,
        Value1,
        Value2
    }
}
