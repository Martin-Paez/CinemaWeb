using Application.Dto.Response.EntityProxy;
using Application.Interfaces.IServices.IRoom;
using ConsoleView;

namespace ConsolePresenter.MenuCommandPattern.BasicPrinters.ConcreteBasicPrinters
{
    public class RoomsPrinter : ResponsePrinter<RoomResponse>
    {
        private IGetAllRoomsService _getRooms;

        public RoomsPrinter(
            IEnhacedConsole cmd, 
            IGetAllRoomsService getRooms
            )
            : base(cmd) 
        {
            _getRooms = getRooms;
        }

        protected override IList<RoomResponse> GetTargets()
        {
            return _getRooms.GetAll();
        }

        protected override string ToStr(RoomResponse room)
        {
            return room.Name;
        }

        protected override string MenuTitle()
        {
            return "Salas:\n------\n";
        }
    }
}
