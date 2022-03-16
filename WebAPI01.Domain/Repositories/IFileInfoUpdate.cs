using System;
using System.Threading.Tasks;

namespace WebAPI01.Domain.Repositories
{
    public interface IFileInfoUpdate<T>
    {
        public Task<T> UpdateAsync(Guid id, T file);
    }
}