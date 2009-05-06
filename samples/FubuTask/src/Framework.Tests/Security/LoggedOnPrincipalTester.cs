using System;
using System.Security.Principal;
using FubuMVC.Framework.Security;
using NUnit.Framework;

namespace FubuMVC.Framework.Tests.Security
{
	[TestFixture]
	public class when_converting_userid_from_string_to_most_common_id_types
	{
		[Test]
		public void should_support_guid()
		{
		    new LoggedOnPrincipal<Guid>(
                new GenericIdentity("7A2C5CD7-ECC1-487d-BD6B-9B37ACC622B7")
            );

            // should not throw an exception
		}

        [Test]
        public void should_support_int32()
        {
            new LoggedOnPrincipal<Int32>(
                new GenericIdentity("9999")
            );

            // should not throw an exception
        }

        [Test]
        public void should_support_int64()
        {
            new LoggedOnPrincipal<Int64>(
                new GenericIdentity("9999")
            );

            // should not throw an exception
        }
	}
}