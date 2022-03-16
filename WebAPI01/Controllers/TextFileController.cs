using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI01.Domain.Model;
using WebAPI01.Domain.Repositories;
using WebAPI01.Infrastructure.Repositories;

namespace WebAPI01.API.Controllers
{
    [ApiController]
    public class TextFileController
    {
        private ITextFileRepository _fileRepository;

        public TextFileController(ITextFileRepository textFileRepository)
        {
            _fileRepository = textFileRepository;
        }

        [HttpGet]
        [Route("api/users/{userId}/text-files")]
        public async Task<ActionResult<List<TextFile>>> Get(Guid userId)
        {
            return await _fileRepository.GetUserFilesAsync(userId);
        }
    }
}