using System;

namespace ModelsToTheRescue.Refactored
{
    public class User : Entity
    {
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
    }
}







