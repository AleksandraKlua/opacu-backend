using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI01.Domain.Model;

namespace WebAPI01.Domain.Repositories
{
    public interface IFileRepository : IFileTypeRepository<File>, IFileInfoUpdate<File>
    {
        public Task<List<File>> GetFiles();
    }
}