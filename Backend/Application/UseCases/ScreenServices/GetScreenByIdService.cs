using Application.Dtos.Responses;
using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IRoomServices;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.ScreenServices
{
    public class GetScreenByIdService : 
        GetByIdService<Screen, ScreenResponse>,
        IGetScreenByIdService
    {
        private readonly IScreenQueries _roomQueries;

        public GetScreenByIdService(
            IScreenQueries roomQueries,
            IMapper mapper,
            IErrorMessageFactory errorMessageFactory
            )
            :base( mapper, errorMessageFactory )
        {
            _roomQueries = roomQueries;
        }

        protected override async Task<Screen?> GetFromRepository(int roomId)
        {
            return await _roomQueries.GetBySimpleIntegerPk(roomId);
        }

        protected override Task<bool> ExistsInRepository(int id)
        {
            return _roomQueries.Exists(id);
        }
    }
}
