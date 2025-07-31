using Authentications.Domain.ValueObjects;

namespace Authentications.Infrastructure.Implementations.Persistence.Configurations;

public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
{
    public void Configure(EntityTypeBuilder<UserCredentials> builder)
    {
        builder.ToTable("UserCredentials");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.PasswordSetAt).IsRequired();

        builder.Property(x => x.CurrentPassword)
         .HasConversion(
             v => v.Value,
             v => HashedPassword.Create(v))
         .HasColumnName("PasswordHash")
         .HasMaxLength(200)
         .IsRequired();


        builder.HasMany(x => x.PasswordHistory)
               .WithOne()
               .HasForeignKey("UserCredentialsId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
