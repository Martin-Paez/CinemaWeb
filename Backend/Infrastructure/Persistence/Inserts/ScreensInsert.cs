using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Inserts
{
    public class ScreensInsert : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> builder)
        {
            builder.HasData(
                new Screen { Id = 1, Name = "Sala A", Capacity = 5 },
                new Screen { Id = 2, Name = "Sala B", Capacity = 15 },
                new Screen { Id = 3, Name = "Sala C", Capacity = 35 }
            );
        }
    }
}
