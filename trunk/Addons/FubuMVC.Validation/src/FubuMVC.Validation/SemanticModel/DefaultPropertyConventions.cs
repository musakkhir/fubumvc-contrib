using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.Validation.SemanticModel
{
    public class DefaultPropertyConventions
    {
        private readonly List<DefaultPropertyConvention> _defaultPropertyConventions;
        public DefaultPropertyConventions()
        {
            _defaultPropertyConventions = new List<DefaultPropertyConvention>();
        }

        public void AddDefaultPropertyConvention(DefaultPropertyConvention defaultPropertyConvention)
        {
            if (!_defaultPropertyConventions.Contains(defaultPropertyConvention, new DefaultPropertyConventionComparer()))
                _defaultPropertyConventions.Add(defaultPropertyConvention);
        }

        public IEnumerable<DefaultPropertyConvention> GetDefaultPropertyConventions()
        {
            return _defaultPropertyConventions.AsEnumerable();
        }
    }
}