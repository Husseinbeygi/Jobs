using Domain.UserAgg;
using Framework.OperationResult;
using Framework.Password;

namespace Application.UserApp
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _hasher;

        public UserApplication(IUserRepository repository, IPasswordHasher hasher)
        {
            _repository = repository;
            _hasher = hasher;
        }

        public async Task<OperationResult> AddUser(User user)
        {
            var res = new OperationResult();

            if (_repository.Exist(x => x.Username == user.Username))
            {
                res.AddErrorMessage("نام کاربری تکراری میباشد");
                res.Succeeded = false;
                return res;
            }

            user.Password = _hasher.Hash(user?.Password);

            await _repository.CreateAsync(user);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteUser(Guid Id)
        {
            var res = new OperationResult();
            var user = await _repository.GetAsync(Id);

            if (user == null)
            {
                res.AddErrorMessage("کاربر وجود ندارد");
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(user);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;

        }

        public async Task<OperationResultWithData<IList<User>>> GetAllUsers()
        {
            var res = new OperationResultWithData<IList<User>>();

            var users = await _repository.GetAllAsync();

            res.Data = users;

            return res;
        }

        public async Task<OperationResultWithData<User>> GetUserByUserName(string username)
        {
            var res = new OperationResultWithData<User>();

            var user = await _repository.GetByUserName(username);

            res.Data = user;

            return res;
        }

        public async Task<OperationResultWithData<User>> GetUser(Guid Id)
        {
            var res = new OperationResultWithData<User>();

            var user = await _repository.GetAsync(Id);

            res.Data = user;

            return res;
        }

        public async Task<OperationResult> UpdateUser(User user)
        {
            var res = new OperationResult();
            var userForUpdate = await _repository.GetAsync(user.Id);

            if (userForUpdate == null)
            {
                res.AddErrorMessage("کاربر وجود ندارد");
                res.Succeeded = false;
                return res;
            }

            userForUpdate.Edit(user);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
