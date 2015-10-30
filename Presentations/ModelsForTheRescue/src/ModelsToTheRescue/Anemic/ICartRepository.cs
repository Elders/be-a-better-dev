using System;

namespace ModelsToTheRescue
{
    public interface ICartRepository
    {
        ShoppingCart Get(Guid id);
        void Save(ShoppingCart cart);
    }
}