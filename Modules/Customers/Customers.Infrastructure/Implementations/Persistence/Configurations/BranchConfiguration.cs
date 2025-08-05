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
            address.Property(a => a.Street).HasColumnName("Street")
            .IsRequired().HasMaxLength(100);
            address.Property(a => a.Number).HasColumnName("Number")
            .IsRequired().HasMaxLength(20);
            address.Property(a => a.Neighborhood).HasColumnName("Neighborhood")
            .IsRequired().HasMaxLength(100);
            address.Property(a => a.City).HasColumnName("City")
            .IsRequired().HasMaxLength(100);
            address.Property(a => a.State).HasColumnName("State")
            .IsRequired().HasMaxLength(100);
            address.Property(a => a.Country).HasColumnName("Country")
            .IsRequired().HasMaxLength(100);
            address.Property(a => a.ZipCode).HasColumnName("ZipCode")
            .IsRequired().HasMaxLength(20);
        });

        builder.OwnsOne(b => b.Location, location =>
        {
            location.Property(l => l.Latitude).HasColumnName("Latitude").IsRequired();
            location.Property(l => l.Longitude).HasColumnName("Longitude").IsRequired();
        });
    }
}
