using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMvc.Controllers;

namespace WebAppMvc.ViewModels;

public class OrderViewModel
{
    // All Orders for each client
    public List<List<OrderForClient>>? OrdersForClient { get; set; }
    public OrderForClient? OrderForClient { get; set; }
    public List<SelectListItem> PossibleStatus
    {
        get
        {
            if (OrderForClient?.Order == null) return new List<SelectListItem>();

            var statuses = OrderForClient.Order.OrderStatus switch
            {
                OrderStatusDTO.GEPLAATST => new List<OrderStatusDTO>
                {
                    OrderStatusDTO.GEACCEPTEERD,
                    OrderStatusDTO.GEWEIGERD
                },
                OrderStatusDTO.GEACCEPTEERD => new List<OrderStatusDTO>
                {
                    OrderStatusDTO.AFGEROND
                },
                _ => new List<OrderStatusDTO>()
            };

            return statuses.Select(status => new SelectListItem
            {
                Value = status.ToString(),
                Text = status.ToString()
            }).ToList();
        }
    }

    public string SelectedStatus { get; set; } = string.Empty;

}