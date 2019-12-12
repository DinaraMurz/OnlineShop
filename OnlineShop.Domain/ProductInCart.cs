namespace OnlineShop.Domain
{
    public class ProductInCart : Entity
    {
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
        public int Count { get; set; }
    }
}