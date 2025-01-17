using System;
using Core.Models;

namespace Core.Interfaces;

public interface IOrderItemDatabaseService
{

    /// <summary>
    /// Returns all order items
    /// </summary>
    /// <returns>List of ORderItems</returns>
    Task<List<OrderItem>> GetOrderItems();

    //get order item by id
    Task<List<OrderItem>> GetOrderItemByOrderId(int Id);

    /// <summary>
    /// Get orderitem by ID
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<OrderItem> GetOrderItemById(int Id);


    /// <summary>
    /// Add Order Item to database
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    Task<bool> AddOrderItem(OrderItem orderItem);


    /// <summary>
    ///  Update orderItem
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    Task<bool> UpdateOrderItem(OrderItem orderItem);


}
