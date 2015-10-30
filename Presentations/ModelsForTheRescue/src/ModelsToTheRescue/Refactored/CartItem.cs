using System;

namespace ModelsToTheRescue.Refactored
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public uint Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}