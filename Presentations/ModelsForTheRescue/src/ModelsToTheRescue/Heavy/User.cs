using System;
using System.Collections.Generic;

namespace ModelsToTheRescue.Heavy
{
    public class User : Entity
    {
        private List<Address> shippingAddresses;

        public User(Guid id, string username, string password, IPasswordHashService hashService)
        {
            if (id == default(Guid))
                throw new ArgumentException($"Invalid user id {id}.", nameof(id));

            if (IsValidUsername(username) == false)
                throw new ArgumentException("The username should be alphanumeric, between 6 and 20 symbols, and it could contain '_' or '-'.");

            if (IsValidPassword(password) == false)
                throw new ArgumentException("The password should be between 8 and 20 symbols and it should contain a number, a captial letter and a special symbol.");

            if (hashService == null)
                throw new ArgumentNullException("Hash service is required.", nameof(hashService));

            Id = id;
            Username = username;
            PasswordHash = hashService.GetPasswordHash(password);
        }

        public Guid Id { get; private set; }

        public string Username { get; private set; }

        public string PasswordHash { get; private set; }

        public IEnumerable<Address> ShippingAddresses { get { return shippingAddresses.AsReadOnly(); } }

        public Uri Avatar { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public void ChangePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException("Invalid phone number", nameof(phoneNumber));

            PhoneNumber = phoneNumber;
        }

        public void ChangeEmail(string email)
        {
            if (IsValidEmail(email) == false)
                throw new ArgumentException("Invalid Email Address", nameof(email));

            Email = email;
        }

        public void ChangeAvatar(Uri avatar)
        {
            if (avatar == null)
                throw new ArgumentNullException("Avatar is required.", nameof(avatar));

            Avatar = avatar;
        }

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

        public void ChangeUsername(string username)
        {
            if (IsValidUsername(username) == false)
                throw new ArgumentException("The username should be alphanumeric, between 6 and 20 symbols, and it could contain '_' or '-'.");

            Username = username;
        }

        public void ChangePassword(string oldPassword, string newPassword, IPasswordHashService hashService)
        {
            if (IsValidPassword(newPassword) == false)
                throw new ArgumentException("The password should be between 8 and 20 symbols and it should contain a number, a captial letter and a special symbol.");

            if (hashService.GetPasswordHash(oldPassword) == PasswordHash)
            {
                PasswordHash = hashService.GetPasswordHash(newPassword);
            }
            else
                throw new InvalidOperationException("Wrong passoword");
        }

        public static bool IsValidUsername(string username)
        {
            string usernameRequirements = "^(?=.{6,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
            return System.Text.RegularExpressions.Regex.Match(username, usernameRequirements).Success;
        }

        public static bool IsValidPassword(string password)
        {
            string passwordRequirements = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{8,20}$";
            return System.Text.RegularExpressions.Regex.Match(password, passwordRequirements).Success;
        }

        public static bool IsValidEmail(string email)
        {
            string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            return System.Text.RegularExpressions.Regex.Match(email, emailRegex).Success;
        }

    }
}







