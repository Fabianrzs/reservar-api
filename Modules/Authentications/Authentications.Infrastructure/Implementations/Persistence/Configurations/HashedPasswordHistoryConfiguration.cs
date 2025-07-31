using Authentications.Domain.ValueObjects;

namespace Authentications.Infrastructure.Implementations.Persistence.Configurations;

public class HashedPasswordHistoryConfiguration : IEntityTypeConfiguration<HashedPasswordHistory>
{
    public void Configure(EntityTypeBuilder<HashedPasswordHistory> builder)
    {
        builder.ToTable("PasswordHistories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SetAt).IsRequired();

        builder.Property(p => p.Password)
        .HasConversion(
            v => v.Value,
            v => HashedPassword.Create(v))
        .HasColumnName("PasswordHash")
        .HasMaxLength(200)
        .IsRequired();

    }
}

