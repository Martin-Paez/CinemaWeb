using System.Linq.Expressions;

namespace Application.SpecificationPattern
{
    /// <summary>
    /// Implementacion abstracta de especificion que sera el pader de las
    /// demas especificaciones siguiendo el patron Specification.
    /// </summary>
    /// <typeparam name="T">Tipo de dato de entrada para evaluar la condicion.</typeparam>
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        protected CompositeSpecification() { }

        /// <summary>
        /// Condicion que esta modelanto la Specificacion.
        /// </summary>
        /// <param name="entity">Elemento usado para evaluar la condicion.</param>
        /// <returns></returns>
        public abstract Expression<Func<T, bool>> ToExpression();

        /// <summary>
        /// Operacion AND entre dos especificaciones.
        /// </summary>
        /// <param name="other">Se efectuara la operacion AND entre this y este elemento.</param>
        /// <returns></returns>
        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }
    }
}
