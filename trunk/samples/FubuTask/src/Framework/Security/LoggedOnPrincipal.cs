using System.Security.Principal;
using System.Threading;
using System.Web;

namespace FubuMVC.Framework.Security
{
	public class LoggedOnPrincipal : IPrincipal
	{
		private readonly IIdentity _identity;
		private readonly string _username;
		private static LoggedOnPrincipal _currentStub;

		public LoggedOnPrincipal(IIdentity identity)
		{
			_identity = identity;
            _username = _identity.Name;
		}

		public bool IsInRole(string role)
		{
			//TODO: Implement roles for ASP.NET security stuff
			object TODO_IMPLEMENT_PRINCIPAL_ROLES = null;
			return true;
		}

		public string Username{ get { return _username; } }
		IIdentity IPrincipal.Identity { get { return _identity; } }

		public static LoggedOnPrincipal StubCurrent(LoggedOnPrincipal principal)
		{
			_currentStub = principal;
			return principal;
		}

		public static void StubReset()
		{
			_currentStub = null;
		}

		public static LoggedOnPrincipal Current
		{
			get
			{
				if( _currentStub != null ) return _currentStub;

				if (HttpContext.Current != null)
				{
					return HttpContext.Current.User as LoggedOnPrincipal;
				}

				return Thread.CurrentPrincipal as LoggedOnPrincipal;
			}
		}
	}
}