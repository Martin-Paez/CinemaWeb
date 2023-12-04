using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class MoviesConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            ConfigureNames(builder);
            var constrains = new MovieConstraints();
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.GenreNav)
                .WithMany()
                .HasForeignKey(x => x.Genre);
            builder.Property(x => x.Title)
                .HasMaxLength(constrains.TitleLength);
            builder.Property(x => x.Synopsis)
                .HasMaxLength(constrains.SynopsisLength);
            builder.Property(x => x.PosterUrl)
                .HasMaxLength(constrains.PosterLength);
            builder.Property(x => x.Trailer)
                .HasMaxLength(constrains.TrailerLength);
        }

        public void ConfigureNames(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Peliculas");
            builder.Property(m => m.Id)
                   .HasColumnName("PeliculaId");
            builder.Property(m => m.Title)
                   .HasColumnName("Titulo"); 
            builder.Property(m => m.Synopsis)
                   .HasColumnName("Sonopsis");
            builder.Property(m => m.PosterUrl)
                   .HasColumnName("Poster");
            builder.Property(m => m.Genre)
                   .HasColumnName("Genero");
        }
    }
}