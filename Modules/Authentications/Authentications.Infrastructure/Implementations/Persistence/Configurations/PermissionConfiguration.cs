namespace Authentications.Infrastructure.Implementations.Persistence.Configurations;

/// <summary>
/// Configures the Permission entity.
/// </summary>
public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(250);

        builder.HasIndex(p => p.Code).IsUnique();
    }
}
