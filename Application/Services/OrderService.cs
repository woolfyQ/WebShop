using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderInterface<Order, OrderDTO>
    {
        private readonly IOrderInterface<Order, OrderDTO> _orderRepository;

        public OrderService(IOrderInterface<Order, OrderDTO> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Create(OrderDTO orderDTO)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Cart = orderDTO.Cart,
                User = orderDTO.User,
                DateTime = DateTime.Now,
                Products = orderDTO.Products ?? new List<ProductCart>(), // Обрабатываем ситуацию, если продукты не указаны
                TotalPrice = orderDTO.TotalPrice
            };

            // Создание заказа в репозитории
            await _orderRepository.Create(orderDTO);
            return order;
        }

        public async Task<Order> AddProduct(OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderDTO.Id, cancellationToken);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Проверяем, есть ли продукты в корзине
            if (orderDTO.Cart?.Products != null && orderDTO.Cart.Products.Any())
            {
                foreach (var item in orderDTO.Cart.Products)
                {
                    // Создаем новый ProductCart для добавления в заказ
                    var productCart = new ProductCart
                    {
                        Product = item.Product,
                        Amount = item.Amount
                    };

                    // Добавляем productCart в заказ
                    order.Products.Add(productCart);
                    order.TotalPrice += item.Product.Price * item.Amount;
                }

                // Обновляем заказ после добавления продуктов
                await _orderRepository.Update(orderDTO, cancellationToken);
            }
            return order;
        }

        public async Task<Order> Delete(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Удаляем заказ из репозитория
            await _orderRepository.Delete(id, cancellationToken);
            return order;
        }

        public async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return order;
        }

        public async Task<Order> Update(OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderDTO.Id, cancellationToken);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Обновляем данные заказа
            order.Cart = orderDTO.Cart;
            order.User = orderDTO.User;
            order.Products = orderDTO.Products ?? new List<ProductCart>(); 
            order.TotalPrice = orderDTO.TotalPrice;

            // Обновляем заказ в репозитории
            await _orderRepository.Update(orderDTO, cancellationToken);

            return order;
        }
    }
}
