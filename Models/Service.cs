using System.Collections.Generic;

namespace EFDemoApp.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Title { get; set; } = "";
        public decimal Cost { get; set; }
        public double Discount { get; set; }
        public string? Description { get; set; }
        public byte[]? MainImage { get; set; }
        public List<ClientService> ClientServices { get; set; } = new();
    }
}
