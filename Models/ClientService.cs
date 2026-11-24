using System;

namespace EFDemoApp.Models
{
    public class ClientService
    {
        public int ClientID { get; set; }
        public int ServiceID { get; set; }
        public DateTime StartTime { get; set; }
        public string? Comment { get; set; }
        public Client Client { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
