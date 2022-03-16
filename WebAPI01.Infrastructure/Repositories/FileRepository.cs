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
    public class FileRepository : IFileRepository
    {
        private readonly Context _context;

        public FileRepository(Context context)
        {
            _context = context;
        }
        
        public async Task<List<File>> GetFiles()
        {
            return await _context.Files
                .ToListAsync();
        }

        public async Task<List<File>> GetUserFilesAsync(Guid id)
        {
            return await _context.Files
                .Where(f => f.UserId == id)
                .ToListAsync();
        }

        public async Task<File> GetById(Guid id)
        {
            return await _context.Files.FindAsync(id);
        }

        public bool Has(Guid id)
        {
            return _context.Files.Any(f => f.Id == id);
        }

        public bool BelongsToUser(Guid userId, Guid fileId)
        {
            return _context.Files.Any(f => f.Id == fileId && f.UserId == userId);
        }

        public async Task<File> AddAsync(File file)
        {
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<File> UpdateAsync(Guid id, File file)
        {
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<ImageFile> AddAsync(ImageFile file)
        {
            await _context.ImageFiles.AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }
        public async Task<VideoFile> AddAsync(VideoFile file)
        {
            await _context.VideoFiles.AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<AudioFile> AddAsync(AudioFile file)
        {
            await _context.AudioFiles.AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<File> UpdateAsync(File file)
        {
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<File> DeleteAsync(Guid id)
        {
            var file = await _context.Files.FindAsync(id);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return file;
        }
    }
}