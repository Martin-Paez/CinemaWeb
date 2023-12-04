namespace Domain.Entities
{
    public class Screen : SimpleIntegerIdEntity
    {
        public required string Name { get; set; }
        public required int Capacity { get; set; }
    }

    public class ScreenConstraints
    {
        public int NameLength { get; }

        public ScreenConstraints()
        {
            NameLength = 50;
        }
    }
}
