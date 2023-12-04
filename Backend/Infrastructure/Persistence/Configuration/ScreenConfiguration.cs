using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ScreenConfiguration : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> builder)
        {
            var constraints = new ScreenConstraints();
            ConfigureNames(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(constraints.NameLength);
        }

        public void ConfigureNames(EntityTypeBuilder<Screen> builder)
        {
            builder.ToTable("Salas");
            builder.Property(m => m.Id)
                   .HasColumnName("SalaId");
            builder.Property(m => m.Name)
                   .HasColumnName("Nombre");
            builder.Property(m => m.Capacity)
                   .HasColumnName("Capacidad");
        }
    }
}