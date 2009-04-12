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

        public void AddProperty(Property property)
        {
            if (!_properties.Contains(property, new PropertyComparer()))
                _properties.Add(property);
        }
    }
}