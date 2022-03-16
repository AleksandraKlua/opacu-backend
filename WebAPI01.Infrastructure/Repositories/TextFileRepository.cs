using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI01.Domain.Model;
using WebAPI01.Domain.Repositories;
using WebAPI01.Infrastructure.Data;

namespace WebAPI01.Infrastructure.Repositories
{
    public class TextFileRepository : ITextFileRepository
    {
        private readonly Context _context;

        public TextFileRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<TextFile>> GetUserFilesAsync(Guid id)
        {
            var files = from textFile in _context.Set<TextFile>().Include(f => f.File)
                join file in _context.Set<File>() on textFile.FileId equals file.Id
                orderby file.CreatedAt 
                where file.UserId == id
                select textFile;

            return await files.ToListAsync();
        }
        
        
        public async Task<TextFile> GetById(Guid id)
        {
            return await _context.TextFiles.FindAsync(id);
        }
        
        public bool Has(Guid id)
        {
            return _context.TextFiles.Any(f => f.Id == id);
        }

        public bool BelongsToUser(Guid userId, Guid fileId)
        {
            return _context.TextFiles.Any(f => f.Id == fileId && f.File.UserId == userId);
        }

        public async Task<TextFile> AddAsync(TextFile file)
        {
            await _context.TextFiles.AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<TextFile> UpdateAsync(Guid id, TextFile file)
        {
            _context.TextFiles.Update(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<TextFile> DeleteAsync(Guid id)
        {
            var file = await _context.TextFiles.FindAsync(id);
            _context.TextFiles.Remove(file);
            await _context.SaveChangesAsync();
            return file;
        }
    }
}