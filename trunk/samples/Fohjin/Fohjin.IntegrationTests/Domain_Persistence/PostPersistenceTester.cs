using System;
using Fohjin.Core.Domain;
using NUnit.Framework;

namespace Fohjin.IntegrationTests.Domain_Persistence
{
    [TestFixture]
    public class PostPersistenceTester : PersistenceTesterContext<Post>
    {
        [Test]
        public void should_load_and_save_a_post()
        {
            Specification
                .CheckProperty(p => p.Title, "title, anything here")
                .CheckProperty(p => p.Published, DateTime.Parse("12-NOV-2008"))
                .CheckProperty(p => p.Body, "body, anything here")
                .CheckProperty(p => p.Slug, "slug, anything here")
                //.CheckList(p => p.GetComments(), new[] { new Comment() })
                //.CheckList<Tag>(p => p.GetTags, new[] { new Tag() })
                .VerifyTheMappings();
        }
    }
}