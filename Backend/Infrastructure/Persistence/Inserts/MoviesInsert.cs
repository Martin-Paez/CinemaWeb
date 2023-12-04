using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Inserts
{
    public class MoviesInsert : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Caceria en Venecia",
                    Synopsis = "Hércules Poirot asiste a regañadientes a una sesión de espiritismo. Cuando uno de los invitados es asesinado, el detective se ve inmerso en un siniestro mundo de sombras y secretos.",
                    PosterUrl = "https://th.bing.com/th/id/OIP.PwZP-r6lwxa7jqI9VHU7cwAAAA?w=123&h=180&c=7&r=0&o=5&pid=1.7",
                    Trailer = "https://www.youtube.com/watch?v=JymKmSe5TOk&ab_channel=20thCenturyStudiosLA",
                    Genre = 9
                },
                new Movie
                {
                    Id = 2,
                    Title = "Shrek 2",
                    Synopsis = "Cuando Shrek y la princesa Fiona regresan de su luna de miel, los padres de ella los invitan a visitar el reino de Muy Muy Lejano para celebrar la boda. Para Shrek, al que nunca abandona su fiel amigo Asno, esto constituye un gran problema.",
                    PosterUrl = "https://th.bing.com/th/id/OIP.sI0vbZwcYD1oEHt04j1vQwAAAA?w=115&h=180&c=7&r=0&o=5&pid=1.7",
                    Trailer = "https://www.youtube.com/watch?v=xBxVgh-kgAI&ab_channel=JoyasDeLaAnimaci%C3%B3n",
                    Genre = 7
                },
                new Movie
                {
                    Id = 3,
                    Title = "Toc toc toc",
                    Synopsis = "Un niño que escucha voces en su cabeza decide liberarlas contra sus abusivos padres.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/ComingSoon/200x285/988.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=K27WSoKbP_A&ab_channel=BFDistribution",
                    Genre = 10
                },
                new Movie
                {
                    Id = 4,
                    Title = "Hablame",
                    Synopsis = "Mia (Sophie Wilde) ha pasado años evitando el trauma de la muerte de su madre. Esto cambiará cuando sus amigos descubran cómo conjurar espíritus usando una mano embalsamada.¿En los muertos o los vivos?",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/Posters/9df3a1ba153b3a14d3eca296bca7ba35.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=ZYASq2O1qt0&ab_channel=DiamondFilmsLatam",
                    Genre = 10
                },
                new Movie
                {
                    Id = 5,
                    Title = "Gran turismo",
                    Synopsis = "la película es la historia de un adolescente que juega a Gran Turismo y que, gracias a su habilidad con los videojuegos, gana una serie de concursos de Nissan para convertirse en piloto de carreras profesional.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/ComingSoon/200x285/964.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=VF1Aq8KrQO4&ab_channel=SonyPicturesArgentina",
                    Genre = 1
                },
                new Movie
                {
                    Id = 6,
                    Title = "Avatar",
                    Synopsis = "La trama sigue a Jake Sully, un ex-marine enviado a Pandora, donde se encuentra con los Na'vi, una raza alienígena.",
                    PosterUrl = "https://th.bing.com/th/id/OIP.NNDzj9c4s1ntnvDOwTDNagHaLH?w=115&h=180&c=7&r=0&o=5&pid=1.7",
                    Trailer = "https://www.youtube.com/watch?v=AZS_d_hS2dM&ab_channel=20thCenturyStudiosEspa%C3%B1a",
                    Genre = 3
                },
                new Movie
                {
                    Id = 7,
                    Title = "Elementos",
                    Synopsis = "Disney. City, donde conviven los habitantes del fuego, el agua, la tierra y el aire. La historia nos presenta a Ember, una joven dura, ingeniosa y feroz, cuya amistad con un chico divertido y que se deja llevar por la corriente.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/Posters/6aa4601e49a56d928205c271b55fd463.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=TGZQqHi7lrc&ab_channel=DisneyStudiosLA",
                    Genre = 7
                },
                new Movie
                {
                    Id = 8,
                    Title = "Fragmentada",
                    Synopsis = "Irina, una oficial de policía, regresa a la Patagonia. Sin embargo, el asesinato de la hija de una vieja amiga la lleva a involucrarse en un caso que parece ser ignorado.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/Posters/96dd838f4e7b4622c92b79f792a169be.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=1vqyfkFMkos&ab_channel=PensilvaniaFilms",
                    Genre = 6
                },
                new Movie
                {
                    Id = 9,
                    Title = "Blue Beetle",
                    Synopsis = "Jaime Reyes, mientras busca encontrar su propósito en el mundo, el destino interviene cuando Jaime inesperadamente se encuentra en posesión de una antigua reliquia de biotecnología alienígena: el Escarabajo, cambiando para siempre su destino.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/Posters/b47eae0ffae291cbcbba298ab89c1545.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=Shmvk5M-NV8&ab_channel=WarnerBros.PicturesLatinoam%C3%A9rica",
                    Genre = 1
                },
                new Movie
                {
                    Id = 10,
                    Title = "Scary Movie",
                    Synopsis = "Una parodia de los filmes de asesinatos donde un homicida vengativo acecha a un grupo de adolescentes.",
                    PosterUrl = "https://www.originalfilmart.com/cdn/shop/files/scarymovie_2000_original_film_art.webp?v=1683910671",
                    Trailer = "https://www.youtube.com/watch?v=HTLPULt0eJ4&ab_channel=Miramax",
                    Genre = 4,
                },
                new Movie
                {
                    Id = 11,
                    Title = "The sword of the stranger",
                    Synopsis = "En la era Sengoku, durante el conflicto de los Estados, un ronin conocido como Nanashi (que significa \"sin nombre\") rescata a un niño llamado Kotarou y su leal perro Tobimaru en un templo desolado. ",
                    PosterUrl = "https://th.bing.com/th/id/OIP.8RmTep5x66mXiB7HiOqUUgHaLg?w=116&h=180&c=7&r=0&o=5&pid=1.7",
                    Trailer = "https://www.youtube.com/watch?v=dYGpmgxKpNs&ab_channel=RainHNg",
                    Genre = 2
                },
                new Movie
                {
                    Id = 12,
                    Title = "Kang daniel my parade",
                    Synopsis = "Tras su arrolladora carrera como miembro de uno de los grupos de K-pop más populares de Corea, ha decidido emprender su propio camino. Con el lanzamiento de su primer álbum de estudio.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/Posters/354c9bfec5030e674afdbe739711567e.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=OHMGeuaG_54&ab_channel=KinoHeliosPolska",
                    Genre = 8
                },
                new Movie
                {
                    Id = 13,
                    Title = "Batman: el caballero de la noche",
                    Synopsis = "Batman tiene que mantener el equilibrio entre el heroísmo y el vigilantismo para pelear contra un vil criminal conocido como el Guasón, que pretende sumir Ciudad Gótica en la anarquía.",
                    PosterUrl = "https://img.goodfon.ru/wallpaper/original/1/6f/batman-begins-the-dark-knight-4126.jpg",
                    Trailer = "https://www.youtube.com/watch?v=TQfATDZY5Y4&ab_channel=Legendary",
                    Genre = 1,
                },
                new Movie
                {
                    Id = 14,
                    Title = "Guardianes de la Galaxia",
                    Synopsis = "Un aventurero espacial se convierte en la presa de unos cazadores de tesoros después de robar el orbe de un villano traicionero. Cuando descubre su poder, debe hallar la forma de unir a unos rivales para salvar al universo.",
                    PosterUrl = "https://www.lahiguera.net/cinemania/pelicula/6388/guardianes_de_la_galaxia-cartel-5615.jpg",
                    Trailer = "https://www.youtube.com/watch?v=qdIuXCfUKM8&ab_channel=MarvelLatinoam%C3%A9ricaOficial",
                    Genre = 3,
                },
                new Movie
                {
                    Id = 15,
                    Title = "The quiet girl",
                    Synopsis = "En la Irlanda rural de 1981, Cáit, una niña de nueve años, vive retraída entre su familia numerosa. Sus padres la envían a casa de unos parientes. Allí, la pequeña se hará eco del secreto que esta familia adoptiva.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/Posters/012951356fdd8d9145a7c28c1d72e48e.jpg?v=00002177",
                    Trailer = "https://youtu.be/S9ExUXUIGEY",
                    Genre = 6
                },
                new Movie
                {
                    Id = 16,
                    Title = "Mario Bros",
                    Synopsis = "Dos hermanos plomeros, Mario y Luigi, caen por las alcantarillas y llegan a un mundo subterráneo mágico en el que deben enfrentarse al malvado Bowser para rescatar a la princesa Peach, quien ha sido forzada a aceptar casarse con él.",
                    PosterUrl = "https://th.bing.com/th/id/OIP.e8BVI2EbDHPGc8b7-UUqLAHaJQ?w=178&h=223&c=7&r=0&o=5&pid=1.7",
                    Trailer = "https://www.youtube.com/watch?v=S9ExUXUIGEY&ab_channel=zetafilmstrailerszeta",
                    Genre = 1
                },
                new Movie
                {
                    Id = 17,
                    Title = "Barbie",
                    Synopsis = "Exéntrica e individualista, Barbie, una muñeca que vive en 'Barbieland' es expulsada al mundo real por no ser lo suficientemente perfecta.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/Posters/6546c70272a606d297ad53bfb9162189.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=eUP3hlBel5I&ab_channel=WarnerBros.PicturesLatinoam%C3%A9rica",
                    Genre = 4
                },
                new Movie
                {
                    Id = 18,
                    Title = "Escape bajo fuego",
                    Synopsis = "Tom Harris (Gerard Butler) un agente encubierto de la CIA que trabaja en Oriente Medio se verá en peligro a causa de la filtración de su misión encubierta que revelará su verdadera identidad. Atrapado en pleno territorio enemigo.",
                    PosterUrl = "https://static.cinemarkhoyts.com.ar/Images/ComingSoon/200x285/983.jpg?v=00002177",
                    Trailer = "https://www.youtube.com/watch?v=pJMW_S7jPGc&ab_channel=DiamondFilmsLatam",
                    Genre = 1
                },
                new Movie
                {
                    Id = 19,
                    Title = "Cars",
                    Synopsis = "De camino al prestigiado campeonato Copa Pistón, un automóvil de carreras que sólo se preocupa por ganar aprende sobre lo que realmente es importante en la vida de varios vehículos que viven en un pueblo a lo largo de la histórica Ruta 66.",
                    PosterUrl = "https://m.media-amazon.com/images/I/811IcpNS05L.jpg",
                    Trailer = "https://www.youtube.com/watch?v=SbXIj2T-_uk&ab_channel=nickbtube",
                    Genre = 2,
                },
                new Movie
                {
                    Id = 20,
                    Title = "Julio felices por siempre",
                    Synopsis = "Julio es de la generación criada por padres divorciados, tuvo relaciones amorosas que lo desilusionaron: Blanca lo traicionó y Florencia lo aburrió. Pero todo cambia cuando conoce a Claire, se enamoran profundamente.",
                    PosterUrl = "https://cinenacional.com/wp-content/uploads/2023/07/julio-felices-por-siempre-afiche.jpg",
                    Trailer = "https://www.youtube.com/watch?v=jc2vGui9Hqo&ab_channel=Regi%C3%B3nCinema",
                    Genre = 4
                }
            );
        }
    }
}
