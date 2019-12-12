namespace OnlineShop.Domain
{
    public class Product :Entity
    {
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}