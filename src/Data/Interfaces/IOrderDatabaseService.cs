using System;
using Data.Models;

namespace Data.Interfaces;

public interface IOrderDatabaseService
{
    //get orders
    Task<List<OrderEntity>> GetOrders();
    // get order id by customer id
    Task<List<int>> GetOrderIdByCustomerId(int customerId);
    Task<List<OrderEntity>> GetOrdersByCustomerId(int customerId);
    Task<OrderEntity?> GetOrdersByOrderId(int Id);
    Task<bool> AddOrder(OrderEntity order);
    Task<bool> UpdateOrder(OrderEntity order);


}
