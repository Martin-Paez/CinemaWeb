using Application.Interfaces.IError;
using FluentValidation;
using System.Linq.Expressions;

namespace Application.Validators
{
    public class DbFieldValidator<T> : AbstractValidator<T>
    {
        private readonly IErrorMessageFactory _errorFactory;

        public DbFieldValidator(
            IErrorMessageFactory errorMessageFactory
            )
        {
            _errorFactory = errorMessageFactory;
        }

        public IRuleBuilderOptions<T, int> AddIdFormatRule<Entity>(
            Expression<Func<T, int>> target
            )
        {
            return RuleFor(target)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage(
                    _errorFactory.InvalidIdFormat<Entity>()
                );
        }

        public IRuleBuilderOptions<T, string> AddVarcharRule(
            Expression<Func<T, string?>> target,
            int maxLength,
            string propertyName,
            int minLength = 1
            )
        {
            return RuleFor(target)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(_errorFactory.Empty(propertyName))
                .Length(minLength, maxLength)
                .WithMessage(_errorFactory.InvalidLength(
                    propertyName,
                    maxLength.ToString()
                ));
        }
    }
}