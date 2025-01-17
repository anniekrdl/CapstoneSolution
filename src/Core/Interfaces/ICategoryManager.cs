using Core.Models;
namespace Core.Interfaces;

public interface ICategoryManager
{

    void AddCategory(Category category);
    void RemoveCategory(Category category);
    Task<List<Category>> GetCategories();
    Task<List<Category>> SearchCategorie(string searchTerm);


}