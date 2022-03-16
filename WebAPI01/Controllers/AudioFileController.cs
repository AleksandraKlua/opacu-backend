using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI01.Domain.Model;
using WebAPI01.Domain.Repositories;

namespace WebAPI01.API.Controllers
{
    [ApiController]
    public class AudioFileController
    {
        private IAudioFileRepository _fileRepository;

        public AudioFileController(IAudioFileRepository audioFileRepository)
        {
            _fileRepository = audioFileRepository;
        }

        [HttpGet]
        [Route("api/users/{userId}/audio-files")]
        public async Task<ActionResult<List<AudioFile>>> Get(Guid userId)
        {
            return await _fileRepository.GetUserFilesAsync(userId);
        }
    }
}