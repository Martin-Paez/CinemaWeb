namespace Domain.Entities
{
    public class Genre : SimpleIntegerIdEntity
    {
        public required string Name { get; set; }
    }
}
