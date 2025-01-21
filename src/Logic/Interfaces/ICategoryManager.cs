using Core.DTOs;
using Data.Models;
namespace Logic.Interfaces;

public interface ICategoryManager
{

    void AddCategory(CategoryDTO category);
    void RemoveCategory(CategoryDTO category);
    Task<List<CategoryDTO>> GetCategories();
    Task<List<CategoryDTO>> SearchCategorie(string searchTerm);


}