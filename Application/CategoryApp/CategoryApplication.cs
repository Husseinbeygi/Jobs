using Domain.CategoryAgg;
using Framework;
using Framework.OperationResult;
using Resources;
using Resources.Messages;
using ViewModels.Pages.Admin.Categories;

namespace Application.CategoryApp
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ICategoryRepository _repository;

        public CategoryApplication(ICategoryRepository repository)
        {
            _repository = repository;
        }


        public async Task<OperationResult> AddCategory(CommonViewModel category)
        {
            var res = new OperationResult();

            category.Name = Utility.FixText(category.Name);

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                res.AddErrorMessage(string.Format(Validations.Required, DataDictionary.Name));
                res.Succeeded = false;
                return res;
            }
            if (_repository.Exist(x => x.Name == category.Name) || category.Name == DataDictionary.WithoutParent)
            {
                res.AddErrorMessage(string.Format(Errors.AlreadyExists, DataDictionary.Category));
                res.Succeeded = false;
                return res;
            }

            // If CommonViewModel have ( ParentName ) this Code use.
            //var resParent = await GetCategoryByName(category.ParentName);
            //Guid? parentId = resParent.Succeeded ? resParent.Data.Id : null;

            Guid? parentId = null;

            if (category.ParentId != null)
            {
                parentId = (await _repository.GetAsync((Guid)category.ParentId))?.Id;
            }

            var _category = new Category()
            {
                Name = category.Name,
                ParentId = parentId,
                Ordering = category.Ordering,
                IsActive = category.IsActive,
                IsDeletable = category.IsDeletable,
            };

            await _repository.CreateAsync(_category);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> UpdateCategory(UpdateViewModel category)
        {
            var res = new OperationResult();

            category.Name = Utility.FixText(category.Name);

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                res.AddErrorMessage(string.Format(Validations.Required, DataDictionary.Name));
                res.Succeeded = false;
                return res;
            }

            var categoryForUpdate = await _repository.GetAsync(category.Id);

            if (categoryForUpdate == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisName);
                res.Succeeded = false;
                return res;
            }

            if (category.Name != categoryForUpdate.Name && _repository.Exist(x => x.Name == category.Name) || category.Name == DataDictionary.WithoutParent)
            {
                res.AddErrorMessage(string.Format(Errors.AlreadyExists, DataDictionary.Category));
                res.Succeeded = false;
                return res;
            }

            Guid? parentId = null;

            if (category.ParentId != null)
            {
                parentId = (await _repository.GetAsync((Guid)category.ParentId))?.Id;
            }

            categoryForUpdate.Name = category.Name;
            categoryForUpdate.ParentId = parentId;
            categoryForUpdate.Ordering = category.Ordering;
            categoryForUpdate.IsActive = category.IsActive;
            categoryForUpdate.IsDeletable = category.IsDeletable;

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteCategory(Guid Id)
        {
            var res = new OperationResult();
            var categoryForDelete = await _repository.GetAsync(Id);

            if (categoryForDelete == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
                res.Succeeded = false;
                return res;
            }

            if (!categoryForDelete.IsDeletable)
            {
                var errorMessage = string.Format(Errors.UnableTo, ButtonCaptions.Delete, DataDictionary.Category);
                res.AddErrorMessage(errorMessage);
                res.Succeeded = false;
                return res;
            }

            // Delete Child's: Just in Second Layer of Parent!
            var childs = await _repository.GetChildsById(Id);
            if (childs != null)
            {
                foreach (var child in childs)
                {
                    _repository.Remove(child);
                }
            }

            _repository.Remove(categoryForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<List<Category>>> GetAllCategories()
        {
            var res = new OperationResultWithData<List<Category>>();

            var categories = await _repository.GetAllAsync();

            res.Data = categories;
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<DetailsViewModel>> GetCategory(Guid Id)
        {
            var res = new OperationResultWithData<DetailsViewModel>();

            var category = await _repository.GetAsync(Id);

            if (category == null)
            {
                res.AddErrorMessage(string.Format(Errors.NotFound, DataDictionary.Category));
                res.Succeeded = false;
                return res;
            }

            var parentId = (await _repository.GetAsync(Id)).ParentId;
            var parentName = parentId.HasValue ? (await _repository.GetAsync(parentId.Value)).Name : null;

            DetailsViewModel categoryForView = new()
            {
                Id = category.Id,
                Name = category.Name,
                ParentName = parentName,
                Ordering = category.Ordering,
                IsActive = category.IsActive,
                IsDeletable = category.IsDeletable,
            };

            res.Data = categoryForView;
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<Category>> GetCategoryByName(string categoryName)
        {
            var res = new OperationResultWithData<Category>();

            var category = await _repository.GetByName(categoryName);

            if (category == null)
            {
                res.AddErrorMessage(string.Format(Errors.NotFound, DataDictionary.Category));
                res.Succeeded = false;
                return res;
            }

            res.Data = category;
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<List<Category>>> GetParentCategories()
        {
            var res = new OperationResultWithData<List<Category>>();

            var category = await _repository.GetParents();

            res.Data = category;
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<Dictionary<IndexViewModel, List<IndexViewModel>>>> GetGroupedCategories()
        {
            var res = new OperationResultWithData<Dictionary<IndexViewModel, List<IndexViewModel>>>();

            var categories = new Dictionary<IndexViewModel, List<IndexViewModel>>();

            var parents = await _repository.GetParents();

            if (parents != null)
            {
                foreach (var parent in parents)
                {
                    List<IndexViewModel> childs = new();

                    foreach (var child in await _repository.GetChildsById(parent.Id))
                    {
                        childs.Add(new IndexViewModel()
                        {
                            Id = child.Id,
                            Name = child.Name,
                            IsActive = child.IsActive,
                            IsDeletable = child.IsDeletable,
                        });
                    }

                    categories.Add(new IndexViewModel()
                    {
                        Id = parent.Id,
                        Name = parent.Name,
                        IsActive = parent.IsActive,
                        IsDeletable = parent.IsDeletable,
                    }, childs);
                }
            }

            res.Data = categories;
            res.Succeeded = true;
            return res;
        }
    }
}