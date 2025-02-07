using Core.DTOs;
using Data.Models;
namespace Logic.Interfaces;

public interface ICategoryManager
{

    void AddCategory(CategoryDTO category);
    void RemoveCategory(CategoryDTO category);
    List<CategoryDTO> GetCategories();
    List<CategoryDTO> SearchCategorie(string searchTerm);


}