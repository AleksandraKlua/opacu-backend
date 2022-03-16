using WebAPI01.Domain;
using Microsoft.EntityFrameworkCore;
using WebAPI01.Domain.Model;

namespace WebAPI01.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<File> Files { get; set; }

        public DbSet<ImageFile> ImageFiles { get; set; }
        
        public DbSet<VideoFile> VideoFiles { get; set; }
        
        public DbSet<TextFile> TextFiles { get; set; }
        
        public  DbSet<AudioFile> AudioFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<File>()
                .Property(f => f.CreatedAt)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<File>()
                .Property(f => f.UpdatedAt)
                .ValueGeneratedOnUpdate();
                
            modelBuilder.Entity<File>()
                .HasOne(u => u.User)
                .WithMany(f => f.Files)
                .HasForeignKey(f => f.UserId)
                .IsRequired();

            modelBuilder.Entity<TextFile>()
                .HasOne(f => f.File)
                .WithOne()
                .HasForeignKey<TextFile>(f => f.FileId)
                .IsRequired();
            
            modelBuilder.Entity<ImageFile>()
                .HasOne(f => f.File)
                .WithOne()
                .HasForeignKey<ImageFile>(f => f.FileId)
                .IsRequired();
            
            modelBuilder.Entity<VideoFile>()
                .HasOne(f => f.File)
                .WithOne()
                .HasForeignKey<VideoFile>(f => f.FileId)
                .IsRequired();
            
            modelBuilder.Entity<AudioFile>()
                .HasOne(f => f.File)
                .WithOne()
                .HasForeignKey<AudioFile>(f => f.FileId)
                .IsRequired();
        }
    }
}
