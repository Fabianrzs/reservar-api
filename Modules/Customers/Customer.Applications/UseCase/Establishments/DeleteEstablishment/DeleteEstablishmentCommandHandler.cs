namespace Customers.Application.UseCase.Establishments.DeleteEstablishment;

public sealed class DeleteEstablishmentCommandHandler(
    IEstablishmentRepository establishmentRepository
) : ICommandHandler<DeleteEstablishmentCommand>
{
    public async Task<Result> Handle(DeleteEstablishmentCommand request, CancellationToken cancellationToken)
    {
        Establishment? establishment = await establishmentRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        if (establishment is null)
        {
            return Result.Failure(EstablishmentError.NotFound);
        }

        await establishmentRepository.DeleteAsync(establishment.Id, cancellationToken);

        return Result.Success();
    }
}
