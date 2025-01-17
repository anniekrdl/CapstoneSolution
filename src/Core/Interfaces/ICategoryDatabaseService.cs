using System;
using Core.Models;

namespace Core.Interfaces;

public interface ICategoryDatabaseService
{

    Task<List<Category>> GetAllCategories();

    // Add category to database
    Task AddCategory(Category category);

    // Remove category from database
    Task RemoveCategory(Category category);

    // Search category by name
    Task<List<Category>> SearchCategory(string searchTerm);

}
