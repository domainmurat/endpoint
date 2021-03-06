﻿namespace endpoint.Core.Products
{
    using System.Collections.Generic;
    using endpoint.Core.Baskets;
    using endpoint.Core.Entities;

    public class Product : Entity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public virtual ICollection<BasketProduct> BasketProducts { get; set; }
    }
}
