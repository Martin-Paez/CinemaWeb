using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ConsoleView;
using ConsolePresenter.MenuCommandPattern;

namespace ConsolePresenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = Directory
                .GetCurrentDirectory()
                .ToString();
            var builder = new ConfigurationBuilder()
                .SetBasePath(path + "../../../../")
                .AddJsonFile(
                    "appsettings.json", 
                    optional: false, 
                    reloadOnChange: true
                );
            var configuration = builder.Build();
            var startup = new Startup(configuration);
            var servicesCol = new ServiceCollection();
            startup.ConfigureServices(servicesCol);
            var services = servicesCol.BuildServiceProvider();
            CreateCinemaMenu(services).Execute();
        }

        public static ICommand CreateCinemaMenu(ServiceProvider services)
        {
            CommandsInjector commands = services.GetService<CommandsInjector>()!;
            var view = services.GetService<IEnhacedConsole>()!;
            var getShowsMenu = CreateShowsPrinterMenu(commands, view);
            var mainMenu = services.GetService<MenuInvoker>() !;
            mainMenu.Add(commands.MoviesPrinter, "Listar Peliculas");
            mainMenu.Add(commands.RoomsPrinter, "Listar Salas");
            mainMenu.Add(commands.ShowsInserter, "Agregar Funciones");
            mainMenu.Add(getShowsMenu, "Listar Funciones");
            mainMenu.AddExit();
            return mainMenu;
        }

        public static ICommand CreateShowsPrinterMenu(
            CommandsInjector commands, 
            IEnhacedConsole view
            )
        {
            var menu = new MenuInvoker(view, "Listar funciones por :" +
                                           "\n----------------------\n");
            menu.Add(commands.ShowsByMoviePrinter, "Por pelicula.");
            menu.Add(commands.ShowsByDayPrinter, "Por dia.");
            menu.Add(commands.ShowsByMovieAndDayPrinter,
                "Por dia y película (ambos).");
            menu.Add(commands.ShowsByMovieOrDayPrinter,
                "Por dia ó película (al menos uno).");
            menu.AddExit("Atras");
            return menu;
        }
    }
}