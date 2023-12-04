using Application.SpecificationPattern;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Quieries
{
    public class EFSpecification<T> : CompositeSpecification<T>
    {
        public Expression<Func<T, bool>> _expression;

        public EFSpecification(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
