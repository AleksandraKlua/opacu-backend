using System;
using System.Text.Json.Serialization;

namespace WebAPI01.Domain.Model
{
    public class AudioFile
    {
        public Guid Id { get; set; }
        
        public int Length { get; set; }
        
        public int Bitrate { get; set; }
        
        [JsonIgnore]
        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}