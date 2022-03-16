using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI01.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllAsync();

        public Task<User> GetByIdAsync(Guid id);

        public Task AddAsync(User person);
        public Task UpdateAsync(User person);
        public Task DeleteAsync(Guid id);
    }
}