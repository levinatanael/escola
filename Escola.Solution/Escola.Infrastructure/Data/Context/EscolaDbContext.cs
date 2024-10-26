using Escola.Domain.Entities;
using Escola.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Context
{
    public class EscolaDbContext : DbContext
    {
        public EscolaDbContext(DbContextOptions<EscolaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
