

using WebAPI01.Domain.Model;

namespace WebAPI01.Domain.Repositories
{
    public interface ITextFileRepository : IFileTypeRepository<TextFile>, IFileInfoUpdate<TextFile>
    {
        
    }
}