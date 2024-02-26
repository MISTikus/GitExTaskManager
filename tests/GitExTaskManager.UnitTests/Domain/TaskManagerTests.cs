using GitExtensions.TaskManager.Domain;
using GitExtensions.TaskManager.Services;
using GitExtensions.TaskManager.Utils;
using Moq;

namespace GitExTaskManager.UnitTests.Domain;

public class TaskManagerTests
{
    private readonly Mock<IFileProvider> fileProvider;
    private readonly Mock<ISerializer> serializer;
    private readonly ITaskManager sut;

    public TaskManagerTests()
    {
        this.fileProvider = new Mock<IFileProvider>();
        this.serializer = new Mock<ISerializer>();
        this.sut = new TaskManager(this.fileProvider.Object, this.serializer.Object);
    }

    [Fact]
    public async Task Add_ShouldCreate_File()
    {
        // Arrange
        var title = Guid.NewGuid().ToString();
        var body = "bodyToWrite";

        var model = new Issue(DateTime.Now)
        {
            Description = Guid.NewGuid().ToString(),
            Title = title,
        };

        this.serializer.Setup(x => x.Serialize<Item>(model))
            .Returns(body);

        // Act
        await this.sut.SaveAsync(model);

        // Assert
        this.serializer.Verify(x => x.Serialize<Item>(model), Times.Once);
        fileProvider.Verify(x => x.SaveAsync(
            model,
            null,
            body,
            default), Times.Once);
    }
}
