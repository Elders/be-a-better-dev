using System;

namespace ModelsToTheRescue.Heavy
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
            if (cartId == default(Guid))
                throw new ArgumentException($"Invalid cart id {cartId}");

            var cart = cartRepository.Get(cartId);
            cart.AddItem(item);
            cartRepository.Save(cart);
        }
    }
}





