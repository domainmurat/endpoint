namespace endpoint.Core.Products
{
    using endpoint.Core.Entities;

    public class Product : Entity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
    }
}
