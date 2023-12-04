namespace Application.Interfaces.IUseCases.ITicketServices
{
    public interface IGetTicketsAmountService
    {
        public Task<int> GetTicketsAmount(int showId);
    }
}
