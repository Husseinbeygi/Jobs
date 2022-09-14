using Domain.UserAgg;
using Framework.OperationResult;
using Framework.Password;
using Resources.Messages;
using ViewModels.Pages.Admin.Users;

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

		public async Task<OperationResult> AddUser(CreateViewModel user)
		{
			var res = new OperationResult();

			if (_repository.Exist(x => x.CellPhoneNumber == user.CellPhoneNumber ||
			x.EmailAddress == user.EmailAddress))
			{
				res.AddErrorMessage(Errors.AlreadyExists);
				res.Succeeded = false;
				return res;
			}

			var fixedDescription =
					Framework.Utility.FixText
						(text: user.AdminDescription);

			var hashedPassword = _hasher.Hash(user?.Password);

			var _user = new User(user?.EmailAddress)
			{
				Password = hashedPassword,
				IsEmailAddressVerified = true,
				Ordering = user.Ordering,
				IsActive = user.IsActive,
				AdminDescription = fixedDescription,
				IsProgrammer = user.IsProgrammer,
				IsUndeletable = user.IsUndeletable,
				CellPhoneNumber = user.CellPhoneNumber,
				Role = "User",
			};

			await _repository.CreateAsync(_user);
			await _repository.SaveChangesAsync();
			res.Succeeded = true;
			return res;
		}

		public async Task<OperationResult> DeleteUser(Guid Id)
		{
			var res = new OperationResult();
			var userForDelete = await _repository.GetAsync(Id);

			if (userForDelete == null)
			{
				res.AddErrorMessage
					(message: Errors.ThereIsNotAnyDataWithThisId);
			}

			CheckIfUserIsProgrammer(res, userForDelete);

			CheckIfUserIsUndeletable(res, userForDelete);

			if (res.ErrorMessages.Count > 0)
			{
				res.Succeeded = false;
				return res;
			}

			_repository.Remove(userForDelete);

			await _repository.SaveChangesAsync();
			res.Succeeded = true;
			return res;

		}

		private static void CheckIfUserIsUndeletable
			(OperationResult res, User? userForDelete)
		{
			if (userForDelete.IsUndeletable)
			{
				var errorMessage = string.Format
					(Resources.Messages.Errors.UnableTo,
					Resources.ButtonCaptions.Delete,
					Resources.DataDictionary.User);

				res.AddErrorMessage
					(message: errorMessage);

			}
		}

		private static void CheckIfUserIsProgrammer
			(OperationResult res, User? userForDelete)
		{
			if (userForDelete.IsProgrammer)
			{
				var errorMessage = string.Format
					(Resources.Messages.Errors.UnableTo,
					Resources.ButtonCaptions.Delete,
					Resources.DataDictionary.User);

				res.AddErrorMessage
							(message: errorMessage);

			}
		}


		public async Task<OperationResultWithData<User>> GetUserByUserName(string username)
		{
			var res = new OperationResultWithData<User>();

			var user = await _repository.GetByUserName(username);

			res.Data = user;

			return res;
		}

		public async Task<OperationResultWithData<DetailsViewModel>> GetUser(Guid Id)
		{
			var res = new OperationResultWithData<DetailsViewModel>();

			var user = await _repository.GetAsync(Id);

			var _user = new DetailsViewModel
			{
				EmailAddress = user?.EmailAddress,
				Id = user?.Id,
				InsertDateTime = user?.InsertDateTime,
				IsActive = user.IsActive,
				IsEmailAddressVerified = user.IsEmailAddressVerified,
				IsProgrammer = user.IsProgrammer,
				IsSystemic = user.IsSystemic,
				IsUndeletable = user.IsUndeletable,
				Ordering = user.Ordering,
				Role = user.Role,
				UpdateDateTime = user.UpdateDateTime,
				AdminDescription = user.AdminDescription,
				CellPhoneNumber = user.CellPhoneNumber,
				FullName = user.FullName,
				IsCellPhoneNumberVerified = user.IsCellPhoneNumberVerified,
				IsProfilePublic = user.IsProfilePublic,
				Username = user.Username,
			};

			res.Data = _user;

			return res;
		}

		public async Task<OperationResult> UpdateUser(UpdateViewModel user)
		{

			var res = new OperationResult();
			var userForUpdate = await _repository.GetAsync(user.Id.Value);

			if (userForUpdate == null)
			{
				res.AddErrorMessage
					(message: Errors.ThereIsNotAnyDataWithThisId);

				res.Succeeded = false;
				return res;
			}

			var fixedAdminDescription =
				Framework.Utility.FixText
					(text: user.AdminDescription);

			userForUpdate.SetUpdateDateTime();

			userForUpdate.IsActive = user.IsActive;
			userForUpdate.Ordering = user.Ordering;
			userForUpdate.IsProgrammer = user.IsProgrammer;
			userForUpdate.IsUndeletable = user.IsUndeletable;
			userForUpdate.AdminDescription = fixedAdminDescription;
			userForUpdate.FullName = user.FullName;
			userForUpdate.Username = user.Username;


			await _repository.SaveChangesAsync();
			res.Succeeded = true;
			return res;
		}

		public async Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllUsers()
		{
			var res = new OperationResultWithData<IList<DetailsViewModel>>();

			var users = await _repository.GetAllAsync();

			var _data = new List<DetailsViewModel>();

			foreach (var user in users)
			{
				_data.Add(new DetailsViewModel
				{
					EmailAddress = user.EmailAddress,
					Id = user.Id,
					InsertDateTime = user.InsertDateTime,
					IsActive = user.IsActive,
					IsEmailAddressVerified = user.IsEmailAddressVerified,
					IsProgrammer = user.IsProgrammer,
					IsSystemic = user.IsSystemic,
					IsUndeletable = user.IsUndeletable,
					Ordering = user.Ordering,
					Role = user.Role,
					UpdateDateTime = user.UpdateDateTime,
				});
			}

			res.Data = _data;

			return res;
		}
	}
}
