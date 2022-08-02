using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class MesageRequest
    {
        public string? Phone { get; set; }
        public string? Text { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        Queued,
        Sent,
        Failed
    }
}
