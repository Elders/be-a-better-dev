using System;

namespace ModelsToTheRescue
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public uint Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}