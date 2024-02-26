using GitExtensions.TaskManager.Services;

namespace GitExTaskManager.UnitTests.Services;

public class FileProviderTests
{
    private readonly FileProvider sut;

    public FileProviderTests()
    {
        this.sut = new FileProvider("\\Some", "file");
    }

    [Fact]
    public void Test1()
    {
    }
}