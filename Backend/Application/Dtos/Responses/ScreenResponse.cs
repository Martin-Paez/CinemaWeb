using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class ScreenResponse
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }

        [JsonPropertyName("nombre")]
        public required string Name { get; set; }

        [JsonPropertyName("capacidad")]
        public required int Capacity { get; set; }
    }
}
