namespace Customers.Application.UseCase.Establishments.UpdateEstablishment;
public class UpdateEstablishmentCommandHandler(
    IEstablishmentRepository establishmentRepository
) : ICommandHandler<UpdateEstablishmentCommand>
{
    public async Task<Result> Handle(UpdateEstablishmentCommand request, CancellationToken cancellationToken)
    {
        Establishment? establishment = await establishmentRepository.GetByIdAsync(request.Id, 
            cancellationToken: cancellationToken);

        if (establishment is null)
        {
            return Result.Failure(EstablishmentError.NotFound);
        }

        establishment.UpdateDetails(
            request.Establishment.Name,
            request.Establishment.Description,
            request.Establishment.ContactInfo.Adapt<ContactInfo>()
        );

        await establishmentRepository.UpdateAsync(establishment, cancellationToken);

        return Result.Success();
    }
}
