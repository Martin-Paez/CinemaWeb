using Application.Dtos.Requests;
using Application.Interfaces.IError;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class MovieValidatorBuilder<T>
        where T : MovieOptionalParamsRequest
    {
        protected readonly DbFieldValidator<T> _validator;
        protected readonly MovieConstraints _constraints;
        protected readonly IErrorMessageFactory _errorMessageFactory;

        public MovieValidatorBuilder(
            IErrorMessageFactory errorMessageFactory
            )
        {
            _validator = new DbFieldValidator<T>(errorMessageFactory);
            _constraints = new MovieConstraints();
            _errorMessageFactory = errorMessageFactory;
        }

        public MovieValidatorBuilder<T> WithSynopsis()
        {
            _validator.AddVarcharRule(
                movie => movie.Synopsis,
                _constraints.SynopsisLength,
                "sinopsis"
            );
            return this;
        }

        protected IRuleBuilderOptions<T, string> TitleBuilder()
        {
            return _validator.AddVarcharRule(
                movie => movie.Title,
                _constraints.TitleLength,
                "titulo"
            );
        }

        public MovieValidatorBuilder<T> WithTitle()
        {
            TitleBuilder();
            return this;
        }
        
        public MovieValidatorBuilder<T> WithPosterUrl()
        {
            _validator.AddVarcharRule(
                movie => movie.PosterUrl,
                _constraints.PosterLength,
                "poster"
            );
            return this;
        }

        public MovieValidatorBuilder<T> WithTrailer()
        {
            _validator.AddVarcharRule(
                movie => movie.Trailer,
                _constraints.TrailerLength,
                "trailer"
            );
            return this;
        }

        public DbFieldValidator<T> Build()
        {
            return _validator;
        }
    }
}