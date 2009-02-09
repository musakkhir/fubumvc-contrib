using System;

namespace Fohjin.Core.Services
{
    public interface ISecurityDataService
    {
        Guid? AuthenticateForUserId(string username, string password);
    }
}