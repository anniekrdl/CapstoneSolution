using Core.Interfaces;
using Core.Models;
using Data.Services;
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

        public async Task<Order?> GetOrderById(int id)
        {
            return await _orderDatabaseService.GetOrdersByOrderId(id);

        }

        public async Task<int> CreateOrderId(int customerId)
        {
            int orderId = 0;

            //new order      
            Order order = new Order(null, customerId, null, OrderStatus.AANGEMAAKT);
            await _orderDatabaseService.AddOrder(order);

            // find orderId
            List<int> foundOrders = await _orderDatabaseService.GetOrderIdByCustomerId(customerId);
            //Console.WriteLine($"Orders found total: {foundOrders.Count}. Id: {foundOrders.First()}");
            foreach (int foundOrder in foundOrders)
            {
                Order? order1 = await GetOrderById(foundOrder);

                if (order1 != null && order1.OrderStatus == OrderStatus.AANGEMAAKT)
                {
                    orderId = foundOrder;
                    break;
                }
            }

            return orderId;


        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderId(int orderId)
        {

            //orderItems
            return await _orderItemDatabaseService.GetOrderItemByOrderId(orderId);

        }

        public async Task<List<Order>> GetOrders()
        {
            return await _orderDatabaseService.GetOrders();
        }

        public async Task<bool> PlaceOrderFromShoppingCart(List<ShoppingCartItem> items, int? customerId)
        {
            if (items.Count != 0)
            {
                try
                {
                    if (customerId.HasValue)
                    {
                        int orderId = await CreateOrderId(customerId.Value);

                        foreach (IProductItem item in items)
                        {
                            OrderItem orderItem = new OrderItem(null, orderId, item.ProductId, item.NumberOfItems, item.Product);

                            await _orderItemDatabaseService.AddOrderItem(orderItem);
                            //update Order status

                            Order? order = await GetOrderById(orderId);


                            if (order != null)
                            {
                                order.UpdateOrderStatus(OrderStatus.GEPLAATST);
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

        public async Task<bool> UpdateOrderStatus(Order order, OrderStatus orderStatus)
        {
            OrderStatus currentStatus = order.OrderStatus;

            if ((currentStatus == OrderStatus.GEPLAATST && (orderStatus == OrderStatus.GEWEIGERD || orderStatus == OrderStatus.GEACCEPTEERD)) || (currentStatus == OrderStatus.GEACCEPTEERD && orderStatus == OrderStatus.AFGEROND))
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


        public async Task<bool> UpdateOrder(Order order)
        {
            return await _orderDatabaseService.UpdateOrder(order);
        }


        public async Task<List<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _orderDatabaseService.GetOrdersByCustomerId(customerId);
        }


    }

}