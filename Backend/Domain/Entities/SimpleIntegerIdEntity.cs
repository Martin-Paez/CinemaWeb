namespace Domain.Entities
{
    public abstract class SimpleIntegerIdEntity : IBaseEntity
    {
        public int? Id { get; set; }
    }
}
