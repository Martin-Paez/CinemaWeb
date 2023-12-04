using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            ConfigureNames(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(50);
        }

        public void ConfigureNames(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Generos");
            builder.Property(m => m.Id)
                   .HasColumnName("GeneroId");
            builder.Property(m => m.Name)
                   .HasColumnName("Nombre");
        }
    }
}
