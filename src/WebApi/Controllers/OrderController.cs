using System;
using System.Security.Claims;
using Core.DTOs;
using Logic.Interfaces;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{

    private readonly IOrderManager _orderManager;

    public OrderController(IOrderManager orderManager)
    {
        _orderManager = orderManager;

    }



    [HttpGet]
    public IActionResult GetOrdersForUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        var orders = _orderManager.GetOrdersByCustomerId(int.Parse(userId));
        var result = new List<object>();

        foreach (var order in orders)
        {
            if (order.Id != null)
            {
                var orderItems = _orderManager.GetOrderItemsByOrderId(order.Id.Value);
                result.Add(new
                {
                    Order = order,
                    OrderItems = orderItems
                });
            }
        }

        return Ok(result);
    }


    [HttpPost("new")]
    public IActionResult PostOrderForUser([FromBody] List<ShoppingCartItemDTO> shoppingCartItems)
    {



        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        int customerId = int.Parse(userId);

        //post order from ShoppingCartItemDTO's 
        var orderPlaced = _orderManager.PlaceOrderFromShoppingCart(shoppingCartItems, customerId);

        if (orderPlaced)
        {
            return Ok("Order placed");
        }
        else
        {
            return BadRequest("Order not placed");
        }


    }





}
