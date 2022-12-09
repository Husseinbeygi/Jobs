using Domain.JobAgg;
using Framework.OperationResult;
using Resources.Messages;
using ViewModels.Pages.Admin.Job;

namespace Application.JobApp
{
    public class JobApplication : IJobApplication
    {
        private readonly IJobRepository _jobRepository;

        public JobApplication(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<OperationResult> AddJob(CommonViewModel job)
        {
            var res = new OperationResult();

            if (_jobRepository.Exist(x => x.Name == job.Name))
            {
                res.AddErrorMessage(Errors.AlreadyExists);
                res.Succeeded = false;
                return res;
            }

            var fixedDescription = Framework.Utility.FixText(text: job.Description);

            var fixedAddress = Framework.Utility.FixText(text: job.Address);

            var _job = new Job
            {
                Name = job.Name,
                Description = fixedDescription,
                Address = fixedAddress,
                PhoneNumber1 = job.PhoneNumber1,
                IsActive = job.IsActive,
                IsDeletable = job.IsDeletable,
                IsVerified = job.IsVerified,
                OpeningTime = job.OpeningTime,
                ClosingTime = job.ClosingTime,
                CategoryId = job.CategoryId,
                OwnerId = job.OwnerId,
            };

            await _jobRepository.CreateAsync(_job);
            await _jobRepository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteJob(Guid Id)
        {
            var res = new OperationResult();
            var JobForDelete = await _jobRepository.GetAsync(Id);

            if (JobForDelete == null)
            {
                res.AddErrorMessage(message : Errors.ThereIsNotAnyDataWithThisId);
            }

            CheckIfJobIsUndeletable(res, JobForDelete);

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _jobRepository.Remove(JobForDelete);
            await _jobRepository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        private static void CheckIfJobIsUndeletable
            (OperationResult res, Job? JobForDelete)
        {
            if (JobForDelete.IsUndeletable)
            {
                var errorMessage = string.Format
                    (Errors.UnableTo,
                    Resources.ButtonCaptions.Delete,
                    Resources.DataDictionary.User);

                res.AddErrorMessage
                    (message: errorMessage);

            }
        }

        public async Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllJob()
        {
            var res = new OperationResultWithData<IList<DetailsViewModel>>();

            var jobs = await _jobRepository.GetAllAsync();

            var _data = new List<DetailsViewModel>();

            foreach (var job in jobs)
            {
                _data.Add(new DetailsViewModel
                {
                    Id = job.Id,
                    Name = job.Name,
                    Address = job.Address,
                    Description = job.Description,
                    PhoneNumber1 = job.PhoneNumber1,
                    CategoryId = job.CategoryId,
                    IsActive = job.IsActive,
                    IsDeletable = job.IsDeletable,
                    OpeningTime = job.OpeningTime,
                    ClosingTime = job.ClosingTime,
                    Score = job.Score,
                    IsVerified = job.IsVerified,
                    InsertDateTime = job.InsertDateTime,
                    UpdateDateTime = job.UpdateDateTime
                });
            }

            res.Data = _data;
            return res;
        }

        public async Task<OperationResultWithData<DetailsViewModel>> GetJob(Guid Id)
        {
            var res = new OperationResultWithData<DetailsViewModel>();

            var job = await _jobRepository.GetAsync(Id);

            var _job = new DetailsViewModel
            {
                Id = job.Id,
                Name = job.Name,
                Address = job.Address,
                Description = job.Description,
                PhoneNumber1 = job.PhoneNumber1,
                PhoneNumber2 = job.PhoneNumber2,
                CellPhoneNumber1 = job.CellPhoneNumber1,
                CellPhoneNumber2 = job.CellPhoneNumber2,
                CategoryId = job.CategoryId,
                IsActive = job.IsActive,
                IsDeletable = job.IsDeletable,
                OpeningTime = job.OpeningTime,
                ClosingTime = job.ClosingTime,
                Score = job.Score,
                IsVerified = job.IsVerified,
                InsertDateTime = job.InsertDateTime,
                UpdateDateTime = job.UpdateDateTime
            };

            res.Data = _job;
            return res;
        }

        public async Task<OperationResultWithData<Job>> GetJobByName(string name)
        {
            var res = new OperationResultWithData<Job>();

            var job = await _jobRepository.GetByName(name);

            res.Data = job;

            return res;
        }

        public async Task<OperationResult> UpdateJob(UpdateViewModel job)
        {
            var res = new OperationResult();
            var jobForUpdate = await _jobRepository.GetAsync(job.Id.Value);

            if (jobForUpdate == null)
            {
                res.AddErrorMessage
                    (message: Errors.ThereIsNotAnyDataWithThisId);

                res.Succeeded = false;
                return res;
            }

            var fixedDescription =
            Framework.Utility.FixText
                    (text: job.Description);

            var fixedAddress =
            Framework.Utility.FixText
                    (text: job.Address);

            jobForUpdate.SetUpdateDateTime();

            jobForUpdate.Name = job.Name;
            jobForUpdate.Address = fixedAddress;
            jobForUpdate.Description = fixedDescription;
            jobForUpdate.PhoneNumber1 = job.PhoneNumber1;
            jobForUpdate.PhoneNumber2 = job.PhoneNumber2;
            jobForUpdate.CellPhoneNumber1 = job.CellPhoneNumber1;
            jobForUpdate.CellPhoneNumber2 = job.CellPhoneNumber2;
            jobForUpdate.CategoryId = job.CategoryId;
            jobForUpdate.OpeningTime = job.OpeningTime;
            jobForUpdate.ClosingTime = job.ClosingTime;
            jobForUpdate.IsActive = job.IsActive;
            jobForUpdate.IsDeletable = job.IsDeletable;
            jobForUpdate.IsUndeletable = job.IsUndeletable;
            jobForUpdate.IsVerified = job.IsVerified;

            await _jobRepository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
