using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ModelsToTheRescue.Refactored
{
    public interface IPasswordHashService
    {
        string GetPasswordHash(string password);
    }

}
