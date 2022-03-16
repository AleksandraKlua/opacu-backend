using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using File = WebAPI01.Domain.Model.File;
using SystemFile = System.IO.File;

namespace WebAPI01.Infrastructure.Facades
{
    public sealed class FileUploadProperties
    {
        public String folder { get; }
        private readonly int value;

        public static readonly FileUploadProperties TEXT =
            new FileUploadProperties(
                1,
                "text"
            );

        public static readonly FileUploadProperties IMAGE =
            new FileUploadProperties(
                2,
                "img"
            );

        public static readonly FileUploadProperties VIDEO =
            new FileUploadProperties(
                3,
                "video"
            );

        public static readonly FileUploadProperties AUDIO =
            new FileUploadProperties(
                4,
                "audio"
            );

        public static readonly FileUploadProperties OTHER =
            new FileUploadProperties(
                5,
                "other"
            );

        private FileUploadProperties(int value, String folder)
        {
            this.folder = folder;
            this.value = value;
        }

        public override String ToString()
        {
            return folder;
        }
    }

    public class FileUploadFacade
    {
        public static Dictionary<FileUploadProperties, List<String>> mappings =
            new Dictionary<FileUploadProperties, List<string>>()
            {
                {
                    FileUploadProperties.AUDIO,
                    new List<string>()
                        {"audio/mpeg", "audio/basic", "audio/aac", "audio/mp4", "audio/ogg", "audio/webm"}
                },
                {
                    FileUploadProperties.TEXT,
                    new List<string>()
                    {
                        "text/txt", "application/pdf", "text/csv", "application/msword",
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                        "text/html"
                    }
                },
                {FileUploadProperties.VIDEO, new List<string>() {"video/mpeg", "video/mp4", "video/x-msvideo", "video/webm", "video/quicktime"}},
                {
                    FileUploadProperties.IMAGE,
                    new List<string>()
                    {
                        "image/png", "image/jpg", "image/jpeg", "image/gif", "image/bmp", "image/webp", "image/svg+xml"
                    }
                }
            };

        private IConfiguration _configuration;

        public FileUploadFacade(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private FileUploadProperties GetFileUploadPropertiesFromMimeType(String mimeType)
        {
            foreach (var pair in mappings)
            {
                if (pair.Value.Contains(mimeType))
                {
                    return pair.Key;
                }
            }

            return FileUploadProperties.OTHER;
        }

        public async Task<(File, FileUploadProperties)> Upload(Guid userId, IFormFile file, String title,
            String description)
        {
            var size = file.Length;
            var format = file.ContentType;
            var fileName = GetFileName(userId, file);

            var fileUploadProperties = GetFileUploadPropertiesFromMimeType(format);
            var folderName =
                GetFolderName(fileUploadProperties);
            var pathToSave = GetPathToSave(folderName);

            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return (new File()
            {
                Size = size,
                Path = dbPath,
                Format = format,
                UserId = userId,
                Title = title,
                Description = description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }, fileUploadProperties);
        }
        
        public async Task<File> Remove(File file)
        {
            var pathToDelete = GetPathToDelete(file.Path);
            SystemFile.Delete(pathToDelete);

            return file;
        }

        private string GetPathToSave(string folderName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            return pathToSave;
        }
        
        private string GetPathToDelete(string filePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), filePath);
        }

        private string GetFolderName(FileUploadProperties uploadProperties)
        {
            return Path.Combine(_configuration.GetSection("Static").Value, uploadProperties.ToString());
        }

        private static string GetFileName(Guid userId, IFormFile file)
        {
            var fileName = userId.ToString() + "__" +
                           ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            return fileName;
        }
    }
}