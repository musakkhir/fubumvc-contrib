using System;

namespace FubuSample.Core.Domain
{
    public class Product : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}