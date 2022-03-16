

using WebAPI01.Domain.Model;

namespace WebAPI01.Domain.Repositories
{
    public interface IVideoFileRepository : IFileTypeRepository<VideoFile>, IFileInfoUpdate<VideoFile>
    {
        
    }
}