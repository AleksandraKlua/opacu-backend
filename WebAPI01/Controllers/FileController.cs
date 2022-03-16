using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI01.API.Services;
using WebAPI01.Domain.DTO;
using WebAPI01.Domain.Repositories;
using WebAPI01.Infrastructure;
using WebAPI01.Infrastructure.Data;
using WebAPI01.Infrastructure.Repositories;
using File = WebAPI01.Domain.Model.File;

namespace WebAPI01.API.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileRepository _fileRepository;
        private FileUploadService _fileUploadService;

        public FileController(FileUploadService fileUploadService, IFileRepository fileRepository)
        {
            _fileUploadService = fileUploadService;
            _fileRepository = fileRepository;
        }
        
        [HttpGet]
        [Route("api/files")]
        public async Task<ActionResult<List<File>>> GetFiles()
        {
            return await _fileRepository.GetFiles();
        }

        [HttpGet]
        [Route("api/users/{userId}/files")]
        public async Task<ActionResult<List<File>>> GetUserFiles(Guid userId)
        {
            return await _fileRepository.GetUserFilesAsync(userId);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("api/users/{userId}/files"),]
        public async Task<ActionResult<File>> Upload(
            Guid userId,
            [FromForm] String title,
            [FromForm] String description,
            IFormFile file
        )
        {
            try
            {
                if (file != null)
                {
                    var result = await _fileUploadService.UploadFile(
                        userId,
                        file,
                        title,
                        description
                    );

                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        
        [HttpPatch]
        [Route("api/users/{userId}/files/{fileId}")]
        public async Task<ActionResult<File>> Update(Guid userId, Guid fileId, [FromBody] FileUpdateDto fileUpdateDto)
        {
            if (!_fileRepository.Has(fileId))
            {
                return new NotFoundResult();
            }

            if (!_fileRepository.BelongsToUser(userId, fileId))
            {
                return new ForbidResult();
            }

            var file = await _fileRepository.GetById(fileId);
            if (fileUpdateDto.Title != null)
            {
                file.Title = fileUpdateDto.Title;
            }
            if (fileUpdateDto.Description != null)
            {
                file.Description = fileUpdateDto.Description;
            }
            
            await _fileRepository.UpdateAsync(fileId, file);

            return new AcceptedResult();
        }

        [HttpDelete]
        [Route("api/users/{userId}/files/{fileId}")]
        public async Task<ActionResult<File>> Delete(Guid userId, Guid fileId)
        {
            if (!_fileRepository.Has(fileId))
            {
                return new NotFoundResult();
            }

            if (!_fileRepository.BelongsToUser(userId, fileId))
            {
                return new ForbidResult();
            }

            await _fileUploadService.RemoveFile(fileId);
            
            await _fileRepository.DeleteAsync(fileId);

            return new NoContentResult();
        }
    }
}