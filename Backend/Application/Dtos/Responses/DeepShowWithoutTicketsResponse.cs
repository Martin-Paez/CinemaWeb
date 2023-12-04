using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class DeepShowWithoutTicketsResponse : ShowScheduleResponse
    {
        [JsonPropertyName("pelicula")]
        public required LightMovieResponse MovieNav { get; set; }

        [JsonPropertyName("sala")]
        public required ScreenResponse ScreenNav { get; set; }
    }
}
