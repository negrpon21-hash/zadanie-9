using System;
using System.Collections.Generic;

namespace EFDemoApp.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; } = "";
        public string GenderCode { get; set; } = "";
        public Gender Gender { get; set; } = null!;
        public List<ClientService> ClientServices { get; set; } = new();
    }
}
