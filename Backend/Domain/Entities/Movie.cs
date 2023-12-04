namespace Domain.Entities
{
    public class Movie : SimpleIntegerIdEntity
    {
        public required string Title { get; set; }
        public required string Synopsis { get; set; }
        public required string PosterUrl { get; set; }
        public required string Trailer { get; set; }

        public required int Genre { get; set; }
        public Genre? GenreNav { get; set; }

        public ICollection<Show>? Shows { get; set; }
    }

    public class MovieConstraints
    {
        public int TitleLength { get; }
        public int SynopsisLength { get; }
        public int PosterLength { get; }
        public int TrailerLength { get; }

        public MovieConstraints()
        {
            TitleLength = 50;
            SynopsisLength = 255;
            PosterLength = 100;
            TrailerLength = 100;
        }
    }
}
