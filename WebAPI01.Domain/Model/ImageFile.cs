using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WebAPI01.Domain.Model
{
    public class ImageFile
    {
        public Guid Id { get; set; }
        
        public string Resolution { get; set; }
        
        public string ColorPalette { get; set; }
        
        [JsonIgnore]
        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}