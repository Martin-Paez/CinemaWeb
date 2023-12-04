using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class GenreResponse
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }

        [JsonPropertyName("nombre")]
        public required string Name { get; set; }
    }
}
