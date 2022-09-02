using Domain.UserAgg;
using Framework.OperationResult;

namespace Application.UserApp
{
    public interface IUserApplication
	{
		Task<OperationResult> AddUser(User user);
		Task<OperationResult> DeleteUser(Guid Id);
		Task<OperationResult> UpdateUser(User user);
		Task<OperationResultWithData<User>> GetUser(Guid Id);
		Task<OperationResultWithData<IList<User>>> GetAllUsers();
        Task<OperationResultWithData<User>> GetUserByUserName(string username);
	}
}
