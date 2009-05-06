using System;
using FluentNHibernate.AutoMap;
using FubuMVC.Framework.NHibernate.Config.AutoMapConventions;

namespace FubuMVC.Framework.NHibernate.Config.GuidEntity
{
    public class GuidEntityPersistenceModel<TDomainLayerSupertype> : AutoPersistenceModel
        where TDomainLayerSupertype : class
    {
        public GuidEntityPersistenceModel()
        {
            AddEntityAssembly(typeof (TDomainLayerSupertype).Assembly);
            WithAlterations(x => x.Add<AutoMapAlteration<TDomainLayerSupertype>>());

            WithSetup(s =>
            {
                s.FindIdentity = prop => prop.Name == "Id" && prop.PropertyType == typeof(Guid);
                s.IsBaseType = type => type == typeof(TDomainLayerSupertype);
            });

            ConventionDiscovery.Setup(f =>
            {
                f.Add<ForeignKeyConvention>();
                f.Add<HasManyConvention>();
                f.Add<ReferenceConvention>();
                f.Add<HasManyToManyConvention>();
                f.AddFromAssemblyOf<IdentityColumnConvention>();
            });

            UseOverridesFromAssemblyOf<TDomainLayerSupertype>();
        }
    }
}