using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ConsolePresenter
{
    /// <summary>
    /// Fabrica empleada por los comandos del Package Manager Console para trabajar
    /// con migraciones Entity Framework. La fabrica permite que se trabaje con
    /// inyeccion de dependencias.
    /// </summary>
    public class MigrationToolsFactory : IDesignTimeDbContextFactory<CinemaDbContext>
    {
        public CinemaDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<CinemaDbContext>();
            optionsBuilder.UseSqlServer(configuration["connectionString"]);

            return new CinemaDbContext(optionsBuilder.Options);
        }
    }
}
