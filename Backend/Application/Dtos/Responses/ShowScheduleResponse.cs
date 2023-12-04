using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class ShowScheduleResponse
    {
        [JsonPropertyName("funcionId")]
        public required int Id { get; set; }

        [JsonPropertyName("fecha")]
        public required DateTime Date { get; set; }

        [JsonPropertyName("horario")]
        public required string Schedule { get; set; }

    }
}
