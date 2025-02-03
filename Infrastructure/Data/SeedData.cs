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
                      Name = "Колпак на столб забора. RAL 7024",
                      Price = 2500,
                      Img = "/pics/Kolpak1.jpg",
                      Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
                      Description = ""
                  },
                   new Product
                   {
                       Id = Guid.NewGuid(),
                       Name = "Колпак на столб забор. RAL 7024m",
                       Price = 2500,
                       Img = "/pics/7024(2).jpg",
                       Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
                       Description = ""
                   },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Колпак на столб забора. RAL 8017",
                        Price = 2500,
                        Img = "/pics/8017new(3).jpg",
                        Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
                        Description = ""
                    },
                     new Product
                     {
                         Id = Guid.NewGuid(),
                         Name = "Колпак на столб забора. RAL 9005",
                         Price = 2800,
                         Img = "/pics/9005(2).jpg",
                         Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
                         Description = ""
                     },
                      new Product
                      {
                          Id = Guid.NewGuid(),
                          Name = "Колпак на столб забора. RAL 8017",
                          Price = 2800,
                          Img = "/pics/8017(1).jpg",
                          Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
                          Description = ""

                      },
                       new Product
                       {
                           Id = Guid.NewGuid(),
                           Name = "Колпак на столб забора. Ral 9005",
                           Price = 2800,
                           Img = "/pics/9005(3).jpg",
                           Specs = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора.",
                           Description = ""

                       });

                context.SaveChanges();


            }
        }
    }
}
