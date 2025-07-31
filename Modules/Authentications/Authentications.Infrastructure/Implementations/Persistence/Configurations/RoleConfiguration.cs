namespace Authentications.Infrastructure.Implementations.Persistence.Configurations;

/// <summary>
/// Configures the Role entity.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(r => r.Name).IsUnique();

        builder.HasMany(r => r.Users)
               .WithOne(ur => ur.Role)
               .HasForeignKey(ur => ur.RoleId);

        builder.HasMany(r => r.Permissions)
               .WithOne(rp => rp.Role)
               .HasForeignKey(rp => rp.RoleId);
    }
}
