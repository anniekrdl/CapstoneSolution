using Core.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMvc.Models;
public class ProductViewModel
{
    public ProductDTO? Product { get; set; }  // Dit bevat de Product-gegevens.
    public required List<SelectListItem> Categories { get; set; }
    public double PriceInEuros
    {
        get
        {
            if (Product != null)
            {
                return Product.Price / 100.00;
            }
            return 0;
        }
        set
        {
            if (Product != null)
            {
                Product.Price = (int)(value * 100);
            }
        }

    }
}

