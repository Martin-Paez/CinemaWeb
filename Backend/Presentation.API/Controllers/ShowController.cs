using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces.IUseCases.IShowServices;
using Application.Interfaces.IUseCases.ITicketServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.ErrorHandling;
using Presentation.API.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// Realizar operaciones con las funciones.
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(HttpResponseFactory))]
    public class FuncionController : ControllerBase
    {
        private readonly IGetDeepShowByIdWithoutTicketsService _getShowByService;
        private readonly IGetShowBySpecificationService _getShowBySpecification;
        private readonly IDeleteShowService _deleteShowByService;
        private readonly IAddShowService _addShowByService;
        private readonly IGetTicketsAmountService _getTicketsAmountService;
        private readonly IAddTicketService _addTicketService;
        private readonly IConfiguration _configuration;

        public FuncionController(
            IGetDeepShowByIdWithoutTicketsService getShowByService,
            IDeleteShowService deleteShowByService,
            IAddShowService addShowByService,
            IConfiguration configuration,
            IGetTicketsAmountService getTicketsAmountService,
            IAddTicketService addTicketService,
            IGetShowBySpecificationService getShowBySpecification)
        {
            _getShowByService = getShowByService;
            _deleteShowByService = deleteShowByService;
            _addShowByService = addShowByService;
            _configuration = configuration;
            _getTicketsAmountService = getTicketsAmountService;
            _addTicketService = addTicketService;
            _getShowBySpecification = getShowBySpecification;
        }

        /// <summary>
        /// Retorna funciones filtradas por fecha, titulo y/o genero.
        /// </summary>
        /// <param name="date">Fecha de la funcion.</param>
        /// <param name="title">Titulo de la pelicula.</param>
        /// <param name="genreId">Genero de la pelicula.</param>
        /// <response code="200">
        /// Se completo la operacion sin errores.
        /// </response>
        /// <response code="400">
        /// Bad Request: La informacion recibida es incorrecta.
        /// </response>
        [HttpGet]
        [Route("/api/v1/Funcion")]
        [SwaggerResponse(
            statusCode: 200, 
            type: typeof(List<DeepShowWithoutTicketsResponse>), 
            description: "Success")
        ]
        [SwaggerResponse(
            statusCode: 400, 
            type: typeof(ErrorResponseExample), 
            description: "Bad Request")
        ]
        public async Task<IActionResult> FilterShows(
            [FromQuery(Name ="fecha")]string? date, 
            [FromQuery(Name ="titulo")]string? title, 
            [FromQuery(Name ="genero")]int? genreId
            )
        {
            var result = await _getShowBySpecification.GetBy(
                date, 
                title, 
                genreId
            );
            return Ok(result);
        }

        /// <summary>
        /// Inserta una funcion.
        /// </summary>
        /// <param name="showRequest">InformacioN de la funcion.</param>
        /// <response code="201">
        /// Se registro la funcion correctamente.
        /// </response>
        /// <response code="400">
        /// Bad Request: La informacion recibida es incorrecta.
        /// </response>
        /// <response code="409">
        /// Conflict: Se produjo un error de concurrencia en la BD.
        /// </response>
        [HttpPost]
        [Route("/api/v1/Funcion")]
        [SwaggerResponse(
            statusCode: 201, 
            type: typeof(DeepShowWithoutTicketsResponse), 
            description: "Success")
        ]
        [SwaggerResponse(
            statusCode: 400, 
            type: typeof(ErrorResponseExample), 
            description: "Bad Request")
        ]
        [SwaggerResponse(
            statusCode: 409, 
            type: typeof(ErrorResponseExample), 
            description: "Conflict")
        ]
        public async Task<IActionResult> AddShow(
            [FromBody] ShowRequiredParamsRequest showRequest
            )
        {
            var duration = int.Parse(_configuration["showDurationMinutes"]!);
            var response = await _addShowByService.AddShow(
                showRequest,
                duration
            );
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var url = baseUrl + "/api/v1/Funcion/" + response.Id;
            return Created(url, response);
        }

        [HttpPost]
        [Route("/api/v1/Funcion/All")]
        [SwaggerResponse(
            statusCode: 201,
            type: typeof(DeepShowWithoutTicketsResponse),
            description: "Success")
        ]
        [SwaggerResponse(
            statusCode: 400,
            type: typeof(ErrorResponseExample),
            description: "Bad Request")
        ]
        [SwaggerResponse(
            statusCode: 409,
            type: typeof(ErrorResponseExample),
            description: "Conflict")
        ]
        public async Task<IActionResult> AddShow(
            [FromBody] List<ShowRequiredParamsRequest> showRequests
            )
        {
            var duration = int.Parse(_configuration["showDurationMinutes"]!);
            DeepShowWithoutTicketsResponse response = null;
            foreach (var showRequest in showRequests)
                response = await _addShowByService.AddShow(
                    showRequest,
                    duration
                );
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var url = baseUrl + "/api/v1/Funcion/" + response.Id;
            return Created(url, response);
        }

        /// <summary>
        /// Retorna una funcion.
        /// </summary>
        /// <param name="showId">Id de la funcion.</param>
        /// <response code="200">
        /// Se completo la operacion sin errores.
        /// </response>
        /// <response code="400">
        /// Bad Request: La informacion recibida es incorrecta.
        /// </response>
        /// <response code="404">
        /// Not Fount: No hay un elemento registrado con el id recibido.
        /// </response>
        [HttpGet]
        [Route("/api/v1/Funcion/{id}")]
        [SwaggerResponse(
            statusCode: 200, 
            type: typeof(DeepShowWithoutTicketsResponse), 
            description: "Success")
        ]
        [SwaggerResponse(
            statusCode: 400, 
            type: typeof(ErrorResponseExample), 
            description: "Bad Request")
        ]
        [SwaggerResponse(
            statusCode: 404, 
            type: typeof(ErrorResponseExample), 
            description: "Not Found")
        ]
        public async Task<IActionResult> GetShow(
            [FromRoute(Name ="id")][Required]int showId
            )
        {
            var response = await _getShowByService.GetResponseDto(showId);
            return Ok(response);
        }

        /// <summary>
        /// Borra una funcion.
        /// </summary>
        /// <param name="showId">Id de la funcion.</param>
        /// <response code="200">
        /// Se completo la operacion sin errores.
        /// </response>
        /// <response code="400">
        /// Bad Request: La informacion recibida es incorrecta.
        /// </response>
        /// <response code="404">
        /// Not Fount: No hay un elemento con el id recibido.
        /// </response>
        /// <response code="409">
        /// Conflict: Se produjo un error de concurrencia en la BD.
        /// </response>
        [HttpDelete]
        [Route("/api/v1/Funcion/{id}")]
        [SwaggerResponse(
            statusCode: 200, 
            type: typeof(ShowScheduleResponse), 
            description: "Success")
        ]
        [SwaggerResponse(
            statusCode: 400, 
            type: typeof(ErrorResponseExample), 
            description: "Bad Request")
        ]
        [SwaggerResponse(
            statusCode: 404, 
            type: typeof(ErrorResponseExample), 
            description: "Not Found")
        ]
        [SwaggerResponse(
            statusCode: 409, 
            type: typeof(ErrorResponseExample), 
            description: "Conflict")
        ]
        public async Task<IActionResult> DeleteShow(
            [FromRoute(Name ="id")][Required] int showId
            )
        {
            var result = await _deleteShowByService.DeleteShow(showId);
            return Ok(result);
        }

        /// <summary>
        /// Retorna la cantidad de tickets vendidos para una funcion.
        /// </summary>
        /// <param name="showId">Id de la funcion.</param>
        /// <response code="200">
        /// Se completo la operacion sin errores.
        /// </response>
        /// <response code="400">
        /// Bad Request: La informacion recibida es incorrecta.
        /// </response>
        /// <response code="404">
        /// Not Fount: No hay un elemento con el id recibido.
        /// </response>
        [HttpGet]
        [Route("/api/v1/Funcion/{id}/tickets")]
        [SwaggerResponse(
            statusCode: 200,
            type: typeof(AmountResponse),
            description: "Success"
        )]
        [SwaggerResponse(
            statusCode: 400,
            type: typeof(ErrorResponseExample),
            description: "Bad Request"
        )]
        [SwaggerResponse(
            statusCode: 404,
            type: typeof(ErrorResponseExample),
            description: "Not Found"
        )]
        public async Task<IActionResult> TicketsCount(
            [FromRoute(Name = "id")][Required] int showId
            )
        {
            int amount = await _getTicketsAmountService.GetTicketsAmount(showId);
            return Ok(new AmountResponse() { Amount = amount });
        }

        /// <summary>
        /// Registra un ticket para una funcion.
        /// </summary>
        /// <param name="showId">Id de la funcion</param>
        /// <param name="ticketRequest">Ticket a registrar</param>
        /// <response code="200">
        /// Se completo la operacion sin errores.
        /// </response>
        /// <response code="400">
        /// Bad Request: La informacion recibida es incorrecta.
        /// </response>
        /// <response code="404">
        /// Not Fount: No hay capacidad suficiente
        /// </response>
        [HttpPost]
        [Route("/api/v1/Funcion/{id}/tickets")]
        [SwaggerResponse(
            statusCode: 200, 
            type: typeof(AddTicketsResponse), 
            description: "Success")
        ]
        [SwaggerResponse(
            statusCode: 400, 
            type: typeof(ErrorResponseExample), 
            description: "Bad Request")
        ]
        [SwaggerResponse(
            statusCode: 404, 
            type: typeof(ErrorResponseExample), 
            description: "Not Found")
        ]
        public async Task<IActionResult> AddTicket(
            [FromRoute(Name="id")][Required]int showId,
            [FromBody]TicketRequiredParamsRequest ticketRequest
            )
        {
                var result = await _addTicketService.AddTicket(
                showId, 
                ticketRequest
            );
            return Ok(result);
        }
    }
}   