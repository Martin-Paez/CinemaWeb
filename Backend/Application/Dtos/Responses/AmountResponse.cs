using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class AmountResponse
    {
        [JsonPropertyName("cantidad")]
        public required int Amount { get; set; }
    }
}
