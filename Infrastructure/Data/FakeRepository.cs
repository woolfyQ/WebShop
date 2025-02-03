//using Core;
//using Core.Entity;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Data
//{
//    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
//    {
//        private readonly FakeContext _context;

//        // Конструктор
//        public FakeRepository(FakeContext context)
//        {
//            _context = context ?? throw new ArgumentNullException(nameof(context));
//            {
//                if (!_context.Set<Product>().Any())
//                {
//                    new List<Product>
//                    {
//                    new Product
//                    {
//                    Id = Guid.NewGuid(),
//                    Name = "Колпак на столб забора. RAL 7024",
//                    Price = 2500,
//                    Img = "/pics/7024.jpg",
//                    Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
//                    Description =
//                    "1. Защита от погоды: Колпак защищает верхнюю часть столба от воздействия различных погодных условий, таких как дождь, снег, и ультрафиолетовые лучи, что помогает продлить срок службы забора." +
//                    " 2. Предотвращение скопления влаги и грязи: Благодаря колпаку предотвращается скопление влаги и грязи внутри столба, что может привести к его разрушению и повреждению." +
//                    " 3. Завершенный вид: Добавляя завершенный и стильный вид вашему забору, колпак придает ему эстетическое совершенство, дополняя общий дизайн вашего участка." +
//                    " 4. Защита растений и цветов: Колпак служит дополнительным средством защиты от солнца, помогая сохранить растения и цветы рядом с забором от излишней солнечной экспозиции." +
//                    " 5. Прочность и долговечность: Изготовленный из прочных материалов, колпак является долговечным решением, которое прослужит вам долгие годы, обеспечивая надежную защиту и стильный внешний вид вашего забора."
//                },
//                new Product
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Колпак на столб забор. RAL 7024m",
//                    Price = 2500,
//                    Img = "/pics/7024new.jpg",
//                    Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
//                    Description =
//                    "1. Защита от погоды: Колпак защищает верхнюю часть столба от воздействия различных погодных условий, таких как дождь, снег, и ультрафиолетовые лучи, что помогает продлить срок службы забора." +
//                    " 2. Предотвращение скопления влаги и грязи: Благодаря колпаку предотвращается скопление влаги и грязи внутри столба, что может привести к его разрушению и повреждению." +
//                    " 3. Завершенный вид: Добавляя завершенный и стильный вид вашему забору, колпак придает ему эстетическое совершенство, дополняя общий дизайн вашего участка." +
//                    " 4. Защита растений и цветов: Колпак служит дополнительным средством защиты от солнца, помогая сохранить растения и цветы рядом с забором от излишней солнечной экспозиции." +
//                    " 5. Прочность и долговечность: Изготовленный из прочных материалов, колпак является долговечным решением, которое прослужит вам долгие годы, обеспечивая надежную защиту и стильный внешний вид вашего забора."
//                },
//                new Product
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Планка заборная",
//                    Price = 2800,
//                    Img = "/pics/planka8017.jpg",
//                    Specs = "Ral 8017",
//                    Description = "Планка (J) П образная-деталь, накрывающая верхний край ограды в финальной части монтажа."
//                },
//                new Product
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Планка заборная",
//                    Price = 2800,
//                    Img = "/pics/7024planka.jpg",
//                    Specs = "Ral 7024",
//                    Description = "Планка (J) П образная-деталь, накрывающая верхний край ограды в финальной части монтажа."
//                },
//                new Product
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Планка заборная",
//                    Price = 2800,
//                    Img = "/pics/9005plankaDOP.jpg",
//                    Specs = "Ral 9005",
//                    Description = "Планка (J) П образная-деталь, накрывающая верхний край ограды в финальной части монтажа."
//                }
//                    };
//                };

//                _context.AddRange();
//                _context.SaveChanges();
//            }


//        }

//        public async Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
//        {
//            if (_context == null) throw new InvalidOperationException("DbContext is not initialized.");

//            await _context.AddRangeAsync(entities, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        public async Task Create(TEntity entity, CancellationToken cancellationToken)
//        {
//            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        // Метод для создания нескольких сущностей
//        public async Task Create(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
//        {
//            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        // Метод для удаления сущности
//        public async Task Delete(TEntity entity, CancellationToken cancellationToken)
//        {
//            _context.Set<TEntity>().Remove(entity);
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        // Метод для удаления нескольких сущностей
//        public async Task Delete(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
//        {
//            _context.Set<TEntity>().RemoveRange(entities);
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        // Метод для получения всех сущностей
//        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
//        {
//            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
//        }

//        // Метод для обновления сущности
//        public async Task Update(TEntity entity, CancellationToken cancellationToken)
//        {
//            _context.Set<TEntity>().Update(entity);
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        // Метод для обновления нескольких сущностей
//        public async Task Update(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
//        {
//            _context.Set<TEntity>().UpdateRange(entities);
//            await _context.SaveChangesAsync(cancellationToken);
//        }


//    }

//}
