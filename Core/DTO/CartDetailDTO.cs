namespace Core.DTO
{
    public class CartDetailDTO
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ItemCartDTO> Products { get; set; }
    }
}
