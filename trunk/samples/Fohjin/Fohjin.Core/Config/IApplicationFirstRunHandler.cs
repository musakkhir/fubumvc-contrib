namespace Fohjin.Core.Config
{
    public interface IApplicationFirstRunHandler
    {
        void InitializeIfNecessary();
    }
}