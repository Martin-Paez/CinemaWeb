using System.Text.Json.Serialization;

namespace Application.Dtos.Requests
{
    public class MovieOptionalParamsRequest
    {
        [JsonPropertyName("titulo")]
        public string? Title { get; set; }

        [JsonPropertyName("poster")] 
        public string? PosterUrl { get; set; }

        [JsonPropertyName("trailer")]
        public string? Trailer { get; set; }

        [JsonPropertyName("sinopsis")]
        public string? Synopsis { get; set; }

        [JsonPropertyName("genero")]
        public int? Genre { get; set; }
    }
}
