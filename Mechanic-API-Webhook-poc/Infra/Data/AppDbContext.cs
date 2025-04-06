using Microsoft.EntityFrameworkCore;
using Mechanic_API_Webhook_poc.Domain.Entities; // novo namespace

namespace Mechanic_API_Webhook_poc.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EntityVeiculo> Veiculos { get; set; }
        public DbSet<EntityWebHooks> WebHooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityVeiculo>(e =>
            {
                e.OwnsOne(x => x.Cpf, cpf =>
                {
                    cpf.Property(p => p.Value)
                       .HasColumnName("ClienteCpf")
                       .HasColumnType("varchar(11)")
                       .IsRequired();
                });
            });
        }
    }
}
