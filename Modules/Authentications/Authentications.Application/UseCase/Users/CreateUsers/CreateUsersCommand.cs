namespace Authentications.Application.UseCase.Users.CreateUsers;

public sealed record CreateUsersCommand(
    string Email,
    string? Name
) : ICommand<Guid>;
