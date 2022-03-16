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
    public class VideoFileRepository : IVideoFileRepository
    {
        private readonly Context _context;

        public VideoFileRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<VideoFile>> GetUserFilesAsync(Guid id)
        {
            var files = from textFile in _context.Set<VideoFile>().Include(f => f.File)
                join file in _context.Set<File>() on textFile.FileId equals file.Id
                orderby file.CreatedAt 
                where file.UserId == id
                select textFile;

            return await files.ToListAsync();
        }
        
        
        public async Task<VideoFile> GetById(Guid id)
        {
            return await _context.VideoFiles.FindAsync(id);
        }
        
        public bool Has(Guid id)
        {
            return _context.VideoFiles.Any(f => f.Id == id);
        }

        public bool BelongsToUser(Guid userId, Guid fileId)
        {
            return _context.VideoFiles.Any(f => f.Id == fileId && f.File.UserId == userId);
        }

        public async Task<VideoFile> AddAsync(VideoFile file)
        {
            await _context.VideoFiles.AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<VideoFile> UpdateAsync(Guid id, VideoFile file)
        {
            _context.VideoFiles.Update(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<VideoFile> DeleteAsync(Guid id)
        {
            var file = await _context.VideoFiles.FindAsync(id);
            _context.VideoFiles.Remove(file);
            await _context.SaveChangesAsync();
            return file;
        }
    }
}