using Domain.CategoryAgg;
using Domain.UserAgg;
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
        private readonly IUserRepository _userRepository;

        public CategoryApplication(ICategoryRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }


        public async Task<OperationResult> AddCategory(CreateViewModel category)
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
                EditorUserId = category.EditorUserId,
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
            categoryForUpdate.EditorUserId = category.EditorUserId;
            categoryForUpdate.SetUpdateDateTime();

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

            string? parentName = null;

            if (category.ParentId.HasValue)
            {
                var parent = await _repository.GetAsync(category.ParentId.Value);
                parentName = parent?.Name;
            }

            var editorUser = await _userRepository.GetAsync(category.EditorUserId);

            DetailsViewModel categoryForView = new()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                ParentName = parentName,
                EditorUserId = category.EditorUserId,
                EditorUserName = editorUser?.Username,
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

        public async Task<OperationResultWithData<List<IndexViewModel>>> GetIndexCategories(Guid? Id = null)
        {
            var res = new OperationResultWithData<List<IndexViewModel>>();

            List<Category> categories = new();

            if (!Id.HasValue)
            {
                categories = await _repository.GetParents();
            }
            else
            {
                var category = await _repository.GetAsync(Id.Value);

                if (category == null)
                {
                    res.AddErrorMessage(string.Format(Errors.NotFound, DataDictionary.Category));
                    res.Succeeded = false;
                    return res;
                }

                categories = await _repository.GetChildsById(category.Id);
            }


            List<IndexViewModel> _categories = new();

            foreach (var category in categories)
            {
                var childCount = await GetChildCount(category.Id);

                _categories.Add(new()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Ordering = category.Ordering,
                    IsActive = category.IsActive,
                    IsDeletable = category.IsDeletable,
                    ChildCount = childCount,
                });
            }

            res.Data = _categories;
            res.Succeeded = true;
            return res;
        }

        public async Task<int> GetChildCount(Guid Id)
        {
            var childs = await _repository.GetChildsById(Id);

            var countChilds = childs.Count;

            return countChilds;
        }
    }
}