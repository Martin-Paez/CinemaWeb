using System.Linq.Expressions;

namespace Application.SpecificationPattern
{
    /// <summary>
     /// Operacion AND entre dos especificaciones.
     /// </summary>
     /// <typeparam name="T">Tipo de dato de entrada para evaluar la condicion.</typeparam>
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> _left;
        ISpecification<T> _right;
        
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(T), "candidate");

            var leftBody = new ParameterReplacer(parameter).Visit(leftExpression.Body);
            var rightBody = new ParameterReplacer(parameter).Visit(rightExpression.Body);

            var andExpression = Expression.AndAlso(leftBody, rightBody);

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }

    /// <summary>
    /// Las Specification estan preparadas para usarse en las consultas a la base de
    /// datos. Se aprobecha le poder de consulta de la base de datos y se optimizan 
    /// los recursos de red.
    /// 
    /// La libreria probee un metodo que utiliza el patron visitor para unificar
    /// el parámetro de la expresion, para ello hay que heredar de ExpressionVisitor.
    /// La meta es disponer de una expresion que acepten los metodos de 
    /// EntityFramework para realizar consultas a la base de datos. La combinacion 
    /// de funciones lambda no se pueden procesar por dichos metodos.
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        public ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }
}