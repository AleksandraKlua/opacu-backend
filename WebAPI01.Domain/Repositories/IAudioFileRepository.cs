

using WebAPI01.Domain.Model;

namespace WebAPI01.Domain.Repositories
{
    public interface IAudioFileRepository : IFileTypeRepository<AudioFile>, IFileInfoUpdate<AudioFile>
    {
        
    }
}