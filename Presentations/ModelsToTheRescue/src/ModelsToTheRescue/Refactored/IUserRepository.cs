using System;

namespace ModelsToTheRescue.Refactored
{
    public interface IUserRepository
    {
        User Get(Guid id);
        void Save(User cart);
    }
}