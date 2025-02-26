using Core.DTOs;

namespace WebAppMvc.Models;

public class DetailsViewModel
{
    public ProductDTO productDTO { get; set; }
    public UserDTO? LoggedInUser { get; set; }
}