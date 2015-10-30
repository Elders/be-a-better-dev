using System;

namespace ModelsToTheRescue.Heavy
{
    public class UserService
    {
        private IPasswordHashService passwordHashService;
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository, IPasswordHashService passwordHashService)
        {
            this.userRepository = userRepository;
            this.passwordHashService = passwordHashService;
        }

        public void RegisterUser(Guid userId, string username, string password)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"Invalid user id {userId}");

            var user = new User(userId, username, password, passwordHashService);
            userRepository.Save(user);
        }

        public void ChangeUsername(Guid userId, string newUsername)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"Invalid user id {userId}");

            var user = userRepository.Get(userId);
            user.ChangeUsername(newUsername);
            userRepository.Save(user);
        }

        public void ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"Invalid user id {userId}");

            var user = userRepository.Get(userId);
            user.ChangePassword(oldPassword, newPassword, passwordHashService);
            userRepository.Save(user);
        }
    }
}





