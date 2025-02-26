using Core.DTOs;

namespace WebAppMvc.Models;

public class DetailsViewModel
{
    public required ProductDTO productDTO { get; set; }
    public UserDTO? LoggedInUser { get; set; }
}