using AltOxite.Core.Domain;
using FubuMVC.Core;

namespace AltOxite.Core.Services
{
    public interface ITagService
    {
        Tag CreateOrGetTagForItem(string tag);
    }
}