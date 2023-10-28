using GitExTaskManger.Services;
using GitUIPluginInterfaces;
using Moq;

namespace GitExTaskManager.UnitTests.Services;

public class GitFileProviderTests
{
    private readonly Mock<IExecutable> executor;
    private readonly GitFileProvider sut;

    public GitFileProviderTests()
    {
        executor = new Mock<IExecutable>();
        this.sut = new GitFileProvider("\\Some", executor.Object);
    }

    [Fact]
    public void Test1()
    {
    }
}