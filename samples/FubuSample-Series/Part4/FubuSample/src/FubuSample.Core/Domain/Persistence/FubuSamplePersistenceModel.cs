using FluentNHibernate;

namespace FubuSample.Core.Domain.Persistence
{
    public class FubuSamplePersistenceModel : PersistenceModel
    {
        public FubuSamplePersistenceModel()
        {
            addMappingsFromThisAssembly();
        }
    }
}