using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.Validation.SemanticModel
{
    public class AdditionalProperties
    {
        private readonly List<Property> _properties;

        public AdditionalProperties()
        {
            _properties = new List<Property>();
        }

        public IEnumerable<Property> GetProperties()
        {
            return _properties.AsEnumerable();
        }

        public bool Contains(Property property, PropertyComparer propertyComparer)
        {
            return _properties.Contains(property, propertyComparer);
        }

        public void AddProperty(Property property)
        {
            _properties.Add(property);
        }
    }
}