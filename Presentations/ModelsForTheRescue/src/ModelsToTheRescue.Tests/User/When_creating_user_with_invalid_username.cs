using System;
using Machine.Specifications;
using ModelsToTheRescue.Refactored;

namespace ModelsToTheRescue.Tests
{
    [Subject(nameof(User))]
    public class When_creating_user_with_invalid_username
    {

        Establish context = () =>
        {
            id = Guid.NewGuid();
            username = "1";
            password = "Str0ngP@ssw0rd";
            hashService = new SHA256TestHashService();
        };

        Because of = () => ex = Catch.Exception(() => new User(id, username, password, hashService));

        It shoud_have_thrown_invalid_operation_exception = () => ex.ShouldBeAssignableTo<ArgumentException>();

        static Exception ex;
        static Guid id;
        static string username;
        static string password;
        static IPasswordHashService hashService;
    }
}

