using Domain.Entities;

namespace Application.Interfaces.CQRS.IQueries
{
    public interface IQuery<Entity>
        where Entity : IBaseEntity
    {
        /// <summary>
        /// Retorna todos los registros de la tabla mapeada al tipo Entity. 
        /// </summary>
        //public Task<IList<Entity>> GetAll();
    }
}
