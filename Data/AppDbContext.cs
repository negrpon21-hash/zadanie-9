using EFDemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDemoApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ClientService> ClientServices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=85.236.189.123;Port=13306;Database=isp-44_kuralenkov_rg_zadanie8;Uid=isp-44_kuralenkov_rg;Pwd=isp-44_kuralenkov_rg;Charset=utf8mb4;",
                ServerVersion.AutoDetect("Server=85.236.189.123;Port=13306;Database=isp-44_kuralenkov_rg_zadanie8;Uid=isp-44_kuralenkov_rg;Pwd=isp-44_kuralenkov_rg;Charset=utf8mb4;")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasKey(g => g.Code);
            modelBuilder.Entity<Gender>().ToTable("Gender");

            modelBuilder.Entity<Client>().HasKey(c => c.ID);
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Gender)
                .WithMany(g => g.Clients)
                .HasForeignKey(c => c.GenderCode);

            modelBuilder.Entity<Service>().HasKey(s => s.ID);
            modelBuilder.Entity<Service>().ToTable("Service");

            modelBuilder.Entity<ClientService>().HasKey(cs => new { cs.ClientID, cs.ServiceID });
            modelBuilder.Entity<ClientService>().ToTable("ClientService");
            modelBuilder.Entity<ClientService>()
                .HasOne(cs => cs.Client)
                .WithMany(c => c.ClientServices)
                .HasForeignKey(cs => cs.ClientID);
            modelBuilder.Entity<ClientService>()
                .HasOne(cs => cs.Service)
                .WithMany(s => s.ClientServices)
                .HasForeignKey(cs => cs.ServiceID);
        }
    }
}
