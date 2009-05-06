using System;
using FluentNHibernate.AutoMap;
using FluentNHibernate.AutoMap.Alterations;

namespace FubuMVC.Framework.NHibernate.Config
{
    public class AutoMapAlteration<TDomainLayerSupertype> : IAutoMappingAlteration
        where TDomainLayerSupertype : class
    {
        private static readonly Type EntityType = typeof(TDomainLayerSupertype);

        public void Alter(AutoPersistenceModel model)
        {
            model.Where(IsMappable);
        }

        public static bool IsMappable(Type type)
        {
            return
                EntityType.IsAssignableFrom(type)
                && type.Namespace.StartsWith(EntityType.Namespace);
        }
    }
}