namespace Domain.Entities
{
    public class Ticket : IBaseEntity
    {
        public required Guid TicketId { get; set; }
        public required string User { get; set; }
        public required int ShowId { get; set; }
        public Show? ShowNav { get; set; }
    }

    public class TicketConstraints
    {
        public int userLength { get; }

        public TicketConstraints()
        {
            userLength = 50;
        }
    }
}
