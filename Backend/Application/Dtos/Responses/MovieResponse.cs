using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class MovieResponse : LightMovieResponse
    {
        [JsonPropertyName("trailer")]
        public required string Trailer { get; set; }

        [JsonPropertyName("sinopsis")]
        public required string Synopsis { get; set; }

        [JsonPropertyName("funciones")]
        public required List<ShowScheduleResponse>? Shows { get; set; }
    }
}
