using System;
using Core.Models;

namespace Core.Interfaces;

public interface IOrderDatabaseService
{
    //get orders
    Task<List<Order>> GetOrders();
    // get order id by customer id
    Task<List<int>> GetOrderIdByCustomerId(int customerId);
    Task<List<Order>> GetOrdersByCustomerId(int customerId);
    Task<Order?> GetOrdersByOrderId(int Id);
    Task<bool> AddOrder(Order order);
    Task<bool> UpdateOrder(Order order);


}
