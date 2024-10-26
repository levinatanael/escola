using Escola.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.Data.Configurations
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd(); // Configura o ID como autoincremento

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.DataNascimento)
                .IsRequired();

            builder.Property(a => a.RA)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}