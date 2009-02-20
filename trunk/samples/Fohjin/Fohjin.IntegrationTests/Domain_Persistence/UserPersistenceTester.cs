using Fohjin.Core.Domain;
using Fohjin.Core.Domain.Persistence;
using NUnit.Framework;

namespace Fohjin.IntegrationTests.Domain_Persistence
{
    [TestFixture]
    public class UserPersistenceTester : PersistenceTesterContext<UserPersistenceMap, User>
    {
        public override void ReferencesAdditionalMaps(TestPersistenceModel<UserPersistenceMap, User> model)
        {
            model.IncludeMapping<CommentPersistenceMap, Comment>();
            model.IncludeMapping<TagPersistenceMap, Tag>();
            model.IncludeMapping<PostPersistenceMap, Post>();
        }

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
                .CheckList(u=>u._posts, new[]{new Post()})
                .VerifyTheMappings();
        }
    }
}