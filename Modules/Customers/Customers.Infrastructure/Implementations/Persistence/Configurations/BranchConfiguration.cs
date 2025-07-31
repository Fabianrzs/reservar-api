namespace Customers.Infrastructure.Implementations.Persistence.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(b => b.EstablishmentId)
            .IsRequired();

        builder.HasOne(b => b.Establishment)
            .WithMany(e => e.Branches)
            .HasForeignKey(b => b.EstablishmentId);

        builder.OwnsOne(b => b.Address, address =>
        {
            address.Property(a => a.Street).IsRequired().HasMaxLength(100);
            address.Property(a => a.Number).IsRequired().HasMaxLength(20);
            address.Property(a => a.Neighborhood).IsRequired().HasMaxLength(100);
            address.Property(a => a.City).IsRequired().HasMaxLength(100);
            address.Property(a => a.State).IsRequired().HasMaxLength(100);
            address.Property(a => a.Country).IsRequired().HasMaxLength(100);
            address.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
        });

        builder.OwnsOne(b => b.Location, location =>
        {
            location.Property(l => l.Latitude).IsRequired();
            location.Property(l => l.Longitude).IsRequired();
        });
    }
}
