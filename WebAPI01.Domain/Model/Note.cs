using System;
using System.Text.Json.Serialization;
using WebAPI01.Domain.Model;

namespace WebAPI01.Domain
{
    public class Note
    {
        public Guid Id { get; set; }

        public String Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
