namespace Authentications.Infrastructure.Implementations.Persistence.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Ip).HasMaxLength(100).IsRequired();
        builder.Property(s => s.UserAgent).HasMaxLength(300).IsRequired();
        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.ExpiresAt).IsRequired();
        builder.Property(s => s.Status).IsRequired();
    }
}
