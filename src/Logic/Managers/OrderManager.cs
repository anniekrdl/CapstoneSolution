using Core.DTOs;
using Data.Interfaces;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;
namespace Logic.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderDatabaseService _orderDatabaseService;
        private readonly IOrderItemDatabaseService _orderItemDatabaseService;

        public OrderManager(IOrderDatabaseService orderDatabaseService, IOrderItemDatabaseService orderItemDatabaseService)
        {
            _orderDatabaseService = orderDatabaseService;
            _orderItemDatabaseService = orderItemDatabaseService;

        }

        public async Task<OrderDTO?> GetOrderById(int id)
        {
            var order = await _orderDatabaseService.GetOrdersByOrderId(id);
            return order?.ToOrderDTO();

        }

        public async Task<int> CreateOrderId(int customerId)
        {
            int orderId = 0;

            //new order
            OrderEntity order = new OrderEntity(null, customerId, null, OrderStatus.AANGEMAAKT);
            await _orderDatabaseService.AddOrder(order);

            // find orderId
            List<int> foundOrders = await _orderDatabaseService.GetOrderIdByCustomerId(customerId);
            //Console.WriteLine($"Orders found total: {foundOrders.Count}. Id: {foundOrders.First()}");
            foreach (int foundOrder in foundOrders)
            {
                OrderDTO? order1 = await GetOrderById(foundOrder);

                if (order1 != null && order1.OrderStatus == OrderStatusDTO.AANGEMAAKT)
                {
                    orderId = foundOrder;
                    break;
                }
            }

            return orderId;

        }

        public async Task<List<OrderItemDTO>> GetOrderItemsByOrderId(int orderId)
        {

            //orderItems
            List<OrderItemEntity> orderItems = await _orderItemDatabaseService.GetOrderItemByOrderId(orderId);
            return orderItems.Select(item => item.ToOrderItemDTO()).ToList();

        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            var orders = await _orderDatabaseService.GetOrders();
            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                Date = o.Date,
                OrderStatus = (OrderStatusDTO)o.OrderStatusEnum,
            }).ToList();

        }

        public async Task<bool> PlaceOrderFromShoppingCart(List<ShoppingCartItemDTO> items, int? customerId)
        {
            if (items.Count != 0)
            {
                try
                {
                    if (customerId.HasValue)
                    {
                        int orderId = await CreateOrderId(customerId.Value);

                        //Cannot convert to IProductItem??
                        foreach (var item in items)
                        {
                            //Console.WriteLine($"Item is {item.Id} ");
                            OrderItemEntity orderItem = new OrderItemEntity(null, orderId, item.ProductId, item.NumberOfItems, item.Product?.ToProductEntity());

                            await _orderItemDatabaseService.AddOrderItem(orderItem);
                            //update Order status

                            OrderDTO? order = await GetOrderById(orderId);
                            if (order != null)
                            {
                                order.UpdateOrderStatus(OrderStatusDTO.GEPLAATST);
                                await UpdateOrder(order);
                            }

                        }

                        return true;

                    }
                    else
                    {
                        Console.WriteLine("customerId is null.");
                        return false;
                    }

                }
                catch (System.Exception)
                {

                    return false;
                }

            }
            else
            {
                Console.WriteLine("\nJe winkelwagen is leeg.");
                return false;
            }

        }

        public async Task<bool> UpdateOrderStatus(OrderDTO order, OrderStatusDTO orderStatus)
        {
            OrderStatusDTO currentStatus = order.OrderStatus;

            if ((currentStatus == OrderStatusDTO.GEPLAATST && (orderStatus == OrderStatusDTO.GEWEIGERD || orderStatus == OrderStatusDTO.GEACCEPTEERD)) || (currentStatus == OrderStatusDTO.GEACCEPTEERD && orderStatus == OrderStatusDTO.AFGEROND))
            {
                order.UpdateOrderStatus(orderStatus);
                await UpdateOrder(order);
                return true;

            }
            else
            {

                return false;
            }

        }

        public async Task<bool> UpdateOrder(OrderDTO order)
        {
            //TODO kan dit 2 kanten op?
            OrderEntity entity = order.ToOrderEntity();

            return await _orderDatabaseService.UpdateOrder(entity);
        }

        public async Task<List<OrderDTO>> GetOrdersByCustomerId(int customerId)
        {
            var orderList = await _orderDatabaseService.GetOrdersByCustomerId(customerId);

            return orderList.Select(o => new OrderDTO
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                Date = o.Date,
                OrderStatus = (OrderStatusDTO)o.OrderStatusEnum
            }).ToList();
        }

        OrderDTO? IOrderManager.GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        int IOrderManager.CreateOrderId(int customerId)
        {
            throw new NotImplementedException();
        }

        // bool IOrderManager.PlaceOrderFromShoppingCart(List<ShoppingCartItemDTO> items, int? customerId)
        // {
        //     throw new NotImplementedException();
        // }

        List<OrderDTO> IOrderManager.GetOrders()
        {
            throw new NotImplementedException();
        }

        bool IOrderManager.UpdateOrder(OrderDTO order)
        {
            throw new NotImplementedException();
        }

        List<OrderDTO> IOrderManager.GetOrdersByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        List<OrderItemDTO> IOrderManager.GetOrderItemsByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        bool IOrderManager.UpdateOrderStatus(OrderDTO order, OrderStatusDTO orderStatus)
        {
            throw new NotImplementedException();
        }

        OrderDTO? IOrderManager.PlaceOrderFromShoppingCart(List<ShoppingCartItemDTO> items, int? customerId)
        {
            throw new NotImplementedException();
        }
    }

}