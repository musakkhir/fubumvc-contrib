using System.Collections.Generic;
using AltOxite.Core.Domain;

namespace AltOxite.Core.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetPostsWithDrafts();
    }
}