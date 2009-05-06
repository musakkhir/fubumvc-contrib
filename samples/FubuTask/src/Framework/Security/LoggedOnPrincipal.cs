using System.ComponentModel;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace FubuMVC.Framework.Security
{
	public class LoggedOnPrincipal<TUserId> : IPrincipal
	{
		private readonly IIdentity _identity;
		private readonly TUserId _userId;
		private static LoggedOnPrincipal<TUserId> _currentStub;

		public LoggedOnPrincipal(IIdentity identity)
		{
			_identity = identity;
			_userId = (TUserId) TypeDescriptor
										.GetConverter(typeof (TUserId))
										.ConvertFromString(_identity.Name);
		}

		public bool IsInRole(string role)
		{
			//TODO: Implement roles for ASP.NET security stuff
			object TODO_IMPLEMENT_PRINCIPAL_ROLES = null;
			return true;
		}

		public TUserId UserId { get { return _userId; } }
		IIdentity IPrincipal.Identity { get { return _identity; } }

		public static LoggedOnPrincipal<TUserId> StubCurrent(LoggedOnPrincipal<TUserId> principal)
		{
			_currentStub = principal;
			return principal;
		}

		public static void StubReset()
		{
			_currentStub = null;
		}

		public static LoggedOnPrincipal<TUserId> Current
		{
			get
			{
				if( _currentStub != null ) return _currentStub;

				if (HttpContext.Current != null)
				{
					return HttpContext.Current.User as LoggedOnPrincipal<TUserId>;
				}

				return Thread.CurrentPrincipal as LoggedOnPrincipal<TUserId>;
			}
		}
	}
}