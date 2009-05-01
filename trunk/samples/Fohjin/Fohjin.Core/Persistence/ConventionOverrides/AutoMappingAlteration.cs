using System;
using FluentNHibernate.AutoMap;
using FluentNHibernate.AutoMap.Alterations;
using Fohjin.Core.Domain;

namespace Fohjin.Core.Persistence.ConventionOverrides
{
    public class AutoMappingAlteration : IAutoMappingAlteration
    {
        public void Alter(AutoPersistenceModel model)
        {
            model.Where(IsMappable);
        }

        private static bool IsMappable(Type type)
        {
            if (!typeof(DomainEntity).IsAssignableFrom(type)) return false;

            if (type.Namespace != "Fohjin.Core.Domain") return false;

            if (type == typeof(SiteConfiguration)) return false;

            if (type == typeof(Alias)) return false;

            return true;
        }
    }
}