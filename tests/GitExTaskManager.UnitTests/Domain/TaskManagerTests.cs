using GitExTaskManger.Domain;
using GitExTaskManger.Services;
using GitExTaskManger.Utils;
using Moq;

namespace GitExTaskManager.UnitTests.Domain;

public class TaskManagerTests
{
    private readonly Mock<IFileProvider> fileProvider;
    private readonly Mock<ISerializer> serializer;
    private readonly ITaskManger sut;

    public TaskManagerTests()
    {
        this.fileProvider = new Mock<IFileProvider>();
        this.serializer = new Mock<ISerializer>();
        this.sut = new TaskManger(this.fileProvider.Object, this.serializer.Object);
    }

    [Fact]
    public async Task Add_ShouldCreate_File()
    {
        // Arrange
        var title = Guid.NewGuid().ToString();
        var expectedFileName = $".tasks/issue/{title[..5]}_";
        var body = "bodyToWrite";

        var model = new Issue(DateTime.Now)
        {
            Description = Guid.NewGuid().ToString(),
            Title = title,
        };

        this.serializer.Setup(x => x.Serialize<Item>(model))
            .Returns(body);

        // Act
        await this.sut.AddAsync(model);

        // Assert
        this.serializer.Verify(x => x.Serialize<Item>(model), Times.Once);
        fileProvider.Verify(x => x.CreateAsync(
            It.Is<string>(s => GetVerifyFileNameFunc(s, expectedFileName, ".gtm")),
            body,
            default), Times.Once);
    }

    #region Private
    private static bool GetVerifyFileNameFunc(string actual, string expectedFileName, string ext)
        => actual[..(actual.LastIndexOf('_') + 1)] == expectedFileName
            && actual[actual.LastIndexOf('.')..] == ext;
    #endregion Private
}
