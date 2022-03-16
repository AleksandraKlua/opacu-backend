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
    public class AudioFileRepository : IAudioFileRepository
    {
        private readonly Context _context;

        public AudioFileRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<AudioFile>> GetUserFilesAsync(Guid id)
        {
            var files = from textFile in _context.Set<AudioFile>().Include(f => f.File)
                join file in _context.Set<File>() on textFile.FileId equals file.Id
                orderby file.CreatedAt 
                where file.UserId == id
                select textFile;

            return await files.ToListAsync();
        }
        
        
        public async Task<AudioFile> GetById(Guid id)
        {
            return await _context.AudioFiles.FindAsync(id);
        }
        
        public bool Has(Guid id)
        {
            return _context.AudioFiles.Any(f => f.Id == id);
        }

        public bool BelongsToUser(Guid userId, Guid fileId)
        {
            return _context.AudioFiles.Any(f => f.Id == fileId && f.File.UserId == userId);
        }

        public async Task<AudioFile> AddAsync(AudioFile file)
        {
            await _context.AudioFiles.AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<AudioFile> UpdateAsync(Guid id, AudioFile file)
        {
            _context.AudioFiles.Update(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<AudioFile> DeleteAsync(Guid id)
        {
            var file = await _context.AudioFiles.FindAsync(id);
            _context.AudioFiles.Remove(file);
            await _context.SaveChangesAsync();
            return file;
        }
    }
}