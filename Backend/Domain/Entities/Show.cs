namespace Domain.Entities
{
    public class Show : SimpleIntegerIdEntity
    {
        public required TimeSpan Schedule { get; set; }
        public required DateTime Date { get; set; }
        
        public required int ScreenId { get; set; }
        public Screen? ScreenNav { get; set; }
        
        public required int MovieId { get; set; }
        public Movie? MovieNav { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
    }
}
