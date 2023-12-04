using Domain.Entities;

namespace Application.Interfaces.ICQRS.ICommands
{
    public interface IShowCommands
    {
        public Task<bool> DeleteShow(Show show);

        public Task<bool> AddShow(Show show);
    }
}
