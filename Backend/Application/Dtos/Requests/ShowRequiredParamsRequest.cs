using System.Text.Json.Serialization;

namespace Application.Dtos.Requests
{
    public class ShowRequiredParamsRequest
    {
        [JsonPropertyName("pelicula")]
        public required int MovieId { get; set; }

        [JsonPropertyName("sala")]
        public required int ScreenId { get; set; }

        [JsonPropertyName("fecha")]
        public required string Date { get; set; }

        [JsonPropertyName("horario")]
        public required string Schedule { get; set; }
    }
}
