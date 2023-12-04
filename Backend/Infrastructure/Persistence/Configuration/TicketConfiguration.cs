using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            ConfigureNames(builder);
            var constraints = new TicketConstraints();
            builder.HasOne(x => x.ShowNav)
                .WithMany(s => s.Tickets)
                .HasForeignKey(x => x.ShowId);
            builder.HasKey(x => new { x.TicketId, x.ShowId });
            builder.Property(x => x.User)
                .HasMaxLength(constraints.userLength);
        }
        public void ConfigureNames(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.Property(m => m.ShowId)
                   .HasColumnName("FuncionId");
            builder.Property(m => m.User)
                   .HasColumnName("Usuario");
        }
    }
}