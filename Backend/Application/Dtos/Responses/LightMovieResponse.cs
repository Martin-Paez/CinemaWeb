using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class LightMovieResponse
    {
        [JsonPropertyName("peliculaId")]
        public required int Id { get; set; }

        [JsonPropertyName("titulo")]
        public required string Title { get; set; }

        [JsonPropertyName("poster")]
        public required string PosterUrl { get; set; }

        [JsonPropertyName("genero")]
        public required GenreResponse? GenreNav { get; set; }
    }
}