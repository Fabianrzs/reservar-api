using Customers.Application.UseCase.Branches;

namespace Customers.Application.UseCase.Establishments.CreateEstablishment;

public class CreateEstablishmentCommandHandler(
    IEstablishmentRepository establishmentRepository
) : ICommandHandler<CreateEstablishmentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEstablishmentCommand request, 
        CancellationToken cancellationToken)
    {
        Establishment establishment = request.Establishment.Adapt<Establishment>();

        foreach (BranchDto branch in request.Establishment.Branches!)
        {
            establishment.AddBranch(branch.Adapt<Branch>());
        }

        await establishmentRepository.AddAsync(establishment, cancellationToken);
        //Guardar las imagenes
        return establishment.Id;

    }
}
