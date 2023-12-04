using Application.Dtos.Requests;
using Application.Interfaces.IError;
using Application.Interfaces.IUseCases.IGenreServices;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{

    public class ExtendedMovieValidatorBuilder<T> :
        MovieValidatorBuilder<T>
        where T : MovieOptionalParamsRequest
    {
        private readonly IGetGenreByIdService _getGenreService;

        public ExtendedMovieValidatorBuilder(
            IErrorMessageFactory errorMessageFactory,
            IGetGenreByIdService getGenreService
            )
            : base(errorMessageFactory)
        {
            _getGenreService = getGenreService;
        }

        public MovieValidatorBuilder<T> WithGenre()
        {
            _validator.AddIdFormatRule<Genre>(movie => (int)movie.Genre!)
                .MustAsync(
                    async (genre, cancellation) => await _getGenreService!
                        .Exists((int)genre!)
                 )
                .WithMessage(
                    _errorMessageFactory.NotFoundById<Genre>()
                );
            return this;
        }

        public MovieValidatorBuilder<T> ValidateOnlyNoEmptyAttributes(
            MovieOptionalParamsRequest movieRequest
            )
        {
            if (movieRequest.Title != null) WithTitle();
            if (movieRequest.PosterUrl != null) WithPosterUrl();
            if (movieRequest.Trailer != null) WithTrailer();
            if (movieRequest.Synopsis != null) WithSynopsis();
            if (movieRequest.Genre != null) WithGenre();
            return this;
        }
    }
}
