using System;
using System.Collections.Generic;

namespace ModelsToTheRescue
{
    public class ShoppingCart : Entity
    {
        public Guid Id { get; set; }

        public List<CartItem> Items { get; set; }

        public Guid Owner { get; set; }
    }
}


