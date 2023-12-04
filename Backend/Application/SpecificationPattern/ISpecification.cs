using System.Linq.Expressions;

namespace Application.SpecificationPattern
{
    /// <summary>
    /// Modela condiciones que pueden anidarse con operaciones logicas.
    /// </summary>
    /// <typeparam name="T">Tipo de dato de entrada para evaluar la condicion.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Condicion que esta modelanto la Specificacion.
        /// </summary>
        /// <param name="entity">Elemento usado para evaluar la condicion.</param>
        /// <returns></returns>
        public Expression<Func<T, bool>> ToExpression();

        /// <summary>
        /// Operacion AND entre dos especificaciones.
        /// </summary>
        /// <param name="other">Se efectuara la operacion AND entre this y este elemento.</param>
        /// <returns></returns>
        public ISpecification<T> And(ISpecification<T> spec);
    }
}