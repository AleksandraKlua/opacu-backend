using WebAPI01.Domain.Repositories;
using Xunit;

namespace TestProject1
{
    public class TestFileController
    {
        private readonly TestHelper _testHelper = new TestHelper();
        private readonly IFileRepository _fileRepository;

        public TestFileController()
        {
            _fileRepository = _testHelper.FileRepository;
        }

        [Fact(DisplayName = "Upload file")]
        public void Insert()
        {
            
        }
    }
}