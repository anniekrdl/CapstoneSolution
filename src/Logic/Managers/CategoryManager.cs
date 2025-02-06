using Core.DTOs;
using Data.Interfaces;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;
namespace Logic.Managers
{

    public class CategoryManager : ICategoryManager
    {


        private readonly ICategoryDatabaseService _categoryDatabaseService;

        public CategoryManager(ICategoryDatabaseService categoryDatabaseService)
        {
            _categoryDatabaseService = categoryDatabaseService;

        }

        public async void AddCategory(CategoryDTO category)
        {
            // convert DTO to entity

            await _categoryDatabaseService.AddCategory(category.ToCategoryEntity());
        }

        public async void RemoveCategory(CategoryDTO category)
        {
            await _categoryDatabaseService.RemoveCategory(category.ToCategoryEntity());
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {

            var categories = await _categoryDatabaseService.GetAllCategories();

            return categories.Select(c => c.ToCategoryDTO()).ToList();
        }

        public async Task<List<CategoryDTO>> SearchCategorie(string searchTerm)
        {
            var categories = await _categoryDatabaseService.SearchCategory(searchTerm);
            return categories.Select(c => c.ToCategoryDTO()).ToList();
        }

        List<CategoryDTO> ICategoryManager.GetCategories()
        {
            throw new NotImplementedException();
        }
    }

}