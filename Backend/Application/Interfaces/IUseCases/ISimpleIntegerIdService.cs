using Domain.Entities;
using System.Text;

namespace Application.Interfaces.IUseCases
{
    public interface ISimpleIntegerIdService<Entity>
        where Entity : IBaseEntity
    {
        public Task<bool> Exists(int id);
        public Task<bool> Exists(int id, StringBuilder errorBuilder);

        public Task<Entity> GetEntityStrict(int id);

        public Task<Entity?> GetEntityWithInvalidIdFormatInformation(
            int id,
            StringBuilder errorBuilder
        );
    }

    public interface ISimpleIntegerIdService<Entity, Response> :
        ISimpleIntegerIdService<Entity>
        where Response : class
        where Entity : IBaseEntity
    {
        public Task<Response> GetResponseDto(int entityId);
    }
}
