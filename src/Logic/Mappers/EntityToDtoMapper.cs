using Core.DTOs;
using Data.Models;

namespace Logic.Mappers
{
    public static class EntityToDtoMapper
    {
        public static OrderDTO ToOrderDTO(this OrderEntity entity)
        {
            return new OrderDTO
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                Date = entity.Date,
                OrderStatus = (OrderStatusDTO)entity.OrderStatus
            };
        }

        public static ProductDTO ToProductDTO(this ProductEntity entity)
        {
            return new ProductDTO(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.Price,
                entity.Stock,
                entity.CategoryId,
                entity.ImageUrl
            );
        }

        public static CategoryDTO ToCategoryDTO(this CategoryEntity entity)
        {
            return new CategoryDTO(
                entity.Id,
                entity.Name,
                entity.Description
            );

        }

        public static CustomerDTO ToCustomerDTO(this CustomerEntity entity)
        {
            return new CustomerDTO
            (
                entity.Id,
                entity.UserName,
                entity.Name,
                entity.LastName,
                entity.Email,
                entity.Street,
                entity.Number,
                entity.Addition,
                entity.City
            );
        }

        public static ShoppingCartItemDTO ToShoppingCartItemDTO(this ShoppingCartItemEntity entity)
        {
            return new ShoppingCartItemDTO(
                entity.Id,
                entity.CustomerId,
                entity.ProductId,
                entity.Product?.ToProductDTO(),
                entity.NumberOfItems
            );
        }

        public static OrderItemDTO ToOrderItemDTO(this OrderItemEntity entity)
        {
            return new OrderItemDTO
            (
                entity.Id,
                entity.OrderId,
                entity.ProductId,
                entity.NumberOfItems,
                entity.Product?.ToProductDTO()

            );

        }

        public static OrderEntity ToOrderEntity(this OrderDTO orderDTO)
        {
            return new OrderEntity
            (
                orderDTO.Id,
                orderDTO.CustomerId,
                orderDTO.Date,
                (OrderStatus)orderDTO.OrderStatus

            );

        }

        public static ProductEntity ToProductEntity(this ProductDTO productDTO)
        {
            return new ProductEntity(
                productDTO.Id,
                productDTO.Name,
                productDTO.Description,
                productDTO.Price,
                productDTO.Stock,
                productDTO.CategoryId,
                productDTO.ImageUrl
            );
        }

        public static CategoryEntity ToCategoryEntity(this CategoryDTO categoryDTO)
        {
            return new CategoryEntity(
                categoryDTO.Id,
                categoryDTO.Name,
                categoryDTO.Description

            );
        }

        public static CustomerEntity ToCustomerEntity(this CustomerDTO customerDTO)
        {
            return new CustomerEntity(
                customerDTO.Id,
                customerDTO.UserName,
                customerDTO.Name,
                customerDTO.LastName,
                customerDTO.Email,
                customerDTO.Street,
                customerDTO.Number,
                customerDTO.City,
                customerDTO.Addition
            );
        }

        public static ShoppingCartItemEntity ToShoppingCartEntity(this ShoppingCartItemDTO shoppingCartItem)
        {
            return new ShoppingCartItemEntity(
                shoppingCartItem.Id,
                shoppingCartItem.CustomerId,
                shoppingCartItem.ProductId,
                shoppingCartItem.Product?.ToProductEntity(),
                shoppingCartItem.NumberOfItems
            );
        }
    }
}