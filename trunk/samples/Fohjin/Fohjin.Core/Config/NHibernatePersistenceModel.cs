using FluentNHibernate;
using FluentNHibernate.AutoMap;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence.ConventionOverrides;
using Fohjin.Core.Persistence.Conventions;

namespace Fohjin.Core.Config
{
    public interface INHibernatePersistenceModel
    {
        AutoPersistenceModel GetPersistenceModel();
    }

    public class NHibernatePersistenceModel : INHibernatePersistenceModel
    {
        private readonly AutoPersistenceModel _autoPersistenceModel;

        public NHibernatePersistenceModel()
        {
            _autoPersistenceModel = AutoPersistenceModel
                .MapEntitiesFromAssemblyOf<DomainEntity>()
                .WithAlterations(x =>
                    x.Add<AutoMappingAlteration>())
                .WithSetup(s =>
                {
                    s.FindIdentity = type => type.Name == "ID";
                    s.IsBaseType = type => type == typeof (DomainEntity);
                })
                //.Where(type => 
                //    typeof (DomainEntity).IsAssignableFrom(type) &&
                //    type.Namespace == "Fohjin.Core.Domain")
                .ConventionDiscovery
                    .AddFromAssemblyOf<IdentityColumnConvention>()
                    .UseOverridesFromAssemblyOf<UserMappingOverride>();
        }

        public AutoPersistenceModel GetPersistenceModel()
        {
            return _autoPersistenceModel;
        }
    }
}