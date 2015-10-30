using System;
using System.Collections.Generic;

namespace ModelsToTheRescue.Refactored
{
    public class AddressInformation : Entity
    {
        private List<Address> shippingAddresses;

        public AddressInformation(Guid userId)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"Invalid user id {userId}.", nameof(userId));

            UserId = userId;
        }

        public Guid UserId { get; private set; }

        public IEnumerable<Address> ShippingAddresses { get { return shippingAddresses.AsReadOnly(); } }

        public void AddShippingAddres(Address shippingAddress)
        {
            if (shippingAddress == null)
                throw new ArgumentNullException("Address is required", nameof(shippingAddress));

            if (!shippingAddresses.Contains(shippingAddress))
                shippingAddresses.Add(shippingAddress);
        }

        public void RemoveShippingAddres(Address shippingAddress)
        {
            if (shippingAddress == null)
                throw new ArgumentNullException("Address is required", nameof(shippingAddress));

            if (!shippingAddresses.Contains(shippingAddress))
                shippingAddresses.Add(shippingAddress);
        }
    }
}







