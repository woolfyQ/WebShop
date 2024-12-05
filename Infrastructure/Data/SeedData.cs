using Core.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public class SeedData
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            if (!context.Products.Any())
            {
                context.Products.AddRange(

                 new Product
                 {
                     Id = Guid.NewGuid(),
                     Name = "Тестовый продукт 1",
                     Price = 100,
                     Img = "/pics/Kolpak1.jpg",
                     Specs = "Описание 1",
                     Description = "Описание продукта 1"
                 },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Тестовый продукт 1",
                    Price = 100,
                    Img = "/pics/Kolpak2.jpg",
                    Specs = "",
                    Description = ""
                },
                 new Product
                 {
                     Id = Guid.NewGuid(),
                     Name = "Тестовый продукт 1",
                     Price = 100,
                     Img = "/pics/Kolpak3.jpg",
                     Specs = "Описание 1",
                     Description = "Описание продукта 1"
                 },
                 new Product
                 {
                     Id = Guid.NewGuid(),
                     Name = "Тестовый продукт 2",
                     Price = 200,
                     Img = "/pics/Kolpak4.jpg",
                     Specs = "Описание 2",
                     Description = "Описание продукта 2"
                 },
                  new Product
                  {
                      Id = Guid.NewGuid(),
                      Name = "Тестовый продукт 1",
                      Price = 100,
                      Img = "/pics/Kolpak5.jpg",
                      Specs = "Описание 1",
                      Description = "Описание продукта 1"
                  },
                  new Product
                  {
                      Id = Guid.NewGuid(),
                      Name = "Тестовый продукт 1",
                      Price = 100,
                      Img = "/pics/Kolpak6.jpg",
                      Specs = "Описание 1",
                      Description = "Описание продукта 1"
                  });

                context.SaveChanges();


            }
        }
    }
}
