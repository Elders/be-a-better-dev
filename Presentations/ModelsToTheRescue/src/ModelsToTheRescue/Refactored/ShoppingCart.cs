using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelsToTheRescue.Refactored
{
    public class ShoppingCart : Entity
    {
        private List<CartItem> cartItems;

        public ShoppingCart(Guid id, Guid owner)
        {
            if (id == default(Guid))
                throw new ArgumentException($"Invalid cart id {id}", nameof(id));
            if (owner == default(Guid))
                throw new ArgumentException($"Invalid owner id {id}", nameof(owner));

            Id = id;
            Owner = owner;
            cartItems = new List<CartItem>();
        }

        public Guid Id { get; private set; }

        public IEnumerable<CartItem> Items { get { return cartItems.AsReadOnly(); } }

        public Guid Owner { get; private set; }

        public void AddItem(CartItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var cartItem = cartItems.SingleOrDefault(x => x.ProductId == item.ProductId);

            if (cartItem != null)
                cartItem.Quantity += item.Quantity;
            else
                cartItems.Add(item);
        }
    }
}



