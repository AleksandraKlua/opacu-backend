using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebAPI01.Domain.Repositories;
using WebAPI01.Infrastructure.Facades;
using WebAPI01.Domain.Model;
using File = WebAPI01.Domain.Model.File;

namespace WebAPI01.API.Services
{
    public class FileUploadService
    {
        private FileUploadFacade _fileUploadFacade;
        private IFileRepository _fileRepository;
        private ITextFileRepository _textFileRepository;
        private IImageFileRepository _imageFileRepository;
        private IVideoFileRepository _videoFileRepository;
        private IAudioFileRepository _audioFileRepository;

        public FileUploadService(
            FileUploadFacade fileUploadFacade,
            IFileRepository fileRepository,
            ITextFileRepository textFileRepository,
            IImageFileRepository imageFileRepository,
            IVideoFileRepository videoFileRepository,
            IAudioFileRepository audioFileRepository
        )
        {
            _fileUploadFacade = fileUploadFacade;
            _fileRepository = fileRepository;
            _textFileRepository = textFileRepository;
            _imageFileRepository = imageFileRepository;
            _videoFileRepository = videoFileRepository;
            _audioFileRepository = audioFileRepository;
        }

        public async Task<File> UploadFile(Guid userId, IFormFile file, String title, String description)
        {
            var (uploadedFile, uploadProperties) = await _fileUploadFacade.Upload(userId, file, title, description);

            await _fileRepository.AddAsync(uploadedFile);

            await CreateFileRecord(uploadedFile, uploadProperties);

            return uploadedFile;
        }

        private async Task CreateFileRecord(File uploadedFile, FileUploadProperties uploadProperties)
        {
            switch (uploadProperties.folder)
            {
                case "img":
                {
                    await _imageFileRepository.AddAsync(new ImageFile()
                    {
                        File = uploadedFile,
                        Resolution = "2333x3213",
                        ColorPalette = "RGB",
                    });
                    break;
                }
                case "audio":
                {
                    await _audioFileRepository.AddAsync(new AudioFile()
                    {
                        File = uploadedFile,
                        Bitrate = 1800,
                        Length = 2400,
                    });
                    break;
                }
                case "text":
                {
                    await _textFileRepository.AddAsync(new TextFile()
                    {
                        File = uploadedFile,
                        Encoding = "utf-8",
                        SymbolCount = 2843842
                    });
                    break;
                }
                case "video":
                {
                    await _videoFileRepository.AddAsync(new VideoFile()
                    {
                        File = uploadedFile,
                        Encoding = "pro res",
                        Bitrate = 1800,
                        Length = 2400,
                        Resolution = "1920x1080"
                    });
                    break;
                }
            }
        }

        public async Task RemoveFile(Guid fileId)
        {
            var file = await _fileRepository.GetById(fileId);

            await _fileUploadFacade.Remove(file);
        }
    }
}