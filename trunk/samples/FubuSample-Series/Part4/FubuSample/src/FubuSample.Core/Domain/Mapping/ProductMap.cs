using FluentNHibernate.Mapping;

namespace FubuSample.Core.Domain.Mapping
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(e => e.ID).GeneratedBy.GuidComb();

            Map(p => p.Name);
            Map(p => p.Description);
            
        }
    }
}