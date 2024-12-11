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

        // Реализация метода Create
        public async Task<Order> Create(OrderDTO dto)
        {
            // Преобразуем DTO в сущность Order
            var order = new Order
            {
                Id = dto.Id,  // Генерация нового Id
                User = dto.User,      // Присваиваем пользователя из DTO
                TotalPrice = 0,       // Изначально цена заказа 0
                Products = new List<ProductCart>()  // Инициализируем коллекцию продуктов
            };

            // Добавляем продукты из DTO в заказ
            foreach (var item in dto.Cart.Products)
            {
                var productCart = new ProductCart
                {
                    Product = item.Product,
                    Amount = item.Amount
                };

                order.Products.Add(productCart);
                order.TotalPrice += item.Product.Price * item.Amount;  // Увеличиваем цену заказа
            }

            // Добавляем заказ в контекст и сохраняем
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Возвращаем созданную сущность Order
            return order;
        }

        // Реализация метода Update
        public async Task<Order> Update(OrderDTO dto, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Products)  // Включаем связанные продукты
                .FirstOrDefaultAsync(o => o.Id == dto.Id, cancellationToken);

            if (order == null)
            {
                throw new Exception($"Order with id {dto.Id} not found");
            }

            // Обновляем поля сущности Order
            order.User = dto.User;
            order.TotalPrice = 0;  // Сбрасываем цену, она будет пересчитана ниже
            order.Products.Clear();  // Очищаем старые продукты, если нужно добавить новые

            // Добавляем новые продукты из DTO в заказ
            foreach (var item in dto.Cart.Products)
            {
                var productCart = new ProductCart
                {
                    Product = item.Product,
                    Amount = item.Amount
                };

                order.Products.Add(productCart);
                order.TotalPrice += item.Product.Price * item.Amount;  // Пересчитываем общую цену заказа
            }

            // Обновляем заказ в контексте и сохраняем
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);

            // Возвращаем обновленную сущность Order
            return order;
        }

        // Реализация метода AddProduct
        public async Task<Order> AddProduct(OrderDTO dto, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Products)  // Включаем связанные продукты
                .FirstOrDefaultAsync(o => o.Id == dto.Id, cancellationToken);

            if (order == null)
            {
                return null;
            }

            // Добавляем новые продукты из DTO в заказ
            foreach (var item in dto.Cart.Products)
            {
                var productCart = new ProductCart
                {
                    Product = item.Product,
                    Amount = item.Amount
                };

                order.Products.Add(productCart);
                order.TotalPrice += item.Product.Price * item.Amount;  // Увеличиваем цену заказа
            }

            // Сохраняем изменения в контексте
            await _context.SaveChangesAsync(cancellationToken);

            // Возвращаем обновленную сущность Order
            return order;
        }

        // Реализация метода Delete
        public async Task<Order> Delete(Guid id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Products)  // Включаем связанные продукты
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order == null)
            {
                return null;  // Если заказ не найден, возвращаем null
            }

            // Удаляем заказ из контекста
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);

            // Возвращаем удаленную сущность Order
            return order;
        }

        // Реализация метода GetByIdAsync
        public async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Products)  // Включаем связанные продукты
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            return order;
        }
    }
}

