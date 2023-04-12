using Domain.OwnerAgg;
using Domain.UserAgg;
using Framework.OperationResult;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ViewModels.Pages.Admin.Owner;

namespace Application.OwnerApp
{
    public class OwnerApplication : IOwnerApplication
    {
        private readonly IOwnerRepository _repository;

        public OwnerApplication(IOwnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> AddOwner(CommonViewModel owner)
        {
            var res = new OperationResult();

            if (_repository.Exist(x => x.FName == owner.FName||
            x.LName == owner.LName))
            {
                res.AddErrorMessage(Errors.AlreadyExists);
                res.Succeeded = false;
                return res;
            }

            var _owner = new Owner()
            {
                FName = owner.FName,
                LName = owner.LName,
                JobId = owner.JobId,
                PhoneNumber = owner.PhoneNumber,
                IsUndeletable = owner.IsUndeletable,
                IsActive = owner.IsActive,
            };

            await _repository.CreateAsync(_owner);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteOwner(Guid Id)
        {
            var res = new OperationResult();
            var ownerForDelete = await _repository.GetAsync(Id);

            if (ownerForDelete == null)
            {
                res.AddErrorMessage
                    (message: Errors.ThereIsNotAnyDataWithThisId);
            }

            CheckIfOwnerIsUndeletable(res, ownerForDelete);

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(ownerForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        private static void CheckIfOwnerIsUndeletable
            (OperationResult res, Owner? ownerForDelete)
        {
            if (ownerForDelete.IsUndeletable)
            {
                var errorMessage = string.Format
                    (Errors.UnableTo,
                    Resources.ButtonCaptions.Delete,
                    Resources.DataDictionary.User);

                res.AddErrorMessage
                    (message: errorMessage);

            }
        }

        public async Task<OperationResultWithData<IList<CommonViewModel>>> GetAllOwners()
        {
            var res = new OperationResultWithData<IList<CommonViewModel>>();

            var owners = await _repository.GetAllAsync();

            var _data = new List<CommonViewModel>();

            foreach (var owner in owners)
            {
                _data.Add(new CommonViewModel
                {
                    Id = owner.Id,
                    FName = owner.FName,
                    LName = owner.LName,
                    PhoneNumber = owner.PhoneNumber,
                    JobId = owner.JobId,
                    InsertDateTime = owner.InsertDateTime,
                    IsActive = owner.IsActive,
                    IsSystemic = owner.IsSystemic,
                    IsUndeletable = owner.IsUndeletable,
                    UpdateDateTime = owner.UpdateDateTime,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<CommonViewModel>> GetOwner(Guid Id)
        {
            var res = new OperationResultWithData<CommonViewModel>();

            var owner = await _repository.GetAsync(Id);

            var _owner = new CommonViewModel
            {
                Id = owner.Id,
                FName = owner.FName,
                LName = owner.LName,
                PhoneNumber = owner.PhoneNumber,
                JobId = owner.JobId,
                InsertDateTime = owner.InsertDateTime,
                IsActive = owner.IsActive,
                IsSystemic = owner.IsSystemic,
                IsUndeletable = owner.IsUndeletable,
                UpdateDateTime = owner.UpdateDateTime,
            };

            res.Data = _owner;

            return res;
        }

        public async Task<OperationResultWithData<Owner>> GetOwnerByName(string lname)
        {
            var res = new OperationResultWithData<Owner>();

            var user = await _repository.GetByName(lname);

            res.Data = user;

            return res;
        }

        public async Task<OperationResult> UpdateOwner(CommonViewModel owner)
        {
            var res = new OperationResult();
            var ownerForUpdate = await _repository.GetAsync(owner.Id.Value);

            if (ownerForUpdate == null)
            {
                res.AddErrorMessage
                    (message: Errors.ThereIsNotAnyDataWithThisId);

                res.Succeeded = false;
                return res;
            }

            ownerForUpdate.SetUpdateDateTime();

            ownerForUpdate.IsActive = owner.IsActive;
            ownerForUpdate.IsSystemic = owner.IsSystemic;
            ownerForUpdate.IsUndeletable = owner.IsUndeletable;
            ownerForUpdate.FName = owner.FName;
            ownerForUpdate.LName = owner.LName;
            ownerForUpdate.JobId = owner.JobId;
            ownerForUpdate.PhoneNumber = owner.PhoneNumber;

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
