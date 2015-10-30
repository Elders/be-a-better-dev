using System;

namespace ModelsToTheRescue.Heavy
{
    public interface IUserRepository
    {
        User Get(Guid id);
        void Save(User cart);
    }
}