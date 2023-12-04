using Infrastructure.Repositories.Quieries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application.Services.UseCases.Show;
using Application.Services.UseCases.Movie;
using Application.Services.UseCases.Room;
using Infrastructure.Repositories.Commands;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Application.Dto.Response.StatusResponseNS;
using ConsolePresenter.MenuCommandPattern.BasicPrinters.ConcreteBasicPrinters;
using ConsolePresenter.MenuCommandPattern.FilteringPrinters.ShowFilteringPrinters;
using ConsolePresenter.MenuCommandPattern.Inserters;
using ConsolePresenter.MenuCommandPattern;
using ConsoleView;
using Vista.Command_Pattern;
using Application.Interfaces.IRepository.ICommands;
using Application.Interfaces.IRepository.IQueries;
using Application.Interfaces.IServices.IShow;
using Application.Interfaces.IServices.IMovie;
using Application.Interfaces.IServices.IRoom;

namespace ConsolePresenter
{
    /// <summary>
    /// Clase destinada a agregar a un IServiceCollection todas las
    /// inyecciones de dependencias necesarias para que la aplicacion 
    /// funcione.
    /// </summary>
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationLayerInjections(services);
            InfrasctructureLayerInjections(services);
            PresenterLayerInjections(services);
            ViewLayerInjections(services);
        }

        public void ApplicationLayerInjections(IServiceCollection services)
        {
            services.AddScoped<IAddShowService, AddShowService>();
            services.AddScoped<IGetAllMoviesService, GetAllMoviesService>();
            services.AddScoped<IGetAllRoomsService, GetAllRoomsService>();
            services.AddScoped<IGetShowByService, GetShowsByService>();
            services.AddScoped<IResponseFactory, ResponseFactory>();
        }

        public void InfrasctructureLayerInjections(IServiceCollection services)
        {
            services.AddScoped<IShowCommands, ShowCommands>();
            services.AddScoped<IShowQueries, ShowQueries>();
            services.AddScoped<IRoomQueries, RoomQueries>();
            services.AddScoped<IMovieQueries, MovieQueries>();
            services.AddTransient<DbContext, CinemaDbContext>();
            services.AddDbContext<CinemaDbContext>(
                opts => opts.UseSqlServer(_configuration["connectionString"])
            );
        }

        public void PresenterLayerInjections(IServiceCollection services) 
        {
            services.AddScoped<CommandsInjector>();
            services.AddScoped<MoviesPrinter>();
            services.AddScoped<RoomsPrinter>();
            services.AddScoped<ShowsByDayPrinter>();
            services.AddScoped<ShowsByMoviePrinter>();
            services.AddScoped<ShowsByMovieAndDayPrinter>();
            services.AddScoped<ShowsByMovieOrDayPrinter>();
            services.AddScoped<ShowsInserter>();
            services.AddScoped<MenuInvoker>();
        }

        public void ViewLayerInjections(IServiceCollection services)
        {
            services.AddScoped<IPrinter, Printer>();
            services.AddScoped<IReader, Reader>();
            services.AddScoped<IEnhacedConsole, EnhacedConsole>();
        }
    }
}