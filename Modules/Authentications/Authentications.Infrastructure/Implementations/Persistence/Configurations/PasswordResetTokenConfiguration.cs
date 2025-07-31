namespace Authentications.Infrastructure.Implementations.Persistence.Configurations;

/// <summary>
/// Configures the PasswordResetToken entity for EF Core.
/// </summary>
public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("PasswordResetTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(20); 

        builder.Property(x => x.ExpireAt)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.HasIndex(t => t.Token).IsUnique();

    }
}
