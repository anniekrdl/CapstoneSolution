using System;
using System.Security.Claims;
using Core.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AuthorizedUser")]
public class CartController : ControllerBase
{

    private readonly IShoppingCart _shoppingCart;
    private readonly ICatalogusManager _catalogusManager;

    public CartController(IShoppingCart shoppingCart, ICatalogusManager catalogusManager)
    {
        _shoppingCart = shoppingCart;
        _catalogusManager = catalogusManager;

    }

    [HttpGet]
    public IActionResult GetItemsForUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = int.Parse(userIdClaim);

        var items = _shoppingCart.GetAllItemsByCustomerId(userId, _catalogusManager);

        if (items.IsNullOrEmpty())
        {
            return NoContent();
        }

        return Ok(items);

    }

    [HttpPost]
    public IActionResult AddCartItem([FromBody] ShoppingCartItemDTO shoppingCartItem)
    {

        var isAdded = _shoppingCart.AddShoppingCartItem(shoppingCartItem);
        if (isAdded)
        {
            return Ok("Item added to cart");
        }

        return BadRequest("Item not added");

    }

    [HttpDelete("{cartItemId}")]
    public IActionResult DeleteCartItem(int cartItemId)
    {

        var isRemoved = _shoppingCart.RemoveShoppingCartItemById(cartItemId, _catalogusManager);

        if (isRemoved)
        {
            return Ok("Item successfully removed.");
        }
        else
        {
            return NotFound("Item not found in the shopping cart.");
        }
    }

    [HttpPut]
    public IActionResult EditCartItem([FromBody] ShoppingCartItemDTO shoppingCartItem)
    {

        var isChanged = _shoppingCart.EditShoppingCartItem(shoppingCartItem);
        if (isChanged)
        {
            return Ok("Item changed");
        }
        else
        {
            return BadRequest("Item not found in shopping cart");
        }
    }

}
