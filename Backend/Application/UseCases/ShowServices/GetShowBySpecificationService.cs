using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IGenreServices;
using Application.Interfaces.IUseCases.IShowServices;
using Application.SpecificationPattern;
using Application.Validators;
using Domain.Entities;
using AutoMapper;
using System.Text;

namespace Application.UseCases.ShowServices
{
    /// <summary>
    /// Servicio que permite obtener informacion de las funciones.
    /// </summary>
    public class GetShowBySpecificationService :
        IGetShowBySpecificationService
    {
        private readonly IShowQueries _showQuery;
        private readonly IGetGenreByIdService _getGenreService;
        private readonly IErrorMessageFactory _errMsgFactory;
        private readonly IMapper _mapper;

        public GetShowBySpecificationService(
            IShowQueries repository,
            IMapper mapper,
            IGetGenreByIdService genreQuery,
            IErrorMessageFactory errorMessageFactory
            )
        {
            _showQuery = repository;
            _getGenreService = genreQuery;
            _errMsgFactory = errorMessageFactory;
            _mapper = mapper;
        }

        public async Task<IList<DeepShowWithoutTicketsResponse>> GetBy(
            string? date,
            string? title,
            int? genre
            )
        {
            var errors = new StringBuilder();
            DateTime? datetime = ValidateDateTime(date, errors);
            var specifications = await CreateSpecifications(
                datetime, title, genre, errors 
            );
            IList<Show> shows = null!;
            if (specifications.Count > 0)
            {
                var specification = ApplyAndOperator(specifications);
                shows = await GetBy(specification);
            }
            else
                shows = await _showQuery.GetDeepWithoutTickets();
            return CreateDeepShowResponses(shows);
        }

        private DateTime? ValidateDateTime(
            string? date,
            StringBuilder errorBuilder
            )
        {
            var validator = new DateAndTimeValidator(_errMsgFactory);
            DateTime? datetime = null;
            if (date != null)
                datetime = validator.ValidateDate(date, errorBuilder);
            if (errorBuilder.Length > 0)
                throw new InvalidArgumentsException(errorBuilder.ToString());
            return datetime;
        }

        /// <summary>
        /// Convierte los datos de entrada en especificaciones.
        /// NOTA: Las especificaciones estan preparadas para usarse en las consultas
        /// a la base de datos. Se aprobecha le poder de consulta de la base de datos
        /// y se optimizan los recursos de red.
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="titulo"></param>
        /// <param name="genero"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private async Task<IList<ISpecification<Show>>> CreateSpecifications(
            DateTime? fecha,
            string? titulo,
            int? genero,
            StringBuilder errors
            )
        {
            var specifications = new List<ISpecification<Show>>();
            if (fecha.HasValue)
            {
                var spec = ByDay((DateTime)fecha);
                specifications.Add(spec);
            }
            if (!string.IsNullOrEmpty(titulo))
            {
                var spec = ByTitle(titulo!, errors);
                if (spec != null)
                    specifications.Add(spec);
            }
            if (genero.HasValue)
            {
                var spec = await ByGenre((int)genero);
                if (spec != null)
                    specifications.Add(spec);
            }
            return specifications;
        }

        private ISpecification<Show> ApplyAndOperator(
            IList<ISpecification<Show>> specifications
            )
        {
            var specification = specifications[0];
            var i = 0;
            while (++i < specifications.Count)
                specification = specification.And(specifications[i]);
            return specification;
        }

        private async Task<IList<Show>> GetBy(
           ISpecification<Show> specification
           )
        {
            var entities = await _showQuery.GetDeepWithoutTicketsBy(specification);
            return entities;
        }

        private IList<DeepShowWithoutTicketsResponse> CreateDeepShowResponses(
            IList<Show> shows
            )
        {
            var responses = new List<DeepShowWithoutTicketsResponse>();
            foreach (var entity in shows)
                responses.Add(_mapper.Map<DeepShowWithoutTicketsResponse>(entity));
            return responses;
        }

        /// <summary>
        /// Crea una specification para filtrar todas las funciones de
        /// una pelicula.
        /// </summary>
        /// <param name="movieId">Id de la pelicula</param>
        /// <returns>
        /// Lista de dtos de las funciones que machean
        /// .</returns>
        private ISpecification<Show>? ByTitle(
            string movieTitle,
            StringBuilder errors
            )
        {
            var movieValidator = new MovieValidatorBuilder<MovieOptionalParamsRequest>(_errMsgFactory)
                .WithTitle()
                .Build();
            var movieResult = movieValidator.Validate(
                new MovieOptionalParamsRequest { Title = movieTitle }
            );
            if (movieResult.IsValid)
                return _showQuery.ByTitle(movieTitle);
            errors.Append(movieResult.Errors.First().ErrorMessage);
            return null;
        }

        /// <summary>
        /// Crea una specification para filtrar todas las funciones que
        /// se den en un dia.
        /// </summary>
        /// <param name="date">Dia de la funcion.</param>
        /// <returns>
        /// Lista de dtos de las funciones que machean.
        /// </returns>
        private ISpecification<Show> ByDay(DateTime date)
        {
            return _showQuery.ByDay(date);
        }

        /// <summary>
        /// Crea una specification para filtrar todas las funciones de
        /// peliculas de un determinado genero.
        /// </summary>
        /// <param name="date">Dia de la funcion.</param>
        /// <returns>
        /// Lista de dtos de las funciones que machean.
        /// </returns>
        private async Task<ISpecification<Show>?> ByGenre(
            int genreId
            )
        {
            await _getGenreService.GetResponseDto(genreId);
            return _showQuery.ByGenre(genreId);
        }
    }
}