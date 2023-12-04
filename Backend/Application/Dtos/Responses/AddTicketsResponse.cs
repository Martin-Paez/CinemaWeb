using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class AddTicketsResponse
    {
        [JsonPropertyName("tickets")]
        public required IList<TicketIdDto> Tickets { get; set; }

        [JsonPropertyName("funcion")]
        public required DeepShowWithoutTicketsResponse Show { get; set; }

        [JsonPropertyName("usuario")]
        public required string User { get; set; }
    }
}
