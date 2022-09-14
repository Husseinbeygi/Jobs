using Domain.UserAgg;
using Framework.OperationResult;
using ViewModels.Pages.Admin.Users;

namespace Application.UserApp
{
    public interface IUserApplication
	{
		Task<OperationResult> AddUser(CreateViewModel user);
		Task<OperationResult> DeleteUser(Guid Id);
		Task<OperationResult> UpdateUser(UpdateViewModel user);
		Task<OperationResultWithData<DetailsViewModel>> GetUser(Guid Id);
		Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllUsers();
        Task<OperationResultWithData<User>> GetUserByUserName(string username);
	}
}
