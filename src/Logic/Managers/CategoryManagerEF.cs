using System.Data.Entity;
using Core.DTOs;
using Data.EF;
using Logic.Interfaces;
using Logic.Mappers;

namespace Logic.Managers;

public class CategoryManagerEF : ICategoryManager
{
    private readonly WebshopContext _webshopContext;

    public CategoryManagerEF(WebshopContext webshopContext)
    {
        _webshopContext = webshopContext;
    }
    public void AddCategory(CategoryDTO category)
    {
        _webshopContext.Categories.Add(category.ToCategoryEntity());
    }

    public List<CategoryDTO> GetCategories()
    {
        var categories = _webshopContext.Categories.ToList();
        return categories.Select(c => c.ToCategoryDTO()).ToList();

    }

    public void RemoveCategory(CategoryDTO category)
    {
        _webshopContext.Categories.Remove(category.ToCategoryEntity());
    }

    public List<CategoryDTO> SearchCategorie(string searchTerm)
    {
        var categories = _webshopContext.Categories
            .Where(c => c.Name.Contains(searchTerm))
            .Select(c => c.ToCategoryDTO())
            .ToList();

        return categories;
    }
}