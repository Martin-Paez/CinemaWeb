using Infrastructure.CQRS.Quieries;
using Microsoft.EntityFrameworkCore;
using Infrastructure.CQRS.Commands;
using Infrastructure.Persistence;
using Application.Interfaces.ICQRS.ICommands;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IShowServices;
using Application.Interfaces.IUseCases.IMovieServices;
using Application.Interfaces.IUseCases.IRoomServices;
using Application.Mappers;
using Application.Interfaces.IUseCases.ITicketServices;
using Application.Interfaces.IUseCases.IGenreServices;
using Application.Interfaces.CQRS.ICommands;
using Application.Interfaces.IError;
using Application.UseCases.GenreServices;
using Application.UseCases.MovieServices;
using Application.UseCases.ScreenServices;
using Application.UseCases.ShowServices;
using Application.UseCases.TicketServices;
using Presentation.API.ErrorHandling.ErrorMessageFactorys;
using Presentation.API.ErrorHandling.ErrorMessageFactorys.ErrorsJson;

namespace Presentation.API
{
    /// <summary>
    /// Clase destinada a agregar a un IServiceCollection todas las
    /// inyecciones de dependencias necesarias para que la aplicacion 
    /// funcione.
    /// </summary>
    public static class Startup
    {
        public static void PresentationLayerInjections(
            this IServiceCollection services,
            IConfiguration _configuration
            )
        {
            services.AddLogging();
            services.AddSingleton( x => _configuration );
            services.AddSingleton<IErrorTemplateParser, JsonErrorTemplateParser>();
            services.AddSingleton<IErrorMessageFactory, ErrorMessageFactory>();
        }
        
        public static void ApplicationLayerInjections(
            this IServiceCollection services
            )
        {
            services.AddScoped<IAddShowService, AddShowService>();
            services.AddScoped<IGetMovieByIdService, GetMovieByIdService>();
            services.AddScoped<ICheckOverlappingShowsService, CheckOverlappingShowsService>();
            services.AddScoped<IGetScreenByIdService, GetScreenByIdService>();
            services.AddScoped<IGetGenreByIdService, GetGenreByIdService>();
            services.AddScoped<IUpdateMovieService, UpdateMovieService>();
            services.AddScoped<IDeleteShowService, DeleteShowService>();
            services.AddScoped<IAddTicketService, AddTicketsService>();
            services.AddScoped<IGetTicketsAmountService, GetTicketsAmountService>();
            services.AddScoped<IErrorMessageFactory, ErrorMessageFactory>();
            services.AddScoped<IGetShowBySpecificationService, GetShowBySpecificationService>();
            services.AddScoped<IGetShallowShowByIdService, GetShallowShowByIdService>();
            services.AddScoped<IGetDeepShowByIdWithoutTicketsService, GetDeepShowByIdWithoutTicketsService>();
            services.AddScoped<IExistsMovieTitleService, ExistsMovieTitleService>();
            services.AddScoped<IGetAllGenreService, GetAllGenreService>();
            services.AddAutoMapper(typeof(GenreProfile));
            services.AddAutoMapper(typeof(ScreenProfile));
            services.AddAutoMapper(typeof(ShowProfile));
            services.AddAutoMapper(typeof(TicketProfile));
            services.AddAutoMapper(typeof(MovieProfile));
        }

        public static void InfrasctructureLayerInjections(
            this IServiceCollection services,
            IConfiguration _configuration
            )
        {
            services.AddScoped<IShowCommands, ShowCommands>();
            services.AddScoped<IMovieCommand, MovieCommand>();
            services.AddScoped<IShowQueries, ShowQueries>();
            services.AddScoped<IScreenQueries, ScreenQueries>();
            services.AddScoped<IMovieQueries, MovieQueries>();
            services.AddScoped<IGenreQueries, GenreQueries>();
            services.AddScoped<ITicketCommands, TicketCommands>();
            services.AddDbContext<CinemaDbContext>(
                opts => opts.UseSqlServer(
                    _configuration["connectionString"]
                )
            );
        }
    }
}