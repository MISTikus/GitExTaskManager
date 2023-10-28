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

    [Theory]
    [MemberData(nameof(Add_ShouldCreate_File_Data))]
    public void Add_ShouldCreate_File(ItemType type)
    {
        // Arrange
        var title = Guid.NewGuid().ToString();
        var expectedFileName = $"tasks/{type.ToString().ToLower()}/{title[..5]}_";

        // Act
        this.sut.Add(new(type,
            Title: title,
            Created: DateTime.Now,
            Description: Guid.NewGuid().ToString(),
            Comments: null));

        // Assert
        fileProvider.Verify(x => x.Create(It.Is<string>(s => GetVerifyFileNameFunc(s, expectedFileName, ".gtm"))), Times.Once);
    }

    #region Private
    private static bool GetVerifyFileNameFunc(string actual, string expectedFileName, string ext)
        => actual[..(actual.LastIndexOf('_') + 1)] == expectedFileName
            && actual[actual.LastIndexOf('.')..] == ext;


    public static object[][] Add_ShouldCreate_File_Data = Enum.GetValues<ItemType>().Select(x => new object[] { x }).ToArray();
    #endregion Private
}
