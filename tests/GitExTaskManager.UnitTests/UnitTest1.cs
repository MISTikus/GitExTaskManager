using GitExTaskManger.Services;
using GitUIPluginInterfaces;
using Moq;

namespace Domain.UnitTests;

public class UnitTest1
{
    private readonly Mock<IExecutable> executor;

    public UnitTest1()
    {
        this.executor = new Mock<IExecutable>();
    }

    [Fact]
    public void Test1()
    {
        var xz = new GitSolutionFileProvider("\\Some", this.executor.Object);
    }
}