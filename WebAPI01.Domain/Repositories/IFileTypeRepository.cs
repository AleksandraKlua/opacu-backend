using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebAPI01.Domain.Model;

namespace WebAPI01.Domain.Repositories
{
    public interface IFileTypeRepository<T>
    {
        public Task<List<T>> GetUserFilesAsync(Guid id);
        public Task<T> GetById(Guid id);
        public bool Has(Guid id);
        public bool BelongsToUser(Guid userId, Guid fileId);

        public Task<T> AddAsync(T file);
        public Task<T> DeleteAsync(Guid id);
    }
}