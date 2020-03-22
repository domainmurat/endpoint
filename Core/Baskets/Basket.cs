namespace endpoint.Core.Baskets
{
    using System.Collections.Generic;
    using endpoint.Core.Entities;

    public class Basket : Entity
    {
        public string Name { get; set; }
        public long UserId { get; set; }

        public virtual ICollection<BasketProduct> BasketProducts { get; set; }
    }
}
