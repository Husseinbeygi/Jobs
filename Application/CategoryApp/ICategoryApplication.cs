using Domain.CategoryAgg;
using Framework.OperationResult;
using ViewModels.Pages.Admin.Categories;

namespace Application.CategoryApp
{
    public interface ICategoryApplication
    {
        Task<OperationResult> AddCategory(CommonViewModel category);
        Task<OperationResult> UpdateCategory(UpdateViewModel category);
        Task<OperationResult> DeleteCategory(Guid Id);
        Task<OperationResultWithData<List<Category>>> GetAllCategories();
        Task<OperationResultWithData<DetailsViewModel>> GetCategory(Guid Id);
        Task<OperationResultWithData<Category>> GetCategoryByName(string categoryName);
        Task<OperationResultWithData<List<Category>>> GetParentCategories();
        Task<OperationResultWithData<Dictionary<IndexViewModel, List<IndexViewModel>>>> GetGroupedCategories();
    }
}