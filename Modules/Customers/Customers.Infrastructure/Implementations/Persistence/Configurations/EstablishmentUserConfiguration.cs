namespace Customers.Infrastructure.Implementations.Persistence.Configurations;

public class EstablishmentUserConfiguration : IEntityTypeConfiguration<EstablishmentUser>
{
    public void Configure(EntityTypeBuilder<EstablishmentUser> builder)
    {
        builder.ToTable("EstablishmentUsers");

        builder.HasKey(eu => eu.Id);

        builder.Property(eu => eu.EstablishmentId).IsRequired();
        builder.Property(eu => eu.UserId).IsRequired();

        builder.HasOne(eu => eu.Establishment)
            .WithMany(e => e.Users)
            .HasForeignKey(eu => eu.EstablishmentId);

        builder.HasIndex(eu => new { eu.EstablishmentId, eu.UserId })
            .IsUnique();
    }
}
