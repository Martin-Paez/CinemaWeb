using System.Text.Json.Serialization;

namespace Application.Dtos.Requests
{
    public class TicketRequiredParamsRequest
    {
        [JsonPropertyName("cantidad")]
        public required int Amount { get; set; }


        [JsonPropertyName("usuario")]
        public required string User { get; set; }
    }
}
