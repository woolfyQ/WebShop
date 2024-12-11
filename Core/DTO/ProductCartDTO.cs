using Core.Entity;

namespace Core.DTO
{
    public class ProductCartDTO 
    {
        public Guid Id { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public Guid ProductId { get; set; }


        public static implicit operator ProductCartDTO(ProductCart Productcart) => new()
        {
            Id = Productcart.Id,
            Cart = Productcart.Cart,
            Product = Productcart.Product,
            Amount = Productcart.Amount,
            ProductId = Productcart.ProductId,
        };


    }



}
