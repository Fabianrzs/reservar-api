using Common.Infrastructure.Contexts;

namespace Authentications.Infrastructure.Implementations.Persistence;

/// <summary>
/// Represents the EF Core database context for the Authentications module.
/// Provides access to authentication-related entities.
/// </summary>
public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
    : AppDbContextBase(options)
{
    // DbSet representing the password history for auditing previous hashes.
    public DbSet<HashedPasswordHistory> HashedPasswordHistories => Set<HashedPasswordHistory>();

    // DbSet for one-time password tokens (e.g., for OTP-based login).
    public DbSet<OtpToken> OtpTokens => Set<OtpToken>();

    // DbSet for tokens used during password reset flows.
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    // DbSet representing permissions assigned to roles or users.
    public DbSet<Permission> Permissions => Set<Permission>();

    // DbSet representing user roles (e.g., Admin, User).
    public DbSet<Role> Roles => Set<Role>();

    // DbSet for mapping between roles and permissions.
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    // DbSet for active login sessions.
    public DbSet<Session> Sessions => Set<Session>();

    // DbSet representing the user entity.
    public DbSet<User> Users => Set<User>();

    // DbSet for storing user credentials (e.g., hashed passwords).
    public DbSet<UserCredentials> UserCredentials => Set<UserCredentials>();

    // DbSet for mapping between users and roles.
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    /// <summary>
    /// Configures the model and relationships using Fluent API.
    /// Also sets the default schema for all tables.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set default schema for all entities in this context (e.g., "auth")
        modelBuilder.HasDefaultSchema("auth");

        // Apply all IEntityTypeConfiguration<T> found in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);

        // Allow base class to apply its configurations (if any)
        base.OnModelCreating(modelBuilder);
    }
}
