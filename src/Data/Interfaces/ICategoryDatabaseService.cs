using System;
using Data.Models;

namespace Data
.Interfaces;

public interface ICategoryDatabaseService
{

    Task<List<CategoryEntity>> GetAllCategories();

    // Add category to database
    Task AddCategory(CategoryEntity category);

    // Remove category from database
    Task RemoveCategory(CategoryEntity category);

    // Search category by name
    Task<List<CategoryEntity>> SearchCategory(string searchTerm);

}
