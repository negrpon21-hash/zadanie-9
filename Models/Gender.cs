using System.Collections.Generic;

namespace EFDemoApp.Models
{
    public class Gender
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public List<Client> Clients { get; set; } = new();
    }
}
