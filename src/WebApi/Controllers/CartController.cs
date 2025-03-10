using System;
using System.Security.Claims;
using Core.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        if (userIdClaim == null)
        {
            return BadRequest("User ID not found");
        }
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
        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }


        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || shoppingCartItem == null || shoppingCartItem.CustomerId != int.Parse(userIdClaim))
        {
            return BadRequest("Invalid request.");
        }


        var isAdded = _shoppingCart.AddShoppingCartItem(shoppingCartItem);
        if (isAdded)
        {
            return Ok("Item added to cart");
        }

        return BadRequest("Item not added");



    }

    [HttpDelete]
    public IActionResult DeleteCartItem([FromBody] ShoppingCartItemDTO shoppingCartItem)
    {

        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || shoppingCartItem == null || shoppingCartItem.CustomerId != int.Parse(userIdClaim))
        {
            return BadRequest("Invalid Request.");
        }

        var isRemoved = _shoppingCart.RemoveShoppingCartItem(shoppingCartItem);

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


        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || shoppingCartItem == null || shoppingCartItem.CustomerId != int.Parse(userIdClaim))
        {
            return BadRequest("Invalid Request.");
        }

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
