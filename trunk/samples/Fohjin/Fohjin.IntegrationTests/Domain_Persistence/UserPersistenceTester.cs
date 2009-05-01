using Fohjin.Core.Domain;
using NUnit.Framework;

namespace Fohjin.IntegrationTests.Domain_Persistence
{
    [TestFixture]
    public class UserPersistenceTester : PersistenceTesterContext<User>
    {
        [Test]
        public void should_load_and_save_a_user()
        {
            Specification
                .CheckProperty(u => u.Username, "username, anything here")
                .CheckProperty(u => u.DisplayName, "displayname, anything here")
                .CheckProperty(u => u.HashedEmail, "hashedemail, anything here")
                .CheckProperty(u => u.Password, "password, anything here")
                .CheckProperty(u => u.PasswordSalt, "salt, anything here")
                .CheckProperty(u => u.Status, 99)
                .CheckProperty(u => u.UserRole, UserRoles.SiteUser)
                //.CheckList(u=>u._posts, new[]{new Post()})
                .VerifyTheMappings();
        }
    }
}