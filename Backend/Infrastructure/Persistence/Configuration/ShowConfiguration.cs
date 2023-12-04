using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            ConfigureNames(builder);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.MovieNav)
                .WithMany(p => p.Shows)
                .HasForeignKey(x => x.MovieId)
                .IsRequired(false);
            builder.HasOne(x => x.ScreenNav)
                .WithMany()
                .HasForeignKey(x => x.ScreenId)
                .IsRequired(false);
            builder.Property(x => x.Date)
                .HasColumnType("DATE");
        }

        public void ConfigureNames(EntityTypeBuilder<Show> builder)
        {
            builder.ToTable("Funciones");
            builder.Property(f => f.Id)
                   .HasColumnName("FuncionId");
            builder.Property(f => f.Schedule)
                   .HasColumnName("Horario");
            builder.Property(f => f.Date)
                   .HasColumnName("Fecha");
            builder.Property(f => f.ScreenId)
                   .HasColumnName("SalaId");
            builder.Property(f => f.MovieId)
                   .HasColumnName("PeliculaId");
        }
    }
}
