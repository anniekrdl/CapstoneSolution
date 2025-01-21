using System;
using Core.DTOs;
using Data.Models;

namespace Data
.Interfaces;

public interface IOrderItemDatabaseService
{

    /// <summary>
    /// Returns all order items
    /// </summary>
    /// <returns>List of ORderItems</returns>
    Task<List<OrderItemEntity>> GetOrderItems();

    //get order item by id
    Task<List<OrderItemEntity>> GetOrderItemByOrderId(int Id);

    /// <summary>
    /// Get orderitem by ID
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<OrderItemEntity> GetOrderItemById(int Id);


    /// <summary>
    /// Add Order Item to database
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    Task<bool> AddOrderItem(OrderItemEntity orderItem);


    /// <summary>
    ///  Update orderItem
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    Task<bool> UpdateOrderItem(OrderItemEntity orderItem);


}
