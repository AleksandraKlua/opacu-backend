

using System;
using System.Threading.Tasks;
using WebAPI01.Domain.DTO;
using WebAPI01.Domain.Model;

namespace WebAPI01.Domain.Repositories
{
    public interface IImageFileRepository : IFileTypeRepository<ImageFile>, IFileInfoUpdate<ImageFile>
    {
        
    }
}