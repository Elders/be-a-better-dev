using System;

namespace ModelsToTheRescue.Heavy
{
    public interface ICartRepository
    {
        ShoppingCart Get(Guid id);
        void Save(ShoppingCart cart);
    }
}