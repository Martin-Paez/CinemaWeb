using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.ICommands;
using Application.Interfaces.IUseCases.IGenreServices;
using Application.Interfaces.IUseCases.IMovieServices;
using Application.Validators;
using Domain.Entities;
using System.Text;

namespace Application.UseCases.MovieServices
{
    public class UpdateMovieService : IUpdateMovieService
    {
        private readonly IMovieCommand _movieCommand;
        private readonly IExistsMovieTitleService _existsMovieTitleService;
        private readonly IGetMovieByIdService _getMovieService;
        private readonly IGetGenreByIdService _getGenreService;
        private readonly IErrorMessageFactory _errorMessageFactory;

        public UpdateMovieService(
            IMovieCommand movieCommand,
            IGetMovieByIdService getMovieService,
            IGetGenreByIdService genreQuery,
            IErrorMessageFactory errorMessageFactory,
            IExistsMovieTitleService existsMovieTitleService)
        {
            _movieCommand = movieCommand;
            _getMovieService = getMovieService;
            _getGenreService = genreQuery;
            _errorMessageFactory = errorMessageFactory;
            _existsMovieTitleService = existsMovieTitleService;
        }

        public async Task<MovieResponse> UpdateMovie(
            int movieId,
            MovieOptionalParamsRequest movieRequest
            )
        {
            var errorsBuilder = new StringBuilder();
            Movie? movie = await _getMovieService.GetEntityWithInvalidIdFormatInformation(
                movieId,
                errorsBuilder
            );
            await ValidateAndMergeUpdateRequestDto(movieRequest, movie, errorsBuilder);
            await ValidateRepeatedTitle(movieRequest.Title);
            await _movieCommand.UpdateMovie(movie!);
            return await _getMovieService.GetResponseDto(movieId);
        }

        /// <summary>
        /// Valida la informacion recibida en el DtoRequest en busca de un BadRequest.
        /// Además carga la información del Dto en la Entity.
        /// 
        /// El motivo por el cual tiene ambas responsabilidades es para aprobechar la
        /// logica de analizar cuales son los campos que el usuario desea modificar.
        /// 
        /// Se contempla que el parámetro movie puede ser null para el caso en que se 
        /// halla validado que el formato de la pelicula sea incorrecto y se desee
        /// seguir validando el resto de los campos de la Request, para informar todos
        /// los errores encontrados. 
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="movieRequest"></param>
        /// <returns></returns>
        private async Task ValidateAndMergeUpdateRequestDto(
            MovieOptionalParamsRequest movieRequest,
            Movie? movie,
            StringBuilder errorsBuilder
            )
        {
            var validator = CreateValidatorAndMergeNoEmptyFields(
                movie,
                movieRequest
            );
            var movieResult = await validator.ValidateAsync(movieRequest);
            if (!movieResult.IsValid || movie == null)
            {
                foreach (var err in movieResult.Errors)
                    errorsBuilder.Append(err.ErrorMessage);
                throw new InvalidArgumentsException(errorsBuilder.ToString());
            }
        }

        /// <summary>
        /// Crea un validador personalizado para los campos de la DtoRequest que no son
        /// null. Además carga la información del Dto en la Entity.
        /// 
        /// El motivo por el cual tiene ambas responsabilidades es para aprobechar la
        /// logica de analizar cuales son los campos que el usuario desea modificar.
        /// 
        /// Esta pensado para los campos que no fueron enviados en el json de un Patch.
        /// Se contempla que el parámetro movie puede ser null para el caso en que se 
        /// halla validado que el formato de la pelicula sea incorrecto y se desee
        /// seguir validando el resto de los campos de la Request, para informar todos
        /// los errores encontrados.
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="movieRequest"></param>
        /// <returns></returns>
        private DbFieldValidator<MovieOptionalParamsRequest> CreateValidatorAndMergeNoEmptyFields(
            Movie? movie,
            MovieOptionalParamsRequest movieRequest
            )
        {
            var validator = new ExtendedMovieValidatorBuilder<MovieOptionalParamsRequest>(
                _errorMessageFactory,
                _getGenreService
            );
            if (movie == null)
                validator.ValidateOnlyNoEmptyAttributes(movieRequest);
            else
                ConfigureValidatorAndMergeNoEmptyFields(
                    movie, 
                    movieRequest,
                    validator
                 );
            return validator.Build();
        }

        private DbFieldValidator<MovieOptionalParamsRequest> ConfigureValidatorAndMergeNoEmptyFields(
            Movie movie,
            MovieOptionalParamsRequest movieRequest,
            ExtendedMovieValidatorBuilder<MovieOptionalParamsRequest> validator
            )
        {
            if (movieRequest.Title != null)
            {
                movie.Title = movieRequest.Title!;
                validator.WithTitle();
            }
            if (movieRequest.Genre != null)
            {
                movie.Genre = (int)movieRequest.Genre!;
                validator.WithGenre();
            }
            if (movieRequest.PosterUrl != null)
            {
                movie.PosterUrl = movieRequest.PosterUrl!;
                validator.WithPosterUrl();
            }
            if (movieRequest.Synopsis != null)
            {
                movie.Synopsis = movieRequest.Synopsis!;
                validator.WithSynopsis();
            }
            if (movieRequest.Trailer != null)
            {
                movie.Trailer = movieRequest.Trailer!;
                validator.WithTrailer();
            }
            return validator.Build();
        }

        private async Task ValidateRepeatedTitle(
            string? title
            )
        {
            if (title != null)
            {
                var repeatedTitle = await _existsMovieTitleService
                                .ExistMovieTitle(title!);
                if (repeatedTitle)
                    throw new ConflicException(_errorMessageFactory.unavailableTitle());
            }
        }
    }
}