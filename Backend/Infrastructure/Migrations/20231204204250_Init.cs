using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    GeneroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    SalaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.SalaId);
                });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sonopsis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Trailer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.PeliculaId);
                    table.ForeignKey(
                        name: "FK_Peliculas_Generos_Genero",
                        column: x => x.Genero,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funciones",
                columns: table => new
                {
                    FuncionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horario = table.Column<TimeSpan>(type: "time", nullable: false),
                    Fecha = table.Column<DateTime>(type: "DATE", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funciones", x => x.FuncionId);
                    table.ForeignKey(
                        name: "FK_Funciones_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "PeliculaId");
                    table.ForeignKey(
                        name: "FK_Funciones_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionId = table.Column<int>(type: "int", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => new { x.TicketId, x.FuncionId });
                    table.ForeignKey(
                        name: "FK_Tickets_Funciones_FuncionId",
                        column: x => x.FuncionId,
                        principalTable: "Funciones",
                        principalColumn: "FuncionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Accion" },
                    { 2, "Aventura" },
                    { 3, "Ciencia Ficcion" },
                    { 4, "Comedia" },
                    { 5, "Documental" },
                    { 6, "Drama" },
                    { 7, "Fantasia" },
                    { 8, "Musical" },
                    { 9, "Suspenso" },
                    { 10, "Terror" }
                });

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "SalaId", "Capacidad", "Nombre" },
                values: new object[,]
                {
                    { 1, 5, "Sala A" },
                    { 2, 15, "Sala B" },
                    { 3, 35, "Sala C" }
                });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "PeliculaId", "Genero", "Poster", "Sonopsis", "Titulo", "Trailer" },
                values: new object[,]
                {
                    { 1, 9, "https://th.bing.com/th/id/OIP.PwZP-r6lwxa7jqI9VHU7cwAAAA?w=123&h=180&c=7&r=0&o=5&pid=1.7", "Hércules Poirot asiste a regañadientes a una sesión de espiritismo. Cuando uno de los invitados es asesinado, el detective se ve inmerso en un siniestro mundo de sombras y secretos.", "Caceria en Venecia", "https://www.youtube.com/watch?v=JymKmSe5TOk&ab_channel=20thCenturyStudiosLA" },
                    { 2, 7, "https://th.bing.com/th/id/OIP.sI0vbZwcYD1oEHt04j1vQwAAAA?w=115&h=180&c=7&r=0&o=5&pid=1.7", "Cuando Shrek y la princesa Fiona regresan de su luna de miel, los padres de ella los invitan a visitar el reino de Muy Muy Lejano para celebrar la boda. Para Shrek, al que nunca abandona su fiel amigo Asno, esto constituye un gran problema.", "Shrek 2", "https://www.youtube.com/watch?v=xBxVgh-kgAI&ab_channel=JoyasDeLaAnimaci%C3%B3n" },
                    { 3, 10, "https://static.cinemarkhoyts.com.ar/Images/ComingSoon/200x285/988.jpg?v=00002177", "Un niño que escucha voces en su cabeza decide liberarlas contra sus abusivos padres.", "Toc toc toc", "https://www.youtube.com/watch?v=K27WSoKbP_A&ab_channel=BFDistribution" },
                    { 4, 10, "https://static.cinemarkhoyts.com.ar/Images/Posters/9df3a1ba153b3a14d3eca296bca7ba35.jpg?v=00002177", "Mia (Sophie Wilde) ha pasado años evitando el trauma de la muerte de su madre. Esto cambiará cuando sus amigos descubran cómo conjurar espíritus usando una mano embalsamada.¿En los muertos o los vivos?", "Hablame", "https://www.youtube.com/watch?v=ZYASq2O1qt0&ab_channel=DiamondFilmsLatam" },
                    { 5, 1, "https://static.cinemarkhoyts.com.ar/Images/ComingSoon/200x285/964.jpg?v=00002177", "la película es la historia de un adolescente que juega a Gran Turismo y que, gracias a su habilidad con los videojuegos, gana una serie de concursos de Nissan para convertirse en piloto de carreras profesional.", "Gran turismo", "https://www.youtube.com/watch?v=VF1Aq8KrQO4&ab_channel=SonyPicturesArgentina" },
                    { 6, 3, "https://th.bing.com/th/id/OIP.NNDzj9c4s1ntnvDOwTDNagHaLH?w=115&h=180&c=7&r=0&o=5&pid=1.7", "La trama sigue a Jake Sully, un ex-marine enviado a Pandora, donde se encuentra con los Na'vi, una raza alienígena.", "Avatar", "https://www.youtube.com/watch?v=AZS_d_hS2dM&ab_channel=20thCenturyStudiosEspa%C3%B1a" },
                    { 7, 7, "https://static.cinemarkhoyts.com.ar/Images/Posters/6aa4601e49a56d928205c271b55fd463.jpg?v=00002177", "Disney. City, donde conviven los habitantes del fuego, el agua, la tierra y el aire. La historia nos presenta a Ember, una joven dura, ingeniosa y feroz, cuya amistad con un chico divertido y que se deja llevar por la corriente.", "Elementos", "https://www.youtube.com/watch?v=TGZQqHi7lrc&ab_channel=DisneyStudiosLA" },
                    { 8, 6, "https://static.cinemarkhoyts.com.ar/Images/Posters/96dd838f4e7b4622c92b79f792a169be.jpg?v=00002177", "Irina, una oficial de policía, regresa a la Patagonia. Sin embargo, el asesinato de la hija de una vieja amiga la lleva a involucrarse en un caso que parece ser ignorado.", "Fragmentada", "https://www.youtube.com/watch?v=1vqyfkFMkos&ab_channel=PensilvaniaFilms" },
                    { 9, 1, "https://static.cinemarkhoyts.com.ar/Images/Posters/b47eae0ffae291cbcbba298ab89c1545.jpg?v=00002177", "Jaime Reyes, mientras busca encontrar su propósito en el mundo, el destino interviene cuando Jaime inesperadamente se encuentra en posesión de una antigua reliquia de biotecnología alienígena: el Escarabajo, cambiando para siempre su destino.", "Blue Beetle", "https://www.youtube.com/watch?v=Shmvk5M-NV8&ab_channel=WarnerBros.PicturesLatinoam%C3%A9rica" },
                    { 10, 4, "https://www.originalfilmart.com/cdn/shop/files/scarymovie_2000_original_film_art.webp?v=1683910671", "Una parodia de los filmes de asesinatos donde un homicida vengativo acecha a un grupo de adolescentes.", "Scary Movie", "https://www.youtube.com/watch?v=HTLPULt0eJ4&ab_channel=Miramax" },
                    { 11, 2, "https://th.bing.com/th/id/OIP.8RmTep5x66mXiB7HiOqUUgHaLg?w=116&h=180&c=7&r=0&o=5&pid=1.7", "En la era Sengoku, durante el conflicto de los Estados, un ronin conocido como Nanashi (que significa \"sin nombre\") rescata a un niño llamado Kotarou y su leal perro Tobimaru en un templo desolado. ", "The sword of the stranger", "https://www.youtube.com/watch?v=dYGpmgxKpNs&ab_channel=RainHNg" },
                    { 12, 8, "https://static.cinemarkhoyts.com.ar/Images/Posters/354c9bfec5030e674afdbe739711567e.jpg?v=00002177", "Tras su arrolladora carrera como miembro de uno de los grupos de K-pop más populares de Corea, ha decidido emprender su propio camino. Con el lanzamiento de su primer álbum de estudio.", "Kang daniel my parade", "https://www.youtube.com/watch?v=OHMGeuaG_54&ab_channel=KinoHeliosPolska" },
                    { 13, 1, "https://img.goodfon.ru/wallpaper/original/1/6f/batman-begins-the-dark-knight-4126.jpg", "Batman tiene que mantener el equilibrio entre el heroísmo y el vigilantismo para pelear contra un vil criminal conocido como el Guasón, que pretende sumir Ciudad Gótica en la anarquía.", "Batman: el caballero de la noche", "https://www.youtube.com/watch?v=TQfATDZY5Y4&ab_channel=Legendary" },
                    { 14, 3, "https://www.lahiguera.net/cinemania/pelicula/6388/guardianes_de_la_galaxia-cartel-5615.jpg", "Un aventurero espacial se convierte en la presa de unos cazadores de tesoros después de robar el orbe de un villano traicionero. Cuando descubre su poder, debe hallar la forma de unir a unos rivales para salvar al universo.", "Guardianes de la Galaxia", "https://www.youtube.com/watch?v=qdIuXCfUKM8&ab_channel=MarvelLatinoam%C3%A9ricaOficial" },
                    { 15, 6, "https://static.cinemarkhoyts.com.ar/Images/Posters/012951356fdd8d9145a7c28c1d72e48e.jpg?v=00002177", "En la Irlanda rural de 1981, Cáit, una niña de nueve años, vive retraída entre su familia numerosa. Sus padres la envían a casa de unos parientes. Allí, la pequeña se hará eco del secreto que esta familia adoptiva.", "The quiet girl", "https://youtu.be/S9ExUXUIGEY" },
                    { 16, 1, "https://th.bing.com/th/id/OIP.e8BVI2EbDHPGc8b7-UUqLAHaJQ?w=178&h=223&c=7&r=0&o=5&pid=1.7", "Dos hermanos plomeros, Mario y Luigi, caen por las alcantarillas y llegan a un mundo subterráneo mágico en el que deben enfrentarse al malvado Bowser para rescatar a la princesa Peach, quien ha sido forzada a aceptar casarse con él.", "Mario Bros", "https://www.youtube.com/watch?v=S9ExUXUIGEY&ab_channel=zetafilmstrailerszeta" },
                    { 17, 4, "https://static.cinemarkhoyts.com.ar/Images/Posters/6546c70272a606d297ad53bfb9162189.jpg?v=00002177", "Exéntrica e individualista, Barbie, una muñeca que vive en 'Barbieland' es expulsada al mundo real por no ser lo suficientemente perfecta.", "Barbie", "https://www.youtube.com/watch?v=eUP3hlBel5I&ab_channel=WarnerBros.PicturesLatinoam%C3%A9rica" },
                    { 18, 1, "https://static.cinemarkhoyts.com.ar/Images/ComingSoon/200x285/983.jpg?v=00002177", "Tom Harris (Gerard Butler) un agente encubierto de la CIA que trabaja en Oriente Medio se verá en peligro a causa de la filtración de su misión encubierta que revelará su verdadera identidad. Atrapado en pleno territorio enemigo.", "Escape bajo fuego", "https://www.youtube.com/watch?v=pJMW_S7jPGc&ab_channel=DiamondFilmsLatam" },
                    { 19, 2, "https://m.media-amazon.com/images/I/811IcpNS05L.jpg", "De camino al prestigiado campeonato Copa Pistón, un automóvil de carreras que sólo se preocupa por ganar aprende sobre lo que realmente es importante en la vida de varios vehículos que viven en un pueblo a lo largo de la histórica Ruta 66.", "Cars", "https://www.youtube.com/watch?v=SbXIj2T-_uk&ab_channel=nickbtube" },
                    { 20, 4, "https://cinenacional.com/wp-content/uploads/2023/07/julio-felices-por-siempre-afiche.jpg", "Julio es de la generación criada por padres divorciados, tuvo relaciones amorosas que lo desilusionaron: Blanca lo traicionó y Florencia lo aburrió. Pero todo cambia cuando conoce a Claire, se enamoran profundamente.", "Julio felices por siempre", "https://www.youtube.com/watch?v=jc2vGui9Hqo&ab_channel=Regi%C3%B3nCinema" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_PeliculaId",
                table: "Funciones",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_SalaId",
                table: "Funciones",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_Genero",
                table: "Peliculas",
                column: "Genero");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FuncionId",
                table: "Tickets",
                column: "FuncionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Funciones");

            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
