using Domain.Entities;

namespace Application.Interfaces.CQRS.IQueries
{
    public interface ISimpleIntegerIdQuery<Entity> :
        IQuery<Entity>
        where Entity : SimpleIntegerIdEntity
    {
        public Task<Entity?> GetBySimpleIntegerPk(int id);
        public Task<bool> Exists(int id);
    }
}
