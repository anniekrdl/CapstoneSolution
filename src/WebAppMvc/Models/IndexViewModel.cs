using Core.DTOs;
using Core.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMvc.ViewModels
{
    public class IndexViewModel
    {
        public string SearchTerm { get; set; } = "";

        public List<SelectListItem> SortMethodsList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = SortMethods.NameAscending.ToString(), Text = "Alfabetisch oplopend" },
            new SelectListItem { Value = SortMethods.NameDescending.ToString(), Text = "Alfabetisch aflopend" },
            new SelectListItem { Value = SortMethods.PriceAscending.ToString(), Text = "Prijs oplopend" },
            new SelectListItem { Value = SortMethods.PriceDescending.ToString(), Text = "Prijs aflopend" }
        };

        public string SelectedSortMethod { get; set; } = SortMethods.NameAscending.ToString();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public UserDTO? LoggedInUser { get; set; }
        public List<ProductDTO> Products { get; set; } = new();
    }
}
