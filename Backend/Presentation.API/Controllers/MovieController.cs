using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces.IUseCases.IMovieServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.ErrorHandling;
using Presentation.API.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// Realiza operaciones con las peliculas.
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(HttpResponseFactory))]
    public class PeliculaController : Controller
    {

        private readonly IUpdateMovieService _updateMovieService;
        private readonly IGetMovieByIdService _getMovieByIdService;

        public PeliculaController(
            IUpdateMovieService updateMovieService,
            IGetMovieByIdService getMovieByIdService
            )
        {
            _updateMovieService = updateMovieService;
            _getMovieByIdService = getMovieByIdService;
        }

        /// <summary>
        /// Retorna una pelicula.
        /// </summary>
        /// <param name="movieId">Id de la pelicula.</param>
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
        [Route("/api/v1/Pelicula/{id}")]
        [SwaggerResponse(
            statusCode: 200, 
            type: typeof(MovieResponse), 
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
        public async Task<IActionResult> GetMovie(
            [FromRoute(Name = "id")][Required]int movieId)
        {
            var response = await _getMovieByIdService.GetResponseDto(
                movieId
            );
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion parcial de la informacion de una pelicula.
        /// </summary>
        /// <param name="movieId">Id de la pelicula.</param>
        /// <param name="movieRequest"
        /// >Nueva informacion de la pelicula.
        /// </param>
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
        [HttpPatch]
        [Route("/api/v1/Pelicula/{id}")]
        [SwaggerResponse(
            statusCode: 200, 
            type: typeof(MovieResponse), 
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
        public async Task<IActionResult> UpdateMovie(
            [FromRoute(Name = "id")][Required]int movieId, 
            [FromBody]MovieOptionalParamsRequest movieRequest)
        {
            var result = await _updateMovieService.UpdateMovie(
                movieId,
                movieRequest
            );
            return Ok(result);
        }
    }
}