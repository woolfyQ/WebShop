using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;

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
                UserId = orderDTO.UserId,
                CartId = orderDTO.CartId,
                DateTime = DateTime.Now,
                TotalPrice = orderDTO.TotalPrice
            };

   
            await _orderRepository.Create(orderDTO);
            return order;
        }

        
        public async Task<Order> Delete(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                throw new Exception("Order not found");
            }


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


            order.CartId = orderDTO.CartId;
            order.UserId = orderDTO.UserId;
            order.TotalPrice = orderDTO.TotalPrice;

            await _orderRepository.Update(orderDTO, cancellationToken);

            return order;
        }
    }
}
