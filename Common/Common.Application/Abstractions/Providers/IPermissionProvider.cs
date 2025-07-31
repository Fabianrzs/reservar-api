namespace Common.Application.Abstractions.Providers;
public interface IPermissionProvider
{
    Task<HashSet<string>> GetForUserIdAsync(Guid userId);
}
