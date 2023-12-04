using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces.IUseCases.IGenreServices;
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
    public class GeneroController : Controller
    {
        private readonly IGetAllGenreService _getAllGenreService;

        public GeneroController(IGetAllGenreService getAllGenreService)
        {
            _getAllGenreService = getAllGenreService;
        }

        /// <summary>
        /// Retorna una pelicula.
        /// </summary>
        /// <param name="movieId">Id de la pelicula.</param>
        /// <response code="200">
        /// Se completo la operacion sin errores.
        [HttpGet]
        [Route("/api/v1/Genero")]
        [SwaggerResponse(
            statusCode: 200,
            type: typeof(MovieResponse),
            description: "Success")
        ]
        public async Task<IActionResult> GetAll()
        {
            var response = await _getAllGenreService.GetAll();
            return Ok(response);
        }
    }
}