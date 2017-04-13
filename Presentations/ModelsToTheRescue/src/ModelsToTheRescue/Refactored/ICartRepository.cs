using System;

namespace ModelsToTheRescue.Refactored
{
    public interface ICartRepository
    {
        ShoppingCart Get(Guid id);
        void Save(ShoppingCart cart);
    }
}