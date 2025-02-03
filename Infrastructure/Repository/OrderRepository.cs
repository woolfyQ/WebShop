using Core.Entity;
using Core.DTO;
using Infrastructure.Intefaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderInterface<Order, OrderDTO>
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public async Task<Order> Create(OrderDTO dto)
        {
           
            var order = new Order
            {
                Id = Guid.NewGuid(),  
                UserId = dto.UserId,
                CartId = dto.CartId,
                TotalPrice = dto.TotalPrice, 
                DateTime = dto.DateTime.ToUniversalTime(),       
            };

            
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Возвращаем созданную сущность Order
            return order;
        }

        
        public async Task<Order> Update(OrderDTO dto, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == dto.Id);
            if (order == null)
            {
                throw new Exception($"Order with id {dto.Id} not found");
            }
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);

            
            return order;
        }

        // Реализация метода AddProduct

        // Реализация метода Delete
        public async Task<Order> Delete(Guid id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders  
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order == null)
            {
                return null;  
            }

          
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);

       
            return order;
        }

    
        public async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            return order;
        }
    }
}