using Core.DTOs;
using Data.EF;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;

namespace Logic.Managers;

public class OrderManagerEF : IOrderManager
{
    private readonly WebshopContext _webshopContext;

    public OrderManagerEF(WebshopContext webshopContext)
    {
        _webshopContext = webshopContext;
    }

    public int CreateOrderId(int customerId)
    {
        int orderId = 0;

        // date of today
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        //new order      
        OrderEntity order = new OrderEntity(null, customerId, date, OrderStatus.AANGEMAAKT);
        _webshopContext.Orders.Add(order);
        _webshopContext.SaveChanges();


        // find orderId
        List<OrderDTO> foundOrders = GetOrdersByCustomerId(customerId);

        foreach (OrderDTO foundOrder in foundOrders)
        {
            if (foundOrder.Id != null)
            {

                OrderDTO? order1 = GetOrderById(foundOrder.Id.Value);

                if (order1 != null && order1.OrderStatus == OrderStatusDTO.AANGEMAAKT)
                {
                    orderId = foundOrder.Id.Value;
                    break;
                }

            }

        }

        return orderId;
    }

    public OrderDTO? GetOrderById(int id)
    {
        var orders = _webshopContext.Orders.ToList();
        return orders.Where(o => o.Id == id)
                 .Select(o => o.ToOrderDTO())
                 .FirstOrDefault();
    }

    public List<OrderItemDTO> GetOrderItemsByOrderId(int orderId)
    {

        var orders = _webshopContext.OrderItems
        .ToList()
        .Where(o => o.OrderId == orderId)
                 .Select(o => o.ToOrderItemDTO())
                 .ToList();

        return orders;
    }

    // alleen nuttig voor admin
    public List<OrderDTO> GetOrders()
    {
        var orders = _webshopContext.Orders.ToList();
        return orders.Select(o => o.ToOrderDTO()).ToList();

    }

    public List<OrderDTO> GetOrdersByCustomerId(int customerId)
    {
        var orders = _webshopContext.Orders.ToList();
        return orders.Where(o => o.CustomerId == customerId)
                 .Select(o => o.ToOrderDTO())
                 .ToList();
    }

    public bool PlaceOrderFromShoppingCart(List<ShoppingCartItemDTO> items, int? customerId)
    {
        Console.WriteLine($"items {items.Count}");
        if (items.Count != 0)
        {
            Console.WriteLine("items not null");
            if (customerId.HasValue)
            {
                Console.WriteLine($"{customerId.Value} is customerid.Value");
                int orderId = CreateOrderId(customerId.Value);
                foreach (var item in items)
                {
                    OrderItemEntity orderItem = new OrderItemEntity(null, orderId, item.ProductId, item.NumberOfItems, item.Product?.ToProductEntity());

                    Console.WriteLine($"OrderItemEntity {orderItem.ProductId}");

                    _webshopContext.OrderItems.Add(orderItem);
                    _webshopContext.SaveChanges();

                    OrderDTO? order = GetOrderById(orderId);
                    if (order != null)
                    {
                        order.UpdateOrderStatus(OrderStatusDTO.GEPLAATST);
                        UpdateOrder(order);
                    }
                    else
                    {
                        //Order is null, new order

                    }

                }

                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            //no items in winkelwagen
            return false;
        }
    }

    public bool UpdateOrder(OrderDTO order)
    {
        try
        {
            OrderEntity entity = order.ToOrderEntity();

            _webshopContext.Update(entity);
            _webshopContext.SaveChanges();
            return true;

        }
        catch
        {
            return false;
        }

    }

    public bool UpdateOrderStatus(OrderDTO order, OrderStatusDTO orderStatus)
    {
        OrderStatusDTO currentStatus = order.OrderStatus;

        if ((currentStatus == OrderStatusDTO.GEPLAATST && (orderStatus == OrderStatusDTO.GEWEIGERD || orderStatus == OrderStatusDTO.GEACCEPTEERD)) || (currentStatus == OrderStatusDTO.GEACCEPTEERD && orderStatus == OrderStatusDTO.AFGEROND))
        {
            order.UpdateOrderStatus(orderStatus);
            UpdateOrder(order);
            return true;

        }
        else
        {

            return false;
        }

    }
}