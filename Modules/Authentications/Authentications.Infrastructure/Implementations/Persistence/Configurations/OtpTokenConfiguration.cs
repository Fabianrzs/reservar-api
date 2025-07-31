namespace Authentications.Infrastructure.Implementations.Persistence.Configurations;

public class OtpTokenConfiguration : IEntityTypeConfiguration<OtpToken>
{
    public void Configure(EntityTypeBuilder<OtpToken> builder)
    {
        builder.ToTable("OtpTokens");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Token).IsRequired().HasMaxLength(10);
        builder.Property(x => x.ExpireAt).IsRequired();
    }
}
