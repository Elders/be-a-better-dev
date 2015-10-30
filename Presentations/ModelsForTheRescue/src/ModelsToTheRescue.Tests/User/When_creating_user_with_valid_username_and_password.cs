using System;
using Machine.Specifications;
using ModelsToTheRescue.Refactored;

namespace ModelsToTheRescue.Tests
{

    [Subject(nameof(User))]
    public class When_creating_user_with_valid_username_and_password
    {

        Establish context = () =>
        {
            id = Guid.NewGuid();
            username = "JohnDoe23";
            password = "Str0ngP@ssw0rd";
            hashService = new SHA256TestHashService();
        };

        Because of = () => user = new User(id, username, password, hashService);

        It should_be_created = () => user.ShouldNotBeNull();


        static User user;
        static Guid id;
        static string username;
        static string password;
        static IPasswordHashService hashService;
    }
}