using Domain.OwnerAgg;
using Framework.OperationResult;
using ViewModels.Pages.Admin.Owner;

namespace Application.OwnerApp
{
    public interface IOwnerApplication
    {
        Task<OperationResult> AddOwner(CommonViewModel owner);
        Task<OperationResult> DeleteOwner(Guid Id);
        Task<OperationResult> UpdateOwner(CommonViewModel owner);
        Task<OperationResultWithData<CommonViewModel>> GetOwner(Guid Id);
        Task<OperationResultWithData<IList<CommonViewModel>>> GetAllOwners();
        Task<OperationResultWithData<Owner>> GetOwnerByName(string lname);
    }
}
