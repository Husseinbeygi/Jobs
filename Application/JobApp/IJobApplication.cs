using Domain.JobAgg;
using Framework.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Job;

namespace Application.JobApp
{
    public interface IJobApplication
    {
        Task<OperationResult> AddJob(CommonViewModel job);
        Task<OperationResult> DeleteJob(Guid Id);
        Task<OperationResult> UpdateJob(UpdateViewModel job);
        Task<OperationResultWithData<DetailsViewModel>> GetJob(Guid Id);
        Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllJob();
        Task<OperationResultWithData<Job>> GetJobByName(string name);
    }
}
