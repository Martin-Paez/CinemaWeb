using Application.Interfaces.CQRS.IQueries;
using Domain.Entities;

namespace Application.Interfaces.ICQRS.IQueries
{
    public interface IScreenQueries
        : ISimpleIntegerIdQuery<Screen>
    {
    }
}
