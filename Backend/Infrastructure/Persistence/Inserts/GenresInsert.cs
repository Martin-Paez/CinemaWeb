using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Inserts
{
    public class GenresInsert : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre { Id = 1, Name = "Accion" },
                new Genre { Id = 2, Name = "Aventura" },
                new Genre { Id = 3, Name = "Ciencia Ficcion" },
                new Genre { Id = 4, Name = "Comedia" },
                new Genre { Id = 5, Name = "Documental" },
                new Genre { Id = 6, Name = "Drama" },
                new Genre { Id = 7, Name = "Fantasia" },
                new Genre { Id = 8, Name = "Musical" },
                new Genre { Id = 9, Name = "Suspenso" },
                new Genre { Id = 10, Name = "Terror" }
            );
        }
    }
}
