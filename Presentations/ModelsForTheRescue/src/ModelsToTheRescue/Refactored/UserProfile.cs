using System;

namespace ModelsToTheRescue.Refactored
{
    public class UserProfile : Entity
    {
        public UserProfile(Guid userId)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"Invalid user id {userId}.", nameof(userId));

            UserId = userId;
        }

        public Guid UserId { get; private set; }

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

        public static bool IsValidEmail(string email)
        {
            string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            return System.Text.RegularExpressions.Regex.Match(email, emailRegex).Success;
        }
    }
}







