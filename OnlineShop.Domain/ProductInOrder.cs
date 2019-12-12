namespace OnlineShop.Domain
{
    public class ProductInOrder : Entity
    {
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}