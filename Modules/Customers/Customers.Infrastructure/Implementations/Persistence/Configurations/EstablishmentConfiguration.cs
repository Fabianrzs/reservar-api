namespace Customers.Infrastructure.Implementations.Persistence.Configurations;

public class EstablishmentConfiguration : IEntityTypeConfiguration<Establishment>
{
    public void Configure(EntityTypeBuilder<Establishment> builder)
    {
        builder.ToTable("Establishments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.OwnsOne(e => e.ContactInfo, ci =>
        {
            ci.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);
            ci.Property(c => c.Email).HasMaxLength(100);
            ci.Property(c => c.Website).HasMaxLength(100);
            ci.Property(c => c.InstagramHandle).HasMaxLength(100);
            ci.Property(c => c.FacebookHandle).HasMaxLength(100);
            ci.Property(c => c.WhatsappNumber).HasMaxLength(20);
        });

        builder.Property(e => e.PhotoUrl)
        .HasConversion(
            v => v!.ToString(),
            v => new Uri(v)
        );

        builder.Property(e => e.BannerUrl)
            .HasConversion(
                v => v!.ToString(),
                v => new Uri(v)
            );


        builder.HasMany(e => e.Users)
            .WithOne()
            .HasForeignKey(u => u.EstablishmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Branches)
            .WithOne(b => b.Establishment)
            .HasForeignKey(b => b.EstablishmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
