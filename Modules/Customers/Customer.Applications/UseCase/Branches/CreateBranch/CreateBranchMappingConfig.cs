namespace Customers.Application.UseCase.Branches.CreateBranch;

public class CreateBranchMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBranchCommand, Branch>()
              .MapToConstructor(true);
    }
}
