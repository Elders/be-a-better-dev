using System;
using System.Linq;

namespace ModelsToTheRescue
{
    public class ShoppingCartService
    {
        private ICartRepository cartRepository;

        public ShoppingCartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public void AddToCart(Guid cartId, CartItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (cartId == default(Guid))
                throw new ArgumentException($"Invalid cart id {cartId}");

            var cart = cartRepository.Get(cartId);
            var cartItem = cart.Items.SingleOrDefault(x => x.ProductId == item.ProductId);

            if (cartItem != null)
                cartItem.Quantity += item.Quantity;
            else
                cart.Items.Add(item);

            cartRepository.Save(cart);
        }
    }
}





