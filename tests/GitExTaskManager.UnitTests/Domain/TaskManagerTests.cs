using GitExTaskManger.Domain;
using GitExTaskManger.Services;
using Moq;

namespace GitExTaskManager.UnitTests.Domain;

public class TaskManagerTests
{
    private readonly Mock<IFileProvider> fileProvider;
    private readonly ITaskManger sut;

    public TaskManagerTests()
    {
        this.fileProvider = new Mock<IFileProvider>();
        this.sut = new TaskManger(this.fileProvider.Object);
    }

    [Fact]
    public void Add_ShouldCreate_File()
    {
        // Arrange
        var title = Guid.NewGuid().ToString();
        var expectedFileName = $"tasks/issue/{title[..5]}_";

        // Act
        this.sut.Add(new Issue(DateTime.Now)
        {
            Description = Guid.NewGuid().ToString(),
            Title = title,
        });

        // Assert
        fileProvider.Verify(x => x.Create(It.Is<string>(s => GetVerifyFileNameFunc(s, expectedFileName, ".gtm"))), Times.Once);
    }

    #region Private
    private static bool GetVerifyFileNameFunc(string actual, string expectedFileName, string ext)
        => actual[..(actual.LastIndexOf('_') + 1)] == expectedFileName
            && actual[actual.LastIndexOf('.')..] == ext;
    #endregion Private
}
